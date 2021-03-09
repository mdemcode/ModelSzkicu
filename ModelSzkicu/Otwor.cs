using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Otwor : ElementRysunku {

        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Fi { get; set; }

        public Otwor(double centerX, double centerY, double fi) {
            CenterX = centerX;
            CenterY = centerY;
            Fi = fi;
        }
    }
}

