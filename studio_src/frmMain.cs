using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using System.IO;

using com.jeremyaburns.math;

namespace SkiStuntStudio
{

	public partial class frmMain : Form
	{
		private const Keys DELETE_KEY = Keys.X;
		private const Keys CREATE_KEY = Keys.ControlKey;
		private const Keys INSERT_KEY = Keys.ShiftKey;

		private const Keys MOUSE_WHEEL_UP_KEY   = Keys.A;
		private const Keys MOUSE_WHEEL_DOWN_KEY = Keys.Z;

		private const float MOUSE_SELECT_DIST = 10;

		private const int MAX_UNDOS = 50;


		private SkiStuntLevel level;
		private Vector2D      camera;
		private float         scale;

		private Bitmap   buffer;
		private Graphics gfx;
		private Graphics editorBoxGraphics;

		private Vector2D mouseDownPos; // The location (in level coordinates) where the mouse button was last pressed

		private Point mouse;

		private LevelThing activeThing; // This contains a reference to the object sitting under the mouse.

		private bool     cameraDragging;  // When this is true, the camera moves around with the mouse cursor.
		private Vector2D cameraGrabPoint; // When moving the camera, this is the position in the level where the mouse was initially pressed.

		private bool staleGraphics;	// If this is set to true, the render function is invoked on it's timer interval.

		private bool objectDragging;

		private List<LevelVertex> polygonUnderConstruction; // Stores a list of vertices that will become a polygon. Null if a polygon is not being created.

		private List<Keys> pressedKeys; // Keyboard Event handlers for the form keep this list populated with keys that are currently pressed.

		private Stack<Action> undoStack;
		private Stack<Action> redoStack;



		private void saveJpeg( Bitmap bmp, string path )
		{
			//	EncoderParameters encoderParams = new EncoderParameters(1);
			//	encoderParams.Param[0] = new EncoderParameter( System.Drawing.Imaging.Encoder.Quality, 5 );

				bmp.Save( path, ImageFormat.Jpeg );
		}


		public frmMain( string levelFolder )
		{
				InitializeComponent();

				MouseWheel += new System.Windows.Forms.MouseEventHandler( frmMain_MouseWheel );

				level  = SkiStuntLevel.LoadFolder( levelFolder );
				scale  = 20;
				camera = new Vector2D( level.PlayerStart.x - 10, level.PlayerStart.y - 10 );
			
				mouse = new Point( 0, 0 );
				mouseDownPos = new Vector2D( 0, 0 );

				staleGraphics = true;

				cameraDragging = false;
				objectDragging = false;

				pressedKeys = new List<Keys>();

				undoStack = new Stack<Action>( MAX_UNDOS );
				redoStack = new Stack<Action>( MAX_UNDOS );

				playerStartsWithJetpackToolStripMenuItem.Checked = level.StartWithJetpack;

				createGraphicsObjects();

				txtTitle.Text = level.LevelTitle;

			//	radEdit.BackgroundImage = Image.FromFile( rootFolder + "scripts\\props\\crate.jpg"     );
				radEdit.Checked = true;

				radCrate  .BackgroundImage = Image.FromFile( Program.ROOT_PATH + "scripts\\props\\crate.jpg"     );
				radBigRock.BackgroundImage = Image.FromFile( Program.ROOT_PATH + "scripts\\props\\bigRock.jpg"   );
				radTree   .BackgroundImage = Image.FromFile( Program.ROOT_PATH + "scripts\\props\\tree.jpg"      );
				radBigBall.BackgroundImage = Image.FromFile( Program.ROOT_PATH + "scripts\\props\\giantBall.jpg" );
				radSnowmanHead .BackgroundImage = Image.FromFile( Program.ROOT_PATH + "scripts\\props\\snowmanHead.jpg" );
				radGiantSpBoard.BackgroundImage = Image.FromFile( Program.ROOT_PATH + "scripts\\props\\springBoard.jpg" );
				radSeeSaw      .BackgroundImage = Image.FromFile( Program.ROOT_PATH + "scripts\\props\\seeSaw.jpg" );
				radJetpackCrate.BackgroundImage = Image.FromFile( Program.ROOT_PATH + "scripts\\props\\jetCrate.jpg" );		
				radNopackCrate .BackgroundImage = Image.FromFile( Program.ROOT_PATH + "scripts\\props\\nopackCrate.jpg" );		
		}


