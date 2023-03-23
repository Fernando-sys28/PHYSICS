using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PHYSICS
{
    public class Rope
    {
        public List<VPoint> points;
        public List<VPole> poles;
        public Rope(VPoint start, VPoint end, int numSegments)
        {
            points = new List<VPoint>();
            poles = new List<VPole>();
            float segmentDistance = (end.Pos.X - start.Pos.X) / (numSegments - 1);

            for (int i = 0; i < numSegments; i++)
            {
                float x = start.Pos.X + i * segmentDistance;
                float y = start.Pos.Y + (end.Pos.Y - start.Pos.Y) * i / (numSegments - 1);
                points.Add(new VPoint((int)x, (int)y));
                points[i].instance = (i == 0 || i == numSegments - 1);
                points[i].Radius = 4;
            }

            for (int j = 0; j < points.Count - 1; j++)
            {
                poles.Add(new VPole(points[j], points[j + 1]));
            }
          
        }

        public void Render(Graphics g, int width, int height)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].Render(g, width, height);
            }

            for (int i = 0; i < poles.Count; i++)
            {
                poles[i].Render(g, width, height);
            }


        }

        public void RemovePoint(int index)
        {
            if (index >= 0 && index < points.Count)
            {

                if (index == 0 && points.Count > 1)
                {
                    points[1].instance = true;
                }
                else if (index == points.Count - 1 && points.Count > 1)
                {
                    points[points.Count - 2].instance = true;
                }

                points.RemoveAt(index);
                if (index < poles.Count)
                {
                    poles.RemoveAt(index);
                }
                if (index > 0 && index <= poles.Count)
                {
                    poles.RemoveAt(index - 1);
                }

            }
        }



    }
}
