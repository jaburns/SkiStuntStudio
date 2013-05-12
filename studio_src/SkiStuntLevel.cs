using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using com.jeremyaburns.math;
using com.jeremyaburns.tools;

namespace SkiStuntStudio
{
	public enum EndzoneRequirementType : uint
	{
		Unconcious,
		Alive,
		Grounded,
		OnSkis
	}

	public class SkiStuntLevel : IDisposable
	{
		public const string TEXTURE_NAME_PREFIX = "tex";
		public const string TEXTURE_NAME_SUFFIX = ".jpg";

		
		private List<LevelVertex>  vertices;
		private List<LevelPolygon> polygons;
		private List<LevelObject>  objects;


		private LevelObject playerStart;
		private LevelObject endzoneA;
		private LevelObject endzoneB;

		public LevelObject PlayerStart { get { return playerStart; } }
		public LevelObject EndzoneA    { get { return endzoneA; } }
		public LevelObject EndzoneB    { get { return endzoneB; } }

		public float EndzoneX      { get { return endzoneA.x < endzoneB.x ? endzoneA.x : endzoneB.x ; } }
		public float EndzoneY      { get { return endzoneA.y < endzoneB.y ? endzoneA.y : endzoneB.y ; } }
		public float EndzoneWidth  { get { return Math.Abs( endzoneA.x - endzoneB.x ); } }
		public float EndzoneHeight { get { return Math.Abs( endzoneA.y - endzoneB.y ); } }


		public EndzoneRequirementType EndzoneRequirements;
		public string LevelTitle;
	
		private string _levelFolder;
		private string relativeFolder;

		public bool StartWithJetpack;

		public string GoalText;
		public string HintText;


		private TextureList textureList;
		public  TextureList TextureList { get { return textureList; } }

		
		public string LevelFolder
		{
			get { return _levelFolder; }
			set
			{
				_levelFolder = value;
				relativeFolder = _levelFolder.Substring( _levelFolder.IndexOf( "scripts\\ski" ) + "scripts\\ski".Length );
				while( relativeFolder.IndexOf( "\\" ) >= 0 ) {
					relativeFolder = relativeFolder.Replace( "\\", "/" );
				}
				relativeFolder = "$baseDir" + relativeFolder;
			}
		}

		public string RelativeFolder { get { return _levelFolder; } }





		public SkiStuntLevel( string levelFolder )
		{
				vertices = new List<LevelVertex>();
				polygons = new List<LevelPolygon>();
				objects  = new List<LevelObject>();

				playerStart = new LevelObject( LevelObject.ObjectType.PlayerStart,    0, -10 );
				endzoneA    = new LevelObject( LevelObject.ObjectType.EndzoneHandle, 10,  10 );
				endzoneB    = new LevelObject( LevelObject.ObjectType.EndzoneHandle, 20,  20 );

				this.LevelFolder = levelFolder;

				EndzoneRequirements = EndzoneRequirementType.Unconcious;
				LevelTitle = "New Level";

				StartWithJetpack = false;
				GoalText = "";
				HintText = "";

				textureList = new TextureList( levelFolder );
		}


	#region Generic Add/Remove Methods
		/**
		 * <summary>Add any subclass of LevelThing to the level.</summary>
		 * <param name="thing">The LevelThing to add to the level</param>
		 */
		public void Add( LevelThing thing )
		{
				if( thing is LevelVertex ) {
					AddVertex( thing as LevelVertex );
				}
				else if( thing is LevelPolygon ) {
					AddPolygon( thing as LevelPolygon );
				}
				else if( thing is LevelObject ) {
					AddObject( thing as LevelObject );
				}
		}
		/**
		 * <summary>Remove any subclass of LevelThing from the level.</summary>
		 * <param name="thing">The LevelThing to remove from the level</param>
		 */
		public void Remove( LevelThing thing )
		{
				if( thing is LevelVertex ) {
					RemoveVertex( thing as LevelVertex );
				}
				else if( thing is LevelPolygon ) {
					RemovePolygon( thing as LevelPolygon );
				}
				else if( thing is LevelPolygon ) {
					RemoveObject( thing as LevelObject );
				}
		}
	#endregion