		private bool insertAnObject( float x, float y )
		{
				bool addObject = false;
				LevelObject.ObjectType type = LevelObject.ObjectType.Crate;

				if( radCrate.Checked )
				{
					addObject = true;
					type = LevelObject.ObjectType.Crate;
				}
				else if( radBigRock.Checked )
				{
					addObject = true;
					type = LevelObject.ObjectType.BigRock;
				}
				else if( radTree.Checked )
				{
					addObject = true;
					type = LevelObject.ObjectType.Tree;
				}
				else if( radBigBall.Checked )
				{
					addObject = true;
					type = LevelObject.ObjectType.BigBall;
				}
				else if( radSnowmanHead.Checked )
				{
					addObject = true;
					type = LevelObject.ObjectType.SnowmanHead;
				}
				else if( radGiantSpBoard.Checked )
				{
					addObject = true;
					type = LevelObject.ObjectType.GiantSpBoard;
				}
				else if( radSeeSaw.Checked )
				{
					addObject = true;
					type = LevelObject.ObjectType.SeeSaw;
				}
				else if( radJetpackCrate.Checked )
				{
					addObject = true;
					type = LevelObject.ObjectType.JetpackCrate;
				}
				else if( radNopackCrate.Checked )
				{
					addObject = true;
					type = LevelObject.ObjectType.NopackCrate;
				}

				if( addObject ) 
				{
					LevelObject o = new LevelObject( type, x, y );
					level.AddObject( o );
					recordUndoableAction( new InsertObjectAction( o ) );
					return true;
				}

				return false;
		}


		private void EditorBox_rightMouseClick( Point location )
		{
				if( activeThing is LevelObject ) 
				{
					LevelObject lo = activeThing as LevelObject;

					if( lo.type == LevelObject.ObjectType.PlayerStart   ) return; // No right-click menu for player starting position.
					if( lo.type == LevelObject.ObjectType.EndzoneHandle ) 
					{
						resetMnuEndzoneChecked();
						mnuEndzone.Show( EditorBox, location );
					}
					else
					{
						resetMnuObjectChecked( activeThing as LevelObject );
						mnuObject.Show( EditorBox, location );
					}
				}
				else if( activeThing is LevelPolygon )
				{
					resetMnuPolygon( activeThing as LevelPolygon );
					mnuPolygon.Show( EditorBox, location );
				}
		}


		private void EditorBox_MouseDown( object sender, MouseEventArgs e )
		{
				determineActiveObject( e.X/scale + camera.x , e.Y/scale + camera.y );

				if( e.Button == MouseButtons.Right )
				{
					EditorBox_rightMouseClick( e.Location );
					return;
				}
				
				if( activeThing == null )	// If there's nothing under the mouse when it's clicked, add a vertex.
				{
					if( pressedKeys.Contains( CREATE_KEY ) )
					{
						LevelVertex newVertex = new LevelVertex( e.X/scale + camera.x, e.Y/scale + camera.y );
						level.AddVertex( newVertex );
						activeThing = newVertex;
						staleGraphics = true;
					}
					else if( insertAnObject( e.X/scale + camera.x, e.Y/scale + camera.y ) )
					{
						staleGraphics = true;
					}
					else
					{
						cameraGrabPoint.x = e.X/scale + camera.x;
						cameraGrabPoint.y = e.Y/scale + camera.y;
						cameraDragging = true;
					}
				}
				else if( pressedKeys.Contains( DELETE_KEY ) ) // If the delete modifier is being held, try to delete the activeThing.
				{
					if( activeThing is LevelVertex ) {
						recordUndoableAction( level.RemoveVertex( activeThing as LevelVertex ) );
					} else if( activeThing is LevelPolygon ) {
						recordUndoableAction( new DeletePolygonAction( activeThing as LevelPolygon ) );
						level.RemovePolygon( activeThing as LevelPolygon );
					} else if( activeThing is LevelObject ) {
						recordUndoableAction( new DeleteObjectAction( activeThing as LevelObject ) );
						level.RemoveObject( activeThing as LevelObject );
					}
					activeThing = null;
					staleGraphics = true;
				}
				else if( pressedKeys.Contains( INSERT_KEY ) && activeThing is LevelVertex )
				{
					for( int i = 0 ; i < level.PolygonCount ; ++i )
					{
						if( level.GetPolygon(i).ContainsVertex( activeThing as LevelVertex ) )
						{
							LevelVertex newVertex = new LevelVertex( e.X/scale + camera.x, e.Y/scale + camera.y );
							level.AddVertex( newVertex );
							level.GetPolygon(i).InsertVertex( newVertex, activeThing as LevelVertex );

							recordUndoableAction( new InsertVertexAction( level.GetPolygon(i), activeThing as LevelVertex, newVertex ) );

							activeThing = newVertex;
							staleGraphics = true;
							objectDragging = true;
						}
					}
				}
				else // There's an object under the mouse and no modifier keys are being held, start dragging the object around.
				{
					objectDragging = true;
					mouseDownPos.x = activeThing.x;
					mouseDownPos.y = activeThing.y;	// TODO: find replace mouseDownPos to objectDragInitialPos
				}


				if( pressedKeys.Contains( CREATE_KEY ) && activeThing is LevelVertex ) // If the create modifier is behing held, start/continue creating a polygon.
				{
					if( polygonUnderConstruction == null ) {
						polygonUnderConstruction = new List<LevelVertex>();
					}
					polygonUnderConstruction.Add( activeThing as LevelVertex );
				}
		}


