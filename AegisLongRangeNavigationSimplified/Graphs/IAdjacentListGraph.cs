using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigationSimplified.Edges;

namespace AegisLongRangeNavigationSimplified.Graphs
{
    interface IAdjacentListGraph<TVertex, TEdge> where TEdge : Edge<TVertex>
    {
        public Dictionary<TVertex, List<TEdge>> AdjacentList { get; set; }

    }
}
