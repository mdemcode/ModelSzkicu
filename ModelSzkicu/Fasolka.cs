using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Fasolka : Otwor {

        public double Wysokosc { get; set; }
        public double Szerokosc { get; set; }
        public Kontur Kontur { get; set; }

        public Fasolka(double centerX, double centerY, double fi, double wysokosc, double szerokosc) : base(centerX, centerY, fi) {
            Wysokosc = wysokosc;
            Szerokosc = szerokosc;
            UtworzKontur();
        }
        public Fasolka(Punkt centerPoint, double fi, double wysokosc, double szerokosc) 
            : this(centerPoint.X, centerPoint.Y, fi, wysokosc, szerokosc) {}

        private void UtworzKontur() {
            Kontur = new(Kontur.TypKonturu.Wewn);
            // linia pozioma górna:
            if (Szerokosc > 0.1) {
                Kontur.ElementyKonturu.Add(new LiniaKonturowa(
                    CenterPoint.X, 
                    CenterPoint.Y + Wysokosc + (Fi/2), 
                    CenterPoint.X + Szerokosc,
                    CenterPoint.Y + Wysokosc + (Fi/2),
                    false
                ));
            }
            // łuk prawy górny:
            Kontur.ElementyKonturu.Add(new Luk(
                CenterPoint.X + Szerokosc,
                CenterPoint.Y + Wysokosc + (Fi/2),
                CenterPoint.X + Szerokosc + (Fi/2),
                CenterPoint.Y + Wysokosc,
                (Fi/2),
                false
            ));
            // linia pionowa prawa:
            if (Wysokosc > 0.1) {
                Kontur.ElementyKonturu.Add(new LiniaKonturowa(
                    CenterPoint.X + Szerokosc + (Fi/2), 
                    CenterPoint.Y + Wysokosc, 
                    CenterPoint.X + Szerokosc + (Fi/2),
                    CenterPoint.Y,
                    false
                ));
            }
            // łuk prawy dolny:
            Kontur.ElementyKonturu.Add(new Luk(
                CenterPoint.X + Szerokosc,
                CenterPoint.Y - (Fi/2),
                CenterPoint.X + Szerokosc + (Fi/2),
                CenterPoint.Y,
                -(Fi/2),
                false
            ));
            // linia pozioma dolna:
            if (Szerokosc > 0.1) {
                Kontur.ElementyKonturu.Add(new LiniaKonturowa(
                    CenterPoint.X + Szerokosc, 
                    CenterPoint.Y - (Fi/2), 
                    CenterPoint.X,
                    CenterPoint.Y - (Fi/2),
                    false
                ));
            }
            // łuk lewy dolny
            Kontur.ElementyKonturu.Add(new Luk(
                CenterPoint.X - (Fi/2),
                CenterPoint.Y,
                CenterPoint.X,
                CenterPoint.Y - (Fi/2),
                -(Fi/2),
                false
            ));
            // linia pionowa lewa:
            if (Wysokosc > 0.1) {
                Kontur.ElementyKonturu.Add(new LiniaKonturowa(
                    CenterPoint.X - (Fi/2), 
                    CenterPoint.Y, 
                    CenterPoint.X - (Fi/2),
                    CenterPoint.Y + Wysokosc,
                    false
                ));
            }
            // łuk górny lewy:
            Kontur.ElementyKonturu.Add(new Luk(
                CenterPoint.X - (Fi/2),
                CenterPoint.Y + Wysokosc,
                CenterPoint.X,
                CenterPoint.Y + Wysokosc + (Fi/2),
                (Fi/2),
                false
            ));
        }
        public override void Przesun(double dx, double dy) {
            CenterPoint.Przesun(dx, dy);
            Kontur.Przesun(dx, dy);
        }
        public override void Skaluj(double wspolczynnik) {
            CenterPoint.Skaluj(wspolczynnik);
            Fi *= wspolczynnik;
            Wysokosc *= wspolczynnik;
            Szerokosc *= wspolczynnik;
            Kontur.Skaluj(wspolczynnik);
        }

    }
}

