using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiStuntStudio
{
	/**
	 * The classes in this file represent various undoable actions performed while editing a Ski Stunt Level.
	 * The editor keeps a stack of these actions as they are performed.  When the user requests an 'Undo',
	 * the top Action is popped and reversed.
	 */

	public interface Action {}

	public class MoveThingAction : Action
	{
		public float x0;
		public float y0;
		public float x1;
		public float y1;
		public LevelThing relevantThing;

		public MoveThingAction( float x0, float y0, float x1, float y1, LevelThing relevantThing )
		{
				this.x0 = x0;
				this.y0 = y0;
				this.x1 = x1;
				this.y1 = y1;
				this.relevantThing = relevantThing;
		}
	}

	public class DeleteVertexAction : Action
	{
		public LevelVertex vertexDeleted;
		public List<LevelPolygon> collateralPolygons; // A list of polygons deleted due to insufficient vertex counts post-delete.  Polygons are stored as they were before the vertex removal.
		public List<LevelPolygon> polygonsAffected;
		public List<LevelVertex>  precedingVertices; // Vertices preceding deleted vertex in polygonsAffected.

		public DeleteVertexAction()
		{
				this.collateralPolygons = new List<LevelPolygon>();
				this.polygonsAffected   = new List<LevelPolygon>();
				this.precedingVertices  = new List<LevelVertex>();
		}
	}

	/**
	 * We only need to keep a reference to the polygon in order to bring it back.  All the vertices referenced by it
	 * will be resurrected automatically when the polygon is recreated.
	 */
	public class DeletePolygonAction : Action
	{
		public LevelPolygon relevantPolygon;

		public DeletePolygonAction( LevelPolygon relevantPolygon )
		{
				this.relevantPolygon = relevantPolygon;
		}
	}

	public class VertexCurveQualityAction : Action
	{
		public LevelVertex relevantVertex;
		public int previousCurveQuality;
		public int newCurveQuality;

		public VertexCurveQualityAction( LevelVertex relevantVertex, int previousCurveQuality, int newCurveQuality )
		{
				this.relevantVertex       = relevantVertex;
				this.previousCurveQuality = previousCurveQuality;
				this.newCurveQuality      = newCurveQuality;
		}
	}

	public class InsertObjectAction : Action
	{
		public LevelObject relevantObject;

		public InsertObjectAction( LevelObject relevantObject )
		{
				this.relevantObject = relevantObject;
		}
	}

	public class DeleteObjectAction : Action
	{
		public LevelObject relevantObject;

		public DeleteObjectAction( LevelObject relevantObject )
		{
				this.relevantObject = relevantObject;
		}
	}

	public class InsertVertexAction : Action
	{
		public LevelPolygon targetPolygon;
		public LevelVertex  precedingVertex;
		public LevelVertex  newVertex;

		public InsertVertexAction( LevelPolygon targetPolygon, LevelVertex precedingVertex, LevelVertex newVertex )
		{
				this.targetPolygon   = targetPolygon;
				this.precedingVertex = precedingVertex;
				this.newVertex       = newVertex;
		}
	}

	public class CreatePolygonAction : Action
	{
		public LevelPolygon newPolygon;

		public CreatePolygonAction( LevelPolygon newPolygon )
		{
				this.newPolygon = newPolygon;
		}
	}
}
