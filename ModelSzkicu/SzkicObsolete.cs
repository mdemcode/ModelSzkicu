using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ModelSzkicu.Tools;

namespace ModelSzkicu {
    public class SzkicObsolete {

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

        public SzkicObsolete() {
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

            var aktualnyBlok = "ST";
            var wiersz = 25;
            while (wierszeDSTV[wiersz].Split().First() != "EN") {
                ////var rozbityWiersz = wierszeDSTV[wiersz].Split();
                var pierwszaKomorka = wierszeDSTV[wiersz].Split().First();
                //if (pierwszaKomorka.Trim() == "EN") break;
                if (aktualnyBlok == "ST" && string.IsNullOrEmpty(pierwszaKomorka)) {
                    wiersz++;
                    continue;
                }
                //if (string.IsNullOrEmpty(pierwszaKomorka)) continue;
                aktualnyBlok = pierwszaKomorka.Trim();
                //var linieTmp  = new List<LiniaKonturowa>();
                //var otworyTmp = new List<Otwor>();
                //var lukiTmp   = new List<Luk>();
                //var fasolkiTmp = new List<Fasolka>();
                wiersz++;
                ////rozbityWiersz = wierszeDSTV[++wiersz].Split();
                //var typWidStr = rozbityWiersz.First(x => !string.IsNullOrEmpty(x)).Substring(0,1).ToLower();
                //var typWidoku = typWidStr.ToTypWidoku(); // Narzedzia.StrToTypWidoku(typWidStr);
                ////var typWidoku = rozbityWiersz.First(x => !string.IsNullOrEmpty(x)).Substring(0,1).ToLower().ToTypWidoku();
                //
                //var rozbityWierszPelny = wierszeDSTV[wiersz].Split();
                //var rozbityWierszBezPustych = wierszeDSTV[wiersz].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                // ODCZYTAJ WSZYSTKIE KSZTAŁTY:
                switch (aktualnyBlok) {
                    case "IK":
                    case "AK": // linie i łuki
                        (double X, double Y)? poprzPkt = null;
                        var promienLuku = 0d;
                        var aktywnyWidok = OdczytajDodajTWidok(wierszeDSTV[wiersz]);
                        if (aktywnyWidok == null) {
                            Debug.Print("Błąd odczytu typu widoku!!!"); // teoretycznie nie powinno wystąpić w prawidłowej DSTVce
                        }
                        do {
                            var aktWiersz = wierszeDSTV[wiersz];
                            var pierwszyZnak = aktWiersz.Trim().ToLower().First();
                            if (pierwszyZnak == 'o' || pierwszyZnak == 'v' || pierwszyZnak == 'u' || pierwszyZnak == 'h')
                                aktWiersz = aktWiersz.Trim().Substring(1).Trim();
                            // ToDo - odczyt dodatkowych parametrów
                            // to jeszcze będzie potrzebne (chyba)
                            //var w = rozbityWiersz.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            //if ( w[1].Last() == 't' || w[2].Last() == 't') rozbityWiersz = wierszeDSTV[++wiersz].Split(); // promienie w narożach // || w[1].Last() == 'w' || w[2].Last() == 'w'
                            //
                            var rozbityWiersz = aktWiersz.Split().Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            (double X, double Y) aktPkt = (rozbityWiersz[0].DigitsOnly().GetDouble(), rozbityWiersz[1].DigitsOnly().GetDouble());
                            if (poprzPkt != null) {
                                if (Math.Abs(promienLuku) > 1) {
                                    aktywnyWidok?.Luki.Add(new((double) poprzPkt?.X, (double) poprzPkt?.Y, aktPkt.X, aktPkt.Y, promienLuku, false));
                                    promienLuku = 0d;
                                }
                                else {
                                    aktywnyWidok?.LinieKonturowe.Add(new((double) poprzPkt?.X, (double) poprzPkt?.Y, aktPkt.X, aktPkt.Y, false));
                                }
                            }
                            var promienTmp = rozbityWiersz[2].DigitsOnly().GetDouble();
                            if (Math.Abs(promienTmp) > 1) {
                                promienLuku = promienTmp;
                            }
                            //
                            poprzPkt = aktPkt;
                        } while (string.IsNullOrEmpty(wierszeDSTV[++wiersz].Split().First()));
                        break;
                    case "BO": // otwory i fasolki
                        var typWidokuOtw = wierszeDSTV[wiersz].Trim().First().ToString().ToLower().ToTypWidoku();
                        if (Widoki.All(w => w.Typ != typWidokuOtw)) Widoki.Add(new(typWidokuOtw));
                        var aktywnyWidokOtw = Widoki.Single(w => w.Typ == typWidokuOtw);
                        var pierwszyOtworWbloku = true;
                        do {

                            //var wspPunktu = rozbityWiersz.Select(x => x.DigitsOnly()).Where(x => !string.IsNullOrEmpty(x)).Select(x => x.GetDouble()).ToArray();
                            //// fasolki poziome
                            //if (wspPunktu.Length > 3 && wspPunktu[4] > 1) {
                            //    aktywnyWidok.Fasolki.Add(new(wspPunktu[0], wspPunktu[1], wspPunktu[2], wspPunktu[4]));
                            //    otworyTmp.Add(new TEC_OTWOR_MD { CENTER_X = wspPunktu[0], CENTER_Y = wspPunktu[1], SREDNICA = wspPunktu[2], FASOLKA = - Math.Abs(wspPunktu[4]) });
                            //}
                            //// fasolki pionowe
                            //if (wspPunktu.Length > 3 && wspPunktu[5] > 1) {
                            //    otworyTmp.Add(new TEC_OTWOR_MD { CENTER_X = wspPunktu[0], CENTER_Y = wspPunktu[1], SREDNICA = wspPunktu[2], FASOLKA = Math.Abs(wspPunktu[5]) });
                            //}
                            //// otwory zwykłe
                            //if (wspPunktu[2] > 1) {
                            //    if (wspPunktu.Length < 4 || (wspPunktu[4] < 1 && wspPunktu[5] < 1)) {
                            //        otworyTmp.Add(new TEC_OTWOR_MD { CENTER_X = wspPunktu[0], CENTER_Y = wspPunktu[1], SREDNICA = wspPunktu[2], FASOLKA = 0 });
                            //    }
                            //}
                            //
                        } while (string.IsNullOrEmpty(wierszeDSTV[++wiersz].Split().First()));
                        break;
                    default: // inne, nieobsługiwane
                        do {
                            // ToDo - odczyt innychh bloków
                        } while (string.IsNullOrEmpty(wierszeDSTV[++wiersz].Split().First()));
                        break;
                }
                //// WSZYSTKIE KSZTAŁTY DODAĆ DO ODPOWIEDNIEGO RZUTU:
                //TEC_WIDOK_MD widok;
                //using (var sesja = Session_Factory.OpenSessionProton) {
                //    var widoki = sesja.Query<TEC_WIDOK_MD>().Where(w => w.ID_SZKICU == this && w.TYP == typWidoku);
                //    if (widoki.Any()) widok = widoki.FirstOrDefault();
                //    else {
                //        widok = new TEC_WIDOK_MD {ID_WIDOKU = Guid.NewGuid(), ID_SZKICU = this, TYP = typWidoku};
                //        BazaDanych.Zapisz_nowy_do_bazy(widok);
                //    }
                //}
                //linieTmp.ForEach(linia => { linia.ID_LINII = Guid.NewGuid(); linia.ID_WIDOKU = widok; BazaDanych.Zapisz_nowy_do_bazy(linia); });
                //otworyTmp.ForEach(otw => { otw.ID_OTWORU   = Guid.NewGuid(); otw.ID_WIDOKU   = widok; BazaDanych.Zapisz_nowy_do_bazy(otw); });
                //lukiTmp.ForEach(luk => { luk.ID_LUKU       = Guid.NewGuid(); luk.ID_WIDOKU   = widok; BazaDanych.Zapisz_nowy_do_bazy(luk); });
            }

        }

        private Widok OdczytajDodajTWidok(string wiersz) {
            var pierwszyZnakWiersza = wiersz.Trim().ToLower().First();
            if (pierwszyZnakWiersza != 'o' && pierwszyZnakWiersza != 'v' && pierwszyZnakWiersza != 'u' && pierwszyZnakWiersza != 'h') 
                return null;
            var typ = pierwszyZnakWiersza.ToString().ToTypWidoku();
            if (Widoki.All(w => w.Typ != typ)) Widoki.Add(new(typ));
            return Widoki.Single(w => w.Typ == typ);
        }
    }
}

