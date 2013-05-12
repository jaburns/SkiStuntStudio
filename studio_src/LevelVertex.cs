using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using com.jeremyaburns.math;

namespace SkiStuntStudio
{
	public class LevelVertex : LevelThing
	{
		private int _curveQuality;
		public int curveQuality {
			get { return _curveQuality; }
			set { _curveQuality = ( value > 0 ? value : 0 ); }
		}

		public LevelVertex( float x, float y )
		{
				this.x = x;
				this.y = y;

				_curveQuality = 0;
		}

		public LevelVertex( float x, float y, int curveQuality )
		{
				this.x = x;
				this.y = y;
				this.curveQuality = curveQuality;
		}

		public Vector2D GetVector2D()
		{
				return new Vector2D( x, y );
		}
	}
}
