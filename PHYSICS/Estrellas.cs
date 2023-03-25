using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHYSICS
{
    public class Estrellas
    {
        public Image textura;
        int x,y;
        public Estrellas(int x, int y, Image img)
        {
            this.x = x;
            this.y = y;
            this.textura = img;
        }

        public void Render(Graphics g, int width, int height)
        {
            g.DrawImage(textura, x, y, textura.Width, textura.Height);
        }
    }
}
