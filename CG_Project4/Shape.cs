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
    abstract class Shape
    {
        public enum Values
        {
            start,
            end,
            edge,
            def
        }

        protected Image fillImage = null;
        protected Color color;
        protected Color fillColor;
        protected Point startVert;
        protected Point endVert;
        protected int thickness;
        protected int vertexSize;
        protected Values value = Values.def;
        protected Scanline sc;
        protected bool filled = false;
        protected bool isColor = true;

        public abstract void draw(ref Bitmap bmp);
        protected abstract void setEnds(ref Bitmap bmp);
        public abstract void changeVert(ref Bitmap bmp, Point newPoint, Point oldPoint);
        public abstract void changeColor(ref Bitmap bmp, Color newColor);
        public abstract void changeThickness(ref Bitmap bmp, int newThickness);
        public abstract Values isMouseOverVertex(MouseEventArgs e);
        public abstract void delete(ref Bitmap bmp);
        public Point[] getPoints() => new Point[2] { startVert, endVert };
        public int getThickness() => thickness;
        public Color GetColor() => color;
        public int getProximity() => vertexSize;
        public bool getFilled() => filled;
        public Color getFillColor() => fillColor;
        
        public bool getIsColor() => isColor;
        public Image getFillImage() => fillImage;

        public void Fill(ref Bitmap bmp, Color color, Image image = null, bool isColor = true)
        {
            this.fillColor = color;
            this.isColor = isColor;
            this.fillImage = image;


            sc = new Scanline(this, color, image, isColor);
            sc.ScanLine(ref bmp);
            filled = true;
        }
    }
}
