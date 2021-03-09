using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ModelSzkicu {
    public class Szkic {

        public List<Widok> Widoki { get; set; }

        public Szkic() {
            Widoki = new();
        }

        public void DodajWidok(TypWidoku typ) {
            if (Widoki.Any(w => w.Typ == typ)) {
                Debug.Print("Zdublowany widok!!!");
            }
            Widoki.Add(new Widok(typ));
        }

    }
}

