using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSTVmodel;
using ModelSzkicu.Tools;

namespace ModelSzkicu {
    public class Szkic {

        // model dstv
        public DstvModel _dstvModel;
        // błędy szkicu
        public List<(string typ, string opis)> BledySzkicu { get; } = new(); // "[typ błędu]: opis błędu"
        public bool MaBledy => BledySzkicu.Any();
        // źródło szkicu
        public ZrodloSzkicu _zrodloSzkicu;
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
        public Szkic(ZrodloSzkicu zrodlo, string adresPlikuLubIdSzkicuWBazie) {
            switch (zrodlo) {
                case ZrodloSzkicu.Pusty:
                    break;
                case ZrodloSzkicu.PlikNC:
                    if (!WczytajZPlikuNc(adresPlikuLubIdSzkicuWBazie)) return;
                    break;
                case ZrodloSzkicu.BazaDanych:
                    break;
                default:
                    BledySzkicu.Add(("Błąd tworzenia szkicu", "Nieobsługiwane źródło pliku!"));
                    break;
            }
        }
        #endregion


        #region WCZYTANIE DANYCH Z MODELU DSTV
        public bool WczytajZPlikuNc(string adresPlikuNc) {
            _zrodloSzkicu = ZrodloSzkicu.PlikNC;
            _dstvModel = new(adresPlikuNc);
            if (_dstvModel.WczytanyZBledami) {
                foreach (var blad in _dstvModel.BledyWczytywania) {
                    BledySzkicu.Add(("Błąd wczytywania pliku DSTV", blad));
                }
                return false;
            }
            return WczytajGeometrieZModeluDstv();
        }
        private bool WczytajGeometrieZModeluDstv() {
            _dstvModel.Bloki.ForEach(blok => {
                switch (blok.Typ) {
                    case TypBloku.ST:
                        break;
                    case TypBloku.AK: // kontur zewn.
                        ImportujKonturZmodeluDstv(blok.WierszeBloku, true);
                        break;
                    case TypBloku.IK: // kontur wewn.
                        ImportujKonturZmodeluDstv(blok.WierszeBloku, false);
                        break;
                    case TypBloku.BO: // otwory / fasolki
                        foreach (string wiersz in blok.WierszeBloku) {
                            ImportujOtworZmodeluDstv(wiersz);
                        }
                        break;
                    case TypBloku.PU:
                        break;
                    case TypBloku.KO:
                        break;
                    case TypBloku.SI:
                        break;
                    case TypBloku.KA:
                        break;
                    case TypBloku.SC:
                        break;
                    case TypBloku.TO:
                        break;
                    case TypBloku.UE:
                        break;
                    case TypBloku.PR:
                        break;
                    case TypBloku.EN:
                        break;
                    default:
                        break;
                }
            });
            return true;
        }

        private void ImportujKonturZmodeluDstv (string[] wierszeBloku, bool zewn) {
            if (!OdczytajTypWidokuZWierszaBloku(wierszeBloku[0], out TypWidoku typWidoku)) return;
            Kontur.TypKonturu typ = zewn ? Kontur.TypKonturu.Zewn : Kontur.TypKonturu.Wewn;
            Kontur kontur = new(typ);
            Punkt poprzPkt = null;
            //Punkt akt_pkt = null;
            double promienLuku = 0d;
            foreach (string w in wierszeBloku) {
                string wiersz = w;
                if (wiersz.Trim().StartsWith(typWidoku.ToString())) wiersz = wiersz.Trim()[1..];
                string[] rozbityWiersz = wiersz.Split().Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if ( rozbityWiersz[1].Last() == 't' || rozbityWiersz[1].Last() == 'w') continue; // promienie w narożach - ignoruję
                var wspPunktu = rozbityWiersz.Select(x => x.DigitsOnly())
                    .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.GetDouble()).ToArray();
                // tu trzeba zrobić odczyt parametrów dodatkowych (np. fazy)
                var aktPkt = new Punkt(wspPunktu[0], wspPunktu[1]);
                if (poprzPkt != null) {
                    if (Math.Abs(promienLuku) > 1) { // ignoruję łuki o r < 1 - zamieniam na linię
                        kontur.ElementyKonturu.Add(new Luk(
                            poprzPkt.X, 
                            poprzPkt.Y, 
                            aktPkt.X, 
                            aktPkt.Y, 
                            promienLuku, 
                            false
                        ));
                        promienLuku = 0d;
                    }
                    else {
                        kontur.ElementyKonturu.Add( new LiniaKonturowa(
                            poprzPkt.X,
                            poprzPkt.Y,
                            aktPkt.X,
                            aktPkt.Y,
                            true
                        ));
                    }
                }
                if (Math.Abs(wspPunktu[2]) > 1) {
                    promienLuku = wspPunktu[2];
                }
                //
                poprzPkt = aktPkt;
            }
            Widok widokSzkicu = typWidoku switch {
                TypWidoku.o => WidokO,
                TypWidoku.v => WidokV,
                TypWidoku.u => WidokU,
                TypWidoku.h => WidokH,
                _ => null
            };
            if (widokSzkicu == null) {
                BledySzkicu.Add(("Błąd importu konturu z modelu DSTV", $"Nieobsługiwany typ widoku: '{typWidoku}"));
                return;
            }
            widokSzkicu.Kontury.Add(kontur);
        }