		private void EditorBox_MouseMove( object sender, MouseEventArgs e )
		{
				mouse.X = e.X;	// The rendering code needs to know where the mouse is, so we keep track of it.
				mouse.Y = e.Y;

				if( cameraDragging )
				{
					camera.x = cameraGrabPoint.x - e.X/scale;
					camera.y = cameraGrabPoint.y - e.Y/scale;
				}
				else if( objectDragging )
				{
					activeThing.SetPosition( e.X/scale + camera.x, e.Y/scale + camera.y );
				}
				else {
					determineActiveObject( e.X/scale + camera.x , e.Y/scale + camera.y );
				}

				staleGraphics = true;
		}


		private void determineActiveObject( float mx, float my )
		{
				LevelThing closestThing = level.ClosestThingToPoint( mx, my );
				
				if( closestThing == null ) return;

				activeThing = null;
			
				float dx = closestThing.x - mx;
				float dy = closestThing.y - my;
			
				if( dx*dx + dy*dy < MOUSE_SELECT_DIST*MOUSE_SELECT_DIST / (scale*scale) ) {
					activeThing = closestThing;
				}
		}


		private void condensePolygonUnderConstruction()
		{
				if( polygonUnderConstruction.Count >= 3 ) 
				{
					LevelPolygon newPolygon = new LevelPolygon( polygonUnderConstruction.ToArray() );
					level.AddPolygon( newPolygon );

					recordUndoableAction( new CreatePolygonAction( newPolygon ) );
				}
				else {
					level.CullUnusedVertices();
				}

				polygonUnderConstruction = null;
				staleGraphics = true;
		}


		private void mouseWheelBehavior( bool inwards )
		{
				if( activeThing is LevelVertex )
				{
					LevelVertex v = activeThing as LevelVertex;
					if( inwards ) {
						recordUndoableAction( new VertexCurveQualityAction( v, v.curveQuality, ++v.curveQuality ) );
					} else {
						recordUndoableAction( new VertexCurveQualityAction( v, v.curveQuality, --v.curveQuality ) );
					}
					staleGraphics = true;
				}
				else if( activeThing is LevelObject )
				{
					LevelObject o = activeThing as LevelObject;
					if( inwards ) {
						o.rotation += 5f * (float)Math.PI / 180f;
					} else {
						o.rotation -= 5f * (float)Math.PI / 180f;
					}
					staleGraphics = true;
				}
				else
				{
					float clickDistFromCamX = mouse.X/scale;
					float clickDistFromCamY = mouse.Y/scale;
					float clickX = clickDistFromCamX + camera.x;
					float clickY = clickDistFromCamY + camera.y;

					if( inwards ) {
						scale *= 1.25F;
						clickDistFromCamX /= 1.25F;
						clickDistFromCamY /= 1.25F;
					} else {
						scale /= 1.25F;
						clickDistFromCamX *= 1.25F;
						clickDistFromCamY *= 1.25F;
					}

					camera.x = clickX - clickDistFromCamX;
					camera.y = clickY - clickDistFromCamY;

					staleGraphics = true;
				}
		}


		private void frmMain_MouseWheel( object sender, MouseEventArgs e )
		{
				mouseWheelBehavior( e.Delta > 0 );
		}


