using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CG_Project4
{
    [Serializable]
    internal class MRectangle : Shape
    {
        private int midX, midY;
        private List<Line> lines;
        private Line toChangeLine = null;

        public int Top { get { return Math.Min(lines[0].getPoints()[0].Y, Math.Min(lines[1].getPoints()[0].Y, Math.Min(lines[2].getPoints()[0].Y, lines[3].getPoints()[0].Y))); } }
        public int Right { get { return Math.Max(lines[0].getPoints()[0].X, Math.Max(lines[1].getPoints()[0].X, Math.Max(lines[2].getPoints()[0].X, lines[3].getPoints()[0].X))); } }
        public int Bottom { get { return Math.Max(lines[0].getPoints()[0].Y, Math.Max(lines[1].getPoints()[0].Y, Math.Max(lines[2].getPoints()[0].Y, lines[3].getPoints()[0].Y))); } }
        public int Left { get { return Math.Min(lines[0].getPoints()[0].X, Math.Min(lines[1].getPoints()[0].X, Math.Min(lines[2].getPoints()[0].X, lines[3].getPoints()[0].X))); } }

        public MRectangle(Point startVert, Point endVert, int thickness, Color color, int proximity)
        {
            this.startVert = startVert;
            this.endVert = endVert;
            this.thickness = thickness;
            this.color = color;
            this.vertexSize = proximity;
         
            Point upperRight = new Point(endVert.X, startVert.Y);
            Point lowerLeft = new Point(startVert.X, endVert.Y);

            lines = new List<Line>
            {
                new Line(startVert, upperRight, thickness, color, proximity),
                new Line(upperRight, endVert, thickness, color, proximity),
                new Line(endVert, lowerLeft, thickness, color, proximity),
                new Line(lowerLeft, startVert, thickness, color, proximity)
            };
        }
        public override void changeColor(ref Bitmap bmp, Color newColor)
        {
            delete(ref bmp);
            this.color = newColor;

            foreach (Line line in lines)
            {
                line.changeColor(ref bmp, this.color);
            }

            setEnds(ref bmp);
        }

        public override void changeThickness(ref Bitmap bmp, int newThickness)
        {
            delete(ref bmp);
            this.thickness = newThickness;

            foreach (Line line in lines)
            {
                line.changeThickness(ref bmp, this.thickness);
            }

            draw(ref bmp);
        }

        public override void changeVert(ref Bitmap bmp, Point newPoint, Point oldPoint)
        {
            delete(ref bmp);

            int changeX = newPoint.X - oldPoint.X;
            int changeY = newPoint.Y - oldPoint.Y;

            Values oldValue = Values.def;
            if (this.value == Values.start && toChangeLine == null)
            {
                foreach (Line line in lines)
                {
                    Point[] points = line.getPoints();
                    Point[] newPoints = new Point[2]
                    { 
                        new Point(points[0].X + changeX, points[0].Y + changeY),
                        new Point(points[1].X + changeX, points[1].Y + changeY)
                    };

                    oldValue = line.setValue(Values.start);
                    line.changeVert(ref bmp, newPoints[0], points[0]);
                    line.setValue(Values.end);
                    line.changeVert(ref bmp, newPoints[1], points[1]);

                    line.setValue(oldValue);
                }
            }
            else if (this.value == Values.start)
            {
                Point endPoints = lines[2].getPoints()[0];
                Point upperRight = new Point(endPoints.X, newPoint.Y);
                Point lowerLeft = new Point(newPoint.X, endPoints.Y);

                lines = new List<Line>
                {
                    new Line(newPoint, upperRight, thickness, color, vertexSize),
                    new Line(upperRight, endPoints, thickness, color, vertexSize),
                    new Line(endPoints, lowerLeft, thickness, color, vertexSize),
                    new Line(lowerLeft, newPoint, thickness, color, vertexSize)
                };
            }
            else if (this.value == Values.end)
            {
                Point begPoint = lines[0].getPoints()[0];
                Point endPoint = lines[2].getPoints()[0];
                Point upperRight, lowerLeft;

                if (lines[0] == toChangeLine)
                {
                    upperRight = newPoint;
                    lowerLeft = new Point(begPoint.X, endPoint.Y);
                    begPoint = new Point(begPoint.X, newPoint.Y);
                    endPoint = new Point(newPoint.X, endPoint.Y);
                }
                else if (lines[1] == toChangeLine)
                {
                    upperRight = new Point(newPoint.X, begPoint.Y);
                    lowerLeft = new Point(begPoint.X, newPoint.Y);
                    endPoint = newPoint;
                }
                else
                {
                    upperRight = new Point(endPoint.X, begPoint.Y);
                    lowerLeft = newPoint;
                    begPoint = new Point(newPoint.X, begPoint.Y);
                    endPoint = new Point(endPoint.X, newPoint.Y);
                }

                lines = new List<Line>
                {
                    new Line(begPoint, upperRight, thickness, color, vertexSize),
                    new Line(upperRight, endPoint, thickness, color, vertexSize),
                    new Line(endPoint, lowerLeft, thickness, color, vertexSize),
                    new Line(lowerLeft, begPoint, thickness, color, vertexSize)
                };
            }
            else if (this.value == Values.edge)
            {
                Point begPoint = lines[0].getPoints()[0];
                Point endPoint = lines[2].getPoints()[0]; 
                Point upperRight = new Point(endPoint.X, begPoint.Y);
                Point lowerLeft = new Point(begPoint.X, endPoint.Y);

                if (lines[0] == toChangeLine)
                {
                    begPoint = new Point(begPoint.X, newPoint.Y);
                    upperRight = new Point(endPoint.X, newPoint.Y);
                }
                else if (lines[1] == toChangeLine)
                {
                    upperRight = new Point(newPoint.X, begPoint.Y);
                    endPoint = new Point(newPoint.X, endPoint.Y);
                }
                else if (lines[2] == toChangeLine)
                {
                    endPoint = new Point(endPoint.X, newPoint.Y);
                    lowerLeft = new Point(begPoint.X, newPoint.Y);
                }
                else
                {
                    lowerLeft = new Point(newPoint.X, endPoint.Y);
                    begPoint = new Point(newPoint.X, begPoint.Y);
                }

                lines = new List<Line>
                {
                    new Line(begPoint, upperRight, thickness, color, vertexSize),
                    new Line(upperRight, endPoint, thickness, color, vertexSize),
                    new Line(endPoint, lowerLeft, thickness, color, vertexSize),
                    new Line(lowerLeft, begPoint, thickness, color, vertexSize)
                };
            }

            draw(ref bmp);
            this.value = Values.def;
            toChangeLine = null;
        }

        public override void delete(ref Bitmap bmp)
        {
            Color oldColor = this.color;
            this.color = Color.White;
            draw(ref bmp);
            this.color = oldColor;
        }

        public override void draw(ref Bitmap bmp)
        {
            setEnds(ref bmp);
            foreach (Line line in lines)
            {
                line.changeColor(ref bmp, this.color);
            }
        }

        public override Values isMouseOverVertex(MouseEventArgs e)
        {
            if (e.X > midX - vertexSize && e.X < midX + vertexSize && e.Y > midY - vertexSize && e.Y < midY + vertexSize)
            {
                return this.value = Values.start;
            }

            foreach (Line line in lines)
            {
                if (line.isMouseOverVertex(e) != Values.def)
                {
                    toChangeLine = line;
                    return this.value = line.isMouseOverVertex(e);
                }
            }

            return this.value = Values.def;
        }

        protected override void setEnds(ref Bitmap bmp)
        {
            Point[] startPoints = lines[0].getPoints();
            Point[] endPoints = lines[1].getPoints();
            midX = startPoints[0].X + (endPoints[1].X - startPoints[0].X) / 2;
            midY = startPoints[0].Y + (endPoints[1].Y - startPoints[0].Y) / 2;

            for (int i = -vertexSize; i < vertexSize/4; i++)
            {
                for (int j = -vertexSize; j < vertexSize/4; j++)
                {
                    int sx = midX + j;
                    int sy = midY + i;

                    bmp.SetPixel(sx, sy, color);
                }
            }
        }

        public List<Line> getLines() => lines;
    }
}
