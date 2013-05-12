using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.jeremyaburns.math
{
	static public class JMath
	{
		/**
		 * <summary>Takes a floating point value to an integer power by successive multiplication.
		 * This is generally faster than using Math.pow() when using an integer power<summary>
		 * <param name="num">Base</param>
		 * <param name="pow">Exponent</param>
		 */
		static public float IntPower( float num, int pow )
		{
				if( pow == 0 ) return 1;

				float x;

				if( pow <= -1 ) for( x = 1 ; pow <= -1 ; ++pow ) x /= num;
				else            for( x = 1 ; pow >=  1 ; --pow ) x *= num;

				return x;
		}

		/**
		 * <summary>Returns true if 'number' is a power of 'baze'.</summary>
		 */
		static public bool IsPowerOf( int number, int baze )
		{
				const double EPSILON = 1e-100;

				if( number <  baze ) return false;
				if( number == baze ) return true;

				double log = Math.Log( number, baze );
				int ilog = (int)log;

				return ( Math.Abs( log - (double)ilog ) < EPSILON );
		}

		/**
		 * <summary>Computes a row of Pascal's triangle and returns it as an array of integers.</summary>
		 * <param name="row">The row of the triangle to compute.</param>
		 */
		static public int[] PascalsTriangleRow( int row )
		{
				if( row < 1 ) return null;

				int[] ret = new int[ row ];

				ret[0] = 1;
				for( int i = 1 ; i < row ; ++i )  {
					ret[i] = ret[i-1] * ( row - i ) / i; 
				}
				return ret;
		}

		/**
		 * <summary>Given a list of Vector2Ds representing the handles of an arbitrary order Bezier curve,
		 * this function discretizes it in to a series of line segments and returns it as a list
		 * of Vector2Ds representing the points in the line.</summary>
		 * <param name="points">The handles of the Bezier curve.</param>
		 * <param name="segments">Number of segments to divide the curve in to.</param>
		 */
		static public List<Vector2D> BreakBezierCurveIntoSegments( List<Vector2D> points, int segments )
		{
				if( points.Count < 2 ) return null;

				List<Vector2D> ret = new List<Vector2D>();
					
				ret.Add( points[0].Clone() );
				
				if( points.Count == 2 ) {
					ret.Add( points[1].Clone() );
					return ret;
				}
				
				int   degree = points.Count - 1;
				int[] coeffs = JMath.PascalsTriangleRow( degree+1 );
				
				float inc = 1 / (float)segments;
				float lastSegError = 1 - inc/2;
				
				float nextX, nextY, coeff;

				for( float t = inc ; t < lastSegError ; t += inc )
				{
					nextX = nextY = 0;

					for( int i = 0 ; i <= degree ; ++i )
					{
						coeff = coeffs[i] * JMath.IntPower( t, i ) * JMath.IntPower( 1-t, degree-i );
						nextX += coeff * points[i].x;
						nextY += coeff * points[i].y;
					}

					ret.Add( new Vector2D( nextX, nextY ) );
				}

				ret.Add( new Vector2D( points[degree].x, points[degree].y ) );
				
				return ret;
		}
	}
}
