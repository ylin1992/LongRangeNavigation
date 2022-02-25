using System;
using System.Collections.Generic;
using AegisLongRangeNavigation.Edges;
using AegisLongRangeNavigation.Graphs;
namespace AegisLongRangeNavigation.Graphs
{
	public class WeightedUndirectedGraph<TEdge, TVertex> : IGraph<TVertex, TEdge> where TEdge : IEdge<TVertex>
	{
		public Dictionary<TVertex, List<TEdge>> Table { get; }
		public bool IsDirected { get; }
		public WeightedUndirectedGraph()
		{
			IsDirected = false;
			Table = new Dictionary<TVertex, List<TEdge>>();
		}

		public WeightedUndirectedGraph(List<TEdge> edges)
		{
			// TODO: Implement adding edges by List of TEdge
		}

		public void AddEdge(TVertex f, TVertex t, double weight, Func<TVertex, TVertex, double, TEdge> edgeFactory)
		{
			if (!Table.ContainsKey(f))
            {
                Table.Add(f, new List<TEdge>());
            }
            Table[f].Add(edgeFactory(f, t, weight));

            //if (!Table.ContainsKey(t))
            //{
            //    Table.Add(t, new List<TEdge>());
            //}
            //Table[t].Add(edgeFactory(t, f, weight));
        }

		public override string ToString()
		{
			string res = "";
			foreach (KeyValuePair<TVertex, List<TEdge>> set in Table)
			{
				//res += set.Key;
				foreach (TEdge edge in set.Value)
				{
                    res += edge;
					res += "\n";
				}
				//res += "\n";
			}
			return res;
		}
	}
}

