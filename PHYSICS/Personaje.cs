using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHYSICS
{
    public class Personaje
    {
        public Image textura;
        public VPoint persona;

        public Personaje(VPoint a, Image img)
        {
            persona = a;
            Init(img);
        }
        public void Init(Image img)
        {     
            textura= img;
        }

        public void Render(Graphics g, int width, int height) {

            g.DrawImage(textura, persona.Pos.X, persona.Pos.Y, textura.Width, textura.Height);
        }
    }
}
