using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Widok {

        //
        public TypWidoku Typ { get; set; }
        //
        public List<LiniaKonturowa> LinieKonturowe { get; set; }
        // linie wymiarowe czy całe wymiary ???
        public List<Otwor> Otwory { get; set; }
        public List<Fasolka> Fasolki { get; set; }
        public List<Luk> Luki { get; set; }

        public Widok(TypWidoku typ) {
            Typ = typ;
            LinieKonturowe = new();
            Otwory = new();
            Fasolki = new();
            Luki = new();
        }

        public void DodajLinieKonturu(double x1, double x2, double y1, double y2, bool ukryta) {
            LinieKonturowe.Add(new(x1, x2, y1, y2, ukryta));
        }

    }
    
    public enum TypWidoku { o, v, u, h }
}