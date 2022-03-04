using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Vertices
{
    public class Vertex2D : CoordinateBasedHeuristicVertex, ICoordinateBasedVertex
    {
        public double[] Coordinates { get; private set; }
        public List<Vertex2D> Neighbors { get; set; }
        private Vertex2D(int index, double[] coord) : base(index, coord)
        {
            Index = index;
            Coordinates = coord;
            Neighbors = new List<Vertex2D>();
        }

        // Vertex Factory
        public static Vertex2D CreateVertex(int index, double[] coord)
        {
            if (coord == null || coord.Length != 2)
            {
                return null;
            }

            return new Vertex2D(index, coord);
        }

        public override bool UpdateCoordinate(double[] coord)
        {
            if (coord != null && coord.Length == 2)
            {
                Coordinates = coord;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "{ Index: " + Index + " ("
                + Coordinates[0].ToString()
                + ", " + Coordinates[1].ToString()
                + ")}";
        }
    }
}
