using System;
namespace AegisLongRangeNavigation.Edges
{
    public class WeightedEdge<TVertex> : IEdge<TVertex>, IHeuristic, IWeighted
    {
        public TVertex From { get; }
        public TVertex To { get; }
        public double Weight { get; set; }
        public WeightedEdge(TVertex f, TVertex t, double w)
        {
            From = f;
            To = t;
            Weight = w;
        }

        public override string ToString()
        {
            if (From == null || To == null) return "null";
            return From.ToString() + "-- " + Weight + " -->" + To.ToString();
        }

    }
}
