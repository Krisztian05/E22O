using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cseveges
{
    internal class Beszelgetes
    {
        public DateTime Kezdet { get; set; }
        public DateTime Veg { get; set; }
        public string Kezdemenyezo { get; set; }
        public string Fogado { get; set; }

        public Beszelgetes(DateTime kezdet, DateTime veg, string kezdemenyezo, string fogado)
        {
            Kezdet = kezdet;
            Veg = veg;
            Kezdemenyezo = kezdemenyezo;
            Fogado = fogado;
        }
    }
}
