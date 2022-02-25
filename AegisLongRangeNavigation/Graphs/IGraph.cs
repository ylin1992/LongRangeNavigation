using System;
using System.Collections.Generic;
using AegisLongRangeNavigation.Edges;
namespace AegisLongRangeNavigation.Graphs
{
	public interface IGraph<TVertex, TEdge> where TEdge : IEdge<TVertex> 
	{
		public Dictionary<TVertex, List<TEdge>> Table { get; }
		public void AddEdge(TVertex f, TVertex t, double weight, Func<TVertex, TVertex, double, TEdge> edgeFactory);
		public bool IsDirected { get; }
	}
}

