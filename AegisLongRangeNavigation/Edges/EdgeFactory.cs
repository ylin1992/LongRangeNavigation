using System;
namespace AegisLongRangeNavigation.Edges
{
	public class EdgeFactory<TVertex, TEdge> where TEdge : IEdge<TVertex>
	{
		public Func<TVertex, TVertex, double,
			WeightedEdge<TVertex>> WeightedEdgeCreator { get; }

		public EdgeFactory()
		{
			WeightedEdgeCreator = CreateWeightedEdge<TVertex, TEdge>;
		}

		private WeightedEdge<TVertex> CreateWeightedEdge<TVertex, TEdge>(
			TVertex v1, TVertex v2, double w)
		{
			return new WeightedEdge<TVertex>(v1, v2, w);
		}
	}
}

