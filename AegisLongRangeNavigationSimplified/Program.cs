using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

using AegisLongRangeNavigationSimplified.Parsers;
using AegisLongRangeNavigationSimplified.Graphs;
using AegisLongRangeNavigationSimplified.Edges;
using AegisLongRangeNavigationSimplified.Vertices;

using AegisLongRangeNavigationKDTreeLib.AegisKDTree;
using AegisLongRangeNavigationKDTreeLib.AegisKDTree.TreeType;


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
            int[] wayPoints = new int[] {0, 1500};
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

            // Test adjusting weight
            udwGraph.ApplyHumanModel(6, -3.5, 0.05);
            parser.WriteGraphToJsons("./dtmE_Adj.json", "./dtmV_Adj.json", udwGraph);


            // Testing Vertex3D with KDTreeLib
            Vertex3D[] vertex3Ds = new Vertex3D[] {
                new Vertex3D(0, new double[] { 1,2, 3}),
                new Vertex3D(1, new double[] { 2,3,8}),
                new Vertex3D(2, new double[] { 1,8,111}),
                new Vertex3D(3, new double[] { 7,5,10}),
                new Vertex3D(4, new double[] { 11,4,-2}),
            };

            KDTree<double, Vertex3D> tree = new KDTree<double, Vertex3D>(2, new DoubleOperable());
            foreach (Vertex3D v in vertex3Ds)
            {
                double[] coord = new double[] { v.Coordinates[0], v.Coordinates[1] };
                KDNode<double, Vertex3D> n = new KDNode<double, Vertex3D>(
                    new Tuple<List<double>, Vertex3D>(new List<double>(coord), v));
                tree.Insert(n);
            }
            Console.WriteLine(tree.Count);
            KDNode<double, Vertex3D> n1 = tree.GetNearestNeighbor(new double[] { 1, 2});
            KDNode<double, Vertex3D> n2 = tree.GetNearestNeighbor(new double[] { 2, 2 });
            KDNode<double, Vertex3D> n3 = tree.GetNearestNeighbor(new double[] { 1, 7 });
            Console.WriteLine(n1);
            Console.WriteLine(n2);
            Console.WriteLine(n3);
        }
    }
}
