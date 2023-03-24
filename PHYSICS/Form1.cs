using PHYSICS.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace PHYSICS
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        VPoint mousePoint;
        VBody R;
        public bool mouse,dibujar;
        private Image background;
        bool defeat = false;
        public Form1()
        {
            InitializeComponent();      
            init();
        }


        public void init()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            R = new VBody();
            R.ropeList.Add(new Rope(new VPoint(250, 0), new VPoint(300, 100), 8));
            R.ropeList.Add(new Rope(new VPoint(pictureBox1.Width / 2, 0), new VPoint(pictureBox1.Width / 2, 100), 8));
            R.ropeList.Add(new Rope(new VPoint(600, 0), new VPoint(350, 120), 8));
            R.ropeList.Add(new Rope(new VPoint(450, 350), new VPoint(350, 120), 8));

            R.perso.Add(new Personaje(new VPoint(pictureBox1.Width / 2 + 150, 520), Images.Personaje1, 40));
            R.perso.Add(new Personaje(new VPoint(350, 100), Images.Dulce, 30));
            JoinRopes(R.perso[1].persona);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            mouse = true;
            background = Images.CutTheRopeBackground;
            g.DrawImage(background, new Point(0, 0));
            bool collision = false;

            for (int i = 0; i < R.ropeList.Count; i++)
            {
                R.ropeList[i].Render(g, pictureBox1.Width, pictureBox1.Height);
                
            }

            for (int i = 0; i < R.perso.Count; i++)
            {
                R.perso[i].Render(g, pictureBox1.Width, pictureBox1.Height);
            }

            // Check for collisions between Personaje objects
            for (int i = 0; i < R.perso.Count; i++)
            {
                for (int j = i + 1; j < R.perso.Count; j++)
                {
                    Personaje p1 = R.perso[i];
                    Personaje p2 = R.perso[j];
                    float distance = VPoint.Distance1(p1.persona, p2.persona);
                    if (distance < (p1.persona.diameter + p2.persona.diameter) / 2)
                    {
                        collision = true;
                        R.perso[0].textura = Images.Personaje2;
                        R.ropeList.Clear();
                        R.perso.RemoveAt(j);
                        j--;
                    }
                }
            }

            if (mouse == true)
            {
                for (int i = 0; i < R.ropeList.Count; i++)
                {
                    for (int j = 1; j < R.ropeList[i].points.Count; j++)
                    {
                        R.ropeList[i].points[j].instance = false;
                    }
                }
            }
            pictureBox1.Invalidate();
            if (collision)
            {
                DialogResult result = MessageBox.Show("You win! Do you want to restart the game?", "Game Over", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    init();
                }
                else
                {
                    Close();
                }
            }

            if (R.perso.Count > 1 && R.perso[1].persona.Pos.Y + R.perso[1].persona.diameter >= pictureBox1.Height)
            {
                
                defeat = true;
                
            }

            if (defeat)
            {
               
                R.ropeList.Clear();
                R.perso.RemoveAt(1);
                DialogResult result = MessageBox.Show("You Defeat! Do you want to restart the game?", "Game Over", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    init();
                }
                else
                {
                    Close();
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
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

        public void JoinRopes(VPoint newPoint)
        {
            dibujar = true;
            for(int i = 0; i < R.ropeList.Count; i++)
            {
                Rope NewRope= R.ropeList[i];

                VPoint lastPoint = NewRope.points[NewRope.points.Count-1 ];

                NewRope.points.Add(newPoint);
                NewRope.poles.Add(new VPole(lastPoint, newPoint));
            }
           
        }

    }
}
