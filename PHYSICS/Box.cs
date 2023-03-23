using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PHYSICS
{
    public class Box
    {
        VPoint a, b, c, d;
        VPole p1, p2, p3, p4, p5, p6;
        PointF[] pts; 
        public Box(VPoint a, VPoint b, VPoint c, VPoint d) {

            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;

            pts= new PointF[4];

            p1 = new VPole(a, b);
            p2 = new VPole(b, c);
            p3 = new VPole(c, d);
            p4 = new VPole(d, a);
            p5 = new VPole(b, d);
            p6 = new VPole(c, a);

        }

        public void Render(Graphics g, int width, int height)
        {

            a.Render(g, width, height);
            b.Render(g, width, height);
            c.Render(g, width, height);
            d.Render(g, width, height);

            pts[0] = new PointF(a.Pos.X, a.Pos.Y);
            pts[1] = new PointF(b.Pos.X, b.Pos.Y);
            pts[2] = new PointF(c.Pos.X, c.Pos.Y);
            pts[3] = new PointF(d.Pos.X, d.Pos.Y);

           // g.FillPolygon(Brushes.AliceBlue, pts);

            p1.Render(g, width, height);
            p2.Render(g, width, height);
            p3.Render(g, width, height);
            p4.Render(g, width, height);
            p5.Render(g, width, height);
            p6.Render(g, width, height);

        }
    }
}
