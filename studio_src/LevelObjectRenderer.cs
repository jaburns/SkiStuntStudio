using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using com.jeremyaburns.math;

namespace SkiStuntStudio
{
	static class LevelObjectRenderer
	{
		static private void drawRotatedRectangle( Graphics gfx, Pen oPen, float scale,
												  float ox, float oy, float camx, float camy,
			                                      float x0, float y0,
			                                      float x1, float y1,
			                                      float x2, float y2,
		                                          float x3, float y3, float angle )
		{
				Vector2D p0 = new Vector2D( x0, y0, angle );
				Vector2D p1 = new Vector2D( x1, y1, angle );
				Vector2D p2 = new Vector2D( x2, y2, angle );
				Vector2D p3 = new Vector2D( x3, y3, angle );
				gfx.DrawLine( oPen,
					scale*( ox + p0.x - camx ),
					scale*( oy + p0.y - camy ),
					scale*( ox + p1.x - camx ),
					scale*( oy + p1.y - camy ) );
				gfx.DrawLine( oPen,
					scale*( ox + p1.x - camx ),
					scale*( oy + p1.y - camy ),
					scale*( ox + p2.x - camx ),
					scale*( oy + p2.y - camy ) );
				gfx.DrawLine( oPen,
					scale*( ox + p2.x - camx ),
					scale*( oy + p2.y - camy ),
					scale*( ox + p3.x - camx ),
					scale*( oy + p3.y - camy ) );
				gfx.DrawLine( oPen,
					scale*( ox + p3.x - camx ),
					scale*( oy + p3.y - camy ),
					scale*( ox + p0.x - camx ),
					scale*( oy + p0.y - camy ) );
		}


		static public void RenderObject( Graphics gfx, LevelObject o, Pen oPen, Vector2D camera, float scale )
		{
				switch( o.type )
				{
					case LevelObject.ObjectType.PlayerStart:
						Vector2D p1 = new Vector2D( -0.75f,  0.5f, o.rotation );
						Vector2D p2 = new Vector2D(  1.25f,  0.5f, o.rotation );
						Vector2D p3 = new Vector2D(     0f, -1.5f, o.rotation );
						Vector2D p4 = new Vector2D(     0f,  0.5f, o.rotation );
						gfx.DrawLine( oPen,
							scale*( o.x + p1.x - camera.x ),
							scale*( o.y + p1.y - camera.y ),
							scale*( o.x + p2.x - camera.x ),
							scale*( o.y + p2.y - camera.y ) );
						gfx.DrawLine( oPen,
							scale*( o.x + p3.x - camera.x ),
							scale*( o.y + p3.y - camera.y ),
							scale*( o.x + p4.x - camera.x ),
							scale*( o.y + p4.y - camera.y ) );
						break;

					case LevelObject.ObjectType.BigRock:
						drawRotatedRectangle( gfx, oPen, scale, o.x, o.y, camera.x, camera.y,
							0f, -0.375f,
							1f, -0.375f,
							1f,  0.375f,
							0f,  0.375f, o.rotation );
						break;

					case LevelObject.ObjectType.Tree:
						Vector2D p5 = new Vector2D( -1f,  0f, o.rotation );
						Vector2D p6 = new Vector2D(  1f,  0f, o.rotation );
						Vector2D p7 = new Vector2D(  0f, -3f, o.rotation );
						gfx.DrawLine( oPen,
							scale*( o.x + p5.x - camera.x ),
							scale*( o.y + p5.y - camera.y ),
							scale*( o.x + p6.x - camera.x ),
							scale*( o.y + p6.y - camera.y )
						);
						gfx.DrawLine( oPen,
							scale*( o.x + p6.x - camera.x ),
							scale*( o.y + p6.y - camera.y ),
							scale*( o.x + p7.x - camera.x ),
							scale*( o.y + p7.y - camera.y )
						);
						gfx.DrawLine( oPen,
							scale*( o.x + p7.x - camera.x ),
							scale*( o.y + p7.y - camera.y ),
							scale*( o.x + p5.x - camera.x ),
							scale*( o.y + p5.y - camera.y )
						);
						break;

					case LevelObject.ObjectType.BigBall:
						Vector2D p8 = new Vector2D( 0.4f, -0.8f, o.rotation );
						gfx.DrawEllipse( oPen, 
							scale*( o.x - 0.8f + p8.x - camera.x ),
							scale*( o.y - 0.8f + p8.y - camera.y ),
							1.6f*scale, 1.6f*scale );
						break;

					case LevelObject.ObjectType.SnowmanHead:
						Vector2D p9 = new Vector2D( 0.2f, -0.4f, o.rotation );
						gfx.DrawEllipse( oPen, 
							scale*( o.x - 0.4f + p9.x - camera.x ),
							scale*( o.y - 0.4f + p9.y - camera.y ),
							0.8f*scale, 0.8f*scale );
						break;

					case LevelObject.ObjectType.GiantSpBoard:
						drawRotatedRectangle( gfx, oPen, scale, o.x, o.y, camera.x, camera.y,
							                  0f, 0f,
											  7f, 0f,
											  7f, 0.4f,
											  0f, 0.4f, o.rotation );
						Vector2D pA = new Vector2D( 7f, 0.4f, o.rotation );
						gfx.DrawLine( oPen,
							scale*( o.x        - camera.x ),
							scale*( o.y        - camera.y ),
							scale*( o.x + pA.x - camera.x ),
							scale*( o.y + pA.y - camera.y )
						);
						break;

					case LevelObject.ObjectType.SeeSaw:
						drawRotatedRectangle( gfx, oPen, scale, o.x, o.y, camera.x, camera.y,
							                  0f, 0f,
											  5f, 0f,
											  5f, 0.4f,
											  0f, 0.4f, o.rotation );
						break;

					case LevelObject.ObjectType.NopackCrate:
						Vector2D pB = new Vector2D( 0.2f, -0.8f, o.rotation );
						Vector2D pC = new Vector2D( 0.8f, -0.2f, o.rotation );
						gfx.DrawLine( oPen,
							scale*( o.x + pB.x - camera.x ),
							scale*( o.y + pB.y - camera.y ),
							scale*( o.x + pC.x - camera.x ),
							scale*( o.y + pC.y - camera.y )
						);
						goto case LevelObject.ObjectType.JetpackCrate;

					case LevelObject.ObjectType.JetpackCrate:
						Vector2D pD = new Vector2D( 0.7f, -0.7f, o.rotation );
						Vector2D pE = new Vector2D( 0.3f, -0.3f, o.rotation );
						gfx.DrawLine( oPen,
							scale*( o.x + pD.x - camera.x ),
							scale*( o.y + pD.y - camera.y ),
							scale*( o.x + pE.x - camera.x ),
							scale*( o.y + pE.y - camera.y )
						);
						goto case LevelObject.ObjectType.Crate;

					case LevelObject.ObjectType.Crate:
						drawRotatedRectangle( gfx, oPen, scale, o.x, o.y, camera.x, camera.y,
							0f,  0f,
							1f,  0f,
							1f, -1f,
							0f, -1f, o.rotation );
						break;
				}
		}
	}
}
