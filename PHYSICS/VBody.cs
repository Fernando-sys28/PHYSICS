using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHYSICS
{
    public class VBody
    {
        public List<Rope> ropeList;
        public List<Personaje> perso;
        
        public VBody() {

            ropeList = new List<Rope>();
            perso= new List<Personaje>();
        }

    }
}
