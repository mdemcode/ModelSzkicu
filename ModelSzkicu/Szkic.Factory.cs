using DSTVmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSzkicu
{
    public partial class Szkic {

        public static class Factory {

            public static Szkic UtworzNowy(ZrodloSzkicu zrodlo, DstvModel dstvModel) {
                Szkic nowySzkic = new() {
                    _zrodloSzkicu = zrodlo,
                    _dstvModel = dstvModel
                };
                if (dstvModel.WczytanyZBledami) {
                    foreach (var blad in nowySzkic._dstvModel.BledyWczytywania) {
                        nowySzkic.BledySzkicu.Add(("Błąd wczytywania pliku DSTV", blad));
                    }
                }
                nowySzkic.WczytajGeometrieZModeluDstv();
                return nowySzkic;
            }
            public static Szkic UtworzZPlikuNc(string adresPliku) 
                => UtworzNowy(ZrodloSzkicu.PlikNC, new DstvModel(adresPliku));
            public static Szkic UtworzZWierszyDstv(string[] wierszeDstv)
                => UtworzNowy(ZrodloSzkicu.Wiersze, new DstvModel(wierszeDstv));
            public static Szkic UtworzZeStringu(string tekstDstv) {
                string[] wiersze = tekstDstv.Split('\n');
                return UtworzZWierszyDstv(wiersze);
            }
            /// <summary>  Not implemented, yet!!!  </summary>
            public static Szkic UtworzZBazyDanych(string jakiesOdniesienieDoObiektuWdb) {
                // ToDo - wczytaj szkic z bazy danych
                return new Szkic();
            }
            public static Szkic PustySzkic() => new();
        }
    }
}
