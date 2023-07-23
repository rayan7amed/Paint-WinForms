using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_Project4
{
    [Serializable]
    class Polygon : Shape
    {
        private List<Line> lines;
        private bool finished = false;
        private Line toChangeLine = null;

        public Polygon(Point startVert, Point endVert, int thickness, Color color, int size)
        {
            lines = new List<Line>();

            lines.Add(new Line (startVert, endVert, thickness, color, size));

            this.startVert = startVert;
            this.endVert = endVert; 
            this.thickness = thickness;
            this.color = color;
            this.vertexSize = size;
        }
        public override void changeColor(ref Bitmap bmp, Color newColor)
        {
            delete(ref bmp);
            this.color = newColor;

            foreach (Line line in lines)
            {
                line.changeColor(ref bmp, this.color);
            }
        }

        public override void changeThickness(ref Bitmap bmp, int newThickness)
        {
            delete(ref bmp);
            this.thickness = newThickness;

            foreach (Line line in lines)
            {
                line.changeThickness(ref bmp, this.thickness);
            }
        }

        public override void changeVert(ref Bitmap bmp, Point newPoint, Point oldPoint)
        {
            delete (ref bmp);

            int ind = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i] == toChangeLine)
                {
                    ind = i;
                    break;
                }
            }

            Line above = null, below = null;
            if (ind != -1)
            {
                if (ind == 0)
                {
                    above = lines[ind + 1];
                    if (finished)
                    {
                        below = lines[lines.Count - 1];
                    }
                }
                else if (ind == lines.Count -1)
                {
                    below = lines[ind - 1];
                    if (finished)
                    {
                        above = lines[0];
                    }
                }
                else
                {
                    above = lines[ind + 1];
                    below = lines[ind - 1];
                }
            }

            Values[] oldValues = new Values[2] { Values.def, Values.def};

            if (this.value == Values.start)
            {
                if (below != null)
                {
                    oldValues[0] = below.setValue(Values.end);
                    below.changeVert(ref bmp, newPoint, oldPoint);
                }

                toChangeLine.changeVert(ref bmp, newPoint, oldPoint);
            }
            else if (this.value == Values.end)
            {
                if (above != null)
                {
                    oldValues[1] = above.setValue(Values.start);
                    above.changeVert(ref bmp, newPoint, oldPoint);
                }

                toChangeLine.changeVert(ref bmp, newPoint, oldPoint);
            }   
            else if (this.value == Values.edge)
            {
                toChangeLine.changeVert(ref bmp, newPoint, oldPoint);

                if (below != null)
                {
                    oldValues[0] = below.setValue(Values.end);
                    below.changeVert(ref bmp, toChangeLine.getPoints()[0], oldPoint);
                }
                if (above != null)
                {
                    oldValues[1] = above.setValue(Values.start);
                    above.changeVert(ref bmp, toChangeLine.getPoints()[1], oldPoint);
                }
            }

            draw(ref bmp);

            if (below != null)
                below.setValue(oldValues[0]);
            if (above != null)
                above.setValue(oldValues[1]);
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
            if (endVert.X >= startVert.X - vertexSize && endVert.X <= startVert.X + vertexSize && endVert.Y >= startVert.Y - vertexSize && startVert.Y <= endVert.Y + vertexSize)
            {
                endVert = startVert;
                finished = true;
            }

            foreach (Line line in lines)
            {
                line.draw(ref bmp);
            }
        }

        protected override void setEnds(ref Bitmap bmp)
        {
            throw new NotImplementedException();
        }

        public void addVert(Point newEndPoint)
        {
            if (!finished)
            {
                lines.Add(new Line(endVert, newEndPoint, thickness, color, vertexSize));
                endVert = newEndPoint;
            }
        }

        public bool getFinished()
        {
            return finished;
        }

        public override Values isMouseOverVertex(MouseEventArgs e)
        {
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

        public List<Line> getLines() => lines;
    }
}
