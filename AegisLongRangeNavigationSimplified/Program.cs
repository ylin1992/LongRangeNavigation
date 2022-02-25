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
            /*
            JSONParser parser = new JSONParser("./vertices5.json", "./edges5.json");
            UndirectedWeightedGraph udwGraph = parser.CreateWieghtedUndirectedGraph();
            Console.WriteLine(udwGraph);
            */

            /*
            Vertex3D v1 = new Vertex3D(0, new double[]{ 1,2,3});
            Vertex3D v2 = new Vertex3D(1, new double[] { 4,5,6 });
            TestEdge<Vertex3D> testEdge = new TestEdge<Vertex3D>(v1, v2, 3.0);
            Console.WriteLine(testEdge);
            */

            Vertex3D v3d1 = Vertex3D.CreateVertex(0, new double[] { 1, 2, 3});
            Vertex3D v3d2 = Vertex3D.CreateVertex(1, new double[] { 21, 22, 23 });
            Console.WriteLine(v3d1.UpdateCoordinate(new double[] { 4, 5, 6}));
            Console.WriteLine(v3d1 == null);
            Console.WriteLine(v3d1);
            TestEdge<Vertex3D> testEdge3D = new TestEdge<Vertex3D>(v3d1, v3d2, 0.5);
            Console.WriteLine(testEdge3D);

            Vertex2D v2d1 = Vertex2D.CreateVertex(0, new double[] { 1, 2 });
            Vertex2D v2d2 = Vertex2D.CreateVertex(0, new double[] { 3, 4 });
            TestEdge<Vertex2D> testEdge2D = new TestEdge<Vertex2D>(v2d1, v2d2, 0.5);
            Console.WriteLine(testEdge2D);

        }
    }
}
