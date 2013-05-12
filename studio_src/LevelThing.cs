using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiStuntStudio
{
	public abstract class LevelThing
	{
		public event EventHandler PositionChange;

		private float _x;
		private float _y;

		public float x {
			get { return _x; }
			set { _x = value;
				if( PositionChange != null ) PositionChange( this, null );
			}
		}

		public float y {
			get { return _y; }
			set { _y = value;
				if( PositionChange != null ) PositionChange( this, null );
			}
		}

		virtual public void SetPosition( float x, float y )
		{
				_x = x;
				_y = y;
				if( PositionChange != null ) PositionChange( this, null );
		}
	}
}
