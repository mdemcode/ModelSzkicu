using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSzkicu.Tools {
    public static class Rozszerzenia {

        //------------------//
        //---  STRING    ---//
        //------------------//

        public static string DigitsOnly(this string value) {
            if (string.IsNullOrEmpty(value)) return value;
            var arr = value.ToCharArray();
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
        public static TypWidoku ToTypWidoku(this string typ) {
            return typ switch {
                "o" => TypWidoku.o,
                "v" => TypWidoku.v,
                "u" => TypWidoku.u,
                "h" => TypWidoku.h,
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
                TypeCode.Single => float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out _),
                TypeCode.Double => double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out _),
                TypeCode.Decimal => decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out _),
                TypeCode.Boolean => bool.TryParse(value, out _),
                TypeCode.Char => char.TryParse(value, out _),
                TypeCode.SByte => sbyte.TryParse(value, out _),
                TypeCode.Byte => byte.TryParse(value, out _),
                TypeCode.DateTime => DateTime.TryParse(value, out _),
                TypeCode.Empty or TypeCode.DBNull => false,
                _ => throw new ArgumentOutOfRangeException(nameof(T))
            };
        }


    }
}
