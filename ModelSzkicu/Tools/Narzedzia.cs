using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSzkicu.Tools {
    internal static class Narzedzia {
        static void tmp()
        {
            Szkic szkic = new(Szkic.ZrodloSzkicu.PlikNC, "");
            foreach (ElementRysunku elemSzkicu in szkic.WszystkieElementySzkicu)
            {
                switch (elemSzkicu)
                {
                    case Luk:
                        break;
                    case Fasolka:
                        break;
                    case Otwor:
                        break;
                        // itd ...
                    default:
                        break;
                }
            }
        }


    }
}
