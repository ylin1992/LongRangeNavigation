using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigationSimplified.Parsers;

namespace AegisLongRangeNavigationSimplified
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONParser parser = new JSONParser("./vertices5.json", "./edges5.json");
            UndirectedWeightedGraph udwGraph = parser.CreateWieghtedUndirectedGraph();
            Console.WriteLine(udwGraph);
        }
    }
}