		private void EditorBox_MouseUp( object sender, MouseEventArgs e )
		{
				if( objectDragging )
				{
					activeThing.SetPosition( e.X/scale + camera.x, e.Y/scale + camera.y );
					recordUndoableAction( new MoveThingAction( mouseDownPos.x, mouseDownPos.y,
						                                       e.X/scale + camera.x, e.Y/scale + camera.y, activeThing ) );
				}

				cameraDragging = false;
				objectDragging = false;
		}


	#region Graphics and Rendering

		private void RenderTimer_Tick(object sender, EventArgs e)
		{
				if( staleGraphics )
				{
					render();
					staleGraphics = false;
				}
		}

		private void createGraphicsObjects()
		{
				if( EditorBox.Width == 0 ) return; // Don't create a new graphics object when minimized.

				buffer = new Bitmap( EditorBox.Width, EditorBox.Height );
				gfx = Graphics.FromImage( buffer );

				editorBoxGraphics = EditorBox.CreateGraphics();
		}

		private void render()
		{
				gfx.Clear( Color.Black );

				// Draw all the polygons in the map.
				for( int i = 0 ; i < level.PolygonCount ; ++i )
				{
					List<Vector2D> polygonPoints     = level.GetPolygon(i).GetFinalPolygonPoints();
					List<Point>    polygonDrawPoints = new List<Point>();

					for( int j = 0 ; j < polygonPoints.Count ; ++j )
					{
						polygonDrawPoints.Add( new Point( (int)(scale*( polygonPoints[j].x - camera.x )),
						                                  (int)(scale*( polygonPoints[j].y - camera.y )) ) );
					}

					bool isActive = (level.GetPolygon(i) == activeThing);
					
					Brush b;
					if( isActive ) {
						b = new SolidBrush( Color.OrangeRed );
					} else {
						if( level.GetPolygon(i).TextureID >= 0 ) {
							b = level.TextureList.GetTextureBrush( level.GetPolygon(i).TextureID );
						} else {
							b = new SolidBrush( level.GetPolygon(i).Color );
						}
					}

					gfx.FillPolygon( b, polygonDrawPoints.ToArray() );
					gfx.DrawPolygon( new Pen( Color.Gray, 2 ), polygonDrawPoints.ToArray() );

					int diam = isActive ? 6 : 4 ;
					int radius = diam / 2;
					gfx.DrawEllipse( new Pen( Color.OrangeRed, 1 ), scale*( level.GetPolygon(i).x - camera.x ) - radius, scale*( level.GetPolygon(i).y - camera.y ) - radius, diam, diam );
				}

				// If a polygon is currently being created, draw it.
				if( polygonUnderConstruction != null )
				{
					Pen pucPen = new Pen( Color.WhiteSmoke, 2 );

					Point p0 = new Point( (int)(scale*( polygonUnderConstruction[0].x - camera.x )),
						                  (int)(scale*( polygonUnderConstruction[0].y - camera.y )) );
					Point p1;

					for( int i = 1 ; i < polygonUnderConstruction.Count ; ++i )
					{
						p1 = new Point( (int)(scale*( polygonUnderConstruction[i].x - camera.x )),
							            (int)(scale*( polygonUnderConstruction[i].y - camera.y )) );
						gfx.DrawLine( pucPen, p0, p1 );
						p0 = p1;
					}

					p1 = new Point( mouse.X, mouse.Y );
					gfx.DrawLine( pucPen, p0, p1 );
				}

				// Draw all the vertices in the map.
				for( int i = 0 ; i < level.VertexCount ; ++i )
				{
					int diam = (level.GetVertex(i) == activeThing) ? 6 : 4 ;
					int radius = diam / 2;
					Color vertexColor = (level.GetVertex(i).curveQuality > 0) ? Color.Goldenrod : Color.White ;
					gfx.DrawEllipse( new Pen( vertexColor, 1 ), scale*( level.GetVertex(i).x - camera.x ) - radius, scale*( level.GetVertex(i).y - camera.y ) - radius, diam, diam );
				}

				for( int i = 0 ; i < level.ObjectCount ; ++i )
				{
					drawObject( level.GetObject(i) );
				}

				drawObject( level.PlayerStart );
				drawObject( level.EndzoneA );
				drawObject( level.EndzoneB );

				gfx.DrawRectangle( new Pen( Color.Blue, 1 ), scale*( level.EndzoneX - camera.x ), scale*( level.EndzoneY - camera.y ),
					                                         scale*( level.EndzoneWidth ), scale*( level.EndzoneHeight ) );

				editorBoxGraphics.DrawImageUnscaled( buffer, 0, 0 );
		}

