using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Luk : ElementRysunku {

        public Punkt StartPoint {get; set; }
        public Punkt EndPoint { get; set; }
        public double R { get; set; } // dodatni: kierunek clockwise / ujemny: counterclockwise
        public bool Ukryty { get; set; }

        public Luk(double startX, double startY, double endX, double endY, double r, bool ukryty) {
            StartPoint = new (startX, startY);
            EndPoint = new (endX, endY);
            R = r;
            Ukryty = ukryty;
        }

        public override void Przesun(double dx, double dy) {
            StartPoint.Przesun(dx, dy);
            EndPoint.Przesun(dx, dy);
        }
        public override void Skaluj(double wspolczynnik) {
            StartPoint.Skaluj(wspolczynnik);
            EndPoint.Skaluj(wspolczynnik);
            R *= wspolczynnik;
        }
    }
}
