using System;
namespace AegisLongRangeNavigation.Edges
{
	public class UnWeightedEdge<TVertex> : IEdge<TVertex>
	{
		public TVertex From { get; }
		public TVertex To { get; }
		public UnWeightedEdge(TVertex f, TVertex t)
		{
			From = f;
			To = t;
		}
		public override string ToString()
		{
			if (From == null || To == null) return "null";
			return From.ToString() + "-->" + To.ToString();
		}
	}
}

