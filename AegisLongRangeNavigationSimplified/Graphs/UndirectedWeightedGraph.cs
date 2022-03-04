using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigationSimplified.Edges;
using AegisLongRangeNavigationSimplified.Vertices;

namespace AegisLongRangeNavigationSimplified.Graphs
{
    public class UndirectedWeightedGraph<TVertex, TEdge> : Graph<TVertex, WeightedEdge<TVertex>>, IAdjacentListGraph<TVertex, WeightedEdge<TVertex>> where TVertex : Vertex
    {
        public bool IsDirected { get; private set; }
        public Dictionary<TVertex, List<WeightedEdge<TVertex>>> AdjacentList { get; set; }
        public UndirectedWeightedGraph()
        {
            IsDirected = false;
            VerticesList = new Dictionary<int, TVertex>();
            AdjacentList = new Dictionary<TVertex, List<WeightedEdge<TVertex>>>();
        }

        public override bool AddEdge(WeightedEdge<TVertex> edge)
        {
            if (edge != null)
            {
                TVertex from = edge.From;
                if (!AdjacentList.ContainsKey(from))
                {
                    AdjacentList.Add(from, new List<WeightedEdge<TVertex>>());
                }
                AdjacentList[from].Add(edge);

                if (!VerticesList.ContainsKey(from.Index))
                {
                    VerticesList[from.Index] = from;
                }
                if (!VerticesList.ContainsKey(edge.To.Index))
                {
                    VerticesList[edge.To.Index] = edge.To;
                }

                return true;
            }
            else 
            {
                return false; 
            }
        }

        public override string ToString()
        {
            string res = "";
            foreach (KeyValuePair<TVertex, List<WeightedEdge<TVertex>>> set in AdjacentList)
            {
                //res += set.Key;
                foreach (WeightedEdge<TVertex> edge in set.Value)
                {
                    res += edge;
                    res += "\n";
                }
                //res += "\n";
            }
            return res;
        }

        // TODO: build another dictionary storing reverse-searching data
        public override TVertex GetVertexByIndex(int idx)
        {
            foreach (TVertex v in AdjacentList.Keys)
            {
                if (v.Index == idx) return v;
            }
            return null;
        }

        public override int GetVerticesCount()
        {
            if (AdjacentList == null) return 0;
            return AdjacentList.Count;
        }
    }
}
