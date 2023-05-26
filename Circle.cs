using System.Drawing;

namespace Rasterization
{
    public class Circle
    {
        public Circle(int x_pos, int y_pos, int R, Color color, int is_aa)
        {
            this.x_pos = x_pos;
            this.y_pos = y_pos;
            this.R = R;
            this.color = color;
            this.is_aa = is_aa;
        }

        public int x_pos { get; set; }
        public int y_pos { get; set; }
        public int R { get; set; }
        public Color color { get; set; }
        public int is_aa { get; set; }
    }
}
