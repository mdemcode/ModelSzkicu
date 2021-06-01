using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelSzkicu {
    public class LiniaKonturowa : Linia {

        public bool Ukryta { get; set; }
        public bool Przerwana => SkladowePrzerwanej.Any();
        public List<LiniaKonturowa> SkladowePrzerwanej = new();

        public LiniaKonturowa(double x1, double y1, double x2, double y2, bool ukryta) : base(x1, y1, x2, y2) {
            Ukryta = ukryta;
            if (ukryta) Grubosc *= 0.7;
        }

    }
}

