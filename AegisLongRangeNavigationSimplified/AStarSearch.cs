using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigationSimplified.Parsers;
using AegisLongRangeNavigationSimplified.Graphs;
using AegisLongRangeNavigationSimplified.Edges;
using AegisLongRangeNavigationSimplified.Vertices;
using Priority_Queue;

namespace AegisLongRangeNavigationSimplified
{
    // TODO: Revise generic types
    class AStarSearch<TGraph, TVertex, TEdge> 
        where TGraph: UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>>
        where TVertex: Vertex3D
        where TEdge: WeightedEdge<Vertex3D>
    {
        public TGraph G { get; private set; }
        public AStarSearch(TGraph g)
        {
            G = g;
        }
        private void debug(Dictionary<TVertex, TVertex> comeFrom)
        {
            Console.WriteLine("==== Vertices Table =====");
            foreach (KeyValuePair<TVertex, TVertex> keyValue in comeFrom)
            {
                Console.WriteLine(keyValue.Key.Index + " <- " + keyValue.Value.Index);
            }
            Console.WriteLine("========================");

        }

        // TODO: Refractor to Generic types
        // TODO: Modify Vertex to HeuristicVertex
        public Dictionary<TVertex, TVertex> Solve(TVertex s, TVertex t)
        {
            resetDistance();
            SimplePriorityQueue<TVertex> F = new SimplePriorityQueue<TVertex>();
            s.Distance = 0.0;
            F.Enqueue(s, (float)s.Distance + (float)s.GetHeuristicPara(t.Coordinates));
            Dictionary<TVertex, TVertex> comeFrom = new Dictionary<TVertex, TVertex>();

            
            while (F.Count != 0)
            {
                
                
                TVertex c = F.Dequeue();
                //Console.WriteLine(F.Count + ", Index: " + c.Index);
                if (c.Index == t.Index)
                {
                    //comeFrom[c] = t;
                    //debug(comeFrom);
                    return comeFrom;
                }

                foreach (TEdge edge in G.AdjacentList[c])
                {
                    TVertex neighbor = (TVertex)edge.To;
                    double g = c.Distance + edge.Weight;
                    //Console.WriteLine(g + " " + neighbor.Distance);
                    if (g < neighbor.Distance)
                    {
                        comeFrom[neighbor] = c;
                        neighbor.Distance = g;
                        F.EnqueueWithoutDuplicates(neighbor, (float)neighbor.Distance + (float)neighbor.GetHeuristicPara(t.Coordinates));
                    }
                }
                //debug(comeFrom);
            }
            
            return null;
        }

        private void resetDistance()
        {
            foreach (TVertex v in G.AdjacentList.Keys)
            {
                v.Distance = float.MaxValue;
            }
        }

        public List<TVertex> GetPath(Dictionary<TVertex, TVertex> comeFrom, TVertex s, TVertex t)
        {
            if (comeFrom == null) return null;
            List<TVertex> path = new List<TVertex>();
            while (t.Index != s.Index)
            {
                Console.Write(t.Index + "<-");
                path.Add(t);
                t = comeFrom[t];
            }
            Console.WriteLine(s.Index);
            path.Add(s);
            path.Reverse();
            return path;
        }
    }
}
