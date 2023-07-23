using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_Project4
{
    [Serializable]
    internal class Circle : Shape
    {
        private int radius;
        private bool upper = false;
        private float slope = 0f;
        public Circle(Point startVert,  Point endVert, int thickness, Color color, int size) : base()
        {
            this.startVert = startVert;
            this.endVert = endVert;
            this.thickness = thickness;
            this.color = color;
            this.vertexSize = size;

            radius = (int)Math.Sqrt(Math.Pow(endVert.X - startVert.X, 2) + Math.Pow(endVert.Y - startVert.Y, 2));
        }

        public override void changeColor(ref Bitmap bmp, Color newColor)
        {
            delete(ref bmp);
            this.color = newColor;
            draw(ref bmp);
        }

        public override void changeThickness(ref Bitmap bmp, int newThickness)
        {
            return;
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
                radius = (int)Math.Sqrt(Math.Pow(endVert.X - startVert.X, 2) + Math.Pow(endVert.Y - startVert.Y, 2));
            }

            draw(ref bmp);
        }

        public override void draw(ref Bitmap bmp)
        {
            int x = 0;
            int y = radius;
            int d = 3 - 2 * radius;

            setEnds(ref bmp);

            while (y > x)
            {
                if (upper)
                    PlotUpperPoints(x, y, ref bmp);
                else 
                    PlotPoints(x, y, ref bmp);

                if (d < 0)
                {
                    d += 4 * x + 6;
                }
                else
                {
                    d += 4 * (x - y) + 10;
                    --y;
                }

                ++x;
            }
        }

        public override void delete(ref Bitmap bmp)
        {
            Color oldColor = this.color;
            this.color = Color.White;
            draw(ref bmp);
            this.color = oldColor;
        }

        private void PlotPoints(int x, int y, ref Bitmap bmp)
        {
            int plusX = startVert.X + x >= bmp.Width ? bmp.Width - 1 : startVert.X + x;
            int plusY = startVert.Y + y >= bmp.Height ? bmp.Height - 1: startVert.Y + y;
            int minusX = startVert.X - x < 0 ? 0 : startVert.X - x;
            int minusY = startVert.Y - y < 0 ? 0 : startVert.Y - y;

            int plusXY = startVert.X + y >= bmp.Width ? bmp.Width - 1: startVert.X + y;
            int plusYX = startVert.Y + x >= bmp.Height ? bmp.Height - 1: startVert.Y + x;
            int minusXY = startVert.X - y < 0 ? 0 : startVert.X - y;
            int minusYX = startVert.Y - x < 0 ? 0 : startVert.Y - x;

            bmp.SetPixel(plusX, plusY, color);
            bmp.SetPixel(minusX, plusY, color);
            bmp.SetPixel(plusX, minusY, color);
            bmp.SetPixel(minusX, minusY, color);
            bmp.SetPixel(plusXY, plusYX, color);
            bmp.SetPixel(minusXY, plusYX, color);
            bmp.SetPixel(plusXY, minusYX, color);
            bmp.SetPixel(minusXY, minusYX, color);
        }

        private void PlotUpperPoints(int x, int y, ref Bitmap bmp)
        {
            float yInter = startVert.Y - slope * startVert.X;

            int plusX = startVert.X + x >= bmp.Width ? bmp.Width - 1 : startVert.X + x;
            int plusY = startVert.Y + y >= bmp.Height ? bmp.Height - 1 : startVert.Y + y;
            int minusX = startVert.X - x < 0 ? 0 : startVert.X - x;
            int minusY = startVert.Y - y < 0 ? 0 : startVert.Y - y;

            int plusXY = startVert.X + y >= bmp.Width ? bmp.Width - 1 : startVert.X + y;
            int plusYX = startVert.Y + x >= bmp.Height ? bmp.Height - 1 : startVert.Y + x;
            int minusXY = startVert.X - y < 0 ? 0 : startVert.X - y;
            int minusYX = startVert.Y - x < 0 ? 0 : startVert.Y - x;

            if (plusX * slope + yInter >= endVert.Y && plusY <= endVert.Y)
                bmp.SetPixel(plusX, plusY, color);

            if (minusX * slope + yInter >= endVert.Y && plusY <= endVert.Y)
                bmp.SetPixel(minusX, plusY, color);

            if (plusX * slope + yInter >= endVert.Y && minusY <= endVert.Y)
                bmp.SetPixel(plusX, minusY, color);

            if (minusX * slope + yInter >= endVert.Y && minusY <= endVert.Y)
                bmp.SetPixel(minusX, minusY, color);

            if (plusXY * slope + yInter >= endVert.Y && plusYX <= endVert.Y)
                bmp.SetPixel(plusXY, plusYX, color);

            if (minusXY * slope + yInter >= endVert.Y && plusYX <= endVert.Y)
                bmp.SetPixel(minusXY, plusYX, color);

            if (plusXY * slope + yInter >= endVert.Y && minusYX <= endVert.Y)
                bmp.SetPixel(plusXY, minusYX, color);

            if (minusXY * slope + yInter >= endVert.Y && minusYX <= endVert.Y)
                bmp.SetPixel(minusXY, minusYX, color);
        }

        protected override void setEnds(ref Bitmap bmp)
        {
            for (int i = -vertexSize; i < vertexSize / 4; i++)
            {
                for (int j = -vertexSize; j < vertexSize / 4; j++)
                {
                    int sx = startVert.X + j;
                    int sy = startVert.Y + i;

                    bmp.SetPixel(sx, sy, color);
                }
            }
        }

        private bool isCircumference(Point point)
        {
            int rDown = (int)Math.Sqrt(Math.Pow(point.X - startVert.X - vertexSize, 2) + Math.Pow(point.Y - startVert.Y - vertexSize, 2));
            int rUp = (int)Math.Sqrt(Math.Pow(point.X - startVert.X + vertexSize, 2) + Math.Pow(point.Y - startVert.Y + vertexSize, 2));
            return (radius >= rDown && radius <= rUp) || (radius >= rUp && radius <= rDown);
        }

        public override Values isMouseOverVertex(MouseEventArgs e)
        {
            if (e.X > startVert.X - vertexSize && e.X < startVert.X + vertexSize && e.Y > startVert.Y - vertexSize && e.Y < startVert.Y + vertexSize)
            {
                return value = Shape.Values.start;
            }
            else if (isCircumference(new Point(e.X, e.Y)))
            {
                return value = Shape.Values.end;
            }

            return this.value = Values.def;
        }

        public void SetUpper(bool up)
        {
            this.upper = up;
        }

        public void SetSlop(float slope)
        {
            this.slope = slope;
        }
    }
}
