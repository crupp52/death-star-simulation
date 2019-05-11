using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DeathStar
{
    class Munkas
    {
        public string Nev { get; private set; }
        public int Tempo { get; private set; }
        public int Allapot { get; private set; }
        public bool Figyelik { get; set; }
        public bool Elkeszult { get { return this.Allapot == 100; } }

        public Munkas(string nev, int allapot, int tempo)
        {
            this.Nev = nev;
            this.Allapot = allapot;
            this.Tempo = tempo;
        }

        public static List<Munkas> GetMunkasok()
        {
            XDocument xdoc = XDocument.Load("deathstar.xml");
            return (from e in xdoc.Descendants("munkas")
                    select new Munkas(e.Attribute("nev").Value, int.Parse(e.Attribute("allapot").Value), int.Parse(e.Attribute("tempo").Value))).ToList();
        }

        public void Lep()
        {
            this.Allapot++;
            if (this.Figyelik)
            {
                Thread.Sleep(this.Tempo / 2);
            }
            else
            {
                Thread.Sleep(this.Tempo);
            }
        }

        public override string ToString()
        {
            return $"{this.Nev}: {this.Allapot}%";
        }
    }
}
