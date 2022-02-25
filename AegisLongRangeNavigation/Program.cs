using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigation.Edges;
using AegisLongRangeNavigation.Vertices;
using AegisLongRangeNavigation.Graphs;
using AegisLongRangeNavigation.Parsers;


namespace AegisLongRangeNavigation
{
    class Program
    {
        static void Main(string[] args)
        {
            JSONParser<Vertex, WeightedEdge<Vertex>> jsonParser = new JSONParser<Vertex, WeightedEdge<Vertex>>("./vertices5.json", "./edges5.json");
            WeightedUndirectedGraph<WeightedEdge<Vertex>, Vertex> g = jsonParser.CreateWieghtedUndirectedGraph();
            Console.WriteLine(g);
          
        }
    }
}
