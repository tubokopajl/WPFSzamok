using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSzamok
{
    internal class KvizFeladat
    {
        public string Kerdes { get; private set; }

        public int Valasz  { get; private set; }

        public int Pont { get; private set; }

        public string Kategoria { get; private set; }


        public KvizFeladat(string sor1, string sor2)
        {
            //sor1=Kerdes
            Kerdes = sor1;
            //sor2=Valasz and Pont and Kategoria
            string[] adatok = sor2.Split(' ');
            Valasz = int.Parse(adatok[0]);
            Pont = int.Parse(adatok[1]);
            Kategoria = adatok[2];

        
        }
    }
}
