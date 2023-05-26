
namespace Rasterization
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fillWithImageButton = new System.Windows.Forms.Button();
            this.loadImageToFillButton = new System.Windows.Forms.Button();
            this.fillPolygonButton = new System.Windows.Forms.Button();
            this.rectangleClipButton = new System.Windows.Forms.Button();
            this.rectangleButton = new System.Windows.Forms.Button();
            this.addEdgeButton = new System.Windows.Forms.Button();
            this.addVertexButton = new System.Windows.Forms.Button();
            this.createPolygonButton = new System.Windows.Forms.Button();
            this.bezierCurveButton = new System.Windows.Forms.Button();
            this.aaCircleButton = new System.Windows.Forms.Button();
            this.aaLineButton = new System.Windows.Forms.Button();
            this.colorsComboBox = new System.Windows.Forms.ComboBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.editElementsButton = new System.Windows.Forms.Button();
            this.circleButton = new System.Windows.Forms.Button();
            this.numericUpDownThickLine = new System.Windows.Forms.NumericUpDown();
            this.lineButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawingBox = new System.Windows.Forms.PictureBox();
            this.insidePoligonsTextBox = new System.Windows.Forms.TextBox();
            this.insidePoligonsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThickLine)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.insidePoligonsButton);
            this.splitContainer1.Panel1.Controls.Add(this.insidePoligonsTextBox);
            this.splitContainer1.Panel1.Controls.Add(this.fillWithImageButton);
            this.splitContainer1.Panel1.Controls.Add(this.loadImageToFillButton);
            this.splitContainer1.Panel1.Controls.Add(this.fillPolygonButton);
            this.splitContainer1.Panel1.Controls.Add(this.rectangleClipButton);
            this.splitContainer1.Panel1.Controls.Add(this.rectangleButton);
            this.splitContainer1.Panel1.Controls.Add(this.addEdgeButton);
            this.splitContainer1.Panel1.Controls.Add(this.addVertexButton);
            this.splitContainer1.Panel1.Controls.Add(this.createPolygonButton);
            this.splitContainer1.Panel1.Controls.Add(this.bezierCurveButton);
            this.splitContainer1.Panel1.Controls.Add(this.aaCircleButton);
            this.splitContainer1.Panel1.Controls.Add(this.aaLineButton);
            this.splitContainer1.Panel1.Controls.Add(this.colorsComboBox);
            this.splitContainer1.Panel1.Controls.Add(this.removeButton);
            this.splitContainer1.Panel1.Controls.Add(this.editElementsButton);
            this.splitContainer1.Panel1.Controls.Add(this.circleButton);
            this.splitContainer1.Panel1.Controls.Add(this.numericUpDownThickLine);
            this.splitContainer1.Panel1.Controls.Add(this.lineButton);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.drawingBox);
            this.splitContainer1.Size = new System.Drawing.Size(836, 532);
            this.splitContainer1.SplitterDistance = 102;
            this.splitContainer1.TabIndex = 0;
            // 
            // fillWithImageButton
            // 
            this.fillWithImageButton.Location = new System.Drawing.Point(531, 62);
            this.fillWithImageButton.Name = "fillWithImageButton";
            this.fillWithImageButton.Size = new System.Drawing.Size(89, 33);
            this.fillWithImageButton.TabIndex = 18;
            this.fillWithImageButton.Text = "fill with image";
            this.fillWithImageButton.UseVisualStyleBackColor = true;
            this.fillWithImageButton.Click += new System.EventHandler(this.fillWithImageButton_Click);
            // 
            // loadImageToFillButton
            // 
            this.loadImageToFillButton.Location = new System.Drawing.Point(625, 62);
            this.loadImageToFillButton.Name = "loadImageToFillButton";
            this.loadImageToFillButton.Size = new System.Drawing.Size(105, 33);
            this.loadImageToFillButton.TabIndex = 17;
            this.loadImageToFillButton.Text = "load image to fill";
            this.loadImageToFillButton.UseVisualStyleBackColor = true;
            this.loadImageToFillButton.Click += new System.EventHandler(this.loadImageToFillButton_Click);
            // 
            // fillPolygonButton
            // 
            this.fillPolygonButton.Location = new System.Drawing.Point(436, 62);
            this.fillPolygonButton.Name = "fillPolygonButton";
            this.fillPolygonButton.Size = new System.Drawing.Size(89, 33);
            this.fillPolygonButton.TabIndex = 16;
            this.fillPolygonButton.Text = "fill polygon";
            this.fillPolygonButton.UseVisualStyleBackColor = true;
            this.fillPolygonButton.Click += new System.EventHandler(this.fillPolygonButton_Click);
            // 
            // rectangleClipButton
            // 
            this.rectangleClipButton.Enabled = false;
            this.rectangleClipButton.Location = new System.Drawing.Point(342, 62);
            this.rectangleClipButton.Name = "rectangleClipButton";
            this.rectangleClipButton.Size = new System.Drawing.Size(88, 33);
            this.rectangleClipButton.TabIndex = 15;
            this.rectangleClipButton.Text = "rectangle clip";
            this.rectangleClipButton.UseVisualStyleBackColor = true;
            this.rectangleClipButton.Click += new System.EventHandler(this.rectangleClipButton_Click);
            // 
            // rectangleButton
            // 
            this.rectangleButton.Location = new System.Drawing.Point(260, 62);
            this.rectangleButton.Name = "rectangleButton";
            this.rectangleButton.Size = new System.Drawing.Size(76, 33);
            this.rectangleButton.TabIndex = 14;
            this.rectangleButton.Text = "rectangle";
            this.rectangleButton.UseVisualStyleBackColor = true;
            this.rectangleButton.Click += new System.EventHandler(this.rectangleButton_Click);
            // 
            // addEdgeButton
            // 
            this.addEdgeButton.Enabled = false;
            this.addEdgeButton.Location = new System.Drawing.Point(179, 62);
            this.addEdgeButton.Name = "addEdgeButton";
            this.addEdgeButton.Size = new System.Drawing.Size(75, 33);
            this.addEdgeButton.TabIndex = 13;
            this.addEdgeButton.Text = "add edge";
            this.addEdgeButton.UseVisualStyleBackColor = true;
            this.addEdgeButton.Click += new System.EventHandler(this.addEdgeButton_Click);
            // 
            // addVertexButton
            // 
            this.addVertexButton.Enabled = false;
            this.addVertexButton.Location = new System.Drawing.Point(98, 62);
            this.addVertexButton.Name = "addVertexButton";
            this.addVertexButton.Size = new System.Drawing.Size(75, 33);
            this.addVertexButton.TabIndex = 12;
            this.addVertexButton.Text = "add vertex";
            this.addVertexButton.UseVisualStyleBackColor = true;
            this.addVertexButton.Click += new System.EventHandler(this.addVertexButton_Click);
            // 
            // createPolygonButton
            // 
            this.createPolygonButton.Location = new System.Drawing.Point(3, 62);
            this.createPolygonButton.Name = "createPolygonButton";
            this.createPolygonButton.Size = new System.Drawing.Size(89, 33);
            this.createPolygonButton.TabIndex = 11;
            this.createPolygonButton.Text = "create polygon";
            this.createPolygonButton.UseVisualStyleBackColor = true;
            this.createPolygonButton.Click += new System.EventHandler(this.createPolygonButton_Click);
            // 
            // bezierCurveButton
            // 
            this.bezierCurveButton.Location = new System.Drawing.Point(699, 23);
            this.bezierCurveButton.Name = "bezierCurveButton";
            this.bezierCurveButton.Size = new System.Drawing.Size(76, 33);
            this.bezierCurveButton.TabIndex = 10;
            this.bezierCurveButton.Text = "bezier curve";
            this.bezierCurveButton.UseVisualStyleBackColor = true;
            this.bezierCurveButton.Click += new System.EventHandler(this.bezierCurveButton_Click);
            // 
            // aaCircleButton
            // 
            this.aaCircleButton.Location = new System.Drawing.Point(313, 23);
            this.aaCircleButton.Name = "aaCircleButton";
            this.aaCircleButton.Size = new System.Drawing.Size(76, 33);
            this.aaCircleButton.TabIndex = 9;
            this.aaCircleButton.Text = "aa circle";
            this.aaCircleButton.UseVisualStyleBackColor = true;
            this.aaCircleButton.Click += new System.EventHandler(this.aaCircleButton_Click);
            // 
            // aaLineButton
            // 
            this.aaLineButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.aaLineButton.Location = new System.Drawing.Point(149, 23);
            this.aaLineButton.Name = "aaLineButton";
            this.aaLineButton.Size = new System.Drawing.Size(76, 33);
            this.aaLineButton.TabIndex = 8;
            this.aaLineButton.Text = "aa line";
            this.aaLineButton.UseVisualStyleBackColor = true;
            this.aaLineButton.Click += new System.EventHandler(this.aaLineButton_Click);
            // 
            // colorsComboBox
            // 
            this.colorsComboBox.FormattingEnabled = true;
            this.colorsComboBox.Location = new System.Drawing.Point(572, 30);
            this.colorsComboBox.Name = "colorsComboBox";
            this.colorsComboBox.Size = new System.Drawing.Size(121, 21);
            this.colorsComboBox.TabIndex = 6;
            this.colorsComboBox.SelectedIndexChanged += new System.EventHandler(this.colorsComboBox_SelectedIndexChanged);
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Location = new System.Drawing.Point(490, 23);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(76, 33);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // editElementsButton
            // 
            this.editElementsButton.Location = new System.Drawing.Point(395, 23);
            this.editElementsButton.Name = "editElementsButton";
            this.editElementsButton.Size = new System.Drawing.Size(89, 33);
            this.editElementsButton.TabIndex = 4;
            this.editElementsButton.Text = "edit elements";
            this.editElementsButton.UseVisualStyleBackColor = true;
            this.editElementsButton.Click += new System.EventHandler(this.editElementsButton_Click);
            // 
            // circleButton
            // 
            this.circleButton.Location = new System.Drawing.Point(231, 23);
            this.circleButton.Name = "circleButton";
            this.circleButton.Size = new System.Drawing.Size(76, 33);
            this.circleButton.TabIndex = 3;
            this.circleButton.Text = "circle";
            this.circleButton.UseVisualStyleBackColor = true;
            this.circleButton.Click += new System.EventHandler(this.circleButton_Click);
            // 
            // numericUpDownThickLine
            // 
            this.numericUpDownThickLine.Location = new System.Drawing.Point(85, 30);
            this.numericUpDownThickLine.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownThickLine.Name = "numericUpDownThickLine";
            this.numericUpDownThickLine.Size = new System.Drawing.Size(58, 20);
            this.numericUpDownThickLine.TabIndex = 2;
            this.numericUpDownThickLine.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownThickLine.ValueChanged += new System.EventHandler(this.numericUpDownThickLine_ValueChanged);
            // 
            // lineButton
            // 
            this.lineButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lineButton.Location = new System.Drawing.Point(3, 23);
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(76, 33);
            this.lineButton.TabIndex = 0;
            this.lineButton.Text = "line";
            this.lineButton.UseVisualStyleBackColor = true;
            this.lineButton.Click += new System.EventHandler(this.lineButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(836, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.cleanToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.optionsToolStripMenuItem.Text = "options";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.saveToolStripMenuItem.Text = "save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.loadToolStripMenuItem.Text = "load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // cleanToolStripMenuItem
            // 
            this.cleanToolStripMenuItem.Name = "cleanToolStripMenuItem";
            this.cleanToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.cleanToolStripMenuItem.Text = "clean";
            this.cleanToolStripMenuItem.Click += new System.EventHandler(this.cleanToolStripMenuItem_Click);
            // 
            // drawingBox
            // 
            this.drawingBox.Location = new System.Drawing.Point(3, -5);
            this.drawingBox.Name = "drawingBox";
            this.drawingBox.Size = new System.Drawing.Size(833, 431);
            this.drawingBox.TabIndex = 0;
            this.drawingBox.TabStop = false;
            this.drawingBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawingBox_MouseDown);
            // 
            // insidePoligonsTextBox
            // 
            this.insidePoligonsTextBox.Location = new System.Drawing.Point(781, 31);
            this.insidePoligonsTextBox.Name = "insidePoligonsTextBox";
            this.insidePoligonsTextBox.Size = new System.Drawing.Size(52, 20);
            this.insidePoligonsTextBox.TabIndex = 19;
            // 
            // insidePoligonsButton
            // 
            this.insidePoligonsButton.Location = new System.Drawing.Point(736, 62);
            this.insidePoligonsButton.Name = "insidePoligonsButton";
            this.insidePoligonsButton.Size = new System.Drawing.Size(88, 33);
            this.insidePoligonsButton.TabIndex = 20;
            this.insidePoligonsButton.Text = "inside poligons";
            this.insidePoligonsButton.UseVisualStyleBackColor = true;
            this.insidePoligonsButton.Click += new System.EventHandler(this.insidePoligonsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 532);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThickLine)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox drawingBox;
        private System.Windows.Forms.Button lineButton;
        private System.Windows.Forms.NumericUpDown numericUpDownThickLine;
        private System.Windows.Forms.Button circleButton;
        private System.Windows.Forms.Button editElementsButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ComboBox colorsComboBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanToolStripMenuItem;
        private System.Windows.Forms.Button aaLineButton;
        private System.Windows.Forms.Button aaCircleButton;
        private System.Windows.Forms.Button bezierCurveButton;
        private System.Windows.Forms.Button createPolygonButton;
        private System.Windows.Forms.Button addVertexButton;
        private System.Windows.Forms.Button addEdgeButton;
        private System.Windows.Forms.Button rectangleButton;
        private System.Windows.Forms.Button rectangleClipButton;
        private System.Windows.Forms.Button fillPolygonButton;
        private System.Windows.Forms.Button fillWithImageButton;
        private System.Windows.Forms.Button loadImageToFillButton;
        private System.Windows.Forms.TextBox insidePoligonsTextBox;
        private System.Windows.Forms.Button insidePoligonsButton;
    }
}

