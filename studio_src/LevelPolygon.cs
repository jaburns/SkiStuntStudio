using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

using com.jeremyaburns.math;

namespace SkiStuntStudio
{
	// The x and y coordinates of a LevelPolygon store the average position of the vertices that make it up.
	public class LevelPolygon : LevelThing
	{
		private List<LevelVertex> vertices;

		public float Friction;		// Really this is an edge-specific variable, but editing every possible edge of a polygon is a pain in the ass.
		public float Bounce;		// If you increase this, you make a trampoline.
		public float Solidity;		// Some variable I don't fully understand that's a parameter for a GND object, less means you fall through it a bit.
		public bool  SolidAtAll;	// If this is false, a GND file isn't generated for the polygon and the player can't interact with it.

		public int   TextureID; // Texture file to use as the texture for the polyon.  Set to -1 to use a solid color.
		public Color Color;     // Color of the polygon. If TextureID >= 0, this is ignored.

		public float TextureScaleX;
		public float TextureScaleY;
		public float TextureOffsetX;
		public float TextureOffsetY;

		public bool UsingTexture { get { return TextureID >= 0; } }


		private void init()
		{
				Friction   = 0.0f;
				Bounce     = 0.0f;
				Solidity   = 1.0f;
				SolidAtAll = true;

				TextureID = -1;	// Default: use no texture.
				Color     = Color.White; //Default color is white.

				TextureScaleX  = TextureScaleY  = 1.0f;
				TextureOffsetX = TextureOffsetY = 0.0f;

				this.vertices = new List<LevelVertex>();
		}

		public LevelPolygon()
		{
				init();
		}

		public LevelPolygon( LevelVertex[] vertices )
		{
				init();
				
				foreach( LevelVertex vertex in vertices ) {
					vertex.PositionChange += new EventHandler( vertex_PositionChange );
					this.vertices.Add( vertex );
				}

				calculateAverageVertexPosition();
		}

		/**
		 * <summary>
		 * This function will generate a polygon from a set of points described in the format Ski Stunt Simulator uses (GND.TXT / GND.PTS).
		 * It can be used to import an existing level shape, or a single polygon from another Studio level.
		 * </summary>
		 */
		static public LevelPolygon CreateFromFile( string path )
		{
				char[] separators = { ' ', '\t' };
				string[] curLine;
				int curLegitIndex;
				float curReadX = 0.0f;

				LevelPolygon ret = new LevelPolygon();

				using( StreamReader s = new StreamReader( path ) ) {
				while( !s.EndOfStream ) {

					curLegitIndex = 0;
					curLine = s.ReadLine().Split( separators );

					for( int i = 0 ; i < curLine.Length ; ++i )
					{
						if( curLine[i].Length == 0 ) continue;

						switch( curLegitIndex++ )
						{
							case 0:
								curReadX = float.Parse( curLine[i] );
								break;
							case 1:
								ret.AddVertex( new LevelVertex( curReadX, -float.Parse( curLine[i] ) ) );
								break;
							case 2:
								ret.Friction = float.Parse( curLine[i] );
								break;
						}
					}

				}
				s.Close();
				}
				
				return ret;
		}



		public void ListenToTextureList( TextureList textureList )
		{
				textureList.TextureDelete += textureList_textureDelete;
				// TODO:
				// Check to make sure the textureIndex is not out of range here.  If you delete a polygon, then delete all the textures
				// then undo your delete, there will be problems here.
		}

		public void StopListeningToTextureList( TextureList textureList )
		{
				textureList.TextureDelete -= textureList_textureDelete;
		}

		private void textureList_textureDelete( object sender, int textureIndex )
		{
				if( TextureID > textureIndex )
				{
					TextureID--;
				}
				else if( TextureID == textureIndex )
				{
					TextureID = -1;
					Color     = Color.White;
				}
		}


		/**
		 * <summary>Gives the number of vertices that make up the polygon</summary>
		 */
		public int VertexCount { get { return vertices.Count; } }

		/**
		 * <summary>Gives the number of vertices in the polygon that have a curveQuality equal to zero.
		 * These vertices represent the points where the polygon is actually tied to the vertex.</summary>
		 */
		public int NonBezierVertexCount {
		get {
				int count = 0;
				foreach( LevelVertex v in vertices ) {
					if( v.curveQuality == 0 ) count++;
				}
				return count;
			}
		}

		/**
		 * <summary>Provides access to one of the vertex objects in the polygon</summary>
		 * <param name="index">An index number between 0 and VertexCount</param>
		 */
		public LevelVertex GetVertex( int index )
		{
				while( index < 0 ) index += vertices.Count;
				return vertices[ index % vertices.Count ];
		}

