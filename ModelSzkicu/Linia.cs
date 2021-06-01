using System;

namespace ModelSzkicu {
    public abstract class Linia : ElementRysunku {

        //public double X1 { get; set; }
        //public double Y1 { get; set; }
        //public double X2 { get; set; }
        //public double Y2 { get; set; }
        public Punkt StartPoint { get; set; }
        public Punkt EndPoint { get; set; }

        public Linia(double x1, double y1, double x2, double y2) {
            //X1 = x1;
            //X2 = x2;
            //Y1 = y1;
            //Y2 = y2;
            StartPoint = x1 < x2 ? new (x1, y1) : new (x2, y2); // - startpoint zawsze po lewej
            EndPoint = x1 < x2 ? new (x2, y2) : new (x1, y1); // - endpoint zawsze po prawej
        }
        
        public override void Przesun(double dx, double dy) {
            StartPoint.Przesun(dx, dy);
            EndPoint.Przesun(dx, dy);
        }
        public override void Skaluj(double wspolczynnik) {
            StartPoint.Skaluj(wspolczynnik);
            EndPoint.Skaluj(wspolczynnik);
        }
    }
}

