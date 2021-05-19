using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSTVmodel;

namespace ModelSzkicu {
    public class Szkic {

        public List<string> BledySzkicu { get; } = new(); // "[typ błędu]: opis błędu"
        //
        public List<Widok> Widoki { get; } = new();
        public Widok WidokO => Widoki.Single(w => w.Typ == TypWidoku.o);
        public Widok WidokV => Widoki.Single(w => w.Typ == TypWidoku.v);
        public Widok WidokU => Widoki.Single(w => w.Typ == TypWidoku.u);
        public Widok WidokH => Widoki.Single(w => w.Typ == TypWidoku.h);
        //

        // metadane z dstv
        public string NazwaPliku => _dstvModel.NazwaPliku;
        public int Szt => _dstvModel.Szt;
        // itd. ...

        // prywatne
        private readonly DstvModel _dstvModel;

        public Szkic(string adresPlikuNc) {
            _dstvModel = new(adresPlikuNc);
            if (_dstvModel.WczytanyZBledami) {
                ImportujBledyWczytywaniaDstv();
                return;
            }
            // // //

        }

        private void ImportujBledyWczytywaniaDstv() {
            foreach (var blad in _dstvModel.BledyWczytywania) {
                BledySzkicu.Add($"[Błąd wczytywania DSTV]: {blad}");
            }
        }
        private void WczytajGeometrieZModeluDstv() {

        }

    }
}
