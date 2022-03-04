using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigationSimplified.Vertices;

namespace AegisLongRangeNavigationSimplified.Edges
{
    public class WeightedEdge<TVertex> : Edge<TVertex>, IPositiveWeighted<TVertex>
    {
        public TVertex From { get; }
        public TVertex To { get; }
        public double Weight { get; set; }
        public WeightedEdge(TVertex f, TVertex t, double w) : base(f, t)
        {
            From = f;
            To = t;
            Weight = w;
        }

        public override string ToString()
        {
            if (From == null || To == null) return "null";
            return From.ToString() + "-- " + Weight + " -->" + To.ToString();
        }

        public bool UpdateWeight(double w)
        {
            if (w <= 0) return false;
            Weight = w;
            return true;
        }
    }
}
