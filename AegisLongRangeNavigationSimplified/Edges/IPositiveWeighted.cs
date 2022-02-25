using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Edges
{
    interface IPositiveWeighted<TVertex>
    {
        public double Weight { get; set; }
        public bool UpdateWeight(double w);
    }
}
