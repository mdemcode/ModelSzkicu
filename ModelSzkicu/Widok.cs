using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Widok {

        //
        public TypWidoku Typ { get; set; }
        //
        public List<LiniaKonturowa> LinieKonturowe { get; set; }
        public List<Otwor> Otwory { get; set; }
        public List<Fasolka> Fasolki { get; set; }
        public List<Luk> Luki { get; set; }

        public Widok(TypWidoku typ) {
            Typ = typ;
            LinieKonturowe = new();

        }

    }
    
    public enum TypWidoku { o, v, u, h }
}