		private void drawObject( LevelObject o )
		{
				LevelObjectRenderer.RenderObject( gfx, o, new Pen( Color.Cyan, 1 ), camera, scale );

				// Draw handle
				int sdiam = (o == activeThing) ? 6 : 4 ;
				int sradius = sdiam / 2;
				gfx.DrawEllipse( new Pen( Color.Cyan, 1 ),
								scale*( o.x - camera.x ) - sradius,
								scale*( o.y - camera.y ) - sradius, sdiam, sdiam );
		}

	#endregion

		/**
		 * When the form resizes, the EditorBox resizes as well. In order to paint to the new region properly, a new
		 * graphics object must be created.
		 */
		private void frmMain_Resize( object sender, EventArgs e )
		{
				createGraphicsObjects();
				staleGraphics = true;
		}


	#region Keyboard Input Event Handlers

		private void frmMain_KeyDown( object sender, KeyEventArgs e )
		{
				if( !pressedKeys.Contains( e.KeyCode ) ) {
					pressedKeys.Add( e.KeyCode );
				}

				if( e.KeyCode == MOUSE_WHEEL_UP_KEY ) {
					mouseWheelBehavior( true );
				} else if ( e.KeyCode == MOUSE_WHEEL_DOWN_KEY ) {
					mouseWheelBehavior( false );
				}
		}

		private void frmMain_KeyUp( object sender, KeyEventArgs e )
		{
				if( pressedKeys.Contains( e.KeyCode ) ) {
					pressedKeys.Remove( e.KeyCode );
				}

				if( polygonUnderConstruction != null ) {
					condensePolygonUnderConstruction();
				}
		}

	#endregion

		private void saveLevelToolStripMenuItem_Click(object sender, EventArgs e)
		{
				level.Save();
				Program.levelManager.UpdateLevelList();
		}

	#region Undo and Redo Handlers
		
		/**
		 * This function pushes an Action on to the undoStack and resets the redoStack
		 * to avoid performing actions that are no longer valid.
		 */
		private void recordUndoableAction( Action a )
		{
				undoStack.Push( a );
				redoStack.Clear();
		}

		private void undoToolStripMenuItem_Click( object sender, EventArgs e )
		{
				if( undoStack.Count == 0 ) return;

				Action axn = undoStack.Pop();
				
				if( axn is MoveThingAction )
				{
					MoveThingAction a = axn as MoveThingAction;
					a.relevantThing.SetPosition( a.x0, a.y0 );
				}

				else if( axn is DeleteVertexAction )
				{
					DeleteVertexAction a = axn as DeleteVertexAction;
					level.AddVertex( a.vertexDeleted );
					foreach( LevelPolygon p in a.collateralPolygons ) {
						level.AddPolygon( p );
					}
					for( int i = 0 ; i < a.polygonsAffected.Count ; ++i ) {
						a.polygonsAffected[i].InsertVertex( a.vertexDeleted, a.precedingVertices[i] );
					}
				}

				else if( axn is DeletePolygonAction )
				{
					level.AddPolygon( (axn as DeletePolygonAction).relevantPolygon );
				}

				else if( axn is VertexCurveQualityAction )
				{
					VertexCurveQualityAction a = axn as VertexCurveQualityAction;
					a.relevantVertex.curveQuality = a.previousCurveQuality;
				}

				else if( axn is InsertVertexAction )
				{
					level.RemoveVertex( (axn as InsertVertexAction).newVertex );
				}

				else if( axn is CreatePolygonAction )
				{
					level.RemovePolygon( (axn as CreatePolygonAction).newPolygon );
				}

				else if( axn is InsertObjectAction )
				{
					level.RemoveObject( (axn as InsertObjectAction).relevantObject );
				}

				else if( axn is DeleteObjectAction )
				{
					level.AddObject( (axn as DeleteObjectAction).relevantObject );
				}

				redoStack.Push( axn );
				staleGraphics = true;
		}

