using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_Project4
{
    [Serializable]
    internal class Line : Shape
    {
        public Line(Point startVert, Point endVert, int thickness, Color color, int proximity)
        {
            this.startVert = startVert;
            this.endVert = endVert;
            this.thickness = thickness;
            this.color = color;
            this.vertexSize = proximity;
        }

        public override void changeColor(ref Bitmap bmp, Color newColor)
        {
            delete(ref bmp);
            this.color = newColor;
            draw(ref bmp);
        }

        public override void changeThickness(ref Bitmap bmp, int newThickness)
        {
            delete(ref bmp);
            this.thickness = newThickness;
            draw(ref bmp);
        }

        public override void changeVert(ref Bitmap bmp, Point newPoint, Point oldPoint)
        {
            delete(ref bmp);

            if (this.value == Values.start)
            {
                startVert = newPoint;
            }
            else if (this.value == Values.end)
            {
                endVert = newPoint;
            }
            else if (this.value == Values.edge)
            {
                int dx = newPoint.X - oldPoint.X;
                int dy = newPoint.Y - oldPoint.Y;

                startVert.X = Math.Min(bmp.Width, Math.Max(0, startVert.X + dx));
                startVert.Y = Math.Min(bmp.Height, Math.Max(0, startVert.Y + dy));
                endVert.X = Math.Min(bmp.Width, Math.Max(0, endVert.X + dx));
                endVert.Y = Math.Min(bmp.Height, Math.Max(0, endVert.Y + dy));
            }

            draw(ref bmp);
        }

        private void DrawLine(ref Bitmap bmp)
        {
            int x0 = startVert.X;
            int y0 = startVert.Y;

            int x1 = endVert.X;
            int y1 = endVert.Y;

            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);

            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;

            int err = dx - dy;

            setEnds(ref bmp);

            while (true)
            {
                for (int i = -thickness; i < thickness; i++)
                {
                    for (int j = -thickness; j < thickness; j++)
                    {
                        int px = x0 + i;
                        int py = y0 + j;
                        Color pixel = color;

                        if (px >= 0 && px < bmp.Width && py >= 0 && py < bmp.Height)
                        {
                            bmp.SetPixel(px, py, pixel);
                        }
                    }
                }

                if (x0 == x1 && y0 == y1)
                    break;

                int err2 = 2 * err;

                if (err2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }

                if (err2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        public override void draw(ref Bitmap bmp)
        {
            DrawLine(ref bmp);
        }

        public override void delete(ref Bitmap bmp)
        {
            Color oldColor = this.color;
            this.color = Color.White;
            draw(ref bmp);
            this.color = oldColor;
        }

        protected override void setEnds(ref Bitmap bmp)
        {
            for (int i = -vertexSize / 2; i <= vertexSize / 2; i++)
            {
                for (int j = -vertexSize / 2; j <= vertexSize / 2; j++)
                {
                    int sx = startVert.X + j;
                    int sy = startVert.Y + i;
                    int ex = endVert.X + j; 
                    int ey = endVert.Y + i;

                    if (sx >= bmp.Width || sy >= bmp.Height || sx < 0 || sy < 0 ||
                        ex >= bmp.Width || ey >= bmp.Height || ex < 0 || ey < 0)
                        continue;

                    bmp.SetPixel(sx, sy, color);
                    bmp.SetPixel(ex, ey, color);
                }
            }
        }

        public override Values isMouseOverVertex(MouseEventArgs e)
        {
            if (e.X > startVert.X - vertexSize && e.X < startVert.X + vertexSize && e.Y > startVert.Y - vertexSize && e.Y < startVert.Y + vertexSize)
            {
                return this.value = Values.start;
            }
            else if (e.X > endVert.X - vertexSize && e.X < endVert.X + vertexSize && e.Y > endVert.Y - vertexSize && e.Y < endVert.Y + vertexSize)
            {
                return this.value = Values.end;
            }
            else
            {
                if (endVert.X == startVert.X && (e.Y >= Math.Min(startVert.Y, endVert.Y) && e.Y <= Math.Max(startVert.Y, endVert.Y) &&
                    e.X >= endVert.X - 5 && e.X <= endVert.X + 5))
                {
                    return this.value = Values.edge;
                }
                else
                {
                    float slope = (float)((float)(endVert.Y - startVert.Y) / (float)(endVert.X - startVert.X));
                    float yInter = startVert.Y - slope * startVert.X;

                    if ((e.Y >= slope * e.X + yInter - this.vertexSize && e.Y <= slope * e.X + yInter + this.vertexSize) || (e.Y <= slope * e.X + yInter - this.vertexSize && e.Y >= slope * e.X + yInter + this.vertexSize))
                    {
                        return this.value = Values.edge;
                    }
                }
            }

            return this.value = Values.def;
        }

        public Values setValue(Values value)
        {
            Values oldValue = this.value;
            this.value = value;
            return oldValue;
        }
    }
}