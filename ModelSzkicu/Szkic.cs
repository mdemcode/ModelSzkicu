using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ModelSzkicu {
    public class Szkic {

        public List<Widok> Widoki { get; private set; }
        public Widok WidokO => Widoki.SingleOrDefault(w => w.Typ == TypWidoku.o);
        public Widok WidokV => Widoki.SingleOrDefault(w => w.Typ == TypWidoku.v);
        public Widok WidokU => Widoki.SingleOrDefault(w => w.Typ == TypWidoku.u);
        public Widok WidokH => Widoki.SingleOrDefault(w => w.Typ == TypWidoku.h);
        // dane dodatkowe
        public string NazwaPlikuWgDSTV { get; private set; }
        public string GatunekWgDSTV { get; private set; }
        public int SztukiWgDSTV { get; private set; }
        public string MaterialWgDSTV { get; private set; }
        public string TypProfiluWgDSTV { get; private set; }
        public double LwgDSTV { get; private set; }
        public double BwgDSTV { get; private set; }
        public double HwgDSTV { get; private set; }
        public double GrPolkiWgDSTV { get; private set; }
        public double GrSrodnikaWgDSTV { get; private set; }

        public Szkic() {
            Widoki = new();
        }

        //private void DodajWidok(TypWidoku typ) {
        //    if (Widoki.Any(w => w.Typ == typ)) {
        //        Debug.Print("Zdublowany widok!!!");
        //        return;
        //    }
        //    Widoki.Add(new(typ));
        //}

        //var dstv_wiersze = File.ReadAllLines(plik_nc);
        public void WczytajGeometrieZpliku(IReadOnlyList<string> wierszeDSTV) {
            // dane dodatkowe
            NazwaPlikuWgDSTV = wierszeDSTV[1].Split()[1];
            GatunekWgDSTV = wierszeDSTV[6].Split().Last();
            SztukiWgDSTV = int.Parse(wierszeDSTV[7].Split().Last());
            MaterialWgDSTV = wierszeDSTV[8].Split().Last();
            TypProfiluWgDSTV = wierszeDSTV[9].Split().Last();
            LwgDSTV = double.Parse(wierszeDSTV[10].Split(',', StringSplitOptions.RemoveEmptyEntries).First().Trim());
            BwgDSTV = double.Parse(wierszeDSTV[11].Split().Last());
            HwgDSTV = double.Parse(wierszeDSTV[12].Split().Last());
            GrPolkiWgDSTV = double.Parse(wierszeDSTV[13].Split().Last());
            GrSrodnikaWgDSTV = double.Parse(wierszeDSTV[14].Split().Last());

        }

    }
}

