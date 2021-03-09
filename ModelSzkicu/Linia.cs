using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public abstract class Linia : ElementRysunku {

        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }

        public Linia(double x1, double x2, double y1, double y2) {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
        }

    }
}

