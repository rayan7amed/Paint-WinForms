
namespace CG_Project4
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.polygon = new System.Windows.Forms.ToolStrip();
            this.brushTool = new System.Windows.Forms.ToolStripButton();
            this.polygonTool = new System.Windows.Forms.ToolStripButton();
            this.ellipse = new System.Windows.Forms.ToolStripButton();
            this.rectangle = new System.Windows.Forms.ToolStripButton();
            this.hand = new System.Windows.Forms.ToolStripButton();
            this.trash = new System.Windows.Forms.ToolStripButton();
            this.delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.load = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.thickness = new System.Windows.Forms.ToolStripLabel();
            this.thicknessComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.colorBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.drawArea = new System.Windows.Forms.PictureBox();
            this.polygon.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.colorBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawArea)).BeginInit();
            this.SuspendLayout();
            // 
            // polygon
            // 
            this.polygon.AutoSize = false;
            this.polygon.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.polygon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brushTool,
            this.polygonTool,
            this.ellipse,
            this.rectangle,
            this.hand,
            this.trash,
            this.delete,
            this.toolStripSeparator5,
            this.save,
            this.load,
            this.toolStripSeparator2,
            this.thickness,
            this.thicknessComboBox,
            this.toolStripSeparator3,
            this.toolStripSeparator4,
            this.colorToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.polygon.Location = new System.Drawing.Point(0, 0);
            this.polygon.Name = "polygon";
            this.polygon.Size = new System.Drawing.Size(913, 28);
            this.polygon.Stretch = true;
            this.polygon.TabIndex = 1;
            // 
            // brushTool
            // 
            this.brushTool.CheckOnClick = true;
            this.brushTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.brushTool.Image = ((System.Drawing.Image)(resources.GetObject("brushTool.Image")));
            this.brushTool.Name = "brushTool";
            this.brushTool.Size = new System.Drawing.Size(29, 25);
            this.brushTool.Text = "Brush";
            this.brushTool.Click += new System.EventHandler(this.LineToolClick);
            // 
            // polygonTool
            // 
            this.polygonTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.polygonTool.Image = ((System.Drawing.Image)(resources.GetObject("polygonTool.Image")));
            this.polygonTool.Name = "polygonTool";
            this.polygonTool.Size = new System.Drawing.Size(29, 25);
            this.polygonTool.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.polygonTool.Click += new System.EventHandler(this.polygonTool_Click);
            // 
            // ellipse
            // 
            this.ellipse.CheckOnClick = true;
            this.ellipse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ellipse.Image = ((System.Drawing.Image)(resources.GetObject("ellipse.Image")));
            this.ellipse.Name = "ellipse";
            this.ellipse.Size = new System.Drawing.Size(29, 25);
            this.ellipse.Click += new System.EventHandler(this.ellipse_Click);
            // 
            // rectangle
            // 
            this.rectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rectangle.Image = ((System.Drawing.Image)(resources.GetObject("rectangle.Image")));
            this.rectangle.Name = "rectangle";
            this.rectangle.Size = new System.Drawing.Size(29, 25);
            this.rectangle.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.rectangle.Click += new System.EventHandler(this.rectangle_Click);
            // 
            // hand
            // 
            this.hand.CheckOnClick = true;
            this.hand.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hand.Image = ((System.Drawing.Image)(resources.GetObject("hand.Image")));
            this.hand.Name = "hand";
            this.hand.Size = new System.Drawing.Size(29, 25);
            this.hand.Click += new System.EventHandler(this.hand_Click);
            // 
            // trash
            // 
            this.trash.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.trash.Image = ((System.Drawing.Image)(resources.GetObject("trash.Image")));
            this.trash.Name = "trash";
            this.trash.Size = new System.Drawing.Size(29, 25);
            this.trash.Click += new System.EventHandler(this.trash_Click);
            // 
            // delete
            // 
            this.delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(29, 25);
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // save
            // 
            this.save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.save.Image = ((System.Drawing.Image)(resources.GetObject("save.Image")));
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(29, 25);
            this.save.Text = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // load
            // 
            this.load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.load.Image = ((System.Drawing.Image)(resources.GetObject("load.Image")));
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(29, 25);
            this.load.Text = "Load";
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // thickness
            // 
            this.thickness.Name = "thickness";
            this.thickness.Size = new System.Drawing.Size(71, 25);
            this.thickness.Text = "Thickness";
            // 
            // thicknessComboBox
            // 
            this.thicknessComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.thicknessComboBox.Name = "thicknessComboBox";
            this.thicknessComboBox.Size = new System.Drawing.Size(121, 28);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.colorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("colorToolStripMenuItem.Image")));
            this.colorToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(49, 25);
            this.colorToolStripMenuItem.Text = "Color";
            this.colorToolStripMenuItem.Click += new System.EventHandler(this.colorFill_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.imageToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("imageToolStripMenuItem.Image")));
            this.imageToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(55, 25);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageFill_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 806F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Controls.Add(this.colorBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.drawArea, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 40);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(889, 519);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // colorBox
            // 
            this.colorBox.BackColor = System.Drawing.SystemColors.Control;
            this.colorBox.Controls.Add(this.flowLayoutPanel);
            this.colorBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.colorBox.Location = new System.Drawing.Point(809, 3);
            this.colorBox.Name = "colorBox";
            this.colorBox.Size = new System.Drawing.Size(77, 513);
            this.colorBox.TabIndex = 0;
            this.colorBox.TabStop = false;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Location = new System.Drawing.Point(3, 23);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(71, 487);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // drawArea
            // 
            this.drawArea.BackColor = System.Drawing.Color.White;
            this.drawArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawArea.Location = new System.Drawing.Point(3, 3);
            this.drawArea.Name = "drawArea";
            this.drawArea.Size = new System.Drawing.Size(800, 513);
            this.drawArea.TabIndex = 1;
            this.drawArea.TabStop = false;
            this.drawArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawArea_MouseDown_1);
            this.drawArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawArea_MouseMove_1);
            this.drawArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawArea_MouseUp_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 571);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.polygon);
            this.Name = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawArea_MouseDown_1);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawArea_MouseMove_1);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawArea_MouseUp_1);
            this.polygon.ResumeLayout(false);
            this.polygon.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.colorBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip polygon;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripButton brushTool;
        private System.Windows.Forms.GroupBox colorBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.PictureBox drawArea;
        private System.Windows.Forms.ToolStripButton ellipse;
        private System.Windows.Forms.ToolStripButton trash;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripButton load;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel thickness;
        private System.Windows.Forms.ToolStripComboBox thicknessComboBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton hand;
        private System.Windows.Forms.ToolStripButton delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton polygonTool;
        private System.Windows.Forms.ToolStripButton rectangle;
        private System.Windows.Forms.ToolStripButton colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton imageToolStripMenuItem;
    }
}

