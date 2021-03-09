using System;
using System.Collections.Generic;
using System.Text;

namespace ModelSzkicu {
    public class Luk : ElementRysunku {

        public double StartX { get; set; }
        public double StartY { get; set; }
        public double EndX { get; set; }
        public double EndY { get; set; }
        public double R { get; set; } // dodatni: kierunek clockwise / ujemny: counterclockwise
        public bool Ukryty { get; set; }

        public Luk(double startX, double startY, double endX, double endY, double r, bool ukryty) {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
            R = r;
            Ukryty = ukryty;
        }
    }
}