	#region Vertex-Related Functions
		/**
		 * <summary>Read-only property specifying the total number of vertices in the level.</summary>
		 */
		public int VertexCount { get { return vertices.Count; } }
		/**
		 * <summary>Adds a vertex to the level's set of total vertices if the vertex is not already
		 * a part of the level.</summary>
		 * <param name="vertex">Vertex to add to the level.</param>
		 */
		public void AddVertex( LevelVertex vertex )
		{
				if( !vertices.Contains( vertex ) ) vertices.Add( vertex );
		}
		/**
		 * <summary>Returns a reference to a specified vertex. If the index is out of range, null is returned.</summary>
		 * <param name="index">Index value from 0 to VertexCount.</param>
		 */
		public LevelVertex GetVertex( int index )
		{
				if( index < 0 || index >= vertices.Count ) return null;
				return vertices[ index ];
		}

		// TODO: The SkiStuntLevel object should not be directly tied to the editor's undoable 'Action' paradigm.
		/**
		 * <summary>
		 * Removes a specified LevelVertex from the level.  Any LevelPolygon in the level that uses the specified
		 * vertex will have the vertex remove from it as well.  If said LevelPolygon ends up with less than 3
		 * total vertices, the polygon will be removed from the level.
		 * </summary>
		 * <param name="vertex">Vertex to remove from the level.</param>
		 */
		public DeleteVertexAction RemoveVertex( LevelVertex vertex )
		{
				if( !vertices.Contains( vertex ) ) return null;

				DeleteVertexAction dva = new DeleteVertexAction();

				dva.vertexDeleted = vertex;
				vertices.Remove( vertex );

				foreach( LevelPolygon p in polygons )
				{
					if( p.ContainsVertex( vertex ) )
					{
						if( p.NonBezierVertexCount == 3 ) {
							dva.collateralPolygons.Add( p );
						} else
						{
							dva.polygonsAffected.Add( p );
							dva.precedingVertices.Add( p.GetPrecedingVertex( vertex ) );
							p.RemoveVertex( vertex );
						}
					}
				}

				foreach( LevelPolygon p in dva.collateralPolygons )
				{
					RemovePolygon( p );
				}

				return dva;
		}

		public void CullUnusedVertices()
		{
				List<LevelVertex> unusedVertices = new List<LevelVertex>();
				bool used;

				foreach( LevelVertex v in vertices )
				{
					used = false;

					foreach( LevelPolygon p in polygons )
					{
						if( p.ContainsVertex( v ) ) {
							used = true;
							break;
						}
					}

					if( !used ) unusedVertices.Add( v );
				}

				foreach( LevelVertex v in unusedVertices ) vertices.Remove( v );
		}

	#endregion

