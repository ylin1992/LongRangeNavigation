using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Vertices
{
    public class CoordinateBasedVertex : Vertex, ICoordinateBasedVertex
    {
        public double[] Coordinates { get; private set; }
        public CoordinateBasedVertex(int index, double[] coord) : base(index)
        {
            Coordinates = coord;
        }

        public virtual bool UpdateCoordinate(double[] coord)
        {
            if (coord == null) return false;
            Coordinates = coord;
            return true;
        }
    }
}
