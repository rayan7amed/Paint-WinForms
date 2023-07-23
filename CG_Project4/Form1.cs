using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CG_Project4
{
    public partial class Form1 : Form
    {
        private Bitmap drawAreaBitmap;
        private Bitmap prevBitmap;
        private Bitmap ColorBitmap;
        private PictureBox oldPictureBox;
        private Color color;

        private List<Shape> Shapes;
        private List<MRectangle> Recs;
        private List<Line> clipped;
        private Shape LastPolygon;
        private Shape LastShape;
        private Shape theShape;

        private bool isDrawing = false;
        private bool usingLine = false;
        private bool usingEllipse = false;
        private bool usingPoly = false;
        private bool usingHand = false;
        private bool usingRectangle = false;
        private bool firstDashed = true;

        private int startX = -1;
        private int startY = -1;
        private int endx = -1;
        private int endY = -1;
     

        private int vertexSize = 5;

        public Form1()
        {
            Shapes = new List<Shape>();
            Recs = new List<MRectangle>();
            clipped = new List<Line>();
            theShape = null;
            LastShape = null;
            LastPolygon = null;


            InitializeComponent();

            color = Color.Black;

            drawAreaBitmap = new Bitmap(drawArea.Size.Width, drawArea.Size.Height);
            prevBitmap = new Bitmap(drawArea.Size.Width, drawArea.Size.Height);
            drawArea.Image = drawAreaBitmap;

            FillColors();

            using (Graphics g = Graphics.FromImage(drawAreaBitmap))
            {
                g.Clear(Color.White);
            }

            thicknessComboBox.Text = "1";

        }

        private void LineToolClick(object sender, EventArgs e)
        {
            usingEllipse = false;
            usingHand = false;
            usingPoly = false;
            usingRectangle = false;
            ellipse.Checked = false;
            hand.Checked = false;
            polygonTool.Checked = false;
            rectangle.Checked = false;

            usingLine = !usingLine;

            LastPolygon = null;
        }


        private void drawArea_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Button != MouseButtons.Left)
            {
                isDrawing = false;
                return;
            }

            startX = e.X;
            startY = e.Y;

            if (usingLine || usingEllipse || usingPoly || usingRectangle)
                isDrawing = true;

            if (usingHand)
            {
                foreach (var shape in Shapes)
                {
                    if (shape.isMouseOverVertex(e) != Shape.Values.def)
                    {
                        theShape = shape;
                        break;
                    }
                }
            }
        }

        private void drawArea_MouseMove_1(object sender, MouseEventArgs e)
        {
            drawArea.Cursor = Cursors.Cross;
        }

        private void drawArea_MouseUp_1(object sender, MouseEventArgs e)
        {
            if ((isDrawing && startX >= 0 && startY >= 0) || (usingHand && theShape != null))
            {
                endx = e.X;
                endY = e.Y;
            }
            if (usingLine && isDrawing)
            {
                Line line = new Line(new Point(startX, startY), new Point(endx, endY), int.Parse(thicknessComboBox.Text), color, vertexSize);
                line.draw(ref drawAreaBitmap);
                drawArea.Refresh();
                Shapes.Add(line);
            }
            else if (usingEllipse && isDrawing)
            {
                Circle cirle = new Circle(new Point(startX, startY), new Point(endx, endY), int.Parse(thicknessComboBox.Text), color, vertexSize);
                cirle.draw(ref drawAreaBitmap);
                drawArea.Refresh();

                Shapes.Add(cirle);
            }
            else if (usingPoly && isDrawing)
            {
                if (LastPolygon != null && !((Polygon)LastPolygon).getFinished())
                {
                    ((Polygon)LastPolygon).addVert(new Point(endx, endY));
                    LastPolygon.draw(ref drawAreaBitmap);
                    drawArea.Refresh();
                }
                else
                {
                    Polygon polygon = new Polygon(new Point(startX, startY), new Point(endx, endY), int.Parse(thicknessComboBox.Text), color, vertexSize);
                    polygon.draw(ref drawAreaBitmap);
                    drawArea.Refresh();

                    Shapes.Add(polygon);
                    LastPolygon = polygon;
                }
            }
            
            else if (usingRectangle && isDrawing)
            {
                MRectangle myRectangle = new MRectangle(new Point(startX, startY), new Point(endx, endY), int.Parse(thicknessComboBox.Text), color, vertexSize);
                myRectangle.draw(ref drawAreaBitmap);
                drawArea.Refresh();

                Shapes.Add(myRectangle);
                Recs.Add(myRectangle);
            }
            else if (usingHand && theShape != null)
            {
                Color tmpColor = color;
                Image tmpImage = theShape.getFillImage();
                bool isColor = theShape.getIsColor();

                if (theShape.getFilled())
                { 
                    if (isColor)
                    {
                        theShape.Fill(ref drawAreaBitmap, Color.White);
                        theShape.changeVert(ref drawAreaBitmap, new Point(endx, endY), new Point(startX, startY));
                        theShape.Fill(ref drawAreaBitmap, tmpColor);
                    }
                    else
                    {
                        theShape.Fill(ref drawAreaBitmap, Color.White);
                        theShape.changeVert(ref drawAreaBitmap, new Point(endx, endY), new Point(startX, startY));
                        theShape.Fill(ref drawAreaBitmap, Color.White, DDD, false);
                    }
                    
                }
                else
                    theShape.changeVert(ref drawAreaBitmap, new Point(endx, endY), new Point(startX, startY));


                drawArea.Refresh();
            }

            LiangBarsky();

            isDrawing = false;

            startX = -1;
            startY = -1;
            endx = -1;
            endY = -1;
            LastShape = theShape;
            theShape = null;
        }

        private void save_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream("shapes.bin", FileMode.Create))
            {
                formatter.Serialize(fileStream, Shapes);
            }
        }

        private void load_Click(object sender, EventArgs e)
        {   String flname = "C:\\Users\\rayan\\source\\repos\\CG_Project4\\CG_Project4\\bin\\Debug\\net5.0-windows\\shapes.bin";
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(flname, FileMode.Open))
            {
                Shapes = (List<Shape>)formatter.Deserialize(fileStream);
                foreach (Shape shape in Shapes)
                {
                    if (shape is MRectangle)
                        Recs.Add((MRectangle)shape);

                    shape.draw(ref drawAreaBitmap);

                    if (shape is Polygon && shape.getFilled())
                        shape.Fill(ref drawAreaBitmap, shape.getFillColor(), shape.getFillImage(), shape.getIsColor());
                }

                LiangBarsky();
                drawArea.Refresh();
            }
        }

        private void toolStripButton6_Paint(object sender, PaintEventArgs e)
        {
            ToolStripButton ChooseColorButton = (ToolStripButton)sender;
            Rectangle rect = new Rectangle(0, 0, ChooseColorButton.Width, ChooseColorButton.Height);

            using (SolidBrush brush = new SolidBrush(ChooseColorButton.BackColor))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void FillColors()
        {
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                PictureBox button = new PictureBox();
                button.BackColor = Color.FromKnownColor(color);
                button.Size = new Size(25, 25);
                button.Name = color.ToString();
                button.Click += new EventHandler(this.btnCLick_Event);
                flowLayoutPanel.Controls.Add(button);
            }
        }

        private void RemoveColors()
        {
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor)))
            {
                PictureBox button = new PictureBox();
                button.BackColor = Color.FromKnownColor(color);
                button.Size = new Size(25, 25);
                button.Name = color.ToString();
                button.Click += new EventHandler(this.btnCLick_Event);
                flowLayoutPanel.Controls.Remove(button);
            }
        }

        private void hand_Click(object sender, EventArgs e)
        {
            usingLine = false;
            usingEllipse = false;
            usingPoly = false;
            usingRectangle = false;
            brushTool.Checked = false;
            ellipse.Checked = false;
            polygonTool.Checked = false;
            rectangle.Checked = false;

            usingHand = !usingHand;

            LastPolygon = null;
        }

        private void thicknessComboBox_Click(object sender, EventArgs e)
        {
            if (LastShape != null)
            {
                LastShape.changeThickness(ref drawAreaBitmap, int.Parse(thicknessComboBox.Text));
                drawArea.Refresh();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (LastShape != null)
            {
                LastShape.delete(ref drawAreaBitmap);

                Shapes.Remove(LastShape);

                if (LastShape is MRectangle)
                    Recs.Remove((MRectangle)LastShape);

                if (LastShape is Line && clipped.Contains(LastShape))
                    clipped.Remove((Line)LastShape);

                LastShape = null;
                drawArea.Refresh();
            }
        }

        private void polygonTool_Click(object sender, EventArgs e)
        {
            usingLine = false;
            usingEllipse = false;
            usingHand = false;
            usingRectangle = false;
            brushTool.Checked = false;
            ellipse.Checked = false;
            hand.Checked = false;
            polygonTool.Checked = true;
            rectangle.Checked = false;

            usingPoly = !usingPoly;
            LastPolygon = null;
        }

        private void rectangle_Click(object sender, EventArgs e)
        {
            usingLine = false;
            usingHand = false;
            usingPoly = false;
            usingEllipse = false;
            polygonTool.Checked = false;
            brushTool.Checked = false;
            hand.Checked = false;
            ellipse.Checked = false;

            usingRectangle = !usingRectangle;
            rectangle.Checked = usingRectangle;

            LastPolygon = null;
        }
        private void ellipse_Click(object sender, EventArgs e)
        {
            usingLine = false;
            usingHand = false;
            usingPoly = false;
            usingRectangle = false;
            polygonTool.Checked = false;
            brushTool.Checked = false;
            hand.Checked = false;
            rectangle.Checked = false;

            usingEllipse = !usingEllipse;
            LastPolygon = null;
        }

        private void trash_Click(object sender, EventArgs e)
        {
            Shapes.Clear();
            Recs.Clear();
            clipped.Clear();
            LastShape = null;
            theShape = null;

            using (Graphics g = Graphics.FromImage(drawAreaBitmap))
            {
                g.Clear(Color.White);
                drawArea.Refresh();
            }
        }

        bool Clip(float p, float q, ref float u1, ref float u2)
        {
            float r = q / p;
            if (p == 0)
            {
                if (q < 0)
                    return false;
                return true;
            }
            else if (p < 0)
            {
                if (r > u2)
                    return false;
                else if (r > u1)
                    u1 = r;
            }
            else
            {
                if (r < u1)
                    return false;
                else if (r < u2)
                    u2 = r;
            }

            return true;
        }

        Line LiangBarskyClip(Line shape, MRectangle rectangle)
        {
            Point p1 = shape.getPoints()[0], p2 = shape.getPoints()[1];
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            float u1 = 0, u2 = 1;

            if (Clip(-dx, p1.X - rectangle.Left, ref u1, ref u2))
                if (Clip(dx, rectangle.Right - p1.X, ref u1, ref u2))
                    if (Clip(-dy, p1.Y - rectangle.Top, ref u1, ref u2))
                        if (Clip(dy, rectangle.Bottom - p1.Y, ref u1, ref u2))
                        {
                            int x1 = (int)Math.Round(p1.X + u1 * dx);
                            int y1 = (int)Math.Round(p1.Y + u1 * dy);
                            int x2 = (int)Math.Round(p1.X + u2 * dx);
                            int y2 = (int)Math.Round(p1.Y + u2 * dy);

                            Line line = new Line(new Point(x1, y1), new Point(x2, y2),
                                shape.getThickness(), Color.Blue, shape.getProximity());
                            return line;
                        }

            return null;
        }

        private void LiangBarsky()
        {
            if (Recs.Count == 0)
                return;

            foreach (Line line in clipped)
                line.delete(ref drawAreaBitmap);

            foreach (Shape shape in Shapes)
                shape.draw(ref drawAreaBitmap);

            clipped.Clear();

            foreach (MRectangle rectangle in Recs)
            {
                foreach (Shape shape in Shapes)
                {
                    Line clipLine;
                    if (shape is Line)
                    {
                        Line ll = (Line)shape;
                        clipLine = LiangBarskyClip(ll, rectangle);
                        if (clipLine != null)
                            clipped.Add(clipLine);
                    }
                    else if (shape is Polygon)
                    {
                        Polygon polygon = (Polygon)shape;
                        List<Line> lines = polygon.getLines();
                        foreach (Line line in lines)
                        {
                            clipLine = LiangBarskyClip(line, rectangle);
                            if (clipLine != null)
                                clipped.Add(clipLine);
                        }
                    }
                }
                foreach (MRectangle shape in Recs)
                {
                    Line clipLine;
                    List<Line> lines = shape.getLines();
                    foreach (Line line in lines)
                    {
                        clipLine = LiangBarskyClip(line, rectangle);
                        if (clipLine != null)
                            clipped.Add(clipLine);
                    }
                }
            }
            foreach (Line line in clipped)
                line.draw(ref drawAreaBitmap);

            drawArea.Refresh();
        }

        private void colorFill_Click(object sender, EventArgs e)
        {
            if (LastShape != null)
            {
                ColorDialog colorDialog = new ColorDialog();
                colorDialog.AllowFullOpen = false;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    LastShape.Fill(ref drawAreaBitmap, colorDialog.Color);
                    drawArea.Refresh();
                }

                colorDialog.Dispose();
            }
        }
        Bitmap DDD;

        private void imageFill_Click(object sender, EventArgs e)
        {
            if (LastShape != null)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Open Image";
                dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    DDD = new Bitmap(dialog.FileName);
                    LastShape.Fill(ref drawAreaBitmap, Color.White, new Bitmap(dialog.FileName), false);
                    drawArea.Refresh();
                }
            }
        }
        private void btnCLick_Event(object sender, EventArgs e)
        {
            if (!firstDashed)
            {
                using (Graphics g = Graphics.FromImage(ColorBitmap))
                {
                    g.Clear(oldPictureBox.BackColor);
                    oldPictureBox.Refresh();
                }
            }

            Pen dashedPen = new Pen(Color.Black, 5);
            dashedPen.DashPattern = new float[] { 0.5F, 0.5F };
            Color tmp = ((PictureBox)sender).BackColor;
            dashedPen.Color = Color.FromArgb(255 - tmp.R, 255 - tmp.G, 255 - tmp.B);

            ColorBitmap = new Bitmap(25, 25);

            ((PictureBox)sender).Image = ColorBitmap;

            using (Graphics g = Graphics.FromImage(ColorBitmap))
            {
                g.DrawRectangle(dashedPen, 0, 0, 25, 25);
                ((PictureBox)sender).Refresh();
            }

            firstDashed = false;
            oldPictureBox = (PictureBox)sender;
            dashedPen.Dispose();

            if (LastShape != null)
            {
                LastShape.changeColor(ref drawAreaBitmap, color);
                drawArea.Refresh();
            }
        }



    }
}
