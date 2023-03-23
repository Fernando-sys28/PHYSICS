﻿using System;
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
        float radius, diameter, m, frict = 0.97f;
        float groundFriction = 0.7f;
        Color c;
        int vx, vy;
        SolidBrush brush;
        public bool instance;
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

        public static float Distance1(VPoint a, VPoint b)
        {
            return (float)Math.Sqrt(Math.Pow(b.pos.X - a.pos.X, 2) + Math.Pow(b.pos.Y - a.pos.Y, 2));
        }

        public VPoint(int x, int y)
        {
            Init(x, y, 0,0, Color.OrangeRed);
        }

        public VPoint(int x, int y, Image img)
        {
            Personaje(x, y, 0, 0, img);
        }
        public void Personaje(int x, int y, int vx, int vy, Image C)
        {
            pos = new Vec2(x, y);
            old = new Vec2(x, y);
            gravity = new Vec2(0, 1);
            radius = 5f;
            vel = new Vec2(vx, vy);
            diameter = radius + radius;
            Mass = 4f;
            c = Color.OrangeRed;
            brush = new SolidBrush(c);

        }

        public void Init(int x, int y, int vx, int vy, Color C)
        {
            pos = new Vec2(x, y);
            old = new Vec2(x, y);
            gravity = new Vec2(0, 1);
            radius= 5f;
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
            if (pos.X > width - radius) pos.X = width - radius;
            if (pos.X < radius) pos.X = radius;
            if (pos.Y > height - radius) pos.Y = height - radius;
            if (pos.Y < radius) pos.Y = radius;
        }
        public void Render(Graphics g, int width, int height)
        {
            Update(width, height);
            Constraints(width, height);

            g.FillEllipse(brush, pos.X - radius, pos.Y - radius, diameter, diameter);

        }
    }
}