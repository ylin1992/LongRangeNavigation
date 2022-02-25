using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified
{
    public class WeightedEdge
    {
        public Vertex3D From { get; }
        public Vertex3D To { get; }
        public double Weight { get; set; }
        public WeightedEdge(Vertex3D f, Vertex3D t, double w)
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

    }
}