	#region Polygon-Related Functions
		/**
		 * <summary>Read-only property specifying the total number of polygons in the level.</summary>
		 */
		public int PolygonCount { get { return polygons.Count; } }
		/**
		 * <summary>Adds a polygon to the level's set of total polygons if the polygon is not already
		 * a part of the level.</summary>
		 * <param name="polygon">Polygon to add to the level.</param>
		 */
		public void AddPolygon( LevelPolygon polygon )
		{
				if( polygons.Contains( polygon ) ) return;
			
				polygons.Add( polygon );
				polygon.ListenToTextureList( textureList );

				for( int i = 0 ; i < polygon.VertexCount ; ++i ) {
					AddVertex( polygon.GetVertex( i ) );
				}
		}
		/**
		 * <summary>Returns a reference to a specified vertex.  If the index is out of range, null is returned.</summary>
		 * <param name="index">Index value from 0 to VertexCount.</param>
		 */
		public LevelPolygon GetPolygon( int index )
		{
				if( index < 0 || index >= polygons.Count ) return null;
				return polygons[ index ];
		}
		/**
		 * <summary>Removes a polygon from the level.</summary>
		 * <param name="polygon">Polygon to remove from the level.</param>
		 */
		public void RemovePolygon( LevelPolygon polygon )
		{
				if( ! polygons.Contains( polygon ) ) return;

				polygons.Remove( polygon );

				polygon.StopListeningToTextureList( textureList );

				for( int i = 0 ; i < polygon.VertexCount ; ++i )
				{
					LevelVertex v = polygon.GetVertex( i );
					bool usedElsewhere = false;
					foreach( LevelPolygon p in polygons )
					{
						if( p.ContainsVertex( v ) ) {
							usedElsewhere = true;
							break;
						}
					}
					if( !usedElsewhere ) vertices.Remove( v );
				}
		}
		/**
		 * <summary>Moves a polygon to be rendered in the front of the level.</summary>
		 */
		public void MovePolygonToFront( LevelPolygon polygon )
		{
				if( !polygons.Contains( polygon ) ) return;

				polygons.Remove( polygon );
				polygons.Add( polygon );
		}
		/**
		 * <summary>Moves a polygon to be rendered behind all the others in the level.</summary>
		 */
		public void MovePolygonToBack( LevelPolygon polygon )
		{
				if( !polygons.Contains( polygon ) ) return;

				polygons.Remove( polygon );
				polygons.Insert( 0, polygon );
		}
		/**
		 * <summary>Moves a polygon to be rendered in front of the polygon ahead of it of the level.</summary>
		 */
		public void MovePolygonForward( LevelPolygon polygon )
		{
				if( !polygons.Contains( polygon ) ) return;
				
				int originalIndex = polygons.IndexOf( polygon );
				if( originalIndex == polygons.Count - 1 ) return;

				polygons.Remove( polygon );
				polygons.Insert( originalIndex + 1, polygon );
		}
		/**
		 * <summary>Moves a polygon to be rendered behind the polgon behind it in the level.</summary>
		 */
		public void MovePolygonBackward( LevelPolygon polygon )
		{
				if( !polygons.Contains( polygon ) ) return;
				
				int originalIndex = polygons.IndexOf( polygon );
				if( originalIndex == 0 ) return;

				polygons.Remove( polygon );
				polygons.Insert( originalIndex - 1, polygon );
		}

	#endregion

	#region Object-Related Functions

		public int ObjectCount { get { return objects.Count; } }

		public void AddObject( LevelObject obj )
		{
		/*		if( obj.type == LevelObject.ObjectType.PlayerStart ) {
					playerStart = obj;
				}
				else if( obj.type == LevelObject.ObjectType.EndzoneHandle ) {
					return;
				//	endzoneTopLeft = obj;
				}
		/*		else if( obj.type == LevelObject.ObjectType.EndzoneBottomRight ) {
					endzoneBottomRight = obj;
				}
			*/
				if( ! objects.Contains( obj ) ) {
					objects.Add( obj );
				}
		}

		public LevelObject GetObject( int index )
		{
				if( index < 0 || index >= objects.Count ) return null;
				return objects[ index ];
		}

		public void RemoveObject( LevelObject obj )
		{
				if( ! objects.Contains( obj ) ) return;
				if( obj.type == LevelObject.ObjectType.PlayerStart ) return; // Can not delete player start position.
				objects.Remove( obj );
		}

	#endregion

		/**
		 * <summary>
		 * This function will return the a reference to the closest thing in the level to a given point, be it a
		 * LevelVertex, LevelPolygon, or LevelObject.</summary>
		 * <param name="x">An x-coordinate in the level</param>
		 * <param name="y">A y-coordinate in the level</param>
		 */
		public LevelThing ClosestThingToPoint( float x, float y )
		{
				float dx, dy, d2;

				float      closestDistance2 = float.PositiveInfinity;
				LevelThing closestThing     = null;

				List<LevelThing> things = new List<LevelThing>();

				foreach( LevelVertex  i in vertices ) things.Add( i );
				foreach( LevelPolygon i in polygons ) things.Add( i );
				foreach( LevelObject  i in objects  ) things.Add( i );
				things.Add( playerStart );	
				things.Add( endzoneA );	
				things.Add( endzoneB );	

				for( int i = 0 ; i < things.Count ; ++i )
				{
					dx = things[i].x - x;
					dy = things[i].y - y;
					d2 = dx*dx + dy*dy;

					if( d2 < closestDistance2 ) {
						closestDistance2 = d2;
						closestThing     = things[i];
					}
				}

				return closestThing;
		}


	#region Level Saving and Loading

		public bool Save()
		{
				if( !GenerateTCL() ) { //TODO: The fact that there's a message box in here is pretty greasy.  Use an event and let the editor show the message.
					MessageBox.Show( "Could not save level. Somewhere in the level a polygon is crossing itself.", "Ski Stunt Studio", MessageBoxButtons.OK, MessageBoxIcon.Error );
					return false;
				}
				CreateJpegsForColoredPolygons();
				SaveStudioFile();

				return true;
		}

