using DSTVmodel;
using ModelSzkicu.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSzkicu
{
    public partial class Szkic {

        private bool WczytajGeometrieZModeluDstv() {
            _dstvModel.Bloki.ForEach(blok => {
                switch (blok.Typ) {
                    case TypBloku.ST:
                        break;
                    case TypBloku.AK: // kontur zewn.
                        ImportujKonturZmodeluDstv(blok.WierszeBloku, Kontur.TypKonturu.Zewn);
                        break;
                    case TypBloku.IK: // kontur wewn.
                        ImportujKonturZmodeluDstv(blok.WierszeBloku, Kontur.TypKonturu.Wewn);
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
        private void ImportujKonturZmodeluDstv(string[] wierszeBloku, Kontur.TypKonturu typKonturu) {
            if (!OdczytajTypWidokuZWierszaBloku(wierszeBloku[0], out TypWidoku typWidoku)) return;
            //Kontur.TypKonturu typ = zewn ? Kontur.TypKonturu.Zewn : Kontur.TypKonturu.Wewn;
            Kontur kontur = new(typKonturu);
            Punkt poprzPkt = null;
            //Punkt akt_pkt = null;
            double promienLuku = 0d;
            foreach (string w in wierszeBloku) {
                string wiersz = w;
                if (wiersz.Trim().StartsWith(typWidoku.ToString())) wiersz = wiersz.Trim()[1..]; // odcięcie pierwszwego znaku, jeśli to znak typu widoku (o, v, u, h)
                string[] rozbityWiersz = wiersz.Split().Where(x => !string.IsNullOrEmpty(x)).ToArray();
                if (rozbityWiersz[1].Last() == 't' || rozbityWiersz[1].Last() == 'w') continue; // promienie w narożach - ignoruję
                double[] wspPunktu = rozbityWiersz.Select(x => x.DigitsOnly())
                    .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.GetDouble()).ToArray();
                // tu trzeba zrobić odczyt parametrów dodatkowych (np. fazy)
                Punkt aktPkt = new(wspPunktu[0], wspPunktu[1]);
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
                        kontur.ElementyKonturu.Add(new LiniaKonturowa(
                            poprzPkt.X,
                            poprzPkt.Y,
                            aktPkt.X,
                            aktPkt.Y,
                            false
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
        private void ImportujOtworZmodeluDstv(string wierszBlokuBO) {
            // widok:
            if (!OdczytajTypWidokuZWierszaBloku(wierszBlokuBO, out TypWidoku typOut)) return;
            // parametry otowru:
            string[] rozbityWiersz = wierszBlokuBO.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            double x = rozbityWiersz[1].DigitsOnly().GetDouble();
            double y = rozbityWiersz[2].DigitsOnly().GetDouble();
            double fi = rozbityWiersz[3].DigitsOnly().GetDouble();
            double wysokosc = 0;
            double szerokosc = 0;
            if (rozbityWiersz.Length > 4) {
                wysokosc = rozbityWiersz[4].DigitsOnly().GetDouble();
                szerokosc = rozbityWiersz[5].DigitsOnly().GetDouble();
            }
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
            if (wysokosc > 0.1 || szerokosc > 0.1) {
                widokSzkicu.Fasolki.Add(new Fasolka(x, y, fi, wysokosc, szerokosc));
            }
            else {
                widokSzkicu.Otwory.Add(new Otwor(x, y, fi));
            }
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
                    BledySzkicu.Add(("Błąd importu geometrii z modelu DSTV", $"Nierozpoznany typ widoku: '{rozbityWiersz[0]}. Original DSTV source row: [{wiersz}]"));
                }
                else {
                    BledySzkicu.Add(("Błąd importu geometrii z modelu DSTV", $"{e.Message} # {e.InnerException?.Message ?? "_"}. Original DSTV source row: [{wiersz}]"));
                }
                return false;
            }
        }

    }
}
