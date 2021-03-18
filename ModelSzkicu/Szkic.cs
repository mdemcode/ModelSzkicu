using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ModelSzkicu {
    public class Szkic {

        public List<Widok> Widoki { get; set; }
        public Widok WidokO => Widoki.SingleOrDefault(w => w.Typ == TypWidoku.o);
        public Widok WidokV => Widoki.SingleOrDefault(w => w.Typ == TypWidoku.v);
        public Widok WidokU => Widoki.SingleOrDefault(w => w.Typ == TypWidoku.u);
        public Widok WidokH => Widoki.SingleOrDefault(w => w.Typ == TypWidoku.h);

        public Szkic() {
            Widoki = new();
        }

        private void DodajWidok(TypWidoku typ) {
            if (Widoki.Any(w => w.Typ == typ)) {
                Debug.Print("Zdublowany widok!!!");
                return;
            }
            Widoki.Add(new(typ));
        }

        public void WczytajGeometrieZpliku(IReadOnlyList<string> wierszeDSTV) {

        }
    }
}

