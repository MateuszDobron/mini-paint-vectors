using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Rasterization
{
    public class Polygon
    {
        public Polygon(int thickness)
        {
            this.vertices = new List<Vertex>();
            this.number_of_vertices = 0;
            this.color = Color.Black;
            this.thickness = thickness;
            this.is_filled_with_color = false;
            this.is_filled_with_picture = false;
            this.path_to_picture = "";
        }
        public List<Vertex> vertices { get; set; }
        public int number_of_vertices { get; set; }
        public Color color { get; set; }
        public int thickness { get; set; }
        public bool is_filled_with_color { get; set; }
        public Color color_fill { get; set; }
        public bool is_filled_with_picture { get; set; }
        public string path_to_picture { get; set; }

        public void addVertex(Vertex vertex)
        {
            vertices.Add(vertex);
            number_of_vertices++;
        }

        public void addEdge(int x_pos_1, int y_pos_1, int x_pos_2, int y_pos_2)
        {
            foreach (Vertex v in vertices)
            {
                if (v.x_pos == x_pos_1 && v.y_pos == y_pos_1)
                {
                    v.addConnection(x_pos_2, y_pos_2);
                }
                if (v.x_pos == x_pos_2 && v.y_pos == y_pos_2)
                {
                    v.addConnection(x_pos_1, y_pos_1);
                }
            }
        }
        public void sortByY()
        {
            vertices = vertices.OrderBy(x => x.y_pos).ToList();
        }
        public void sortByX()
        {
            vertices = vertices.OrderBy(x =>x.x_pos).ToList();
        }
    }
}