        private void ImportujOtworZmodeluDstv (string wierszBlokuBO) {
            string [] rozbityWiersz = wierszBlokuBO.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            // widok:
            if (!OdczytajTypWidokuZWierszaBloku(wierszBlokuBO, out TypWidoku typOut)) return;
            //TypWidoku widok;
            //try {
            //    widok = (rozbityWiersz[0])[0].ToTypWidoku();
            //}
            //catch (Exception e) {
            //    if (e is ArgumentOutOfRangeException) {
            //        BledySzkicu.Add(("Błąd importu otworu z modelu DSTV", $"Nieobsługiwany typ widoku: '{rozbityWiersz[0]}. Wiersz otworu: [{wierszBlokuBO}]"));
            //    }
            //    else {
            //        BledySzkicu.Add(("Błąd importu otworu z modelu DSTV", $"{e.Message} # {e.InnerException?.Message ?? "_"}. Wiersz otworu: [{wierszBlokuBO}]"));
            //    }
            //    return;
            //}
            // parametry otowru:
            double x = rozbityWiersz[1].DigitsOnly().GetDouble();
            double y = rozbityWiersz[2].DigitsOnly().GetDouble();
            double fi = rozbityWiersz[3].DigitsOnly().GetDouble();
            double wysokosc = 0;
            double szerokosc = 0;
            if (rozbityWiersz.Length > 4) {
                wysokosc = rozbityWiersz[4].DigitsOnly().GetDouble();
                szerokosc = rozbityWiersz[5].DigitsOnly().GetDouble();
            }
            bool fasolka = wysokosc > 0.1 || szerokosc > 0.1;
            Otwor nowyOtwor = fasolka ? new Fasolka(x, y, fi, wysokosc, szerokosc) : new Otwor(x, y, fi);
            Widok widokSzkicu = typOut switch {
                TypWidoku.o => WidokO,
                TypWidoku.v => WidokV,
                TypWidoku.u => WidokU,
                TypWidoku.h => WidokH,
                _ => null
            };
            if (widokSzkicu == null) {
                BledySzkicu.Add(("Błąd importu otworu z modelu DSTV", $"Nieobsługiwany typ widoku: '{typOut}"));
                return;
            }
            if (fasolka) widokSzkicu.Fasolki.Add((Fasolka)nowyOtwor);
            else widokSzkicu.Otwory.Add(nowyOtwor);
        }

        private bool OdczytajTypWidokuZWierszaBloku(string wiersz, out TypWidoku typOut) {
            string[] rozbityWiersz = wiersz.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            typOut = TypWidoku.err;
            try {
                typOut = (rozbityWiersz[0]).First().ToTypWidoku();
                return true;
            }
            catch (Exception e) {
                if (e is ArgumentOutOfRangeException) {
                    BledySzkicu.Add(("Błąd importu geometrii z modelu DSTV", $"Nierozpoznany typ widoku: '{rozbityWiersz[0]}. Oryginalny wiersz: [{wiersz}]"));
                }
                else {
                    BledySzkicu.Add(("Błąd importu geometrii z modelu DSTV", $"{e.Message} # {e.InnerException?.Message ?? "_"}. Oryginalny wiersz: [{wiersz}]"));
                }
                return false;
            }
        }

        #endregion

        #region WCZYTYWANIE SZKICU Z BAZY DANYCH
        public bool WczytajZBazyDanych() {
            _dstvModel = null;
            _zrodloSzkicu = ZrodloSzkicu.BazaDanych;
            // // //

            return true;
        }
        #endregion

        #region METODY POMOCNICZE
        private IEnumerable<ElementRysunku> ElementySzkicu() {
            var outList = new List<ElementRysunku>();
            Widoki.ForEach(w => {
                //outList.AddRange(w.LinieKonturowe);
                //outList.AddRange(w.Luki);
                w.Kontury.ForEach(k => { outList.AddRange(k.ElementyKonturu); });
                outList.AddRange(w.Otwory);
                outList.AddRange(w.Fasolki);
            });
            return outList;
        }

        #endregion

        public enum ZrodloSzkicu {
            Pusty,
            PlikNC,
            BazaDanych
        }
    }
}