		private void redoToolStripMenuItem_Click( object sender, EventArgs e )
		{
				if( redoStack.Count == 0 ) return;

				Action axn = redoStack.Pop();
				
				if( axn is MoveThingAction )
				{
					MoveThingAction a = axn as MoveThingAction;
					a.relevantThing.SetPosition( a.x1, a.y1 );
				}

				else if( axn is DeleteVertexAction )
				{
					axn = level.RemoveVertex( (axn as DeleteVertexAction).vertexDeleted );
				}
					
				else if( axn is DeletePolygonAction )
				{
					level.RemovePolygon( (axn as DeletePolygonAction).relevantPolygon );
				}
					
				else if( axn is VertexCurveQualityAction )
				{
					VertexCurveQualityAction a = axn as VertexCurveQualityAction;
					a.relevantVertex.curveQuality = a.newCurveQuality;
				}
				
				else if( axn is InsertVertexAction )
				{
					InsertVertexAction a = axn as InsertVertexAction;
					a.targetPolygon.InsertVertex( a.newVertex, a.precedingVertex );
				}
				
				else if( axn is CreatePolygonAction )
				{
					level.AddPolygon( (axn as CreatePolygonAction).newPolygon );
				}

				else if( axn is InsertObjectAction )
				{
					level.AddObject( (axn as InsertObjectAction).relevantObject );
				}

				else if( axn is DeleteObjectAction )
				{
					level.RemoveObject( (axn as DeleteObjectAction).relevantObject );
				}
				
				undoStack.Push( axn );		 
				staleGraphics = true;
		}

	#endregion

		private void skiStuntSimulatorToolStripMenuItem_Click(object sender, EventArgs e)
		{
				Process.Start( new ProcessStartInfo( Program.ROOT_PATH + "\\bin\\skiStunt.exe" ) );
		}

		private void playerStartsWithJetpackToolStripMenuItem_Click(object sender, EventArgs e)
		{
				playerStartsWithJetpackToolStripMenuItem.Checked = !playerStartsWithJetpackToolStripMenuItem.Checked;
				level.StartWithJetpack = playerStartsWithJetpackToolStripMenuItem.Checked;
		}

	#region Polygon Context Menu

		private void resetMnuPolygon( LevelPolygon p )
		{
				frictionToolStripTextBox.Text = p.Friction.ToString();
				bounceToolStripTextBox  .Text = p.Bounce  .ToString();
				solidityToolStripTextBox.Text = p.Solidity.ToString();
				solidAtAllToolStripMenuItem.Checked = p.SolidAtAll;
		}

		private void frictionToolStripTextBox_TextChanged(object sender, EventArgs e)
		{
				float.TryParse( frictionToolStripTextBox.Text, out (activeThing as LevelPolygon).Friction );
		}

		private void bounceToolStripTextBox_TextChanged(object sender, EventArgs e)
		{
				float.TryParse( bounceToolStripTextBox.Text, out (activeThing as LevelPolygon).Bounce );
		}

		private void solidityToolStripTextBox_TextChanged(object sender, EventArgs e)
		{
				float.TryParse( solidityToolStripTextBox.Text, out (activeThing as LevelPolygon).Solidity );
		}

		private void polygonTextBox_Leave(object sender, EventArgs e)
		{
				resetMnuPolygon( activeThing as LevelPolygon );
		}

		private void polygonBFmenuItem_Click(object sender, EventArgs e)
		{
				level.MovePolygonForward( activeThing as LevelPolygon );
				staleGraphics = true;
		}

		private void polygonSBmenuItem_Click(object sender, EventArgs e)
		{
				level.MovePolygonBackward( activeThing as LevelPolygon );
				staleGraphics = true;
		}

		private void polygonBTFmnuItem_Click(object sender, EventArgs e)
		{
				level.MovePolygonToFront( activeThing as LevelPolygon );
				staleGraphics = true;
		}

		private void polygonSTBmenuItem_Click(object sender, EventArgs e)
		{
				level.MovePolygonToBack( activeThing as LevelPolygon );
				staleGraphics = true;
		}

	#endregion

	#region Object Context Menu

		private void resetMnuObjectChecked( LevelObject o )
		{
				mnuObject_AlwaysActive.Checked = (o.anchor == LevelObject.AnchorType.Free);
				mnuObject_Asleep      .Checked = (o.anchor == LevelObject.AnchorType.Asleep);
				mnuObject_Anchored    .Checked = (o.anchor == LevelObject.AnchorType.Locked);

				mnuObject_AlwaysActive.Enabled = ! o.IsCollectable;
				mnuObject_Anchored    .Enabled = ! o.IsCollectable;
		}

