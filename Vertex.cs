using System;
using System.Collections.Generic;

namespace Rasterization
{
    public class Vertex
    {
        public Vertex(int x_pos, int y_pos)
        {
            this.x_pos = x_pos;
            this.y_pos = y_pos;
            this.x_pos_connections = new List<int>();
            this.y_pos_connections = new List<int>();
            this.number_of_connections = 0;
        }
        public int x_pos { get; set; }
        public int y_pos { get; set; }
        public List<int> x_pos_connections { get; set; }

        public List<int> y_pos_connections { get; set; }

        public int number_of_connections { get; set; }

        public void addConnection(int x_pos, int y_pos)
        {
            x_pos_connections.Add(x_pos);
            y_pos_connections.Add(y_pos);
            number_of_connections++;
        }

        public bool removeConnection(int x_to, int y_to)
        {
            for (int i = 0; i < this.number_of_connections; i++)
            {
                if ((int)Math.Abs(this.x_pos_connections[i] - x_to) <= 3 && (int)Math.Abs(this.y_pos_connections[i] - y_to) <= 3)
                {
                    this.x_pos_connections.RemoveAt(i);
                    this.y_pos_connections.RemoveAt(i);
                    number_of_connections--;
                    return true;
                }
            }
            return false;
        }
    }
}
