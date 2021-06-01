using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Widok {

        public TypWidoku Typ { get; set; }
        //
        public List<LiniaKonturowa> LinieKonturowe { get; set; }
        // linie wymiarowe czy całe wymiary ???
        public List<Otwor> Otwory { get; set; }
        public List<Fasolka> Fasolki { get; set; }
        public List<Luk> Luki { get; set; }

        public Widok(TypWidoku typ) {
            Typ = typ;
            LinieKonturowe = new();
            Otwory = new();
            Fasolki = new();
            Luki = new();
        }

        //public void DodajLinieKonturu(LiniaKonturowa linia) {
        //    LinieKonturowe.Add(linia);
        //}
        //public void DodajOtwor(Otwor otwor) {
        //    Otwory.Add(otwor);
        //}
        //public void DodajFasolke(Fasolka fasolka) {
        //    Fasolki.Add(fasolka);
        //}
        //public void DodajLuk(Luk luk) {
        //    Luki.Add(luk);
        //}
        // lub przez widok.LinieKonturowe.Add(linia) ???

        //Dodaj wymiary

    }
    
    public enum TypWidoku { o, v, u, h }

}