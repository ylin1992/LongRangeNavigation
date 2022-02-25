using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Edges
{
    class TestEdge<TVertex> : Edge<TVertex>, IPositiveWeighted<TVertex>
    {
        public TestEdge(TVertex f, TVertex t, double w) : base(f, t)
        {
            Console.WriteLine(f.ToString() + "," + t.ToString());
            Weight = w;
        }

        public bool UpdateWeight(double w) 
        {
            if (w <= 0) return false;
            Weight = w;
            return true;
        }
        public double Weight { get; set; }
        public override string ToString()
        {
            return From.ToString() + " -> " + To.ToString(); 
        }

    }
}
