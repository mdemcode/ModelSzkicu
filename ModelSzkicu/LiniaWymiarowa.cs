using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSzkicu {

    public class LiniaWymiarowa : Linia {

        public enum TypLiniiWymiarowej { glowna, pomocnicza, ukosnik }
        
        public TypLiniiWymiarowej Typ { get; set; }

        public LiniaWymiarowa(double x1, double y1, double x2, double y2, TypLiniiWymiarowej typ) : base(x1, y1, x2, y2) {
            Grubosc *= 0.5;
            Typ = typ;
        }
        public LiniaWymiarowa(Punkt pkt1, Punkt pkt2, TypLiniiWymiarowej typ) : this(pkt1.X, pkt1.Y, pkt2.X, pkt2.Y, typ) {}
        //    : base(pkt1, pkt2) { // kolejnosc punktow nie ma znaczenia
        //    Grubosc *= 0.5;
        //    Typ = typ;
        //}
    }
}
