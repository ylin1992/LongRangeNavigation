using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Vertices
{
    public interface IHeuristicBasedVertex
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public double GetHeuristicPara(double[] target);
    }
}
