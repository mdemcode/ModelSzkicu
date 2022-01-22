using System;

namespace ModelSzkicu {
    public abstract class Linia : ElementRysunku {

        public Punkt StartPoint { get; set; }
        public Punkt EndPoint { get; set; }
        public bool Pozioma => StartPoint.Y == EndPoint.Y;
        public bool Pionowa => StartPoint.X == EndPoint.X;

        public Linia(double x1, double y1, double x2, double y2) {
            StartPoint = x1 < x2 ? new (x1, y1) : new (x2, y2); // - startpoint zawsze po lewej
            EndPoint = x1 < x2 ? new (x2, y2) : new (x1, y1); // - endpoint zawsze po prawej
        }
        public Linia(Punkt startPoint, Punkt endPoint) : this(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y) {}
        //{
        //    StartPoint = startPoint.X < endPoint.X ? startPoint : endPoint; // - startpoint zawsze po lewej
        //    EndPoint = startPoint.X < endPoint.X ? endPoint : startPoint;
        //}
        
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