		private void CreateJpegsForColoredPolygons()
		{
				List<Color>  requiredColorObjs = new List<Color>();
				List<string> requiredColors    = new List<string>();
				List<string> existingColors    = new List<string>();

				string curColor, extension;

				// Determine what colors the polygons in the level are using.
				foreach( LevelPolygon p in polygons )
				{
					if( p.UsingTexture ) continue;
					curColor = ImageTools.HexColorStringRGB( p.Color ).ToLower();
					if( !requiredColors.Contains( curColor ) ) {
						requiredColors.Add( curColor );
						requiredColorObjs.Add( p.Color );
					}
				}

				// Determine which colors are currently saved as single pixel jpegs in the level folder.
				string[] levelFiles = Directory.GetFiles( _levelFolder );
				foreach( string file in levelFiles )
				{
					curColor = file.Substring( file.LastIndexOf( "\\" ) + 1 );
					curColor = curColor.ToLower();

					// We're looking for .jpg files.  That's what this program creates.
					if( curColor.IndexOf( "." ) < 0 ) continue; 
					extension = curColor.Substring( curColor.IndexOf( "." ) + 1 );
					if( extension != "jpg" ) continue;

					// If we've got a jpeg file whose name is 6 hex characters, we've found an existing 1-color texture file.
					curColor  = curColor.Substring( 0, curColor.IndexOf( "." ) );
					Regex r = new Regex( "^[a-f0-9][a-f0-9][a-f0-9][a-f0-9][a-f0-9][a-f0-9]$" );
					if( r.IsMatch( curColor ) ) {
						existingColors.Add( curColor );
					}
				}

				// Delete existing 1-color textures that are no longer required.
				foreach( string color in existingColors )
				{
					if( !requiredColors.Contains( color ) ) File.Delete( _levelFolder + "\\" + color + ".jpg" );
				}

				// Create the required color textures that don't already exist.
				for( int i = 0 ; i < requiredColors.Count ; ++i )
				{
					if( !existingColors.Contains( requiredColors[i] ) )
					{
						Bitmap newJpeg = new Bitmap( 1, 1 );
						newJpeg.SetPixel( 0, 0, requiredColorObjs[ i ] );
						ImageTools.SaveJpeg( _levelFolder + "\\" + requiredColors[i] + ".jpg", newJpeg, 100 );
						newJpeg.Dispose();
					}
				}
		}

