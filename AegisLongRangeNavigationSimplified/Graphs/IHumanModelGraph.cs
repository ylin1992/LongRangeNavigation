using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisLongRangeNavigationSimplified.Graphs
{
    interface IHumanModelGraph
    {
        public void ApplyHumanModel(double alpha, double beta1, double beta2);
    }
}
