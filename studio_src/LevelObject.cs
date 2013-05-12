using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiStuntStudio
{
	public class LevelObject : LevelThing
	{
		public enum ObjectType : uint
		{
			Crate,
			PlayerStart,
			BigRock,
			Tree,
			BigBall,
			SnowmanHead,
			GiantSpBoard,
			SeeSaw,
			JetpackCrate,
			NopackCrate,
			EndzoneHandle
		};

		public enum AnchorType : uint
		{
			Locked,
			Asleep,
			Free
		};

		public ObjectType type;
		public float rotation;

		private AnchorType _anchor;
		public AnchorType anchor {
			get { return _anchor; }
			set { _anchor = IsCollectable ? AnchorType.Asleep : value; }
		}

		public Boolean IsCollectable { get { return type == ObjectType.JetpackCrate || type == ObjectType.NopackCrate; } }

		public LevelObject() : this( ObjectType.Crate ) {}

		public LevelObject( ObjectType type ) : this( type, 0, 0 ) {}

		public LevelObject( ObjectType type, float x, float y )
		{
				this.x = x;
				this.y = y;

				this.type   = type;
				this.anchor = AnchorType.Asleep;
		}

		public string ScriptName {
		get
		{
				switch( type )
				{
					case ObjectType.Crate:        return "newCrate";
					case ObjectType.BigRock:      return "newBigRock";
					case ObjectType.Tree:         return "newTree";
					case ObjectType.BigBall:      return "newBigBall";
					case ObjectType.SnowmanHead:  return "newSnowmanHead";
					case ObjectType.GiantSpBoard: return "newGiantSpBoard";
					case ObjectType.SeeSaw:       return "newSeeSaw";
					case ObjectType.JetpackCrate: return "newJetpackCrate";
					case ObjectType.NopackCrate:  return "newNopackCrate";
				}
				return "";
		}}
	}
}
