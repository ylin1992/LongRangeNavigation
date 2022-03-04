using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigationSimplified.Parsers;
using AegisLongRangeNavigationSimplified.Graphs;
using AegisLongRangeNavigationSimplified.Edges;
using AegisLongRangeNavigationSimplified.Vertices;

namespace AegisLongRangeNavigationSimplified
{
    class Program
    {
        static void Main(string[] args)
        {
            
            JSON3DParser parser = new JSON3DParser("./dtmV.json", "./dtmE.json");
            UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>> udwGraph = parser.CreateWieghtedUndirectedGraph();
            //Console.WriteLine(udwGraph);

            AStarSearch<UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>>, Vertex3D, WeightedEdge<Vertex3D>> search = new AStarSearch<UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>>, Vertex3D, WeightedEdge<Vertex3D>>(udwGraph);
            List<List<Vertex3D>> paths = new List<List<Vertex3D>>();

            // traverse all graph
            //for (int i = 1; i < udwGraph.GetVerticesCount(); i++)
            //{
            //    Vertex3D s = udwGraph.GetVertexByIndex(0);
            //    Vertex3D t = udwGraph.GetVertexByIndex(i);
            //    Console.WriteLine("Testing from " + s.Index + " to " + t.Index);
            //    if (s != null && t != null)
            //    {
            //        List<Vertex3D> res = search.GetPath(search.Solve(s, t), s, t);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Unable to get nodes");
            //        Console.WriteLine("s:" + s);
            //        Console.WriteLine("t:" + t);
            //    }
            //}

            // Waypoint testing
            Console.WriteLine("==========WayPoints Testing=========");
            int[] wayPoints = new int[] {0, 459,  100, 200, 500, 700, 1500, 1593, 900, 2000, 1200, 1467, 1587, 1763, 1862, 2007};
            for (int i = 0; i < wayPoints.Length - 1; i++)
            {
                Vertex3D s = udwGraph.GetVertexByIndex(wayPoints[i]);
                Vertex3D t = udwGraph.GetVertexByIndex(wayPoints[i + 1]);
                Console.WriteLine("Testing from " + s.Index + " to " + t.Index);
                if (s != null && t != null)
                {
                    paths.Add(search.GetPath(search.Solve(s, t), s, t));
                }
                else
                {
                    Console.WriteLine("Unable to get nodes");
                    Console.WriteLine("s:" + s);
                    Console.WriteLine("t:" + t);
                }
            }

            Console.WriteLine("Generating visualized result...");
            GraphPainter painter = new GraphPainter(udwGraph);
            painter.DrawCanvas();
            foreach (List<Vertex3D> p in paths)
            {
                painter.DrawPath(p);
            }
            painter.SaveFile("res.png");
            
        }
    }
}
