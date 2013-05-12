
using System.Collections.Generic;
using System.Drawing;
using System;

/**
 * This file defines several useful structures and classes for dealing with geometry.
 */
namespace com.jeremyaburns.math
{
	/**
	 * <summary>This structure is useful for representing vectors and points in 2D space.</summary>
	 */
	public struct Vector2D
	{
		public float x;
		public float y;

		public Vector2D( float x, float y )
		{
				this.x = x;
				this.y = y;
		}

		public Vector2D( double x, double y )
		{
				this.x = (float)x;
				this.y = (float)y;
		}

		public Vector2D( float x, float y, float rotation ) : this( x, y )
		{
				rotate( rotation );
		}

		public Vector2D Clone()
		{
				return new Vector2D( x, y );
		}

		public void rotate( float radians )
		{
				float oldx = x;
				x = oldx *  (float)Math.Cos( (double)radians ) + y * (float)Math.Sin( (double)radians );
				y = oldx * -(float)Math.Sin( (double)radians ) + y * (float)Math.Cos( (double)radians );
		}

		public void rotateDegrees( float degrees )
		{
				rotate( degrees * 180f / (float)Math.PI );
		}
	}

	/**
	 * <summary>This structure simply describes a collection of 3 points in 2D space.</summary>
	 */
	public struct Triangle2D
	{
		public Vector2D a;
		public Vector2D b;
		public Vector2D c;

		public Triangle2D( Vector2D a, Vector2D b, Vector2D c )
		{
				this.a = a;
				this.b = b;
				this.c = c;
		}
	}

	/**
	 * <summary>This class provides a collection of static methods useful when dealing with geometry.</summary>
	 */
	public static class Geometry
	{
		/**
		 * <summary>
		 * This method returns true if the point specified as the third parameter lies to the right of
		 * the infinite line extending through 'a' and then 'b' respectively.
		 * </summary>
		 */
		static public bool PointIsRightOfLine( Vector2D a, Vector2D b, Vector2D point )
		{
				float dx = b.x - a.x;
				float dy = b.y - a.y;
				
				if( b.x == a.x ) {
					if( b.y > a.y ) {
						return point.x > a.x;
					}
					else {
						return point.x < a.x;
					}
				}

				float yt = a.y + (dy / dx) * (point.x - a.x);
				return (b.x < a.x) ? (point.y > yt) : (point.y < yt);
		}

		/**
		 * This function calculates, for a list of vertices representing a polygon, the angle turned
		 * at each vertex while walking the perimeter of the polygon.  This is used to determine if
		 * a particular vertex in a polygon is convex or concave.  Also, one can compute whether the
		 * polygon winds clockwise or counterclockwise.
		 */
		static private List<float> ComputeVertexAngles( List<Vector2D> poly )
		{
				float lastAngle;
				float newAngle;
				float diff;

				List<float> angles = new List<float>();

				float dx = poly[0].x - poly[ poly.Count - 1 ].x;
				float dy = poly[0].y - poly[ poly.Count - 1 ].y;

				lastAngle = (float)Math.Atan2( dy, dx );
				
				for( int i = 1 ; i <= poly.Count ; ++i )
				{
					if( i < poly.Count ) {	
						dx = poly[i].x - poly[i-1].x;
						dy = poly[i].y - poly[i-1].y;
					} else {	
						dx = poly[0].x - poly[i-1].x;
						dy = poly[0].y - poly[i-1].y;
					}		

					newAngle = (float)Math.Atan2( dy, dx );
					
					diff = (newAngle - lastAngle);

					while( diff < -Math.PI ) diff += 2 * (float)Math.PI;
					while( diff >  Math.PI ) diff -= 2 * (float)Math.PI;
					
					angles.Add( diff );

					lastAngle = newAngle;
				}

				return angles;
		}

		/**
		 * <summary>
		 * Determines whether a polygon represented as a list of points in 2D space winds clockwise
		 * around its center when traversed from the 0th index upwards.
		 * </summary>
		 * <param name="polygon">List of Vector2Ds which represent the polygon</param>
		 */
		static public bool PolygonWindsClockwise( List<Vector2D> polygon )
		{
				List<float> angles = ComputeVertexAngles( polygon );

				float angleSum = 0;
				for( int i = 0 ; i < angles.Count ; ++i ) {
					angleSum += angles[i];
				}

				return angleSum > 0;
		}

