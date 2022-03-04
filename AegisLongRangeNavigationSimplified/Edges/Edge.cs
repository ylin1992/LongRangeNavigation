using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Edges
{
    public class Edge<TVertex>
    {
        public TVertex From { get; }
        public TVertex To { get; }
        public Edge(TVertex f, TVertex t)
        {
            this.From = f;
            this.To = t;
        }
    }
}
