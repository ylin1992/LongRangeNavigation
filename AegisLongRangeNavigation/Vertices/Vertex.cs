using System;
using AegisLongRangeNavigation.Edges;

namespace AegisLongRangeNavigation.Vertices
{
	public class Vertex
	{
		public int Index { get; }
		public double[] Coordinates { get; }
		public double Distance { get; set; }
		public Vertex(int index, double[] coord)
		{
			Distance = 0.0;
			Coordinates = coord;
			Index = index;
		}

		public double GetHeuristicPara(double[] target) {
			return Math.Pow(Math.Pow(target[0] - Coordinates[0], 2)
				+ Math.Pow(target[1] - Coordinates[1], 2)
				+ Math.Pow(target[2] - Coordinates[2], 2), 0.5);
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

