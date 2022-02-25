using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Vertices
{
    public interface ICoordinateBasedVertex
    {
        public double[] Coordinates { get; }
        public bool UpdateCoordinate(double[] coord);
    }
}