		private void mnuObject_AlwaysActive_Click( object sender, EventArgs e )
		{
				(activeThing as LevelObject).anchor = LevelObject.AnchorType.Free;
		}

		private void mnuObject_Asleep_Click( object sender, EventArgs e )
		{
				(activeThing as LevelObject).anchor = LevelObject.AnchorType.Asleep;
		}

		private void mnuObject_Anchored_Click( object sender, EventArgs e )
		{
				(activeThing as LevelObject).anchor = LevelObject.AnchorType.Locked;
		}

	#endregion

	#region Endzone Context Menu

		private void resetMnuEndzoneChecked()
		{
				mnuEndzone_Unconcious.Checked = (level.EndzoneRequirements == EndzoneRequirementType.Unconcious);
				mnuEndzone_Alive     .Checked = (level.EndzoneRequirements == EndzoneRequirementType.Alive);
				mnuEndzone_Ground    .Checked = (level.EndzoneRequirements == EndzoneRequirementType.Grounded);
				mnuEndzone_Skis      .Checked = (level.EndzoneRequirements == EndzoneRequirementType.OnSkis);
		}
		
		private void mnuEndzone_Unconcious_Click(object sender, EventArgs e)
		{
				level.EndzoneRequirements = EndzoneRequirementType.Unconcious;
		}

		private void mnuEndzone_Alive_Click(object sender, EventArgs e)
		{
				level.EndzoneRequirements = EndzoneRequirementType.Alive;
		}

		private void mnuEndzone_Ground_Click(object sender, EventArgs e)
		{
				level.EndzoneRequirements = EndzoneRequirementType.Grounded;
		}

		private void mnuEndzone_Skis_Click(object sender, EventArgs e)
		{
				level.EndzoneRequirements = EndzoneRequirementType.OnSkis;
		}

	#endregion

		private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
		{
				frmJpegLoader bgLoader = new frmJpegLoader( level.LevelFolder + "\\background.jpg", 1024, 512, level );
				bgLoader.ShowDialog( this );
		}

		private void customPreviewToolStripMenuItem_Click(object sender, EventArgs e)
		{
				frmJpegLoader bgLoader = new frmJpegLoader( level.LevelFolder + "\\preview.jpg", 512, 256, level );
				bgLoader.ShowDialog( this );
		}

		private void txtTitle_TextChanged(object sender, EventArgs e)
		{
				level.LevelTitle = txtTitle.Text;
		}

		private void selectColorTextureToolStripMenuItem_Click(object sender, EventArgs e)
		{
				// One can assume activeThing is of type LevelPolygon because you have to right-click one to get here.
				frmTextureSelect tSel = new frmTextureSelect( level, activeThing as LevelPolygon );
				tSel.ShowDialog( this );
		}

		private void frmMain_Deactivate(object sender, EventArgs e)
		{
				editorBoxGraphics.Clear( Color.Black );
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
				level.Dispose();
		}

		private void importGNDFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
				OpenFileDialog dlg = new OpenFileDialog();

				dlg.CheckFileExists = true;
				dlg.Filter = "GND Files (*.txt,*.pts)|*.TXT;*.PTS|All Files (*.*)|*.*";

				if( dlg.ShowDialog() != DialogResult.OK ) return;

				level.AddPolygon( LevelPolygon.CreateFromFile( dlg.FileName ) );
				staleGraphics = true;
		}

		private void returnToolStripMenuItem_Click(object sender, EventArgs e)
		{
				this.Close();
		}

		private void solidAtAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
				solidAtAllToolStripMenuItem.Checked = !solidAtAllToolStripMenuItem.Checked;
				(activeThing as LevelPolygon).SolidAtAll = solidAtAllToolStripMenuItem.Checked;
		}

	#region Goal and Hint Text Editing
		private void goalTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
				string goalText = level.GoalText;
				DialogResult r = Program.InputBox( "Ski Stunt Studio", "Enter text you want to appear as the goal for the level." , ref goalText );
				if( r != DialogResult.OK ) return;
				level.GoalText = goalText;
		}
		private void hintTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
				string hintText = level.HintText;
				DialogResult r = Program.InputBox( "Ski Stunt Studio", "Enter text you want to appear as the hint for the level." , ref hintText );
				if( r != DialogResult.OK ) return;
				level.HintText = hintText;				
		}
	#endregion
	}
}
