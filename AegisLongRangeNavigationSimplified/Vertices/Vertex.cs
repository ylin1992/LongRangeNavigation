using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Vertices
{
    public class Vertex
    {
        public int Index { get; set; }
        public double Distance { get; set; }
        public Vertex(int index)
        {
            Distance = 0.0;
            Index = index;
        }

    }
}
