using System;
using System.Drawing;

namespace ModelSzkicu {
    public abstract class ElementRysunku {

        public Color Kolor { get; set; }
        public double Grubosc { get; set; } // nieistotna dla tekstów

        protected ElementRysunku() {
            Kolor = Color.Black;
            Grubosc = 1;
        }


    }
}

