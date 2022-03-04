using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigationSimplified.Vertices;
using AegisLongRangeNavigationSimplified.Edges;

namespace AegisLongRangeNavigationSimplified.Graphs
{
    interface IWeightedGraph<TVertex, TEdge> where TEdge : Edge<TVertex>
    {
        public void AddEdge(TVertex f, TVertex t, double w);
    }
}
