using System.Drawing;

namespace ModelSzkicu{

    public abstract class ElementRysunku : IElementRysunku {

        public Color Kolor { get; set; }
        public double Grubosc { get; set; } // nieistotna dla tekstów (ewentualnie wielkość czcionki - do rozważenia)

        protected ElementRysunku() {
            Kolor = Color.Black;
            Grubosc = 1;
        }

        public abstract void Przesun(double dx, double dy);
        public abstract void Skaluj(double wspolczynnik);
    }

    public interface IElementRysunku {
        public Color Kolor { get; set; }
        public double Grubosc { get; set; }
        //
        void Przesun(double dx, double dy);
        void Skaluj(double wspolczynnik);
    }
}

