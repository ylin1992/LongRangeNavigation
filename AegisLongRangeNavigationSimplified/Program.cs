using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

            AStarSearch<UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>>, Vertex3D, WeightedEdge<Vertex3D>> search = new AStarSearch<UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>>, Vertex3D, WeightedEdge<Vertex3D>>(udwGraph);
            List<List<Vertex3D>> paths = new List<List<Vertex3D>>();

            // Waypoint testing
            Console.WriteLine("==========WayPoints Testing=========");
            int[] wayPoints = new int[] {0, 459, 100, 200, 500, 700, 1500, 1593, 900, 2000, 1200, 1467, 1587, 1763, 1862, 2007};
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


            Console.WriteLine("\nGenerating markers.json...");
            List<double[]> waypoints = new List<double[]>();
            foreach (List<Vertex3D> p in paths)
            {
                foreach (Vertex3D v in p)
                {
                    waypoints.Add(v.Coordinates);
                }
            }

            Dictionary<string, List<double[]>> data = new Dictionary<string, List<double[]>>();
            data["waypoints"] = waypoints;
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            //Console.WriteLine(json);
            File.WriteAllText("markers.json", json);
        }
    }
}