		private bool GenerateTCL()
		{
				List<List<Vector2D>>   polygonPoints    = new List<List<Vector2D>>();
				List<List<Triangle2D>> polygonTriangles = new List<List<Triangle2D>>();

				// First we'll triangulate all the polygons to make sure there's no geometry errors.
				for( int i = 0 ; i < polygons.Count ; ++i )
				{
					List<Vector2D>   pts   = polygons[i].GetFinalPolygonPoints();
					List<Triangle2D> trigs = Geometry.TriangulatePolygon( pts );

					if( trigs == null ) return false;

					polygonPoints   .Add( pts   );
					polygonTriangles.Add( trigs );
				}
				
				for( int i = 0 ; i < polygons.Count ; ++i )
				{
					if( polygons[i].SolidAtAll ) // Only generate a pts file if this polygon is interacting with the skier.
					using( StreamWriter ptsFile = new StreamWriter( _levelFolder + "\\gnd" + i.ToString() + ".pts", false ) )
					{
						for( int j = 0 ; j < polygonPoints[i].Count ; ++j )
						{
							ptsFile.Write( polygonPoints[i][j].x.ToString() );
							ptsFile.Write( " " );
							ptsFile.Write( (-polygonPoints[i][j].y).ToString() );
							ptsFile.WriteLine( " " + polygons[i].Friction.ToString() + " 1" );
						}

						ptsFile.Close();
					}

					using( StreamWriter wobjFile = new StreamWriter( _levelFolder + "\\gnd" + i.ToString() + ".wobj", false ) )
					{
						for( int j = 0 ; j < polygonPoints[i].Count ; ++j )
						{
							wobjFile.Write( "v " );
							wobjFile.Write( polygonPoints[i][j].x.ToString() );
							wobjFile.Write( " " );
							wobjFile.Write( (-polygonPoints[i][j].y).ToString() );
							wobjFile.WriteLine( " 0" );
						}
						for( int j = 0 ; j < polygonPoints[i].Count ; ++j )
						{
							wobjFile.Write( "vt " );
							wobjFile.Write( (polygons[i].TextureOffsetX + (polygonPoints[i][j].x / polygons[i].TextureScaleX)).ToString() );
							wobjFile.Write( " " );
							wobjFile.WriteLine( (polygons[i].TextureOffsetY + (polygonPoints[i][j].y / polygons[i].TextureScaleY)).ToString() );
						}

						for( int j = 0 ; j < polygonTriangles[i].Count ; ++j )
						{
							string a = (polygonPoints[i].IndexOf( polygonTriangles[i][j].a ) + 1).ToString();
							string b = (polygonPoints[i].IndexOf( polygonTriangles[i][j].b ) + 1).ToString();
							string c = (polygonPoints[i].IndexOf( polygonTriangles[i][j].c ) + 1).ToString();

							wobjFile.WriteLine( "f " + a + "/" + a + " " + b + "/" + b + " " + c + "/" + c );
						}

						wobjFile.Close();
					}
				}

				using( StreamWriter goFile = new StreamWriter( _levelFolder + "\\go", false ) )
				{
					goFile.WriteLine( "set baseDir ski" );
					goFile.WriteLine( "set scenarioDir " + relativeFolder );
					goFile.WriteLine( "set ::scenarioDir $scenarioDir" );
					goFile.WriteLine( "");
					for( int i = 0 ; i < polygons.Count ; ++i ) 
					{
						if( polygons[i].TextureID >= 0 ) {
							goFile.WriteLine( "loadtexobj $scenarioDir/gnd" + i.ToString() + ".wobj $scenarioDir/"
								 + TEXTURE_NAME_PREFIX + polygons[i].TextureID.ToString() + TEXTURE_NAME_SUFFIX );
						} else {
							goFile.WriteLine( "loadtexobj $scenarioDir/gnd" + i.ToString() + ".wobj $scenarioDir/"
								+ ImageTools.HexColorStringRGB( polygons[i].Color ) + ".jpg" );
						}
						goFile.WriteLine( "loadtexobjtxfm $baseDir/sssDefault.txfm" );

						if( polygons[i].SolidAtAll ) {
							goFile.WriteLine( "gndfile $scenarioDir/gnd" + i.ToString() + ".pts" );
							goFile.WriteLine( "gnd kp " + (2000*polygons[i].Solidity).ToString() );
							goFile.WriteLine( "gnd kd " + (200 - 200*polygons[i].Bounce).ToString() );
							goFile.WriteLine( "gnd cf 0.6" );
						}
						goFile.WriteLine( "");
					}

					goFile.WriteLine( "< $::charDir/common.setup" );
					goFile.WriteLine( "");
					goFile.WriteLine( "set ::restLocX " + ( playerStart.x).ToString() );
					goFile.WriteLine( "set ::restLocY " + (-playerStart.y).ToString() );
					goFile.WriteLine( "set ::goalText \"" + GoalText +"\"" );
					goFile.WriteLine( "set ::hint [list \"" + HintText +"\"]" );

					goFile.WriteLine( "" );
					if( File.Exists( _levelFolder + "\\background.jpg" ) )
						goFile.WriteLine( "texload brick $scenarioDir/background.jpg repeat" );
					else
						goFile.WriteLine( "texload brick ../artwork/moraine128_blur.ppm repeat" );
					goFile.WriteLine( "" );
					goFile.WriteLine( "title \"$::charName >> " + LevelTitle + "\"" );
					goFile.WriteLine( "" );

					if( EndzoneRequirements == EndzoneRequirementType.Unconcious ) {
						goFile.WriteLine( "set ::unconsciousOk 1" );
						goFile.WriteLine( "" );
					}

					// Generate the function responsible for checking if the goal objective has been completed.
					goFile.WriteLine( "proc sideEffects { arg } {" );
					goFile.WriteLine( "" );
					goFile.WriteLine( "    _sideEffects arg" );
					goFile.WriteLine( "    if {$::skierState == $::nextStage} { return }" );
					goFile.WriteLine( "    set goalMet 0" ); 
					goFile.WriteLine( "" );
					if( (uint)EndzoneRequirements >= 1 ) goFile.WriteLine( "    if {$::skierState != $::out} {" );
					if( (uint)EndzoneRequirements >= 2 ) goFile.WriteLine( "    if {$::skierState == $::onGround} {" );
					if( (uint)EndzoneRequirements == 3 ) goFile.WriteLine( "    if {[onSkies $::currState]} {" );
					goFile.WriteLine( "        set x [lindex $::currState 2]" );
					goFile.WriteLine( "        set y [lindex $::currState 3]" );
					goFile.WriteLine( "        if {$x > " +  EndzoneX + " && $x < " +  (EndzoneX+EndzoneWidth)  + "} {" );
					goFile.WriteLine( "        if {$y < " + -EndzoneY + " && $y > " + -(EndzoneY+EndzoneHeight) + "} {" );
					goFile.WriteLine( "            set goalMet 1" );
					goFile.WriteLine( "        }}" );
					if( (uint)EndzoneRequirements == 3 ) goFile.WriteLine( "    }" );
					if( (uint)EndzoneRequirements >= 2 ) goFile.WriteLine( "    }" );
					if( (uint)EndzoneRequirements >= 1 ) goFile.WriteLine( "    }" );
					goFile.WriteLine( "    goal $goalMet" );
					goFile.WriteLine( "}" );
					goFile.WriteLine( "" );

					for( int i = 0 ; i < objects.Count ; ++i ) 
					{
						if( objects[i].type == LevelObject.ObjectType.PlayerStart ) continue;

						string objName = "activeobj" + i.ToString();

						goFile.WriteLine( "" );
						goFile.WriteLine( "< props/" + objects[i].ScriptName + ".setup" );
						goFile.WriteLine( "artfig setname " + objName );

						if( objects[i].IsCollectable ) {
							goFile.WriteLine( "usemonitor forceTrigger 0 -1 1 1 " + objName + "collect \"reset\" \"trig\"" );
							goFile.WriteLine( "usemonitor forceTrigger 0  1 1 1 " + objName + "collect \"reset\" \"trig\"" );
							goFile.WriteLine( "proc " + objName + "collect {argv} {" );
							goFile.WriteLine( "    if {$argv == \"trig\"} {" );
							goFile.WriteLine( objects[i].type == LevelObject.ObjectType.JetpackCrate ? "        enableJetpack" : "        disableJetpack" );
							goFile.WriteLine( "        world setaf " + objName );
							goFile.WriteLine( "        artfig active false" );
							goFile.WriteLine( "    }" );
							goFile.WriteLine( "}" );
						}
					}

					goFile.WriteLine( "" );
					goFile.WriteLine( "proc resetActiveObj {} {" );
					goFile.WriteLine( "" );
					goFile.WriteLine( StartWithJetpack ? "    enableJetpack" : "    disableJetpack" );
					for( int i = 0 ; i < objects.Count ; ++i ) 
					{
						if( objects[i].type == LevelObject.ObjectType.PlayerStart ) continue;
						goFile.WriteLine( "" );
						goFile.WriteLine( "    world setaf activeobj" + i.ToString() );
						goFile.WriteLine( "    restpose " + objects[i].x.ToString() + " " + (-objects[i].y).ToString() + " " + (objects[i].rotation*180f/(float)Math.PI).ToString() );
						if( objects[i].IsCollectable )
						{
							goFile.WriteLine( "    artfig active true" );
							goFile.WriteLine( "    artfig sleep" );
						}
						else switch( objects[i].anchor )
						{
							case LevelObject.AnchorType.Asleep: goFile.WriteLine( "    artfig sleep" ); break;
							case LevelObject.AnchorType.Locked: goFile.WriteLine( "    artfig anchored true" ); break;
						}
					}
					goFile.WriteLine( "}" );

					goFile.WriteLine( "" );
					goFile.WriteLine( "world setaf skier" );	// Define the jetpack and methods to disable/enable it
					goFile.WriteLine( "link setloop jetPack" );
					goFile.WriteLine( "texObj jetPack.skin tex 0" );
					goFile.WriteLine( "loop active false" );
					goFile.WriteLine( "jetpack 3 1.10715 1.307508 -0.10 -0.5 700 step" );
					goFile.WriteLine( "extf name jetpackobject" );
					goFile.WriteLine( "extf eval flameTexObj jetPackFlame.skin" );
					goFile.WriteLine( "extf eval flameSound \"jet -vol 0.85\"" );
					goFile.WriteLine( "extf active false" );
					goFile.WriteLine( "proc enableJetpack {} {" );
					goFile.WriteLine( "    world setaf skier" );
					goFile.WriteLine( "    artfig setlink 3" );
					goFile.WriteLine( "    link setloop jetPack" );
					goFile.WriteLine( "    loop active true" );
					goFile.WriteLine( "    texObj jetPack.skin tex 1" );
					goFile.WriteLine( "    world setextf jetpackobject" );
					goFile.WriteLine( "    extf active true" );
					goFile.WriteLine( "}" );
					goFile.WriteLine( "proc disableJetpack {} {" );
					goFile.WriteLine( "    world setaf skier" );
					goFile.WriteLine( "    artfig setlink 3" );
					goFile.WriteLine( "    link setloop jetPack" );
					goFile.WriteLine( "    loop active false" );
					goFile.WriteLine( "    texObj jetPack.skin tex 0" );
					goFile.WriteLine( "    world setextf jetpackobject" );
					goFile.WriteLine( "    extf active false" );
					goFile.WriteLine( "    texObj jetPackFlame.skin tex 0" );
					goFile.WriteLine( "    extf active false" );
					goFile.WriteLine( "    sound stop \"jet\"" );
					goFile.WriteLine( "}" );

					goFile.Close();
				}

				using( StreamWriter setupFile = new StreamWriter( _levelFolder + "\\setup", false ) )
				{
					setupFile.WriteLine( "set baseDir ski" );
					setupFile.WriteLine( "set scenarioDir " + relativeFolder );
					setupFile.WriteLine( "set ::scenarioDir $scenarioDir" );
					setupFile.WriteLine( "");
					setupFile.WriteLine( "grid" );
					setupFile.WriteLine( "hotzone" );
					if( File.Exists( _levelFolder + "\\preview.jpg" ) )
						setupFile.WriteLine( "texload brick $scenarioDir/preview.jpg repeat" );
					else
						setupFile.WriteLine( "texload brick ../artwork/preview.jpg repeat" );
					setupFile.WriteLine( "title \"$::charName >> " + LevelTitle + "\"" );
					setupFile.WriteLine( "");
					setupFile.Close();
				}

				return true;
		}

