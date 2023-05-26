using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Rasterization
{
    public partial class Form1 : Form
    {
        private string tool = "";
        private string tool_sub;
        private int X = -1;
        private int Y = -1;
        private Bitmap bmp_prev;
        private Bitmap bmp;
        private bool iterated;
        private int thickLineThickness = 1;
        private int width;
        private int height;
        private List<Line> lines = new List<Line>();
        private bool chosen_to_modify = false;
        private Line line_to_modify;
        private int side_to_modify;
        private bool was_in_edit = false;
        private List<Circle> circles = new List<Circle>();
        private string what_to_change;
        private Circle circle_to_modify;
        private string[] colors = { "Yellow", "Orange", "Red", "Green", "Blue", "Brown", "Black", "White", "Pink" };
        private Color color_chosen;
        private bool color_auto_switch;
        private List<Polygon> polygons = new List<Polygon>();
        private Polygon polygon_to_modify;
        private Vertex vertex_to_modify;
        private List<RectangleCustom> rectangles = new List<RectangleCustom>();
        private RectangleCustom rectangle_to_modify;
        private RectangleCustom rectangle_to_clip;
        private bool is_rectangle_to_clip;
        private string path_to_picture_to_fill;
        private bool is_picture_to_fill;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(drawingBox.Width, drawingBox.Height);
            bmp_prev = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            drawingBox.Image = bmp;
            width = drawingBox.Width;
            height = drawingBox.Height;
            foreach (string s in colors)
            {
                colorsComboBox.Items.Add(s);
            }
            colorsComboBox.SelectedItem = "Black";
            color_chosen = Color.Black;
            setButtonsColor();
            is_rectangle_to_clip = false;
            is_picture_to_fill = false;
        }

        private void setButtonsColor()
        {
            lineButton.BackColor = SystemColors.Control;
            aaLineButton.BackColor = SystemColors.Control;
            circleButton.BackColor = SystemColors.Control;
            aaCircleButton.BackColor = SystemColors.Control;
            editElementsButton.BackColor = SystemColors.Control;
            removeButton.BackColor = SystemColors.Control;
            bezierCurveButton.BackColor = SystemColors.Control;
            createPolygonButton.BackColor = SystemColors.Control;
            addVertexButton.BackColor = SystemColors.Control;
            addEdgeButton.BackColor = SystemColors.Control;
            rectangleButton.BackColor = SystemColors.Control;
            rectangleClipButton.BackColor = SystemColors.Control;
            fillPolygonButton.BackColor = SystemColors.Control;
            fillWithImageButton.BackColor = SystemColors.Control;
            loadImageToFillButton.BackColor = SystemColors.Control;
            insidePoligonsButton.BackColor = SystemColors.Control;
        }

        private void afterEditClean()
        {
            if (was_in_edit)
            {
                X = -1;
                Y = -1;
                this.redraw();
                was_in_edit = false;
                removeButton.Enabled = false;
                rectangleClipButton.Enabled = false;
                drawingBox.Image = bmp;
            }
        }
        private void checkClip()
        {
            if (is_rectangle_to_clip)
            {
                foreach (Line line in lines)
                {
                    LiangBarsky(line.x_start, line.y_start, line.x_end, line.y_end, rectangle_to_clip, line.thickness, line.is_aa);
                }
                foreach (Polygon polygon in polygons)
                {
                    foreach (Vertex vertex in polygon.vertices)
                    {
                        for (int i = 0; i < vertex.number_of_connections; i++)
                        {
                            LiangBarsky(vertex.x_pos, vertex.y_pos, vertex.x_pos_connections[i], vertex.y_pos_connections[i], rectangle_to_clip, polygon.thickness, 0);
                        }
                    }
                }
                drawingBox.Image = bmp;
            }
        }

        private void lineButton_Click(object sender, EventArgs e)
        {
            tool = "line";
            this.setButtonsColor();
            lineButton.BackColor = SystemColors.ActiveCaption;
            afterEditClean();
        }

        private void circleButton_Click(object sender, EventArgs e)
        {
            tool = "circle";
            this.setButtonsColor();
            circleButton.BackColor = SystemColors.ActiveCaption;
            afterEditClean();
        }
        private void colorsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            color_chosen = Color.FromName(cmb.Text);
            if (chosen_to_modify && !color_auto_switch)
            {
                if (what_to_change == "line")
                {
                    foreach (Line l in lines)
                    {
                        if (l.x_start == line_to_modify.x_start && l.y_start == line_to_modify.y_start &&
                            l.x_end == line_to_modify.x_end && l.y_end == line_to_modify.y_end &&
                            l.thickness == line_to_modify.thickness)
                        {
                            this.redraw();
                            chosen_to_modify = false;
                            l.color = color_chosen;
                            this.bitmapSelectToModify();
                            removeButton.Enabled = false;
                        }
                    }
                }
                if (what_to_change == "circle_pos" || what_to_change == "circle_radius")
                {
                    foreach (Circle c in circles)
                    {
                        if (circle_to_modify.x_pos - c.x_pos == 0 && circle_to_modify.y_pos - c.y_pos == 0)
                        {
                            this.redraw();
                            chosen_to_modify = false;
                            c.color = color_chosen;
                            this.bitmapSelectToModify();
                            removeButton.Enabled = false;
                        }
                    }
                }
                if (what_to_change == "move_vertex" || what_to_change == "add_edge" || what_to_change == "add_vertex")
                {
                    foreach(Polygon polygon in polygons)
                    {
                        if(polygon == polygon_to_modify)
                        {
                            polygon.color = color_chosen;
                            this.redraw();
                            return;
                        }
                    }
                }
            }
            if (color_auto_switch)
            {
                color_auto_switch = false;
            }
        }
        private void cleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lines.Clear();
            circles.Clear();
            bmp.Dispose();
            bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            bmp_prev.Dispose();
            bmp_prev = new Bitmap(1, 1);
            X = -1;
            Y = -1;
            chosen_to_modify = false;
            drawingBox.Image = bmp;
        }
        private void bitmapSelectToModify()
        {
            addVertexButton.BackColor = SystemColors.Control;
            addEdgeButton.BackColor = SystemColors.Control;
            editElementsButton.BackColor = SystemColors.ActiveCaption;
            rectangleClipButton.Enabled = false;
            tool_sub = "move_vertex";
            foreach (RectangleCustom rectangle in rectangles)
            {
                for (int i = -3; i <= 3; i++)
                {
                    for (int j = -3; j <= 3; j++)
                    {
                        bmp.SetPixel(rectangle.x_min + i, rectangle.y_min + j, Color.Blue);
                        bmp.SetPixel(rectangle.x_max + i, rectangle.y_max + j, Color.Blue);
                    }
                }
            }
            foreach (Line l in lines)
            {
                for (int i = -3; i <= 3; i++)
                {
                    for (int j = -3; j <= 3; j++)
                    {
                        bmp.SetPixel(l.x_start + i, l.y_start + j, Color.Red);
                        bmp.SetPixel(l.x_end + i, l.y_end + j, Color.Red);
                    }
                }
            }
            foreach (Polygon p in polygons)
            {
                foreach (Vertex v in p.vertices)
                {
                    for (int i = -3; i <= 3; i++)
                    {
                        for (int j = -3; j <= 3; j++)
                        {
                            bmp.SetPixel(v.x_pos + i, v.y_pos + j, Color.Green);
                        }
                    }
                }
            }
            foreach (Circle c in circles)
            {
                for (int i = -3; i <= 3; i++)
                {
                    for (int j = -3; j <= 3; j++)
                    {
                        if (c.x_pos + i > 0 && c.x_pos + i < width && c.y_pos + j > 0 && c.y_pos + j < height)
                        {
                            bmp.SetPixel(c.x_pos + i, c.y_pos + j, Color.Red);
                        }
                        if (c.x_pos - c.R + i > 0 && c.x_pos - c.R + i < width && c.y_pos + j > 0 && c.y_pos + j < height)
                        {
                            bmp.SetPixel(c.x_pos - c.R + i, c.y_pos + j, Color.Red);
                        }
                        if (c.x_pos + c.R + i > 0 && c.x_pos + c.R + i < width && c.y_pos + j > 0 && c.y_pos + j < height)
                        {
                            bmp.SetPixel(c.x_pos + c.R + i, c.y_pos + j, Color.Red);
                        }
                        if (c.x_pos + i > 0 && c.x_pos + i < width && c.y_pos + j + c.R > 0
                            && c.y_pos + j + c.R < height)
                        {
                            bmp.SetPixel(c.x_pos + i, c.y_pos + j + c.R, Color.Red);
                        }
                        if (c.x_pos + i > 0 && c.x_pos + i < width && c.y_pos + j - c.R > 0
                            && c.y_pos + j - c.R < height)
                        {
                            bmp.SetPixel(c.x_pos + i, c.y_pos + j - c.R, Color.Red);
                        }
                    }
                }
            }
            drawingBox.Image = bmp;
        }
        private void editElementsButton_Click(object sender, EventArgs e)
        {
            afterEditClean();
            chosen_to_modify = false;
            tool = "edit";
            this.setButtonsColor();
            editElementsButton.BackColor = SystemColors.ActiveCaption;
            this.bitmapSelectToModify();
            was_in_edit = true;
            addEdgeButton.Enabled = true;
            addVertexButton.Enabled = true;
        }

        private void outOfEdit()
        {
            addEdgeButton.Enabled = false;
            addVertexButton.Enabled = false;
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (what_to_change == "move_vertex")
            {
                int i = 0;
                foreach (Polygon p in polygons)
                {
                    if (p == polygon_to_modify)
                    {
                        p.color = Color.White;
                        drawPolygon(p);
                        polygons.RemoveAt(i);
                        break;
                    }
                    i++;
                }
            }
            if (what_to_change == "rectangle")
            {
                int i = 0;
                foreach (RectangleCustom rectangle in rectangles)
                {
                    if (rectangle.Equals(rectangle_to_modify))
                    {
                        if (is_rectangle_to_clip)
                        {
                            if (rectangle_to_modify.Equals(rectangle_to_clip))
                            {
                                is_rectangle_to_clip = false;
                            }
                        }
                        rectangles.RemoveAt(i);
                        break;
                    }
                    i++;
                }
            }
            if (what_to_change == "line")
            {
                if (line_to_modify.is_aa == 0)
                {
                    drawThickLine(line_to_modify.x_start, line_to_modify.y_start, line_to_modify.x_end,
                    line_to_modify.y_end, line_to_modify.thickness, Color.White);
                }
                if (line_to_modify.is_aa == 1)
                {
                    drawaaLine(line_to_modify.x_start, line_to_modify.y_start, line_to_modify.x_end,
                    line_to_modify.y_end, line_to_modify.thickness, Color.White);
                }
                bmp_prev.Dispose();
                bmp_prev = bmp.Clone(new Rectangle(0, 0, width, height), bmp.PixelFormat);
                int i = 0;
                foreach (Line l in lines)
                {
                    if (l.x_start == line_to_modify.x_start && l.y_start == line_to_modify.y_start &&
                        l.x_end == line_to_modify.x_end && l.y_end == line_to_modify.y_end &&
                        l.thickness == line_to_modify.thickness)
                    {
                        lines.RemoveAt(i);
                        break;
                    }
                    i++;
                }
            }
            if (what_to_change == "circle_pos" || what_to_change == "circle_radius")
            {
                if (circle_to_modify.is_aa == 0)
                {
                    drawCircle(circle_to_modify.x_pos, circle_to_modify.y_pos, circle_to_modify.R, Color.White);
                }
                if (circle_to_modify.is_aa == 1)
                {
                    drawaaCircle(circle_to_modify.x_pos, circle_to_modify.y_pos, circle_to_modify.R, Color.White);
                }
                bmp_prev.Dispose();
                bmp_prev = bmp.Clone(new Rectangle(0, 0, width, height), bmp.PixelFormat);
                int i = 0;
                foreach (Circle c in circles)
                {
                    if (circle_to_modify.x_pos - c.x_pos == 0 && circle_to_modify.y_pos - c.y_pos == 0)
                    {
                        circles.RemoveAt(i);
                        break;
                    }
                    i++;
                }
            }
            chosen_to_modify = false;
            removeButton.Enabled = false;
            this.redraw();
            this.bitmapSelectToModify();
        }

        private void numericUpDownThickLine_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown o = (NumericUpDown)sender;
            thickLineThickness = (int)o.Value;
            if (tool == "edit" && chosen_to_modify && what_to_change == "move_vertex")
            {
                foreach (Polygon polygon in polygons)
                {
                    if (polygon == polygon_to_modify)
                    {
                        polygon.thickness = thickLineThickness;
                        this.redraw();
                        foreach (Vertex vertex in polygon.vertices)
                        {
                            for (int i = -3; i <= 3; i++)
                            {
                                for (int j = -3; j <= 3; j++)
                                {
                                    bmp.SetPixel(vertex.x_pos + i, vertex.y_pos + j, Color.Green);
                                }
                            }
                        }
                        for (int i = -3; i <= 3; i++)
                        {
                            for (int j = -3; j <= 3; j++)
                            {
                                bmp.SetPixel(vertex_to_modify.x_pos + i, vertex_to_modify.y_pos + j, Color.Red);
                            }
                        }
                        break;
                    }
                }
            }
            if (tool == "edit" && chosen_to_modify && what_to_change == "line")
            {
                drawThickLine(line_to_modify.x_start, line_to_modify.y_start, line_to_modify.x_end, line_to_modify.y_end,
                    line_to_modify.thickness, Color.White);
                foreach (Line l in lines)
                {
                    if (l.x_start == line_to_modify.x_start && l.y_start == line_to_modify.y_start &&
                        l.x_end == line_to_modify.x_end && l.y_end == line_to_modify.y_end &&
                        l.thickness == line_to_modify.thickness)
                    {
                        l.thickness = thickLineThickness;
                    }
                }
                line_to_modify.thickness = thickLineThickness;
                this.redraw();
                if (side_to_modify == 0)
                {
                    for (int i = -3; i <= 3; i++)
                    {
                        for (int j = -3; j <= 3; j++)
                        {
                            bmp.SetPixel(line_to_modify.x_start + i, line_to_modify.y_start + j, Color.Red);
                        }
                    }
                }
                if (side_to_modify == 1)
                {
                    for (int i = -3; i <= 3; i++)
                    {
                        for (int j = -3; j <= 3; j++)
                        {
                            bmp.SetPixel(line_to_modify.x_end + i, line_to_modify.y_end + j, Color.Red);
                        }
                    }
                }
            }
        }

        private void drawThickLine(int x_start, int y_start, int x_end, int y_end, int thickness, Color col)
        {
            int dx = x_end - x_start;
            int dy = y_end - y_start;

            int d = dy - (dx / 2);
            int x = x_start, y = y_start;
            bmp.SetPixel(x, y, col);
            if (Math.Abs(dx) >= Math.Abs(dy))
            {
                if (dx >= 0 && dy >= 0)
                {
                    while (x < x_end)
                    {
                        x++;
                        if (d < 0)
                            d = d + dy;
                        else
                        {
                            d += (dy - dx);
                            y++;
                        }
                        bmp.SetPixel(x, y, col);
                        for (int i = 1; i <= thickness; i++)
                        {
                            if (y - i >= 0 && y + i <= bmp.Height)
                            {
                                bmp.SetPixel(x, y + i, col);
                                bmp.SetPixel(x, y - i, col);
                            }
                        }
                    }
                }
                if (dx >= 0 && dy < 0)
                {
                    dy = dy * -1;
                    while (x < x_end)
                    {
                        x++;
                        if (d < 0)
                            d = d + dy;
                        else
                        {
                            d += (dy - dx);
                            y--;
                        }
                        bmp.SetPixel(x, y, col);
                        for (int i = 1; i <= thickness; i++)
                        {
                            if (y - i >= 0 && y + i <= bmp.Height)
                            {
                                bmp.SetPixel(x, y + i, col);
                                bmp.SetPixel(x, y - i, col);
                            }
                        }
                    }
                    dy = dy * -1;
                }
                if (dx < 0 && dy >= 0)
                {
                    while (x > x_end)
                    {
                        x--;
                        if (d < 0)
                            d = d + dy;
                        else
                        {
                            d += (dy + dx);
                            y++;
                        }
                        bmp.SetPixel(x, y, col);
                        for (int i = 1; i <= thickness; i++)
                        {
                            if (y - i >= 0 && y + i <= bmp.Height)
                            {
                                bmp.SetPixel(x, y + i, col);
                                bmp.SetPixel(x, y - i, col);
                            }
                        }
                    }
                }
                if (dx < 0 && dy < 0)
                {
                    dy = dy * -1;
                    while (x > x_end)
                    {
                        x--;
                        if (d < 0)
                            d = d + dy;
                        else
                        {
                            d += (dy + dx);
                            y--;
                        }
                        bmp.SetPixel(x, y, col);
                        for (int i = 1; i <= thickness; i++)
                        {
                            if (y - i >= 0 && y + i <= bmp.Height)
                            {
                                bmp.SetPixel(x, y + i, col);
                                bmp.SetPixel(x, y - i, col);
                            }
                        }
                    }
                    dy = dy * -1;
                }
            }
            if (Math.Abs(dx) < Math.Abs(dy))
            {
                if (dx >= 0 && dy >= 0)
                {
                    while (y < y_end)
                    {
                        y++;
                        if (d < 0)
                            d = d + dx;
                        else
                        {
                            d += (dx - dy);
                            x++;
                        }
                        bmp.SetPixel(x, y, col);
                        for (int i = 1; i <= thickness; i++)
                        {
                            if (x - i >= 0 && x + i <= bmp.Width)
                            {
                                bmp.SetPixel(x + i, y, col);
                                bmp.SetPixel(x - i, y, col);
                            }
                        }
                    }
                }
                if (dx >= 0 && dy < 0)
                {
                    while (y > y_end)
                    {
                        y--;
                        if (d < 0)
                            d = d + dx;
                        else
                        {
                            d += (dx + dy);
                            x++;
                        }
                        bmp.SetPixel(x, y, col);
                        for (int i = 1; i <= thickness; i++)
                        {
                            if (x - i >= 0 && x + i <= bmp.Width)
                            {
                                bmp.SetPixel(x + i, y, col);
                                bmp.SetPixel(x - i, y, col);
                            }
                        }
                    }
                }
                if (dx < 0 && dy >= 0)
                {
                    while (y < y_end)
                    {
                        y++;
                        if (d < 0)
                            d = d - dx;
                        else
                        {
                            d += (-dx - dy);
                            x--;
                        }
                        bmp.SetPixel(x, y, col);
                        for (int i = 1; i <= thickness; i++)
                        {
                            if (x - i >= 0 && x + i <= bmp.Width)
                            {
                                bmp.SetPixel(x + i, y, col);
                                bmp.SetPixel(x - i, y, col);
                            }
                        }
                    }
                }
                if (dx < 0 && dy < 0)
                {
                    while (y > y_end)
                    {
                        y--;
                        if (d < 0)
                            d = d - dx;
                        else
                        {
                            d += (-dx + dy);
                            x--;
                        }
                        bmp.SetPixel(x, y, col);
                        for (int i = 1; i <= thickness; i++)
                        {
                            if (x - i >= 0 && x + i <= bmp.Width)
                            {
                                bmp.SetPixel(x + i, y, col);
                                bmp.SetPixel(x - i, y, col);
                            }
                        }
                    }
                }
            }
        }

        private void drawCircle(int x_pos, int y_pos, int R, Color col)
        {
            int d = (1 - R);
            int x = 0;
            int x_left = 0;
            int x_left_side = x - R;
            int x_right_side = x + R;
            int y = R;
            int y_top = y - 2 * R;
            int y_side_top = y - R;
            int y_side_bottom = y - R;
            if (x + x_pos > 0 && x + x_pos < width && y + y_pos > 0 && y + y_pos < height)
            {
                bmp.SetPixel(x + x_pos, y + y_pos, col);
            }
            if (x + x_pos > 0 && x + x_pos < width && y_top + y_pos > 0 && y_top + y_pos < height)
            {
                bmp.SetPixel(x + x_pos, y_top + y_pos, col);
            }
            if (x_left_side + x_pos > 0 && x_left_side + x_pos < width && y_side_top + y_pos > 0 && y_side_top + y_pos < height)
            {
                bmp.SetPixel(x_left_side + x_pos, y_side_top + y_pos, col);
            }
            if (x_right_side + x_pos > 0 && x_right_side + x_pos < width && y_side_top + y_pos > 0 && y_side_top + y_pos < height)
            {
                bmp.SetPixel(x_right_side + x_pos, y_side_top + y_pos, col);
            }
            while (y > x)
            {
                if (d < 0) //move to E
                    d += 2 * x + 3;
                else //move to SE
                {
                    d += (2 * x - 2 * y + 5);
                    y--;
                    y_top++;
                    x_left_side++;
                    x_right_side--;
                }
                x++;
                x_left--;
                y_side_top++;
                y_side_bottom--;
                if (x + x_pos > 0 && x + x_pos < width && y + y_pos > 0 && y + y_pos < height)
                {
                    bmp.SetPixel(x + x_pos, y + y_pos, col);
                }
                if (x_left + x_pos > 0 && x_left + x_pos < width && y + y_pos > 0 && y + y_pos < height)
                {
                    bmp.SetPixel(x_left + x_pos, y + y_pos, col);
                }
                if (x + x_pos > 0 && x + x_pos < width && y_top + y_pos > 0 && y_top + y_pos < height)
                {
                    bmp.SetPixel(x + x_pos, y_top + y_pos, col);
                }
                if (x_left + x_pos > 0 && x_left + x_pos < width && y_top + y_pos > 0 && y_top + y_pos < height)
                {
                    bmp.SetPixel(x_left + x_pos, y_top + y_pos, col);
                }
                if (x_left_side + x_pos > 0 && x_left_side + x_pos < width && y_side_top + y_pos > 0 && y_side_top + y_pos < height)
                {
                    bmp.SetPixel(x_left_side + x_pos, y_side_top + y_pos, col);
                }
                if (x_left_side + x_pos > 0 && x_left_side + x_pos < width && y_side_bottom + y_pos > 0 && y_side_bottom + y_pos < height)
                {
                    bmp.SetPixel(x_left_side + x_pos, y_side_bottom + y_pos, col);
                }
                if (x_right_side + x_pos > 0 && x_right_side + x_pos < width && y_side_top + y_pos > 0 && y_side_top + y_pos < height)
                {
                    bmp.SetPixel(x_right_side + x_pos, y_side_top + y_pos, col);
                }
                if (x_right_side + x_pos > 0 && x_right_side + x_pos < width && y_side_bottom + y_pos > 0 && y_side_bottom + y_pos < height)
                {
                    bmp.SetPixel(x_right_side + x_pos, y_side_bottom + y_pos, col);
                }
            }
            drawingBox.Image = bmp;
        }

        private double fPart(double number)
        {
            double res = number - Math.Floor(number);
            return res;
        }

        private double rfPart(double number)
        {
            double res = ((double)1 - number + Math.Floor(number));
            return res;
        }

        private void drawaaLine(int x_start, int y_start, int x_end, int y_end, int thickness, Color col)
        {
            bool steep = Math.Abs(y_end - y_start) > Math.Abs(x_end - x_start);

            if (steep)
            {
                int cpy = y_start;
                y_start = x_start;
                x_start = cpy;
                cpy = y_end;
                y_end = x_end;
                x_end = cpy;
            }
            if (x_start > x_end)
            {
                int cpy = x_end;
                x_end = x_start;
                x_start = cpy;
                cpy = y_end;
                y_end = y_start;
                y_start = cpy;
            }
            int temp = x_end;
            x_end = x_start;
            x_start = temp;
            int dx = x_end - x_start;
            int dy = y_end - y_start;
            double gradient = (double)Decimal.Divide(dy, dx);
            if (dx == 0)
            {
                gradient = 1;
            }

            int xpxl1 = x_start;
            int xpxl2 = x_end;
            double intersectY = y_end;

            if (steep)
            {
                int x;
                for (x = xpxl1; x >= xpxl2; x--)
                {
                    Color c1 = Color.FromArgb((int)Math.Floor(col.R * rfPart(x) + 255 * fPart(x)),
                        (int)Math.Floor(col.G * rfPart(x) + 255 * fPart(x)),
                        (int)Math.Floor(col.B * rfPart(x) + 255 * fPart(x)));
                    Color c2 = Color.FromArgb((int)Math.Floor(col.R * fPart(x) + 255 * rfPart(x)),
                        (int)Math.Floor(col.G * fPart(x) + 255 * rfPart(x)),
                        (int)Math.Floor(col.B * fPart(x) + 255 * rfPart(x)));
                    bmp.SetPixel((int)Math.Floor(intersectY), x, c1);
                    bmp.SetPixel((int)Math.Floor(intersectY) + 1, x, c2);
                    intersectY += gradient;
                }
            }
            else
            {
                int x;
                for (x = xpxl1; x >= xpxl2; x--)
                {
                    Color c1 = Color.FromArgb((int)Math.Floor((double)col.R * rfPart(intersectY) + (double)255 * fPart(intersectY)),
                        (int)Math.Floor((double)col.G * rfPart(intersectY) + (double)255 * fPart(intersectY)),
                        (int)Math.Floor((double)col.B * rfPart(intersectY) + (double)255 * fPart(intersectY)));
                    Color c2 = Color.FromArgb((int)Math.Floor(col.R * fPart(intersectY) + 255 * rfPart(intersectY)),
                        (int)Math.Floor(col.G * fPart(intersectY) + 255 * rfPart(intersectY)),
                        (int)Math.Floor(col.B * fPart(intersectY) + 255 * rfPart(intersectY)));
                    bmp.SetPixel(x, (int)Math.Floor(intersectY), c1);
                    bmp.SetPixel(x, (int)Math.Floor(intersectY) + 1, c2);
                    intersectY += gradient;
                }
            }
            drawingBox.Image = bmp;
        }

        private double D(double R, double y)
        {
            return Math.Ceiling(Math.Sqrt(R * R - y * y)) - Math.Sqrt(R * R - y * y);
        }
        private double D_inv(double R, double y)
        {
            return (double)1 - D(R, y);
        }
        private void drawaaCircle(int x_pos, int y_pos, int R, Color col)
        {
            int x = R;
            int x_bottom_right = 0;
            int x_bottom_left = 0;
            int y_bottom_right = R;
            int y = 0;
            int y_side_top = 0;
            if (x + x_pos > 0 && x + x_pos < width && y + y_pos > 0 && y + y_pos < height)
            {
                bmp.SetPixel(x + x_pos, y + y_pos, col);
            }
            if (-x + x_pos > 0 && -x + x_pos < width && y + y_pos > 0 && y + y_pos < height)
            {
                bmp.SetPixel(-x + x_pos, y + y_pos, col);
            }
            if (x_bottom_left + x_pos > 0 && x_bottom_left + x_pos < width && y_bottom_right + y_pos > 0 && y_bottom_right + y_pos < height)
            {
                bmp.SetPixel(x_bottom_left + x_pos, y_bottom_right + y_pos, col);
            }
            if (x_bottom_left + x_pos > 0 && x_bottom_left + x_pos < width && -y_bottom_right + y_pos > 0 && -y_bottom_right + y_pos < height)
            {
                bmp.SetPixel(x_bottom_left + x_pos, -y_bottom_right + y_pos, col);
            }
            while (x > y)
            {
                y++;
                x_bottom_right++;
                x_bottom_left--;
                y_side_top--;
                x = (int)Math.Ceiling(Math.Sqrt(R * R - y * y));
                y_bottom_right = (int)Math.Ceiling(Math.Sqrt(R * R - x_bottom_right * x_bottom_right));
                Color c1 = Color.FromArgb((int)Math.Floor(col.R * D(R, y) + 255 * D_inv(R, y)),
                    (int)Math.Floor(col.G * D(R, y) + 255 * D_inv(R, y)),
                    (int)Math.Floor(col.B * D(R, y) + 255 * D_inv(R, y)));
                Color c2 = Color.FromArgb((int)Math.Floor(col.R * D_inv(R, y) + 255 * D(R, y)),
                    (int)Math.Floor(col.G * D_inv(R, y) + 255 * D(R, y)),
                    (int)Math.Floor(col.B * D_inv(R, y) + 255 * D(R, y)));
                if (x + x_pos > 0 && x + x_pos < width && y + y_pos > 0 && y + y_pos < height)
                {
                    bmp.SetPixel(x + x_pos, y + y_pos, c2);
                    bmp.SetPixel(x - 1 + x_pos, y + y_pos, c1);
                }
                if (x + x_pos > 0 && x + x_pos < width && y_side_top + y_pos > 0 && y_side_top + y + y_pos < height)
                {
                    bmp.SetPixel(x + x_pos, y_side_top + y_pos, c2);
                    bmp.SetPixel(x - 1 + x_pos, y_side_top + y_pos, c1);
                }
                if (-x + x_pos > 0 && -x + x_pos < width && y + y_pos > 0 && y + y_pos < height)
                {
                    bmp.SetPixel(-x + x_pos, y + y_pos, c2);
                    bmp.SetPixel(-x + 1 + x_pos, y + y_pos, c1);
                }
                if (-x + x_pos > 0 && -x + x_pos < width && y_side_top + y_pos > 0 && y_side_top + y_pos < height)
                {
                    bmp.SetPixel(-x + x_pos, y_side_top + y_pos, c2);
                    bmp.SetPixel(-x + 1 + x_pos, y_side_top + y_pos, c1);
                }
                if (x_bottom_right + x_pos > 0 && x_bottom_right + x_pos < width && y_bottom_right + y_pos > 0 && y_bottom_right + y_pos < height)
                {
                    bmp.SetPixel(x_bottom_right + x_pos, y_bottom_right + y_pos, c2);
                    bmp.SetPixel(x_bottom_right + x_pos, y_bottom_right + y_pos - 1, c1);
                }
                if (x_bottom_left + x_pos > 0 && x_bottom_left + x_pos < width && y_bottom_right + y_pos > 0 && y_bottom_right + y_pos < height)
                {
                    bmp.SetPixel(x_bottom_left + x_pos, y_bottom_right + y_pos, c2);
                    bmp.SetPixel(x_bottom_left + x_pos, y_bottom_right + y_pos - 1, c1);
                }
                if (x_bottom_right + x_pos > 0 && x_bottom_right + x_pos < width && -y_bottom_right + y_pos > 0 && -y_bottom_right + y_pos < width)
                {
                    bmp.SetPixel(x_bottom_right + x_pos, -y_bottom_right + y_pos, c2);
                    bmp.SetPixel(x_bottom_right + x_pos, -y_bottom_right + y_pos + 1, c1);
                }
                if (x_bottom_left + x_pos > 0 && x_bottom_left + x_pos < width && -y_bottom_right + y_pos > 0 && -y_bottom_right + y_pos < height)
                {
                    bmp.SetPixel(x_bottom_left + x_pos, -y_bottom_right + y_pos, c2);
                    bmp.SetPixel(x_bottom_left + x_pos, -y_bottom_right + y_pos + 1, c1);
                }
            }
            drawingBox.Image = bmp;
        }

        private double Bernstain(int p1, int p2, int p3, int p4, double t)
        {
            return Math.Pow((1 - t), 3) * p1 + 3 * t * Math.Pow((1 - t), 2) * p2 + 3 * Math.Pow(t, 2) * (1 - t) * p3 + Math.Pow(t, 3) * p4;
        }

        private void drawBezierCurve()
        {
            List<double> t = new List<double>();
            for (double i = 0; i <= 1; i = i + 0.02)
            {
                t.Add(i);
            }
            int p3_x = 0;
            int p3_y = 0;
            int p4_x = 200;
            int p4_y = 0;
            int p2_x = 0;
            int p2_y = 200;
            int p1_x = 200;
            int p1_y = 200;
            int x_prev = -1;
            int y_prev = -1;
            int x = -1;
            int y = -1;
            foreach (double t_val in t)
            {
                x = (int)Math.Floor(Bernstain(p1_x, p2_x, p3_x, p4_x, t_val));
                y = (int)Math.Floor(Bernstain(p1_y, p2_y, p3_y, p4_y, t_val));
                bmp.SetPixel(x, y, Color.Black);
                if (x_prev != -1 && y_prev != -1)
                {
                    drawaaLine(x_prev, y_prev, x, y, 0, Color.Black);
                }
                x_prev = x;
                y_prev = y;
            }
            drawingBox.Image = bmp;
        }
        private double max(double[] arr, int n)
        {
            double m = 0;
            for (int i = 0; i < n; ++i)
            {
                if (arr[i] > m)
                {
                    m = arr[i];
                }
            }
            return m;
        }
        private double min(double[] arr, int n)
        {
            double m = 1;
            for (int i = 0; i < n; ++i)
            {
                if (arr[i] < m)
                {
                    m = arr[i];
                }
            }
            return m;
        }
        private void LiangBarsky(double p1_x, double p1_y, double p2_x, double p2_y, RectangleCustom clip, int thickness, int is_aa)
        {
            double p1 = -(p2_x - p1_x);
            double p2 = -p1;
            double p3 = -(p2_y - p1_y);
            double p4 = -p3;

            double q1 = p1_x - clip.x_min;
            double q2 = clip.x_max - p1_x;
            double q3 = p1_y - clip.y_min;
            double q4 = clip.y_max - p1_y;

            double[] posarr = new double[5];
            double[] negarr = new double[5];
            int posind = 1;
            int negind = 1;
            posarr[0] = 1;
            negarr[0] = 0;

            if (p1 != 0)
            {
                double r1 = q1 / p1;
                double r2 = q2 / p2;
                if (p1 < 0)
                {
                    negarr[negind++] = r1;
                    posarr[posind++] = r2;
                }
                else
                {
                    negarr[negind++] = r2;
                    posarr[posind++] = r1;
                }
            }

            if (p3 != 0)
            {
                double r3 = q3 / p3;
                double r4 = q4 / p4;
                if (p3 < 0)
                {
                    negarr[negind++] = r3;
                    posarr[posind++] = r4;
                }
                else
                {
                    negarr[negind++] = r4;
                    posarr[posind++] = r3;
                }
            }

            double xn1, yn1, xn2, yn2;
            double rn1, rn2;
            rn1 = max(negarr, negind);
            rn2 = min(posarr, posind);

            if (rn1 > rn2)
            {
                // line is outside of the clipping window
                return;
            }

            xn1 = p1_x + p2 * rn1;
            yn1 = p1_y + p4 * rn1;

            xn2 = p1_x + p2 * rn2;
            yn2 = p1_y + p4 * rn2;

            if (is_aa == 0)
            {
                this.drawThickLine((int)xn1, (int)yn1, (int)xn2, (int)yn2, thickness, Color.Orange);
            }
            if (is_aa == 1)
            {
                this.drawaaLine((int)xn1, (int)yn1, (int)xn2, (int)yn2, thickness, Color.Orange);
            }
        }
        public int findEquationAndX(int x1, int y1, int x2, int y2, int y)
        {
            if(x1 != x2)
            {
                double y1_y2 = y1 - y2;
                double x1_x2 = x1 - x2;
                double a = y1_y2 / x1_x2;
                double b = y1 - (a * x1);
                double y_b = y - b;
                int result = (int)Math.Floor(y_b / a);
                return result;
            }
            else
            {
                return x1;
            }
        }
        private void fillPolygonHandle(Polygon polygon)
        {
            if(polygon.is_filled_with_color)
            {
                fillPolygon(polygon, false);
            }
            else
            {
                Bitmap picture = (Bitmap)Image.FromFile(polygon.path_to_picture);
                polygon.sortByX();
                int x_min = polygon.vertices[0].x_pos;
                int x_max = polygon.vertices[polygon.number_of_vertices - 1].x_pos;
                int width = x_max - x_min;
                polygon.sortByY();
                int y_min = polygon.vertices[0].y_pos;
                int y_max = polygon.vertices[polygon.number_of_vertices - 1].y_pos;
                int height = y_max - y_min;
                picture.SetResolution(width, height);
                using (Graphics gr = Graphics.FromImage(picture))
                {
                    gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.DrawImage(picture, new Rectangle(0, 0, width, height));
                }
                fillPolygon(polygon, true, picture);
            }
        }
        private void fillPolygon(Polygon polygon, bool with_picture, Bitmap picture = null)
        {
            polygon.sortByY();
            int y_min = polygon.vertices[0].y_pos;
            int y = y_min + 1;
            int y_max = polygon.vertices[polygon.number_of_vertices - 1].y_pos;
            List<int> vertices_y_smaller = new List<int>();
            List<int> vertices_y_smaller_connected_to = new List<int>();
            List<int> x_intersections = new List<int>();
            while (y < y_max)
            {
                vertices_y_smaller.Clear();
                vertices_y_smaller_connected_to.Clear();
                x_intersections.Clear();
                for (int i = 0; i < polygon.number_of_vertices; i++)
                {
                    if (polygon.vertices[i].y_pos <= y)
                    {
                        for (int j = 0; j < polygon.vertices[i].number_of_connections; j++)
                        {
                            if (polygon.vertices[i].y_pos_connections[j] > y)
                            {
                                vertices_y_smaller.Add(i);
                                vertices_y_smaller_connected_to.Add(j);
                            }
                        }
                    }
                }
                for (int i = 0; i < vertices_y_smaller.Count; i++)
                {
                    x_intersections.Add(findEquationAndX(polygon.vertices[vertices_y_smaller[i]].x_pos,
                        polygon.vertices[vertices_y_smaller[i]].y_pos,
                        polygon.vertices[vertices_y_smaller[i]].x_pos_connections[vertices_y_smaller_connected_to[i]],
                        polygon.vertices[vertices_y_smaller[i]].y_pos_connections[vertices_y_smaller_connected_to[i]],
                        y));
                }
                x_intersections.Sort();
                if (x_intersections.Count % 2 == 0)
                {
                    for (int i = 0; i < x_intersections.Count; i += 2)
                    {
                        if(!with_picture)
                        {
                            this.drawThickLine(x_intersections[i], y, x_intersections[i + 1], y, 1, polygon.color_fill);
                        }
                        if (with_picture)
                        {
                            int x_current = x_intersections[i];
                            polygon.sortByX();
                            int height = picture.Height;
                            int width = picture.Width;
                            int x_min = polygon.vertices[0].x_pos;
                            int x_max = polygon.vertices[polygon.number_of_vertices - 1].x_pos;
                            while (x_current <= x_intersections[i + 1])
                            {
                                bmp.SetPixel(x_current, y, picture.GetPixel(x_current - x_min, y - y_min));
                                x_current++;
                            }
                            
                        }
                    }
                }
                y++;
            }
        }
        private void redraw()
        {
            bmp.Dispose();
            bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            foreach (Line line in lines)
            {
                if (line.is_aa == 0)
                {
                    drawThickLine(line.x_start, line.y_start, line.x_end, line.y_end, line.thickness, line.color);
                }
                if (line.is_aa == 1)
                {
                    drawaaLine(line.x_start, line.y_start, line.x_end, line.y_end, line.thickness, line.color);
                }
            }
            foreach (RectangleCustom rectangle in rectangles)
            {
                drawRectangle(rectangle);
            }
            foreach (Circle circle in circles)
            {
                if (circle.is_aa == 0)
                {
                    drawCircle(circle.x_pos, circle.y_pos, circle.R, circle.color);
                }
                if (circle.is_aa == 1)
                {
                    drawaaCircle(circle.x_pos, circle.y_pos, circle.R, circle.color);
                }
            }
            foreach (Polygon polygon in polygons)
            {
                drawPolygon(polygon);
            }
            checkClip();
            drawingBox.Image = bmp;
        }
        private void drawPolygon(Polygon polygon)
        {
            if (polygon.is_filled_with_color || polygon.is_filled_with_picture)
            {
                fillPolygonHandle(polygon);
            }
            foreach (Vertex vertex in polygon.vertices)
            {
                for (int i = 0; i < vertex.number_of_connections; i++)
                {
                    this.drawThickLine(vertex.x_pos, vertex.y_pos, vertex.x_pos_connections[i], vertex.y_pos_connections[i], polygon.thickness, polygon.color);
                }
            }
        }
        private void drawRectangle(RectangleCustom rectangle)
        {
            foreach (Vertex vertex in rectangle.vertices)
            {
                for (int i = 0; i < 1; i++)
                {
                    this.drawThickLine(vertex.x_pos, vertex.y_pos, vertex.x_pos_connections[i], vertex.y_pos_connections[i], rectangle.thickness, rectangle.color);
                }
            }
        }
        private void drawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            iterated = false;
            if(tool == "inside_poligons")
            {
                int count = 0;
                List<int> vertices_y_smaller_con_y_larger = new List<int>();
                List<int> connections = new List<int>();
                bool left;
                bool right;
                foreach (Polygon polygon in polygons)
                {
                    vertices_y_smaller_con_y_larger.Clear();
                    connections.Clear();
                    left = false;
                    right = false;
                    for(int j = 0; j < polygon.vertices.Count; j++)
                    {
                        for (int i = 0; i < polygon.vertices[j].number_of_connections; i++)
                        {
                            if(polygon.vertices[j].y_pos < e.Y && polygon.vertices[j].y_pos_connections[i] > e.Y)
                            {
                                vertices_y_smaller_con_y_larger.Add(j);
                                connections.Add(i);
                            }
                        }
                    }
                    for(int i = 0; i < vertices_y_smaller_con_y_larger.Count; i++)
                    {
                        int x = findEquationAndX(polygon.vertices[vertices_y_smaller_con_y_larger[i]].x_pos, 
                            polygon.vertices[vertices_y_smaller_con_y_larger[i]].y_pos,
                            polygon.vertices[vertices_y_smaller_con_y_larger[i]].x_pos_connections[connections[i]],
                            polygon.vertices[vertices_y_smaller_con_y_larger[i]].y_pos_connections[connections[i]],
                            e.Y);
                        if (x < e.X)
                        {
                            left = true;
                        }
                        if (x > e.X)
                        {
                            right = true;
                        }
                    }
                    if(left && right)
                    {
                        count++;
                    }
                }
                insidePoligonsTextBox.Text = count.ToString();
                return;
            }
            if (tool == "create_polygon")
            {
                int i = 0;
                foreach (Line l in lines)
                {
                    if (l.is_aa == 0)
                    {
                        if ((int)Math.Abs(e.X - l.x_start) <= 3 && (int)Math.Abs(e.Y - l.y_start) <= 3)
                        {
                            Polygon p = new Polygon(thickLineThickness);
                            p.addVertex(new Vertex(l.x_start, l.y_start));
                            p.addVertex(new Vertex(l.x_end, l.y_end));
                            p.addEdge(l.x_start, l.y_start, l.x_end, l.y_end);
                            lines.RemoveAt(i);
                            polygons.Add(p);
                            this.redraw();
                            return;
                        }
                        if ((int)Math.Abs(e.X - l.x_end) <= 3 && (int)Math.Abs(e.Y - l.y_end) <= 3)
                        {
                            Polygon p = new Polygon(thickLineThickness);
                            p.addVertex(new Vertex(l.x_start, l.y_start));
                            p.addVertex(new Vertex(l.x_end, l.y_end));
                            p.addEdge(l.x_start, l.y_start, l.x_end, l.y_end);
                            lines.RemoveAt(i);
                            polygons.Add(p);
                            this.redraw();
                            return;
                        }
                    }
                    i++;
                }
            }
            if (X == -1 && Y == -1 && !iterated && (tool == "line" || tool == "circle" || tool == "aaline" || tool == "aacircle" || tool == "bezier_curve" || tool == "rectangle"))
            {
                X = e.X;
                Y = e.Y;
                iterated = true;
            }
            if (tool == "line")
            {
                if (X != -1 && Y != -1 && !iterated)
                {
                    lines.Add(new Line(X, Y, e.X, e.Y, thickLineThickness, color_chosen, 0));
                    X = -1;
                    Y = -1;
                    iterated = true;
                    this.redraw();
                }
            }
            if (tool == "aaline")
            {
                if (X != -1 && Y != -1 && !iterated)
                {
                    lines.Add(new Line(X, Y, e.X, e.Y, 0, color_chosen, 1));
                    X = -1;
                    Y = -1;
                    iterated = true;
                    this.redraw();
                }
            }
            if (tool == "circle")
            {
                if (X != -1 && Y != -1 && !iterated)
                {
                    int R = (int)Math.Sqrt(Math.Pow(X - e.X, 2) + Math.Pow(Y - e.Y, 2));
                    circles.Add(new Circle(X, Y, R, color_chosen, 0));
                    X = -1;
                    Y = -1;
                    iterated = true;
                    this.redraw();
                }
            }
            if (tool == "aacircle")
            {
                if (X != -1 && Y != -1 && !iterated)
                {
                    int R = (int)Math.Sqrt(Math.Pow(X - e.X, 2) + Math.Pow(Y - e.Y, 2));
                    circles.Add(new Circle(X, Y, R, color_chosen, 1));
                    X = -1;
                    Y = -1;
                    iterated = true;
                    this.redraw();
                }
            }
            if (tool == "bezier_curve")
            {
                if (X != -1 && Y != -1 && !iterated)
                {
                    this.drawBezierCurve();
                    X = -1;
                    Y = -1;
                    iterated = true;
                    this.redraw();
                }
            }
            if (tool == "rectangle")
            {
                if (X != -1 && Y != -1 && !iterated)
                {
                    RectangleCustom rectangleCustom = new RectangleCustom(X, Y, e.X, e.Y);
                    rectangles.Add(rectangleCustom);
                    X = -1;
                    Y = -1;
                    iterated = true;
                    this.redraw();
                }
            }
            if (tool == "edit")
            {
                if (!chosen_to_modify && !iterated)
                {
                    if (tool_sub == "add_vertex")
                    {
                        foreach (Polygon p in polygons)
                        {
                            foreach (Vertex v in p.vertices)
                            {
                                if ((int)Math.Abs(e.X - v.x_pos) <= 3 && (int)Math.Abs(e.Y - v.y_pos) <= 3)
                                {
                                    this.redraw();
                                    for (int i = -3; i <= 3; i++)
                                    {
                                        for (int j = -3; j <= 3; j++)
                                        {
                                            bmp.SetPixel(v.x_pos + i, v.y_pos + j, Color.Red);
                                        }
                                    }
                                    drawingBox.Image = bmp;
                                    chosen_to_modify = true;
                                    polygon_to_modify = p;
                                    what_to_change = "add_vertex";
                                    vertex_to_modify = v;
                                    return;
                                }
                            }
                        }

                    }
                    if (tool_sub == "move_vertex" || tool_sub == "add_edge")
                    {
                        foreach (Polygon p in polygons)
                        {
                            foreach (Vertex v in p.vertices)
                            {
                                if ((int)Math.Abs(e.X - v.x_pos) <= 3 && (int)Math.Abs(e.Y - v.y_pos) <= 3)
                                {
                                    this.redraw();
                                    for (int x = 0; x < p.number_of_vertices; x++)
                                    {
                                        for (int i = -3; i <= 3; i++)
                                        {
                                            for (int j = -3; j <= 3; j++)
                                            {
                                                bmp.SetPixel(p.vertices[x].x_pos + i, p.vertices[x].y_pos + j, Color.Green);
                                            }
                                        }
                                    }
                                    for (int i = -3; i <= 3; i++)
                                    {
                                        for (int j = -3; j <= 3; j++)
                                        {
                                            bmp.SetPixel(v.x_pos + i, v.y_pos + j, Color.Red);
                                        }
                                    }
                                    drawingBox.Image = bmp;
                                    chosen_to_modify = true;
                                    polygon_to_modify = p;
                                    if (tool_sub == "add_edge")
                                    {
                                        what_to_change = "add_edge";
                                    }
                                    else
                                    {
                                        what_to_change = "move_vertex";
                                        removeButton.Enabled = true;
                                    }
                                    vertex_to_modify = v;
                                    return;
                                }
                            }
                        }
                    }
                    foreach (RectangleCustom rectangle in rectangles)
                    {
                        if ((int)Math.Abs(e.X - rectangle.x_min) <= 3 && (int)Math.Abs(e.Y - rectangle.y_min) <= 3)
                        {
                            this.redraw();
                            for (int i = -3; i <= 3; i++)
                            {
                                for (int j = -3; j <= 3; j++)
                                {
                                    bmp.SetPixel(rectangle.x_min + i, rectangle.y_min + j, Color.Red);
                                }
                            }
                            for (int i = -3; i <= 3; i++)
                            {
                                for (int j = -3; j <= 3; j++)
                                {
                                    bmp.SetPixel(rectangle.x_max + i, rectangle.y_max + j, Color.Blue);
                                }
                            }
                            drawingBox.Image = bmp;
                            chosen_to_modify = true;
                            rectangle_to_modify = new RectangleCustom(rectangle.x_min, rectangle.y_min, rectangle.x_max, rectangle.y_max);
                            what_to_change = "rectangle";
                            side_to_modify = 0;
                            removeButton.Enabled = true;
                            rectangleClipButton.Enabled = true;
                            return;

                        }
                        if ((int)Math.Abs(e.X - rectangle.x_max) <= 3 && (int)Math.Abs(e.Y - rectangle.y_max) <= 3)
                        {
                            this.redraw();
                            for (int i = -3; i <= 3; i++)
                            {
                                for (int j = -3; j <= 3; j++)
                                {
                                    bmp.SetPixel(rectangle.x_min + i, rectangle.y_min + j, Color.Blue);
                                }
                            }
                            for (int i = -3; i <= 3; i++)
                            {
                                for (int j = -3; j <= 3; j++)
                                {
                                    bmp.SetPixel(rectangle.x_max + i, rectangle.y_max + j, Color.Red);
                                }
                            }
                            drawingBox.Image = bmp;
                            chosen_to_modify = true;
                            rectangle_to_modify = new RectangleCustom(rectangle.x_min, rectangle.y_min, rectangle.x_max, rectangle.y_max);
                            what_to_change = "rectangle";
                            side_to_modify = 1;
                            removeButton.Enabled = true;
                            return;
                        }
                    }
                    foreach (Line l in lines)
                    {
                        if ((int)Math.Abs(e.X - l.x_start) <= 3 && (int)Math.Abs(e.Y - l.y_start) <= 3)
                        {
                            this.redraw();
                            for (int i = -3; i <= 3; i++)
                            {
                                for (int j = -3; j <= 3; j++)
                                {
                                    bmp.SetPixel(l.x_start + i, l.y_start + j, Color.Red);
                                }
                            }
                            drawingBox.Image = bmp;
                            chosen_to_modify = true;
                            side_to_modify = 0;
                            line_to_modify = new Line(l.x_start, l.y_start, l.x_end, l.y_end, l.thickness, l.color, l.is_aa);
                            removeButton.Enabled = true;
                            what_to_change = "line";
                            if (l.color != color_chosen)
                            {
                                color_auto_switch = true;
                            }
                            colorsComboBox.SelectedIndex = colorsComboBox.Items.IndexOf(l.color.Name);
                            return;
                        }
                        if ((int)Math.Abs(e.X - l.x_end) <= 3 && (int)Math.Abs(e.Y - l.y_end) <= 3)
                        {
                            this.redraw();
                            for (int i = -3; i <= 3; i++)
                            {
                                for (int j = -3; j <= 3; j++)
                                {
                                    bmp.SetPixel(l.x_end + i, l.y_end + j, Color.Red);
                                }
                            }
                            drawingBox.Image = bmp;
                            chosen_to_modify = true;
                            side_to_modify = 1;
                            line_to_modify = new Line(l.x_start, l.y_start, l.x_end, l.y_end, l.thickness, l.color, l.is_aa);
                            removeButton.Enabled = true;
                            what_to_change = "line";
                            if (l.color != color_chosen)
                            {
                                color_auto_switch = true;
                            }
                            colorsComboBox.SelectedIndex = colorsComboBox.Items.IndexOf(l.color.Name);
                            return;
                        }
                    }
                    foreach (Circle c in circles)
                    {
                        if ((int)Math.Abs(e.X - c.x_pos) <= 3 && (int)Math.Abs(e.Y - c.y_pos) <= 3)
                        {
                            this.redraw();
                            for (int i = -3; i <= 3; i++)
                            {
                                for (int j = -3; j <= 3; j++)
                                {
                                    if (c.x_pos + i > 0 && c.x_pos + i < width && c.y_pos + j > 0 && c.y_pos < height)
                                    {
                                        bmp.SetPixel(c.x_pos + i, c.y_pos + j, Color.Red);
                                    }
                                }
                            }
                            drawingBox.Image = bmp;
                            chosen_to_modify = true;
                            what_to_change = "circle_pos";
                            circle_to_modify = new Circle(c.x_pos, c.y_pos, c.R, c.color, c.is_aa);
                            removeButton.Enabled = true;
                            if (c.color != color_chosen)
                            {
                                color_auto_switch = true;
                            }
                            colorsComboBox.SelectedIndex = colorsComboBox.Items.IndexOf(c.color.Name);
                            return;
                        }
                        if (((int)Math.Abs(e.X - c.x_pos - c.R) <= 3 && (int)Math.Abs(e.Y - c.y_pos) <= 3) ||
                            ((int)Math.Abs(e.X - c.x_pos + c.R) <= 3 && (int)Math.Abs(e.Y - c.y_pos) <= 3) ||
                            ((int)Math.Abs(e.X - c.x_pos) <= 3 && (int)Math.Abs(e.Y - c.y_pos - c.R) <= 3) ||
                            ((int)Math.Abs(e.X - c.x_pos) <= 3 && (int)Math.Abs(e.Y - c.y_pos + c.R) <= 3))
                        {
                            this.redraw();
                            for (int i = -3; i <= 3; i++)
                            {
                                for (int j = -3; j <= 3; j++)
                                {
                                    if (c.x_pos - c.R + i > 0 && c.x_pos - c.R + i < width && c.y_pos + j > 0 && c.y_pos + j < height)
                                    {
                                        bmp.SetPixel(c.x_pos - c.R + i, c.y_pos + j, Color.Red);
                                    }
                                    if (c.x_pos + c.R + i > 0 && c.x_pos + c.R + i < width && c.y_pos + j > 0 && c.y_pos + j < height)
                                    {
                                        bmp.SetPixel(c.x_pos + c.R + i, c.y_pos + j, Color.Red);
                                    }
                                    if (c.x_pos + i > 0 && c.x_pos + i < width && c.y_pos + j + c.R > 0
                                        && c.y_pos + j + c.R < height)
                                    {
                                        bmp.SetPixel(c.x_pos + i, c.y_pos + j + c.R, Color.Red);
                                    }
                                    if (c.x_pos + i > 0 && c.x_pos + i < width && c.y_pos + j - c.R > 0
                                        && c.y_pos + j - c.R < height)
                                    {
                                        bmp.SetPixel(c.x_pos + i, c.y_pos + j - c.R, Color.Red);
                                    }
                                }
                            }
                            drawingBox.Image = bmp;
                            chosen_to_modify = true;
                            what_to_change = "circle_radius";
                            circle_to_modify = new Circle(c.x_pos, c.y_pos, c.R, c.color, c.is_aa);
                            removeButton.Enabled = true;
                            if (c.color != color_chosen)
                            {
                                color_auto_switch = true;
                            }
                            colorsComboBox.SelectedIndex = colorsComboBox.Items.IndexOf(c.color.Name);
                            return;
                        }
                        iterated = true;
                    }
                }
                if (chosen_to_modify && !iterated)
                {
                    if (what_to_change == "add_vertex")
                    {
                        foreach (Polygon p in polygons)
                        {
                            if (p == polygon_to_modify)
                            {
                                foreach (Vertex v in p.vertices)
                                {
                                    if (v.x_pos == vertex_to_modify.x_pos && v.y_pos == vertex_to_modify.y_pos)
                                    {
                                        v.addConnection(e.X, e.Y);
                                    }
                                }
                                Vertex vertex = new Vertex(e.X, e.Y);
                                vertex.addConnection(vertex_to_modify.x_pos, vertex_to_modify.y_pos);
                                p.addVertex(vertex);
                                this.redraw();
                                drawingBox.Image = bmp;
                                chosen_to_modify = false;
                                this.bitmapSelectToModify();
                            }
                        }
                    }
                    if (what_to_change == "add_edge")
                    {
                        foreach (Polygon p in polygons)
                        {
                            if (p == polygon_to_modify)
                            {
                                foreach (Vertex v in p.vertices)
                                {
                                    if ((int)Math.Abs(e.X - v.x_pos) <= 3 && (int)Math.Abs(e.Y - v.y_pos) <= 3)
                                    {
                                        v.addConnection(vertex_to_modify.x_pos, vertex_to_modify.y_pos);

                                        for (int i = 0; i < p.number_of_vertices; i++)
                                        {
                                            if (p.vertices[i].x_pos == vertex_to_modify.x_pos && p.vertices[i].y_pos == vertex_to_modify.y_pos)
                                            {
                                                p.vertices[i].addConnection(v.x_pos, v.y_pos);
                                            }
                                        }
                                        chosen_to_modify = false;
                                        this.redraw();
                                        this.bitmapSelectToModify();
                                    }
                                }
                            }
                        }
                    }
                    if (what_to_change == "move_vertex")
                    {
                        int x_pos = vertex_to_modify.x_pos;
                        int y_pos = vertex_to_modify.y_pos;
                        foreach (Polygon p in polygons)
                        {
                            if (p == polygon_to_modify)
                            {
                                foreach (Vertex vertex in p.vertices)
                                {
                                    if (vertex.removeConnection(x_pos, y_pos))
                                    {
                                        vertex.addConnection(e.X, e.Y);
                                    }
                                    if (vertex.x_pos == x_pos && vertex.y_pos == y_pos)
                                    {
                                        vertex.x_pos = e.X;
                                        vertex.y_pos = e.Y;
                                    }
                                }
                                chosen_to_modify = false;
                                this.redraw();
                                this.bitmapSelectToModify();
                            }
                        }
                    }
                    if (what_to_change == "rectangle")
                    {
                        if (side_to_modify == 0)
                        {
                            foreach (RectangleCustom rectangle in rectangles)
                            {
                                if (rectangle.Equals(rectangle_to_modify))
                                {
                                    rectangle.sortVertices();
                                    rectangle.move(e.X, e.Y, rectangle_to_modify.x_max, rectangle_to_modify.y_max);
                                    chosen_to_modify = false;
                                    this.removeButton.Enabled = false;
                                    if (is_rectangle_to_clip)
                                    {
                                        if (rectangle_to_modify.Equals(rectangle_to_clip))
                                        {
                                            rectangle_to_clip.move(rectangle.x_min, rectangle.y_min, rectangle.x_max, rectangle.y_max);
                                        }
                                    }
                                    this.redraw();
                                    this.bitmapSelectToModify();
                                    return;
                                }
                            }
                        }
                        if (side_to_modify == 1)
                        {
                            foreach (RectangleCustom rectangle in rectangles)
                            {
                                if (rectangle.Equals(rectangle_to_modify))
                                {
                                    rectangle.sortVertices();
                                    rectangle.move(rectangle_to_modify.x_min, rectangle_to_modify.y_min, e.X, e.Y);
                                    chosen_to_modify = false;
                                    this.removeButton.Enabled = false;
                                    if (rectangle_to_modify.Equals(rectangle_to_clip))
                                    {
                                        rectangle_to_clip.move(rectangle.x_min, rectangle.y_min, rectangle.x_max, rectangle.y_max);
                                    }
                                    this.redraw();
                                    this.bitmapSelectToModify();
                                    return;
                                }
                            }
                        }
                    }
                    if (what_to_change == "line")
                    {
                        foreach (Line l in lines)
                        {
                            if (l.x_start == line_to_modify.x_start && l.y_start == line_to_modify.y_start &&
                                l.x_end == line_to_modify.x_end && l.y_end == line_to_modify.y_end &&
                                l.thickness == line_to_modify.thickness)
                            {
                                if (l.is_aa == 0)
                                {
                                    if (side_to_modify == 0)
                                    {
                                        l.x_start = e.X;
                                        l.y_start = e.Y;
                                    }
                                    if (side_to_modify == 1)
                                    {
                                        l.x_end = e.X;
                                        l.y_end = e.Y;
                                    }
                                }
                                if (l.is_aa == 1)
                                {
                                    if (side_to_modify == 0)
                                    {
                                        l.x_start = e.X;
                                        l.y_start = e.Y;
                                    }
                                    if (side_to_modify == 1)
                                    {
                                        l.x_end = e.X;
                                        l.y_end = e.Y;
                                    }
                                }
                                chosen_to_modify = false;
                                this.redraw();
                                this.bitmapSelectToModify();
                                removeButton.Enabled = false;
                            }
                        }
                    }
                    if (what_to_change == "circle_pos")
                    {
                        foreach (Circle c in circles)
                        {
                            if (circle_to_modify.x_pos - c.x_pos == 0 && circle_to_modify.y_pos - c.y_pos == 0)
                            {
                                chosen_to_modify = false;
                                c.x_pos = e.X;
                                c.y_pos = e.Y;
                                this.redraw();
                                this.bitmapSelectToModify();
                                removeButton.Enabled = false;
                            }
                        }
                    }
                    if (what_to_change == "circle_radius")
                    {
                        foreach (Circle c in circles)
                        {
                            if (circle_to_modify.x_pos - c.x_pos == 0 && circle_to_modify.y_pos - c.y_pos == 0)
                            {
                                int R_new = (int)Math.Sqrt(Math.Pow(circle_to_modify.x_pos - e.X, 2) + Math.Pow(circle_to_modify.y_pos - e.Y, 2));
                                c.R = R_new;
                                chosen_to_modify = false;
                                this.redraw();
                                this.bitmapSelectToModify();
                                removeButton.Enabled = false;
                            }
                        }
                    }
                    iterated = true;
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Save vector image";
            save.CheckPathExists = true;
            save.DefaultExt = ".txt";
            save.Filter = "text file (*.txt)|*.TXT";
            if (save.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(save.FileName);
                string to_write = string.Empty;
                foreach (Line l in lines)
                {
                    to_write += "l:" + l.x_start.ToString() + ":" + l.y_start.ToString() + ":" + l.x_end.ToString() + ":" +
                        l.y_end.ToString() + ":" + l.thickness.ToString() + ":" + l.color.Name + ":" + l.is_aa.ToString() + "\n";
                }
                foreach (Circle c in circles)
                {
                    to_write += "c:" + c.x_pos.ToString() + ":" + c.y_pos.ToString() + ":" + c.R.ToString() + ":" + c.color.Name + ":" +
                        c.is_aa.ToString() + "\n";
                }
                using (FileStream fs = File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(to_write);
                    fs.Write(info, 0, info.Length);
                }
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cleanToolStripMenuItem_Click(sender, e);
            var dialog = new OpenFileDialog();
            dialog.Title = "Open image";
            dialog.Filter = "Text file (*.txt)|*.TXT";
            string contents = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(dialog.FileName);
                contents = File.ReadAllText(path);
            }
            string[] elements = contents.Split('\n');
            foreach (string element in elements)
            {
                string[] to_draw = element.Split(':');
                if (to_draw[0] == 'l'.ToString())
                {
                    lines.Add(new Line(int.Parse(to_draw[1]), int.Parse(to_draw[2]), int.Parse(to_draw[3]),
                        int.Parse(to_draw[4]), int.Parse(to_draw[5]), Color.FromName(to_draw[6]), int.Parse(to_draw[7])));
                }
                if (to_draw[0] == "c".ToString())
                {
                    circles.Add(new Circle(int.Parse(to_draw[1]), int.Parse(to_draw[2]), int.Parse(to_draw[3]), Color.FromName(to_draw[4]), int.Parse(to_draw[5])));
                }
            }
            foreach (Line l in lines)
            {
                drawThickLine(l.x_start, l.y_start, l.x_end, l.y_end, l.thickness, l.color);
            }
            foreach (Circle c in circles)
            {
                drawCircle(c.x_pos, c.y_pos, c.R, c.color);
            }
            drawingBox.Image = bmp;
        }

        private void aaLineButton_Click(object sender, EventArgs e)
        {
            tool = "aaline";
            outOfEdit();
            setButtonsColor();
            aaLineButton.BackColor = SystemColors.ActiveCaption;
            afterEditClean();
        }

        private void aaCircleButton_Click(object sender, EventArgs e)
        {
            tool = "aacircle";
            outOfEdit();
            setButtonsColor();
            aaCircleButton.BackColor = SystemColors.ActiveCaption;
            afterEditClean();
        }

        private void bezierCurveButton_Click(object sender, EventArgs e)
        {
            tool = "bezier_curve";
            outOfEdit();
            setButtonsColor();
            bezierCurveButton.BackColor = SystemColors.ActiveCaption;
            afterEditClean();
        }

        private void createPolygonButton_Click(object sender, EventArgs e)
        {
            afterEditClean();
            was_in_edit = true;
            this.redraw();
            foreach (Line l in lines)
            {
                if (l.is_aa == 0)
                {
                    for (int i = -3; i <= 3; i++)
                    {
                        for (int j = -3; j <= 3; j++)
                        {
                            bmp.SetPixel(l.x_start + i, l.y_start + j, Color.Red);
                            bmp.SetPixel(l.x_end + i, l.y_end + j, Color.Red);
                        }
                    }
                }
            }
            tool = "create_polygon";
            drawingBox.Image = bmp;
            outOfEdit();
            setButtonsColor();
            createPolygonButton.BackColor = SystemColors.ActiveCaption;
        }

        private void addVertexButton_Click(object sender, EventArgs e)
        {
            if (tool_sub != "add_vertex")
            {
                this.redraw();
                chosen_to_modify = false;
                bitmapSelectToModify();
            }
            tool_sub = "add_vertex";
            setButtonsColor();
            addVertexButton.BackColor = SystemColors.ActiveCaption;
        }

        private void addEdgeButton_Click(object sender, EventArgs e)
        {
            if (tool_sub != "add_edge")
            {
                redraw();
                chosen_to_modify = false;
                bitmapSelectToModify();
            }
            tool_sub = "add_edge";
            setButtonsColor();
            addEdgeButton.BackColor = SystemColors.ActiveCaption;
        }

        private void rectangleButton_Click(object sender, EventArgs e)
        {
            afterEditClean();
            tool = "rectangle";
            outOfEdit();
            setButtonsColor();
            rectangleButton.BackColor = SystemColors.ActiveCaption;

        }

        private void rectangleClipButton_Click(object sender, EventArgs e)
        {
            bool iterated = false;
            if (!is_rectangle_to_clip)
            {
                rectangle_to_clip = new RectangleCustom(rectangle_to_modify.x_min, rectangle_to_modify.y_min,
                    rectangle_to_modify.x_max, rectangle_to_modify.y_max);
                is_rectangle_to_clip = true;
                iterated = true;
            }
            if (is_rectangle_to_clip && !iterated)
            {
                if (rectangle_to_clip.Equals(rectangle_to_modify))
                {
                    is_rectangle_to_clip = false;
                }
            }
            this.redraw();
            setButtonsColor();
            chosen_to_modify = false;
            removeButton.Enabled = false;
            this.bitmapSelectToModify();
        }

        private void fillPolygonButton_Click(object sender, EventArgs e)
        {
            foreach (Polygon polygon in polygons)
            {
                if (polygon == polygon_to_modify)
                {
                    if (!polygon.is_filled_with_color)
                    {
                        polygon.is_filled_with_color = true;
                        polygon.is_filled_with_picture = false;
                        polygon.color_fill = color_chosen;
                    }
                    else
                    {
                        polygon.is_filled_with_color = false;
                    }
                }
            }
            this.redraw();
            this.bitmapSelectToModify();
        }

        private void loadImageToFillButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Open image to fill polygon";
            dialog.Filter = "Image file (*.jpg)|*.JPG";
            string contents = string.Empty;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path_to_picture_to_fill = Path.GetFullPath(dialog.FileName);
            }
            is_picture_to_fill = true;
        }

        private void fillWithImageButton_Click(object sender, EventArgs e)
        {
            foreach (Polygon polygon in polygons)
            {
                if (polygon == polygon_to_modify)
                {
                    if (polygon.is_filled_with_color)
                    {
                        polygon.is_filled_with_color = false;
                    }
                    if (!polygon.is_filled_with_picture && is_picture_to_fill)
                    {
                        polygon.is_filled_with_picture = true;
                        polygon.path_to_picture = (string)path_to_picture_to_fill.Clone();
                    }
                    else
                    {
                        polygon.is_filled_with_picture = false;
                    }
                }
            }
            chosen_to_modify = false;
            this.redraw();
            this.bitmapSelectToModify();
        }

        private void insidePoligonsButton_Click(object sender, EventArgs e)
        {
            tool = "inside_poligons";
        }
    }
}