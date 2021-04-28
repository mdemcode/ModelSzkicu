using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSTVmodel;

namespace ModelSzkicu {
    public class Szkic {

        public List<Widok> Widoki { get; private set; }
        public Widok WidokO => Widoki.Single(w => w.Typ == TypWidoku.o) ?? null;
        public Widok WidokV => Widoki.Single(w => w.Typ == TypWidoku.v) ?? null;
        public Widok WidokU => Widoki.Single(w => w.Typ == TypWidoku.u) ?? null;
        public Widok WidokH => Widoki.Single(w => w.Typ == TypWidoku.h) ?? null;
        // metadane z dstv
        public string NazwaPliku => _dstvModel.NazwaPliku;

        public int Szt => _dstvModel.Szt;
        // itd. ...

        // prywatne
        private DstvModel _dstvModel;
        private bool _bladWczytaniaPliku => !_dstvModel.PlikWczytany || _dstvModel.WczytanyZBledami;

        public Szkic(string adresPlikuNc) {
            _dstvModel = new DstvModel(adresPlikuNc);
            if (_bladWczytaniaPliku) return;
            Widoki = new();

        }

        private void WczytajGeometrieZModeluDstv() {

        }

    }
}
