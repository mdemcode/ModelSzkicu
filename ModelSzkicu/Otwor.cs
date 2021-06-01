using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Otwor : ElementRysunku {

        public Punkt CenterPoint {get; set; }
        public double Fi { get; set; }

        public Otwor(double centerX, double centerY, double fi) {
            CenterPoint = new (centerX, centerY);
            Fi = fi;
        }

        public override void Przesun(double dx, double dy) {
            CenterPoint.Przesun(dx, dy);
        }
        public override void Skaluj(double wspolczynnik) {
            CenterPoint.Skaluj(wspolczynnik);
            Fi *= wspolczynnik;
        }
    }
}

