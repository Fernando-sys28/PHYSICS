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
        VPoint mousePoint;
        VBody R;
        public bool mouse;
        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            R= new VBody();

             points = new VPoint[]
            {
                new VPoint(50, 50),
                new VPoint(100, 100),
                new VPoint(150, 50),
                new VPoint(200, 100),
                new VPoint(250, 50),
            };

            b = new Box(new VPoint(0, 0), new VPoint(0, 100), new VPoint(100, 100), new VPoint(100, 0));
            R.ropeList.Add(new Rope(new VPoint(200,50), new VPoint(350, 120),5));           
            R.ropeList.Add(new Rope(new VPoint(500, 50), new VPoint(pictureBox1.Width / 2, 120), 5));
           

            R.perso.Add(new Personaje(new VPoint(pictureBox1.Width / 2+100, 320), Images.Personaje1));
            R.perso.Add(new Personaje(new VPoint(345, 100), Images.Dulce));

            Rope rope = R.ropeList[0];
            Rope rope2 = R.ropeList[1];

            // Obtener el último punto de la lista de puntos de la cuerda.
            VPoint lastPoint = rope.points[rope.points.Count - 1];
            VPoint lastPoint2 = rope2.points[rope2.points.Count - 1];

            // Crear un nuevo VPoint con las coordenadas del nuevo punto y agregarlo a la lista de puntos de la cuerda.
            R.perso[1].persona.instance = true;
            R.perso[1].persona.Radius = 4;
            rope.points.Add(R.perso[1].persona);
            rope2.points.Add(R.perso[1].persona);

            // Crear un nuevo VPole con el último punto existente y el nuevo punto agregado, y agregarlo a la lista de polos de la cuerda.
            VPole newPole = new VPole(lastPoint, R.perso[1].persona);
            VPole newPole2 = new VPole(lastPoint2, R.perso[1].persona);
            rope.poles.Add(newPole);
           rope.poles.Add(newPole2);

            

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            /*foreach (VPoint v in points)
            {
                //v.Render(g, pictureBox1.Width, pictureBox1.Height);
            }

            //b.Render(g, pictureBox1.Width, pictureBox1.Height);*/

            pictureBox1.Refresh();
            g.Clear(Color.Black);
            
            for (int i = 0; i < R.ropeList.Count; i++)
            {
                R.ropeList[i].Render(g, pictureBox1.Width, pictureBox1.Height);
                
            }

            for (int i = 0; i < R.perso.Count; i++)
            {
                R.perso[i].Render(g, pictureBox1.Width, pictureBox1.Height);
            }

            if (mouse == true)
            {
                for (int i = 0; i < R.ropeList.Count; i++)
                {
                    R.ropeList[i].points[R.ropeList[i].points.Count - 1].instance = false;
                    R.ropeList[i].points[R.ropeList[i].points.Count - 2].instance = false;
                }
            }
            pictureBox1.Invalidate();

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            mouse = true;
            mousePoint = new VPoint(e.X, e.Y);

            float minDistance = 10;
            int ropeIndex = -1;
            int pointIndex = -1;

            for (int i = 0; i < R.ropeList.Count; i++)
            {
                Rope r = R.ropeList[i];
                for (int j = 0; j < r.points.Count; j++)
                {
                    float distance = VPoint.Distance1(mousePoint, r.points[j]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        ropeIndex = i;
                        pointIndex = j;
                    }
                    
                }
            }

            if (ropeIndex >= 0 && pointIndex >= 0)
            {
                R.ropeList[ropeIndex].RemovePoint(pointIndex);
            }
        }
    }
}
