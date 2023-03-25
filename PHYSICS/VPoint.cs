using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PHYSICS
{
    public class VPoint
    {
        Vec2 pos, old, vel, gravity;
        public float Mass;
        public float radius, diameter, m, frict = 0.97f;
        float groundFriction = 0.7f;
        public Color c { get; set; }
        public SolidBrush brush { get; set; }
        public bool instance;
        public bool check=false;
        int id;
        public Vec2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public float Radius
        {
            get { return radius; }
            set { radius = value; diameter = radius + radius; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Vec2 GRAVITY
        {
            get { return gravity; }
            set { gravity = value; }
        }

        public static float Distance1(VPoint a, VPoint b)
        {
            return (float)Math.Sqrt(Math.Pow(b.pos.X - a.pos.X, 2) + Math.Pow(b.pos.Y - a.pos.Y, 2));
        }

        public VPoint(int x, int y)
        {
            Init(x, y, 0,0);
        }

        public VPoint(int x, int y,int id)
        {
            this.id = id;
            Init(x, y, 0, 0);
        }

        public VPoint(int x, int y, bool instance)
        {
            this.instance = instance;
            Init(x, y, 0, 0);
        }

        public VPoint(int x, int y,float vx, float vy)
        {
            Init(x, y, vx, vy);
        }
        public void Init(int x, int y, float vx, float vy)
        {
            pos = new Vec2(x, y);
            old = new Vec2(x, y);
            gravity = new Vec2(0, 1);
            gravity *= 0.65f;
            radius = 5f;
            vel=new Vec2(vx, vy);
            diameter = radius + radius;
            Mass = 4f;
            c= Color.OrangeRed;
            brush = new SolidBrush(c);

        }
        public void Update(int width, int height)
        {
            if (!instance)
            {
                vel = (pos - old) * frict;

                if (pos.Y >= height - radius && vel.MagSqr() > 0.000001)
                {
                    m = vel.Length();
                    vel /= m;
                    vel *= (m * groundFriction);
                }
                old = pos;
                pos += vel + gravity;
            }
        }
        public void Constraints(int width, int height)
        {
            if (pos.X > width - radius) { pos.X = width - radius; old.X = (pos.X + vel.X); }
            if (pos.X < radius) { pos.X = radius; old.X = (pos.X + vel.X); }
            if (pos.Y > height - radius) { pos.Y = height - radius; old.Y = (pos.Y + vel.Y); }
            if (pos.Y < radius) { pos.Y = radius; old.Y = (pos.Y + vel.Y); }
        }
        public void Render(Graphics g, int width, int height)
        {
            Update(width, height);
            Constraints(width, height);
            if (!check) {
                g.FillEllipse(brush, pos.X - radius, pos.Y - radius, diameter, diameter);
            }
            
        }
    }
}
