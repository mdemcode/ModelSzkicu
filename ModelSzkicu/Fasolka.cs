using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Fasolka : Otwor {

        public double Dlugosc { get; set; } // dodatnia: fasolka pionowa / ujemna: fasolka pozioma (albo odrotnie !?!)

        public Fasolka(double centerX, double centerY, double fi, double dlugosc) : base(centerX, centerY, fi) {
            Dlugosc = dlugosc;
        }
        public Fasolka(Punkt centerPoint, double fi, double dlugosc) : base(centerPoint, fi) {
            Dlugosc = dlugosc;
        }
    }
}

