using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegisLongRangeNavigationSimplified.Edges;
using AegisLongRangeNavigationSimplified.Vertices;
using AegisLongRangeNavigationKDTreeLib.AegisKDTree;
using AegisLongRangeNavigationKDTreeLib.AegisKDTree.TreeType;

namespace AegisLongRangeNavigationSimplified.Graphs
{
    public class UndirectedWeightedGraph<TVertex, TEdge> : Graph<TVertex, WeightedEdge<TVertex>>, 
        IHumanModelGraph, IAdjacentListGraph<TVertex, WeightedEdge<TVertex>> where TVertex : Vertex
    {
        public static double ANGLE_THRESHOLD = 40.0;
        public bool IsDirected { get; private set; }
        public Dictionary<TVertex, List<WeightedEdge<TVertex>>> AdjacentList { get; set; }
        public UndirectedWeightedGraph()
        {
            IsDirected = false;
            VerticesList = new Dictionary<int, TVertex>();
            AdjacentList = new Dictionary<TVertex, List<WeightedEdge<TVertex>>>();
        }

        public override bool AddEdge(WeightedEdge<TVertex> edge)
        {
            if (edge != null)
            {
                TVertex from = edge.From;
                if (!AdjacentList.ContainsKey(from))
                {
                    AdjacentList.Add(from, new List<WeightedEdge<TVertex>>());
                }
                AdjacentList[from].Add(edge);

                if (!VerticesList.ContainsKey(from.Index))
                {
                    VerticesList[from.Index] = from;
                }
                if (!VerticesList.ContainsKey(edge.To.Index))
                {
                    VerticesList[edge.To.Index] = edge.To;
                }

                return true;
            }
            else 
            {
                return false; 
            }
        }

        public override string ToString()
        {
            string res = "";
            foreach (KeyValuePair<TVertex, List<WeightedEdge<TVertex>>> set in AdjacentList)
            {
                //res += set.Key;
                foreach (WeightedEdge<TVertex> edge in set.Value)
                {
                    res += edge;
                    res += "\n";
                }
                //res += "\n";
            }
            return res;
        }

        // TODO: build another dictionary storing reverse-searching data
        public override TVertex GetVertexByIndex(int idx)
        {
            foreach (TVertex v in AdjacentList.Keys)
            {
                if (v.Index == idx) return v;
            }
            return null;
        }

        public override int GetVerticesCount()
        {
            if (AdjacentList == null) return 0;
            return AdjacentList.Count;
        }

        // This method can only be used for 3D model.
        // Applying human model for 2D graph is meaningless
        public void ApplyHumanModel(double alpha, double beta1, double beta2)
        {
            foreach (KeyValuePair<TVertex, List<WeightedEdge<TVertex>>> pair in AdjacentList)
            {
                foreach (WeightedEdge<TVertex> edge in pair.Value)
                {
                    TVertex from = edge.From;
                    TVertex to = edge.To;
                    // TODO: Unsafe code, used for preliminary development
                    // TODO: Do type check before calculation.
                    //Console.WriteLine("Before adjustment: " + edge.ToString());


                    double dis = Math.Pow(Math.Pow((from.Coordinates[0] - to.Coordinates[0]), 2)
                        + Math.Pow((from.Coordinates[1] - to.Coordinates[1]), 2)
                        + Math.Pow((from.Coordinates[2] - to.Coordinates[2]), 2), 0.5);

                    double lengthInXY = Math.Pow(Math.Pow((from.Coordinates[0] - to.Coordinates[0]), 2)
                        + Math.Pow((from.Coordinates[1] - to.Coordinates[1]), 2), 0.5);
                    double deltaZ = (from.Coordinates[2] - to.Coordinates[2]);

                    double angle = Math.Atan2(deltaZ, lengthInXY) * 180.0 / Math.PI;
                    // if angle > threshold, set it to infinity (set the edge as unreachable)
                    if (Math.Abs(angle) > ANGLE_THRESHOLD)
                    { 
                        edge.UpdateWeight(double.PositiveInfinity);
                        continue;
                    }

                    // TODO: Prevent Overflow
                    double velocity = alpha * Math.Exp(beta1 * Math.Abs(Math.Tan(angle) / 180.0 * Math.PI + beta2));
                    double coeff = dis / velocity;
                    edge.UpdateWeight(coeff * edge.Weight);
                    //Console.WriteLine("After adjustment: " + edge.ToString());
                }
            }
        }

        public override TVertex GetNearestNeighbor(double[] target)
        {
            throw new NotImplementedException();
        }
    }
}