		/**
		 * <summary>
		 * This method returns true if the point specified as the fourth parameter lies inside of the
		 * triangle defined by the three vertices 'a', 'b', and 'c'.
		 * </summary>
		 */
		static public bool PointIsInTriangle( Vector2D a, Vector2D b, Vector2D c, Vector2D pt )
		{
				bool ta = PointIsRightOfLine( a, b, pt );
				bool tb = PointIsRightOfLine( b, c, pt );
				bool tc = PointIsRightOfLine( c, a, pt );
				return (ta && tb && tc) || (!ta && !tb && !tc) ;
		}

		// TODO: Check explicitly for complex polygons and fail.  Right now it just fails if it finds itself in a loop.
		/**
		 * <summary>
		 * Call this method to compute the constituent triangles of an arbitrary simple polygon.
		 * </summary>
		 * <param name="polygon">An ordered array of points representing the perimeter of the polygon to be
		 * triangulated.</param>
		 * <returns>If the polygon did not cross over itself (i.e. was a simple polygon), the function
		 * will return its triangulation as a List of Triangle2Ds.  If there was a problem, like the polygon
		 * was complex, null is returned.  However, this function is currently not guarenteed to fail on
		 * a complex polygon.  It is possible for it to just return some weird triangles if the polygon is
		 * complex.</returns>
		 */
		static public List<Triangle2D> TriangulatePolygon( List<Vector2D> polygon )
		{
				List<Triangle2D> triangles = new List<Triangle2D>();
				List<Vector2D> poly = new List<Vector2D>();	// Create a List from the point array. We're going to chip away at it.

				poly.InsertRange( 0, polygon );

				bool    curveRight;  // True if the inside of the polygon is to the left while walking through the point array provided.

				int	prevPt;		// These are indices in the poly List.
				int	curPt;
				int	nextPt;

				bool failed;		// This is set to true when the current clipping attempt fails.
				

				List<float> angles = ComputeVertexAngles( poly );

				// Determine the curvature of the polygon by the sum of the vertex angles
				float angleSum = 0;
				for( int i = 0 ; i < angles.Count ; ++i ) {
					angleSum += angles[i];
				}
				curveRight = angleSum > 0;

				curPt = 0;

				bool nothingWasClipped = true; // This flag is used to check if nothing has been clipped in a cycle through all the points.
											   // If this remains true for an entire cycle, the polygon supplied crosses itself and cannot be triangulated.
				// Let's roll..
				while( poly.Count > 3 )
				{
					if( ++curPt >= poly.Count ) {
						if( nothingWasClipped ) return null;
						nothingWasClipped = true;
						curPt = 0;
					}

					prevPt = (curPt > 0)            ? (curPt - 1) : (poly.Count - 1);
					nextPt = (curPt < poly.Count-1) ? (curPt + 1) : 0;

					if( ( curveRight && angles[curPt] < 0 )	  // If we're at a vertex with a convex angle,
					 || (!curveRight && angles[curPt] > 0 ) ) // continue since there's nothing we can do here.
					{
						continue;
					}

					// Check to see if one of the polygon's other points is inside the ear we're examining.
					failed = false;
					for( int i = 0 ; i < poly.Count ; ++i ) 
					{
						if( i != prevPt && i != curPt && i != nextPt )
						if( Geometry.PointIsInTriangle( poly[ prevPt ], poly[ curPt ], poly[ nextPt ], poly[ i ] ) )
						{
							failed = true;	// There's a point in here, we can't clip it.  Move on.
							break;
						}
					}
					if( failed ) continue;
					
					// This ear is OK to clip from the polygon.  Store it.
					triangles.Add( new Triangle2D( poly[ prevPt ], poly[ curPt ], poly[ nextPt ] ) );

					// Clip the ear from the polygon and remove the middle vertex.
					poly.RemoveAt( curPt );

					angles = ComputeVertexAngles( poly );

					nothingWasClipped = false;
				}

				
				triangles.Add( new Triangle2D( poly[0], poly[1], poly[2] ) );
				
				return triangles;
		}
	}
}