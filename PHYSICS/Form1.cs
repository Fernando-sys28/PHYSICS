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
        int count;
        bool collision, defeat;
        bool l1,l2,l3;
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
            initEstrellas();
            R.ropeList.Add(new Rope(new VPoint(350, 50), new VPoint(450, 200), 10));
            R.ropeList.Add(new Rope(new VPoint(pictureBox1.Width / 2, 50), new VPoint(450, 200), 10));
            R.ropeList.Add(new Rope(new VPoint(700, 0), new VPoint(450, 200), 10));
            R.perso.Add(new Personaje(new VPoint(pictureBox1.Width / 2 + 100, 580), Images.Personaje1, 40));
            R.perso.Add(new Personaje(new VPoint(450, 200), Images.Dulce, 25));

           JoinRopes(R.perso[1].persona);

            R.estre.Add(new Personaje(new VPoint(500, 380), Images.estrella, 40));
            R.estre.Add(new Personaje(new VPoint(pictureBox1.Width / 2 + 100, 400), Images.estrella, 40));
            R.estre.Add(new Personaje(new VPoint(720, 500), Images.estrella, 40));
            
        }

        public void init2()
        {
           
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            R = new VBody();
            initEstrellas();
            for (int b = 0; b < 10; b++)
                R.spikeballs.Add(new Personaje(new VPoint(600 + b * 14, (int)(250)), Images.spike,20, true));

            for (int b = 0; b < 10; b++)
                R.spikeballs.Add(new Personaje(new VPoint(600 + b * 14, (int)(420)), Images.spike, 20, true));

            R.ropeList.Add(new Rope(new VPoint(400, 40), new VPoint(450, 150), 10));
            R.ropeList.Add(new Rope(new VPoint(pictureBox1.Width / 2, 40), new VPoint(450, 150), 10));
            R.ropeList.Add(new Rope(new VPoint(pictureBox1.Width / 2, 150), new VPoint(450, 150), 10));
            R.ropeList.Add(new Rope(new VPoint(pictureBox1.Width / 2, 270), new VPoint(450, 200), 8));

            R.perso.Add(new Personaje(new VPoint(pictureBox1.Width / 2+50, 570), Images.Personaje1, 40));
            R.perso.Add(new Personaje(new VPoint(480, 150), Images.Dulce, 25));
            JoinRopes(R.perso[1].persona);

            R.estre.Add(new Personaje(new VPoint(390, 320), Images.estrella, 40));
            R.estre.Add(new Personaje(new VPoint(500, 520), Images.estrella, 40));
            R.estre.Add(new Personaje(new VPoint(600, 500), Images.estrella, 40));
            
        }

        public void initEstrellas()
        {
            R.estrella.Add(new Estrellas(1000, 10, Images.image));
            R.estrella.Add(new Estrellas(1060, 10, Images.image));
            R.estrella.Add(new Estrellas(1120, 10, Images.image));
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            mouse = true;
            collision = false;
            background = Images.CutTheRopeBackground;
            g.DrawImage(background, new Point(0, 0));
            bool win = false;
             defeat = false;
            //dibujar ropes
            for (int i = 0; i < R.ropeList.Count; i++)
            {
                R.ropeList[i].Render(g, pictureBox1.Width, pictureBox1.Height);

            }

            //dibujar personajes
            for (int i = 0; i < R.perso.Count; i++)
            {
                R.perso[i].Render(g, pictureBox1.Width, pictureBox1.Height);
            }

            //dibujar estrellas VPoint
            for (int i = 0; i < R.estre.Count; i++)
            {
                R.estre[i].Render(g, pictureBox1.Width, pictureBox1.Height);
            }

            //dibujar marcador estrellas
            for (int i = 0; i < R.estrella.Count; i++)
            {
                R.estrella[i].Render(g, pictureBox1.Width, pictureBox1.Height);
            }

            //dibujar spikeballs
            for (int i = 0; i < R.spikeballs.Count; i++)
            {
                R.spikeballs[i].Render(g, pictureBox1.Width, pictureBox1.Height);
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
       
            win = CheckCollisionPersonajes(win);
            count = CheckCollision();
            CheckSpikeCollision();

            if (collision)
            {
                if (count==1)
                {
                    R.estrella[0].textura = Images.estrella;
                }
                if (count == 2)
                {
                    R.estrella[1].textura = Images.estrella;
                }
                if (count == 3)
                {
                    R.estrella[2].textura = Images.estrella;
                }
            }

            if (win)
            {
                R.perso[0].textura = Images.Personaje2;
                R.ropeList.Clear();
                DialogResult result = MessageBox.Show("You win! You grabbed "+count+ "/3 stars\nDo you want to restart the game?", "Game Over", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    init();
                    if (l1)
                    {
                        init();
                    }
                    if (l2)
                    {
                        init2();
                    }
                    count = 0;
                }
                else
                {
                    Close();
                }
            }

            if (R.perso.Count > 1 && R.perso[1].persona.Pos.Y + R.perso[1].persona.diameter-10 >= pictureBox1.Height)
            {
                for (int j = R.perso.Count - 1; j >= 0; j--)
                {
                    R.perso.RemoveAt(j);
                    R.ropeList.Clear();
                }
                defeat = true;             
            }

            if (defeat)
            {
                DialogResult result = MessageBox.Show("You Defeat! Do you want to restart the game?", "Game Over", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    init();
                    if (l1)
                    {
                        init();
                    }
                    if (l2)
                    {
                        init2();
                    }
                    count = 0;
                }
                else
                {
                    Close();
                }
            }

            pictureBox1.Invalidate();
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
                for (int j = 1; j < r.points.Count; j++)
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
            for (int i = 0; i < R.ropeList.Count; i++)
            {
                Rope NewRope = R.ropeList[i];
                VPoint lastPoint = NewRope.points[NewRope.points.Count -1];
                newPoint.check = true;
                NewRope.points.Add(newPoint);
                NewRope.poles.Add(new VPole(lastPoint, newPoint));         
            }
        }

        public bool CheckCollisionPersonajes(bool collision)
        {
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
                        R.perso.RemoveAt(j);
                        
                    }
                }
            }
            return collision;
        }

        public void CheckSpikeCollision()
        {
            for (int i = R.spikeballs.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < R.perso.Count; j++)
                {
                    float distance = VPoint.Distance1(R.spikeballs[i].persona, R.perso[j].persona);
                    if (distance < R.spikeballs[i].persona.radius + R.perso[j].persona.radius)
                    {
                        defeat = true;
                        R.perso.RemoveAt(j);
                    }
                }
            }
        }
        public int CheckCollision()
        {

            for (int i = 0; i < R.perso.Count; i++)
            {

                for (int j = 0; j < R.estre.Count; j++)
                {
                    Personaje p1 = R.perso[i];
                    Personaje p2 = R.estre[j];

                    float distance = VPoint.Distance1(p1.persona, p2.persona);
                    if (distance < (p1.persona.diameter + p2.persona.diameter) / 2)
                    {
                        collision = true;
                        count++;
                        R.estre.RemoveAt(j);
                    }
                }
            }
            return count;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            l1 = true;
            l2 = false;
            l3 = false;
            init();
            count = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            l1 = false;
            l2 = true;
            l3 = false;
            init2();
            count = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            l1 = false;
            l2 = false;
            l3 = true;
        }

        

    }
}
