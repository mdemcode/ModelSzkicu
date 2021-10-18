using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSzkicu {
    public class Kontur {

        public TypKonturu Typ { get; }
        public List<ElementRysunku> ElementyKonturu = new();

        public Kontur(TypKonturu typ) {
            Typ = typ;
        }
        
        public void Skaluj(double wspolczynnik) {
            foreach (var element in ElementyKonturu) {
                element.Skaluj(wspolczynnik);
            }
        }
        public void Przesun(double dx, double dy) {
            foreach (var element in ElementyKonturu) {
                element.Przesun(dx, dy);
            }
        }


        public enum TypKonturu { Zewn, Wewn }

    }
}
