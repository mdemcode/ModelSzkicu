using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSTVmodel;
using ModelSzkicu.Tools;

namespace ModelSzkicu 
{
    public partial class Szkic {

        // model dstv
        private DstvModel _dstvModel;
        //public DstvModel DstvModel => _dstvModel;
        // błędy szkicu
        public List<(string typ, string opis)> BledySzkicu { get; } = new(); // "[typ błędu]: opis błędu"
        public bool MaBledy => BledySzkicu.Any();
        // źródło szkicu
        private ZrodloSzkicu _zrodloSzkicu;
        //
        public List<Widok> Widoki { get; } = new();
        public Widok WidokO { 
            get {
                if (Widoki.All(w => w.Typ != TypWidoku.o)) Widoki.Add(new Widok(TypWidoku.o));
                return Widoki.Single(w => w.Typ == TypWidoku.o); 
            } 
        }
        public Widok WidokV { 
            get {
                if (Widoki.All(w => w.Typ != TypWidoku.v)) Widoki.Add(new Widok(TypWidoku.v));
                return Widoki.Single(w => w.Typ == TypWidoku.v); 
            } 
        }
        public Widok WidokU {
            get {
                if (Widoki.All(w => w.Typ != TypWidoku.u)) Widoki.Add(new Widok(TypWidoku.u));
                return Widoki.Single(w => w.Typ == TypWidoku.u); 
            } 
        }
        public Widok WidokH {
            get {
                if (Widoki.All(w => w.Typ != TypWidoku.h)) Widoki.Add(new Widok(TypWidoku.h));
                return Widoki.Single(w => w.Typ == TypWidoku.h); 
            } 
        }
        //
        public IEnumerable<ElementRysunku> WszystkieElementySzkicu => ElementySzkicu();


        #region KONSTRUKTOR
        //public Szkic(ZrodloSzkicu zrodlo, string tekstPliku_adresPliku_idBazy) { // drugi parametr zależny od pierwszego (źródła)
        //    switch (zrodlo)
        //    {
        //        case ZrodloSzkicu.Pusty:
        //            BledySzkicu.Add(("Błąd aplikacji", "Brak implementacji dla pustego szkicu."));
        //            break;
        //        case ZrodloSzkicu.String:
        //            string[] wiersze = tekstPliku_adresPliku_idBazy.Split('\n');
        //            _ = WczytajZWierszy(wiersze);
        //            break;
        //        case ZrodloSzkicu.PlikNC:
        //            if (!WczytajZPlikuNc(tekstPliku_adresPliku_idBazy)) return;
        //            break;
        //        case ZrodloSzkicu.BazaDanych:
        //            BledySzkicu.Add(("Błąd aplikacji", "Brak implementacji szkicu z bazy danych."));
        //            break;
        //        case ZrodloSzkicu.Wiersze:
        //            BledySzkicu.Add(("Błąd implementacji", "Inicjalizacja szkicu z wierszy w stringu, z niewłaściwego konstruktora!"));
        //            break;
        //        default:
        //            BledySzkicu.Add(("Błąd tworzenia szkicu", "Nieobsługiwane źródło szkicu!"));
        //            break;
        //    }
        //}
        //public Szkic(string[] wiersze) {
        //    _zrodloSzkicu = ZrodloSzkicu.Wiersze;
        //    WczytajZWierszy(wiersze);
        //}
        private Szkic() { }
        #endregion

        #region METODY POMOCNICZE
        private IEnumerable<ElementRysunku> ElementySzkicu() {
            List<ElementRysunku> outList = new ();
            Widoki.ForEach(w => {
                w.Kontury.ForEach(k => { outList.AddRange(k.ElementyKonturu); });
                outList.AddRange(w.Otwory);
                outList.AddRange(w.Fasolki);
                // ToDo - add dimensions
            });
            return outList;
        }

        #endregion

        public enum ZrodloSzkicu {
            Pusty,
            String,
            Wiersze,
            PlikNC,
            BazaDanych
        }
    }
}