		private void SaveStudioFile()
		{
				StreamWriter studioFile = new StreamWriter( _levelFolder + "\\studioFile.txt", false );

				studioFile.WriteLine( LevelTitle );
				studioFile.WriteLine( GoalText   );
				studioFile.WriteLine( HintText   );

				studioFile.WriteLine( playerStart.x );
				studioFile.WriteLine( playerStart.y );
				studioFile.WriteLine( endzoneA.x    );
				studioFile.WriteLine( endzoneA.y    );
				studioFile.WriteLine( endzoneB.x    );
				studioFile.WriteLine( endzoneB.y    );
				studioFile.WriteLine( (uint)EndzoneRequirements );
				
				studioFile.WriteLine( vertices.Count );
				for( int i = 0 ; i < vertices.Count ; ++i )
				{
					studioFile.WriteLine( vertices[i].x );
					studioFile.WriteLine( vertices[i].y );
					studioFile.WriteLine( vertices[i].curveQuality );
				}

				studioFile.WriteLine( polygons.Count );
				for( int i = 0 ; i < polygons.Count ; ++i )
				{
					studioFile.WriteLine( polygons[i].Friction   );
					studioFile.WriteLine( polygons[i].Bounce     );
					studioFile.WriteLine( polygons[i].Solidity   );
					studioFile.WriteLine( polygons[i].SolidAtAll );
					
					studioFile.WriteLine( polygons[i].TextureID );
					if( polygons[i].TextureID >= 0 ) {
						studioFile.WriteLine( polygons[i].TextureOffsetX );
						studioFile.WriteLine( polygons[i].TextureOffsetY );
						studioFile.WriteLine( polygons[i].TextureScaleX );
						studioFile.WriteLine( polygons[i].TextureScaleY );
					} else {
						studioFile.WriteLine( polygons[i].Color.ToArgb() );
					}

					studioFile.WriteLine( polygons[i].VertexCount );
					for( int j = 0 ; j < polygons[i].VertexCount ; ++j ) 
					{
						studioFile.WriteLine( vertices.IndexOf( polygons[i].GetVertex(j) ) );
					}
				}

				studioFile.WriteLine( objects.Count );
				for( int i = 0 ; i < objects.Count ; ++i )
				{
					studioFile.WriteLine( objects[i].x );
					studioFile.WriteLine( objects[i].y );
					studioFile.WriteLine( objects[i].rotation );
					studioFile.WriteLine( (uint)objects[i].type );
					studioFile.WriteLine( (uint)objects[i].anchor );
				}

				studioFile.Close();
		}

