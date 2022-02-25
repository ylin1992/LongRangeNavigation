using System;
namespace AegisLongRangeNavigation.Edges
{
	public interface IEdge<TVertex>
	{
		TVertex From { get; }
		TVertex To { get; }
		public string ToString();
	}
}

