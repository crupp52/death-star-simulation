using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathStar
{
    class Rohamosztagos
    {
        static int globalID = 1000;

        public string ID { get; private set; }
        public Munkas Felugyelve { get; private set; }

        public Rohamosztagos()
        {
            this.ID = "F" + globalID++;
            this.Felugyelve = null;
        }

        public void Felugyel(Munkas munkas)
        {
            this.Felugyelve = munkas;
            if (munkas != null)
            {
                this.Felugyelve.Figyelik = true;
            }
        }

        public void FelugyeletVege()
        {
            this.Felugyelve.Figyelik = false;
            this.Felugyelve = null;
        }

        public override string ToString()
        {
            if (Felugyelve != null)
            {
                return $"{this.ID}: {this.Felugyelve.Nev}";
            }
            else
            {
                return $"{this.ID}: szabad...";
            }
            
        }
    }
}
