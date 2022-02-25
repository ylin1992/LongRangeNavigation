using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified
{
    public class UndirectedWeightedGraph
    {
		public Dictionary<Vertex3D, List<WeightedEdge>> Table { get; }
		public bool IsDirected { get; }
		public UndirectedWeightedGraph()
		{
			IsDirected = false;
			Table = new Dictionary<Vertex3D, List<WeightedEdge>>();
		}

		public UndirectedWeightedGraph(List<WeightedEdge> edges)
		{
			// TODO: Implement adding edges by List of TEdge
		}

		public void AddEdge(Vertex3D f, Vertex3D t, double weight)
		{
			if (!Table.ContainsKey(f))
			{
				Table.Add(f, new List<WeightedEdge>());
			}
			Table[f].Add(new WeightedEdge(f, t, weight));
		}

		public override string ToString()
		{
			string res = "";
			foreach (KeyValuePair<Vertex3D, List<WeightedEdge>> set in Table)
			{
				//res += set.Key;
				foreach (WeightedEdge edge in set.Value)
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
