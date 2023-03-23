using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PHYSICS
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        VPoint[] points;
        Box b;
        Rope r1,r;
        VPoint mousePoint;
        bool click=false;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;

             points = new VPoint[]
            {
                new VPoint(50, 50),
                new VPoint(100, 100),
                new VPoint(150, 50),
                new VPoint(200, 100),
                new VPoint(250, 50),
            };

            b = new Box(new VPoint(0, 0), new VPoint(0, 100), new VPoint(100, 100), new VPoint(100, 0));
            r = new Rope(pictureBox1.Width / 2,0, 10,10);
            r1 = new Rope(pictureBox1.Width / 3, 150, 10, 10);


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            pictureBox1.Refresh(); // Add this line to erase the entire picture box
            g.Clear(Color.Black);
            foreach (VPoint v in points)
            {
                //v.Render(g, pictureBox1.Width, pictureBox1.Height);
            }

            //b.Render(g, pictureBox1.Width, pictureBox1.Height);
            r.Render(g, pictureBox1.Width, pictureBox1.Height);
            r1.Render(g, pictureBox1.Width, pictureBox1.Height);

            pictureBox1.Invalidate();

            if (click == true)
            {
                //mousePoint.Render(g, pictureBox1.Width, pictureBox1.Height);
            }
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            click = true;
            mousePoint = new VPoint(e.X, e.Y);

            float minDistance = 10;
            int index=-1;

            // Calcular la distancia entre el punto del mouse y cada punto en la cuerda
            for (int i = 0; i < r.points.Count; i++)
            {
                float distance = VPoint.Distance1(mousePoint, r.points[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    index = i;
                }
            }

            if (index >= 0)
            {
                r.RemovePoint(index);
            
            }
        }
    }
}