		/**
		 * <summary>Returns true if the polygon contains the specified vertex object.</summary>
		 */
		public bool ContainsVertex( LevelVertex vertex )
		{
				return vertices.Contains( vertex );
		}

		/**
		 * <summary>Adds a vertex to the polygon at the end of its internal vertex list, provided that
		 * the specified vertex object is not already part of the polygon.</summary>
		 */
		public void AddVertex( LevelVertex vertex )
		{
				if( !vertices.Contains( vertex ) ) {
					vertices.Add( vertex );
					calculateAverageVertexPosition();
				}
		}

		/**
		 * <summary>Adds a vertex to the polygon at the end of its internal vertex list, provided that
		 * the specified vertex object is not already part of the polygon.</summary>
		 */
		public void InsertVertex( LevelVertex vertex, LevelVertex afterVertex )
		{
				if( !vertices.Contains( afterVertex ) ) afterVertex = vertices[0];
				if( !vertices.Contains( vertex ) ) {
					vertices.Insert( vertices.IndexOf( afterVertex ) + 1, vertex );
					calculateAverageVertexPosition();
				}
		}

		public void RemoveVertex( LevelVertex vertex )
		{
				if( vertices.Contains( vertex ) ) {
					vertices.Remove( vertex );
					calculateAverageVertexPosition();
				}
		}


		public LevelVertex GetPrecedingVertex( LevelVertex v )
		{
				if( ! vertices.Contains( v ) ) return null;

				int i = vertices.IndexOf( v ) - 1;
				
				return ( i < 0 ) ? vertices[ vertices.Count - 1 ] : vertices[ i ];
		}


		override public void SetPosition( float x, float y )
		{
				float dx = x - this.x;
				float dy = y - this.y;

				foreach( LevelVertex v in vertices )
				{
					v.x += dx;
					v.y += dy;
				}

				calculateAverageVertexPosition();
		}

		/**
		 * <summary> This function breaks up the Bezier curves in the polygon and returns a list of
		 * points that describe the actual shape of the polygon as opposed to a list of vertices
		 * where some are simply Bezier curve handles.</summary>
		 */
		public List<Vector2D> GetFinalPolygonPoints()
		{
				List<Vector2D> polygonPoints = new List<Vector2D>();
				List<Vector2D> currentBezier = new List<Vector2D>();
				
				for( int j = 0 ; j < vertices.Count ; ++j )
				{
					if( vertices[j].curveQuality > 0 )
					{
						currentBezier.Add( vertices[ j ].GetVector2D() );
					}
					else if( currentBezier.Count > 0 )
					{
						currentBezier.Add( vertices[ j ].GetVector2D() );

						List<Vector2D> segs = JMath.BreakBezierCurveIntoSegments( currentBezier, vertices[ j-1 ].curveQuality + 2 );
						segs.RemoveAt( 0 );
						segs.RemoveAt( segs.Count - 1 );
						polygonPoints.InsertRange( polygonPoints.Count, segs );

						polygonPoints.Add( vertices[ j ].GetVector2D() );

						currentBezier.Clear();

						if( j < vertices.Count - 1 && vertices[ j+1 ].curveQuality > 0 )
						{
							currentBezier.Add( vertices[ j ].GetVector2D() );
						}
					}
					else if( j < vertices.Count - 1 && vertices[ j+1 ].curveQuality > 0 )
					{
						polygonPoints.Add( vertices[ j ].GetVector2D() );
						currentBezier.Add( vertices[ j ].GetVector2D() );
					}
					else
					{
						polygonPoints.Add( vertices[ j ].GetVector2D() );
					}
				}

				if( ! Geometry.PolygonWindsClockwise( polygonPoints ) ) polygonPoints.Reverse();
				
				return polygonPoints;
		}

		/**
		 * This is called whenever the position of a vertex that is part of this polygon changes.
		 * When this happens, we need to recalculate the average vertex position.
		 */
		private void vertex_PositionChange( object target, EventArgs e )
		{
				calculateAverageVertexPosition();
		}

		/**
		 * Determines the average vertex position and stores it in the polygon's x, y coordinate
		 * variables.  This position is where the polygon handle is painted in the editor.
		 */
		private void calculateAverageVertexPosition()
		{
				float sx = 0, sy = 0;
				for( int i = 0 ; i < vertices.Count ; ++i ) {
					sx += vertices[i].x;
					sy += vertices[i].y;
				}
				this.x = sx / vertices.Count;
				this.y = sy / vertices.Count;
		}
	}
}
