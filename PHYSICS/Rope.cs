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

        public Rope(float x, float y, int segments, int intervale)
        {
            points = new List<VPoint>();
            poles = new List<VPole>();
            for (int i=0; i < segments; i++)
            {
                points.Add(new VPoint((int)x + i * intervale, (int)y));
                points[i].instance = false;
                points[i].Radius = 4;
            }
            points[0].instance=true;

            for(int j = 0; j < points.Count-1; j++)
            {
                poles.Add(new VPole(points[j], points[j+1]));
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
            // Ensure that the index is within bounds
            if (index >= 0 && index < points.Count)
            {
                // If the point to be removed is the first point, update the instance flag of the new first point
                if (index == 0 && points.Count > 1)
                {
                    points[1].instance = true;
                }

                // Remove the point and any corresponding poles
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
