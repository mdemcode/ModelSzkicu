using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelSzkicu {
    public class Punkt {

        public double X {get; set;}
        public double Y {get; set;}

        public Punkt (double x, double y) {
            X = x;
            Y = y;
        }

        public void Przesun(double dx, double dy) {
            X += dx;
            Y += dy;
        }
        public void Skaluj(double wspolczynnik) {
            X *= wspolczynnik;
            Y *= wspolczynnik;
        }
    }
}
