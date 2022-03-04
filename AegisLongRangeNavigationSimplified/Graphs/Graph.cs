using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigationSimplified.Edges;
using AegisLongRangeNavigationSimplified.Vertices;

namespace AegisLongRangeNavigationSimplified.Graphs
{
    public abstract class Graph<TVertex, TEdge> where TEdge : Edge<TVertex>
    {
        
        public bool IsDirected { get; set; }
        public Dictionary<int, TVertex> VerticesList { get; set; }
        public List<TEdge> EdgesList { get; set; }
        public abstract bool AddEdge(TEdge edge);
        public virtual int GetEdgesCount() 
        {
            if (EdgesList == null) return 0;
            return EdgesList.Count;
        }
        public virtual int GetVerticesCount()
        {
            if (VerticesList == null) return 0;
            return VerticesList.Count();
        }

        public abstract TVertex GetVertexByIndex(int idx);
    }
}
