using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHYSICS
{
    public class VPole
    {
        float stiffness, damp, length, tot, m1, m2, dis, diff;
        Vec2 dxy, offset;
        Pen brush;
        VPoint a, b;

        public VPole(VPoint a, VPoint b)
        {
            this.a = a;
            this.b = b;
            stiffness = 2f;
            damp = 0.43f;
            length =a.Pos.Distance(b.Pos);
            brush = new Pen(Color.SaddleBrown,3);
            tot = a.Mass + b.Mass;
            m1 = b.Mass / tot;
            m2 = a.Mass / tot;
        }
        public void Update()
        {
            Vec2 dxy = b.Pos - a.Pos;
            float dis = dxy.Length();
            float diff = stiffness * (length - dis) / dis;
            Vec2 offset = dxy * diff * damp;
            if (!a.instance) {a.Pos -= offset * m1; }
            if (!b.instance) {b.Pos += offset * m2; }
        }

        public void Render(Graphics g, int width, int height)
        {
            Update();
            g.DrawLine(brush, a.Pos.X, a.Pos.Y, b.Pos.X, b.Pos.Y);
        }
    }
}
