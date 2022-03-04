using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Vertices
{
    public class Vertex3D : CoordinateBasedHeuristicVertex
    {
		public int Index { get; }
		public double[] Coordinates { get; private set; }
		public double Distance { get; set; }
		
		public Vertex3D(int index, double[] coord) : base(index, coord)
		{
			Index = index;
			Coordinates = coord;
			Distance = float.MaxValue;
		}
		public static Vertex3D CreateVertex(int index, double[] coord)
		{
			if (coord == null || coord.Length != 3)
			{
				return null;
			}

			return new Vertex3D(index, coord);
		}

		public override bool UpdateCoordinate(double[] coord)
		{
			if (coord != null && coord.Length == 3)
			{
				Coordinates = coord;
				return true;
			}
			return false;
		}
		public override double GetHeuristicPara(double[] target)
		{
			return Math.Pow(Math.Pow(target[0] - Coordinates[0], 2)
				+ Math.Pow(target[1] - Coordinates[1], 2), 0.5); // ignore z axis
		}

		public override string ToString()
		{
			return "{ Index: " + Index + " ("
				+ Coordinates[0].ToString()
				+ ", " + Coordinates[1].ToString()
				+ ", " + Coordinates[2].ToString()
				+ ")}";
		}
	}
}
