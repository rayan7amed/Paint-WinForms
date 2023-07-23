using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CG_Project4
{
    [Serializable]
    internal class Scanline
    {
        private Shape shape;
        private Color color;
        private Image image;
        private List<int> indices;
        private List<Point> points;
        private bool isColor;
        private int y = 0;
        private Point startPt = new Point(0, 0), endPt = new Point(0, 0);


        public Scanline(Shape shape, Color color, Image image = null, bool isColor = true)
        {
            this.shape = shape;
            this.color = color;
            this.image = image;
            this.isColor = isColor;
            setIndices();
        }

        private void setIndices()
        {

            indices = new List<int>();
            points = new List<Point>();
            if (shape is not Polygon && (shape as Polygon).getFinished())
                return;

            List<Line> lines = (shape as Polygon).getLines().ToList();

            foreach (Line line in lines)
                points.Add(line.getPoints()[0]);
            var sortedIndices = Enumerable.Range(0, points.Count)
            .OrderBy(i => points[i].Y)
            .ToArray();

            indices.AddRange(sortedIndices);
            
        }

        public void Fill(ref Bitmap bmp, List<Edge> AET)
        {
            for (int i = 0; i + 1 < AET.Count; i += 2)
            {
                for (int x = (int)AET[i].XMin; x < (int)AET[i + 1].XMin; x++)
                {
                    if (x < 0 || y < 0 || x > bmp.Width || y > bmp.Height)
                        continue;
                    bmp.SetPixel(x, y, color);
                }
            }
        }

        public void FillImage(ref Bitmap bmp, List<Edge> AET)
        {
            Bitmap tmp = new Bitmap((Bitmap) image, (bmp.Size));
            for (int i = 0; i + 1 < AET.Count; i += 2)
            {
                for (int x = (int)AET[i].XMin; x < (int)AET[i + 1].XMin; x++)
                {
                    if (x < 0 || y < 0 || x > bmp.Width || y > bmp.Height)
                        continue;
                    bmp.SetPixel(x, y, tmp.GetPixel(x, y));
                }
            }
        }

        public void ScanLine(ref Bitmap bmp)
        {
            int k = 0;
            int i = indices[k];
            y = points[indices[0]].Y;
            int yMin = points[indices[0]].Y;
            int yMax = points[indices[indices.Count - 1]].Y;
            List<Edge> AET = new List<Edge>();
            while (y < yMax)
            {
                while (points[i].Y == y)
                {
                    if (i == 0)
                    {
                        if (points[points.Count - 1].Y > points[i].Y)
                            AET.Add(new Edge(points[i], points[points.Count - 1]));
                    }
                    else
                    {
                        if (points[i - 1].Y > points[i].Y)
                            AET.Add(new Edge(points[i], points[i - 1]));
                    }

                    if (i == points.Count - 1)
                    {
                        if (points[0].Y > points[i].Y)
                            AET.Add(new Edge(points[i], points[0]));
                    }
                    else
                    {
                        if (points[i + 1].Y > points[i].Y)
                            AET.Add(new Edge(points[i], points[i + 1]));
                    }

                    ++k;
                    i = indices[k];

                }
                AET.Sort((e1, e2) => e1.XMin.CompareTo(e2.XMin));

                if (this.isColor)
                    Fill(ref bmp, AET);
                else
                    FillImage(ref bmp, AET);

                ++y;
                AET.RemoveAll(edge => edge.YMax == y);
                foreach (Edge edge in AET)
                    edge.increaseX();
                
            }
        }
    }


    [Serializable]
    class Edge
    {
        private int ymax;
        private int ymin;
        private float xmin;
        private float slope;

        public int YMax { get { return ymax; } }
        public int YMin { get { return ymin; } }
        public float XMin { get { return xmin; } }
        public float Slope { get { return slope; } }

        public Edge(Shape shape)
        {
            Point p1 = shape.getPoints()[0], p2 = shape.getPoints()[1];

            this.ymax = Math.Max(p1.Y, p2.Y);
            this.ymin = Math.Min(p1.Y, p2.Y);
            this.slope = (p2.Y - p1.Y) == 0 ? 1 : (float)((float)(p2.X - p1.X) / (float)(p2.Y - p1.Y));
            this.xmin = this.slope > 0 ? Math.Min(p1.X, p2.X) : Math.Max(p1.X, p2.X);
        }
        public Edge(Point p1, Point p2)
        {
            this.ymax = Math.Max(p1.Y, p2.Y);
            this.ymin = Math.Min(p1.Y, p2.Y);
            this.slope = (p2.Y - p1.Y) == 0 ? 1 : (float)((float)(p2.X - p1.X) / (float)(p2.Y - p1.Y));
            this.xmin = this.slope > 0 ? Math.Min(p1.X, p2.X) : Math.Max(p1.X, p2.X);
        }

        public void increaseX()
        {
            xmin += slope;
        }
    }
}