		static public SkiStuntLevel LoadFolder( string folder )
		{
				SkiStuntLevel level = new SkiStuntLevel( folder );
				
				if( ! File.Exists( folder + "\\studioFile.txt" ) ) return level;

				StreamReader studioFile = new StreamReader( folder + "\\studioFile.txt", false );

				level.LevelTitle = studioFile.ReadLine();
				level.GoalText   = studioFile.ReadLine();
				level.HintText   = studioFile.ReadLine();
			
				level.PlayerStart.x = float.Parse( studioFile.ReadLine() );
				level.PlayerStart.y = float.Parse( studioFile.ReadLine() );
				level.endzoneA.x    = float.Parse( studioFile.ReadLine() );
				level.endzoneA.y    = float.Parse( studioFile.ReadLine() );
				level.endzoneB.x    = float.Parse( studioFile.ReadLine() );
				level.endzoneB.y    = float.Parse( studioFile.ReadLine() );
				level.EndzoneRequirements = (EndzoneRequirementType)uint.Parse( studioFile.ReadLine() );

				int vertexCount = int.Parse( studioFile.ReadLine() );
				for( int i = 0 ; i < vertexCount ; ++i )
				{
					level.vertices.Add( new LevelVertex
					(
						float.Parse( studioFile.ReadLine() ),
						float.Parse( studioFile.ReadLine() ),
						int  .Parse( studioFile.ReadLine() )
					));
				}

				int polygonCount = int.Parse( studioFile.ReadLine() );
				for( int i = 0 ; i < polygonCount ; ++i )
				{
					LevelPolygon p = new LevelPolygon();

					p.Friction   = float.Parse( studioFile.ReadLine() );
					p.Bounce     = float.Parse( studioFile.ReadLine() );
					p.Solidity   = float.Parse( studioFile.ReadLine() );
					p.SolidAtAll = bool.Parse(  studioFile.ReadLine() );

					p.TextureID = int.Parse( studioFile.ReadLine() );
					if( p.TextureID >= 0 ) {
						p.TextureOffsetX = float.Parse( studioFile.ReadLine() );
						p.TextureOffsetY = float.Parse( studioFile.ReadLine() );
						p.TextureScaleX  = float.Parse( studioFile.ReadLine() );
						p.TextureScaleY  = float.Parse( studioFile.ReadLine() );
					} else {
						p.Color = Color.FromArgb( int.Parse( studioFile.ReadLine() ) );
					}

					int polyVertCount = int.Parse( studioFile.ReadLine() );
					for( int j = 0 ; j < polyVertCount ; ++j )
					{
						p.AddVertex( level.vertices[ int.Parse( studioFile.ReadLine() ) ] );
					}

					level.AddPolygon( p );
				}

				int objectCount = int.Parse( studioFile.ReadLine() );
				for( int i = 0 ; i < objectCount ; ++i )
				{
					LevelObject o = new LevelObject();
					o.x        = float.Parse( studioFile.ReadLine() );
					o.y        = float.Parse( studioFile.ReadLine() );
					o.rotation = float.Parse( studioFile.ReadLine() );
					o.type   = (LevelObject.ObjectType)uint.Parse( studioFile.ReadLine() );
					o.anchor = (LevelObject.AnchorType)uint.Parse( studioFile.ReadLine() );
					level.AddObject( o );
				}

				studioFile.Close();

				return level;
		}

	#endregion


		public void Dispose()
		{
				textureList.Dispose();
		}
	}
}
