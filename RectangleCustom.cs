using System.Collections.Generic;
using System.Drawing;

namespace Rasterization
{
    public class RectangleCustom
    {
        public RectangleCustom(int x_pos_1, int y_pos_1, int x_pos_2, int y_pos_2)
        {
            is_initialized = false;
            vertices = new List<Vertex>();
            move(x_pos_1, y_pos_1, x_pos_2, y_pos_2);
        }

        public void move(int x_pos_1, int y_pos_1, int x_pos_2, int y_pos_2)
        {
            if (is_initialized)
            {
                for (int i = 0; i < 4; i++)
                {
                    vertices.RemoveAt(0);
                }
            }
            Vertex v = new Vertex(x_pos_1, y_pos_1);
            v.addConnection(x_pos_2, y_pos_1);
            vertices.Add(v);
            v = new Vertex(x_pos_2, y_pos_1);
            v.addConnection(x_pos_2, y_pos_2);
            vertices.Add(v);
            v = new Vertex(x_pos_2, y_pos_2);
            v.addConnection(x_pos_1, y_pos_2);
            vertices.Add(v);
            v = new Vertex(x_pos_1, y_pos_2);
            v.addConnection(x_pos_1, y_pos_1);
            vertices.Add(v);
            is_initialized = true;
            if (x_pos_1 > x_pos_2)
            {
                x_max = x_pos_1;
                x_min = x_pos_2;
            }
            else
            {
                x_max = x_pos_2;
                x_min = x_pos_1;
            }
            if (y_pos_1 > y_pos_2)
            {
                y_max = y_pos_1;
                y_min = y_pos_2;
            }
            else
            {
                y_max = y_pos_2;
                y_min = y_pos_1;
            }
            color = Color.Black;
            thickness = 1;
        }

        public void sortVertices()
        {
            this.move(this.x_min, this.y_min, this.x_max, this.y_max);
        }

        public override bool Equals(object obj)
        {
            RectangleCustom to_compare = (RectangleCustom)obj;
            if (to_compare.x_min == this.x_min && to_compare.y_min == this.y_min &&
                to_compare.x_max == this.x_max && to_compare.y_max == this.y_max)
            {
                return true;
            }
            return false;
        }

        public List<Vertex> vertices;
        public int x_min { get; set; }
        public int x_max { get; set; }
        public int y_min { get; set; }
        public int y_max { get; set; }
        public Color color { get; set; }
        public int thickness { get; set; }
        private bool is_initialized;
    }
}
