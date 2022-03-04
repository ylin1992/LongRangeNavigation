using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Vertices
{
    public abstract class Vertex : IHeuristicBasedVertex
    {
        public int Index { get; set; }
        public double Distance { get; set; }
        public Vertex(int index)
        {
            Distance = float.MaxValue;
            Index = index;
        }

        public virtual bool DecreaseKey(int newDistance) 
        {
            if (newDistance > Distance) return false;
            Distance = newDistance;
            return true;
        }

        public abstract double GetHeuristicPara(double[] target);

    }
}
