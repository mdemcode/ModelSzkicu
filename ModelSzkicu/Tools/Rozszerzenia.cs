using System;
using System.Globalization;
using System.Linq;

namespace ModelSzkicu.Tools {
    public static class Rozszerzenia {

        //--------------------------//
        //---       STRING       ---//
        //--------------------------//

        public static string DigitsOnly(this string value) {
            if (string.IsNullOrEmpty(value)) return value;
            char[] arr = value.ToCharArray();
            return arr.Where(item => char.IsNumber(item) || item is '.' or ',' or '-').Aggregate<char, string>(null, (current, item) => current + item);
        }
        public static double GetDouble(this string value) {
            const double defaultValue = 0;
            if (!double.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out var result) &&
                !double.TryParse(value, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                !double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result)) {
                result = defaultValue;
            }
            return result;
        }
        public static TypWidoku ToTypWidoku(this char typ) {
            return typ switch {
                'o' => TypWidoku.o,
                'v' => TypWidoku.v,
                'u' => TypWidoku.u,
                'h' => TypWidoku.h,
                _ => throw new ArgumentOutOfRangeException(nameof(typ))
            };
        }
        public static bool CanParse<T>(this string value) {
            return Type.GetTypeCode(typeof(T)) switch {
                TypeCode.String or TypeCode.Object => true,
                TypeCode.Int16 => short.TryParse(value, out _),
                TypeCode.UInt16 => ushort.TryParse(value, out _),
                TypeCode.Int32 => int.TryParse(value, out _),
                TypeCode.UInt32 => uint.TryParse(value, out _),
                TypeCode.Int64 => long.TryParse(value, out _),
                TypeCode.UInt64 => ulong.TryParse(value, out _),
                TypeCode.Single => float.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _),
                TypeCode.Double => double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _),
                TypeCode.Decimal => decimal.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out _),
                TypeCode.Boolean => bool.TryParse(value, out _),
                TypeCode.Char => char.TryParse(value, out _),
                TypeCode.SByte => sbyte.TryParse(value, out _),
                TypeCode.Byte => byte.TryParse(value, out _),
                TypeCode.DateTime => DateTime.TryParse(value, out _),
                TypeCode.Empty or TypeCode.DBNull => false,
                _ => throw new ArgumentOutOfRangeException(nameof(T))
            };
        }

        //--------------------------//
        //---       SZKIC        ---//
        //--------------------------//

        //public static void ImportujOtworZDstv (this Szkic szkic, string wierszBlokuBO) {
        //    string [] rozbityWiersz = wierszBlokuBO.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        //    TypWidoku widok;
        //    // widok:
        //    try {
        //        widok = (rozbityWiersz[0])[0].ToTypWidoku();
        //    }
        //    catch (Exception e) {
        //        if (e is ArgumentOutOfRangeException) {
        //            szkic.BledySzkicu.Add(("Błąd importu otworu z modelu DSTV", $"Nieobsługiwany typ widoku: '{rozbityWiersz[0]}"));
        //        }
        //        else {
        //            szkic.BledySzkicu.Add(("Błąd importu otworu z modelu DSTV", $"{e.Message} # {e.InnerException?.Message}"));
        //        }
        //        return;
        //    }
        //    // parametry otowru:
        //    double x = rozbityWiersz[1].DigitsOnly().GetDouble();
        //    double y = rozbityWiersz[2].DigitsOnly().GetDouble();
        //    double fi = rozbityWiersz[3].DigitsOnly().GetDouble();
        //    double wysokosc = 0;
        //    double szerokosc = 0;
        //    if (rozbityWiersz.Length > 4) {
        //        wysokosc = rozbityWiersz[4].DigitsOnly().GetDouble();
        //        szerokosc = rozbityWiersz[5].DigitsOnly().GetDouble();
        //    }
        //    bool fasolka = wysokosc > 0.1 || szerokosc > 0.1;
        //    Otwor nowyOtwor = fasolka ? new Fasolka(x, y, fi, wysokosc, szerokosc) : new Otwor(x, y, fi);
        //    Widok widokSzkicu = widok switch {
        //        TypWidoku.o => szkic.WidokO,
        //        TypWidoku.v => szkic.WidokV,
        //        TypWidoku.u => szkic.WidokU,
        //        TypWidoku.h => szkic.WidokH,
        //        _ => null
        //    };
        //    if (widokSzkicu == null) {
        //        szkic.BledySzkicu.Add(("Błąd importu otworu z modelu DSTV", $"Nieobsługiwany typ widoku: '{widok}"));
        //        return;
        //    }
        //    if (fasolka) widokSzkicu.Fasolki.Add((Fasolka)nowyOtwor);
        //    else widokSzkicu.Otwory.Add(nowyOtwor);
        //}
    }
}
