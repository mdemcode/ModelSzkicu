using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Luk : ElementRysunku {

        public Punkt StartPoint {get; set; }
        public Punkt EndPoint { get; set; }
        public Punkt Ognisko => PoliczOgnisko();
        public double R { get; set; } // dodatni: kierunek clockwise / ujemny: counterclockwise
        public bool Ukryty { get; set; }

        public Luk(double startX, double startY, double endX, double endY, double r, bool ukryty) {
            StartPoint = startX < endX ? new (startX, startY) : new (endX, endY); // StartPoint zawsze po lewej
            EndPoint = startX < endX ? new (endX, endY) :  new (startX, startY);
            R = r;
            Ukryty = ukryty;
        }
        public Luk(Punkt startPoint, Punkt endPoint, double r, bool ukryty) : this(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y, r, ukryty) {}
        //{
        //    StartPoint = startPoint.X < endPoint.X ? startPoint : endPoint; // - startpoint zawsze po lewej
        //    EndPoint = startPoint.X < endPoint.X ? endPoint : startPoint;
        //    R = r;
        //    Ukryty = ukryty;
        //}

        public override void Przesun(double dx, double dy) {
            StartPoint.Przesun(dx, dy);
            EndPoint.Przesun(dx, dy);
        }
        public override void Skaluj(double wspolczynnik) {
            StartPoint.Skaluj(wspolczynnik);
            EndPoint.Skaluj(wspolczynnik);
            R *= wspolczynnik;
        }
        private Punkt PoliczOgnisko() {
            double x = 0;
            double y = 0;
            if (StartPoint.Y < EndPoint.Y && R > 0) { // LG
                x = EndPoint.X;
                y = StartPoint.Y;
            }
            if (StartPoint.Y > EndPoint.Y && R > 0) { // PG
                x = StartPoint.X;
                y = EndPoint.Y;
            }
            if (StartPoint.Y > EndPoint.Y && R < 0) { // LD
                x = EndPoint.X;
                y = StartPoint.Y;
            }
            if (StartPoint.Y < EndPoint.Y && R < 0) { // PD
                x = StartPoint.X;
                y = EndPoint.Y;
            }
            return new Punkt(x, y);
        }
    }
}
