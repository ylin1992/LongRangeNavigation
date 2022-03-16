using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Vertices
{
    public class CoordinateBasedHeuristicVertex : Vertex, ICoordinateBasedVertex
    {
        public override double[] Coordinates { get;  set; }
        public CoordinateBasedHeuristicVertex(int index, double[] coord) : base(index)
        {
            Coordinates = coord;
        }

        public virtual bool UpdateCoordinate(double[] coord)
        {
            if (coord == null) return false;
            Coordinates = coord;
            return true;
        }

        public override double GetHeuristicPara(double[] target)
        {
            return 0.0;
        }
    }
}
