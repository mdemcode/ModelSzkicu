using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class LiniaKonturowa : Linia {

        public bool Ukryta { get; set; }

        public LiniaKonturowa(double x1, double x2, double y1, double y2, bool ukryta) : base(x1, x2, y1, y2) {
            Ukryta = ukryta;
            if (ukryta) Grubosc *= 0.7;
        }

    }
}

