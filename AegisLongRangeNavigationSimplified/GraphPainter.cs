using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using AegisLongRangeNavigationSimplified.Vertices;
using AegisLongRangeNavigationSimplified.Edges;
using AegisLongRangeNavigationSimplified.Graphs;

// The class is only used for testing and visualization purpose,
// which is not included in the final production package
namespace AegisLongRangeNavigationSimplified
{
    class GraphPainter
    {
        private const int WIDTH = 1024;
        private const int HEIGHT = 1024;
        private const int BORDER_BUFFER = 20;
        private const int CIRCLE_DIAMETER = 2;
        private const int WAYPOINT_DIAMETER = 3;
        private const int ENDS_DIAMETER = 5;
        private Color ENDS_COLOR = Color.DarkGreen;
        private Color WAYPOINT_COLOR = Color.Blue;
        private Color ARROW_COLOR = Color.Red;
        private const float WAYPOINT_THICKNESS = 5.0f;
        private const float ARROW_THICKNESS = 3.5f;
        private const float ENDS_THICKNESS = 7.0f;
        private Color ENDS_STRING_COLOR = Color.Black;
        private Font ENDS_FONT;
        private SolidBrush ENDS_BRUSH;
        private int TEXT_OFFSET = 10;

        UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>> g;
        Bitmap bitmap;
        private double minX;
        private double maxX;
        private double minY;
        private double maxY;
        private double scaleX;
        private double scaleY;
        public GraphPainter(UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>> g)
        {
            this.g = g;
            minX = 0;
            minY = 0;
            maxX = 0;
            maxY = 0;
            bitmap = new Bitmap(WIDTH, HEIGHT);

            ENDS_FONT = new Font("Arial", 16);
            ENDS_BRUSH = new SolidBrush(ENDS_STRING_COLOR);

            _extractMinMax();
        }

        private void _extractMinMax()
        {
            foreach (KeyValuePair<int, Vertex3D> pair in g.VerticesList)
            {
                double[] coor = pair.Value.Coordinates;
                minX = Math.Min(minX, coor[0]);
                maxX = Math.Max(maxX, coor[0]);
                minY = Math.Min(minY, coor[1]);
                maxY = Math.Max(maxY, coor[1]);
            }

            scaleX = (double)((WIDTH - BORDER_BUFFER)) / (maxX - minX);
            scaleY = (double)((HEIGHT - BORDER_BUFFER)) / (maxY - minY);
            Console.WriteLine("minX: " + minX.ToString());
            Console.WriteLine("maxX: " + maxX.ToString());
            Console.WriteLine("minY: " + minY.ToString());
            Console.WriteLine("maxY: " + maxY.ToString());
            Console.WriteLine("scaleX: " + scaleX.ToString());
            Console.WriteLine("scaleY: " + scaleY.ToString());

        }

        public void DrawCanvas()
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            Pen blkPen = new Pen(Color.Black);
            foreach (KeyValuePair<int, Vertex3D> pair in g.VerticesList)
            {
                double[] coor = pair.Value.Coordinates;
                int x = (int)((coor[0] - minX) * scaleX);
                int y = (int)((coor[1] - minY) * scaleY);
                //Console.WriteLine(x.ToString() + ", " + y.ToString());
                graphics.DrawEllipse(blkPen, x, y, CIRCLE_DIAMETER, CIRCLE_DIAMETER);
            }

            
        }

        public void DrawPath(List<Vertex3D> path)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            _putWayPoint(path, graphics);
        }

        private void _putWayPoint(List<Vertex3D> path, Graphics graphics)
        {
            Pen arrowPen = new Pen(ARROW_COLOR, ARROW_THICKNESS);
            Pen waypointPen = new Pen(WAYPOINT_COLOR, WAYPOINT_THICKNESS);
            arrowPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            for (int i = 0; i < path.Count - 1; i++)
            {
                double[] coor = path[i].Coordinates;
                double[] coorNext = path[i + 1].Coordinates;
                int x1 = (int)((coor[0] - minX) * scaleX);
                int y1 = (int)((coor[1] - minY) * scaleY);
                int x2 = (int)((coorNext[0] - minX) * scaleX);
                int y2 = (int)((coorNext[1] - minY) * scaleY);

                if (i == 0)
                {
                    Pen endsPend = new Pen(ENDS_COLOR, ENDS_THICKNESS);
                    graphics.DrawEllipse(endsPend, x1, y1, ENDS_DIAMETER, ENDS_DIAMETER);
                    graphics.DrawEllipse(waypointPen, x2, y2, WAYPOINT_DIAMETER, WAYPOINT_DIAMETER);
                    graphics.DrawString(path[i].Index.ToString(), ENDS_FONT, ENDS_BRUSH, x1 + TEXT_OFFSET, y1);
                }
                else if (i == path.Count - 2)
                {
                    Pen endsPend = new Pen(ENDS_COLOR, ENDS_THICKNESS);
                    graphics.DrawEllipse(endsPend, x2, y2, ENDS_DIAMETER, ENDS_DIAMETER);
                    graphics.DrawString(path[i + 1].Index.ToString(), ENDS_FONT, ENDS_BRUSH, x2 + TEXT_OFFSET, y2);
                }
                else
                {
                    graphics.DrawEllipse(waypointPen, x1, y1, WAYPOINT_DIAMETER, WAYPOINT_DIAMETER);
                    graphics.DrawEllipse(waypointPen, x2, y2, WAYPOINT_DIAMETER, WAYPOINT_DIAMETER);
                }
                graphics.DrawLine(arrowPen, x1, y1, x2, y2);
            }

        }

        public void SaveFile(string filename)
        {
            bitmap.Save(filename, ImageFormat.Png);
        }
    }
}
