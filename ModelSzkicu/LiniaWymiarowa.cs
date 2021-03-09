using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelSzkicu {

    public class LiniaWymiarowa : Linia {

        public enum TypLiniiWymiarowej { glowna, pomocnicza, ukosnik}
        
        public TypLiniiWymiarowej Typ { get; set; }

        public LiniaWymiarowa(double x1, double x2, double y1, double y2, TypLiniiWymiarowej typ) : base(x1, x2, y1, y2) {
            Grubosc *= 0.5;
        }

    }
}
