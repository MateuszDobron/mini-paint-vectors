using System.Drawing;

namespace Rasterization
{
    public class Line
    {
        public Line(int x_start, int y_start, int x_end, int y_end, int thickness, Color color, int is_aa)
        {
            this.x_start = x_start;
            this.y_start = y_start;
            this.x_end = x_end;
            this.y_end = y_end;
            this.thickness = thickness;
            this.color = color;
            this.is_aa = is_aa;
        }

        public int x_start { get; set; }
        public int y_start { get; set; }
        public int x_end { get; set; }
        public int y_end { get; set; }
        public int thickness { get; set; }
        public Color color { get; set; }
        public int is_aa { get; set; }
    }
}
