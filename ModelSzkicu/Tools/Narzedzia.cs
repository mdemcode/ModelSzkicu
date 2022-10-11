using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Versioning;

namespace ModelSzkicu.Tools {
    public static class Narzedzia {

        public static PointF PunktToPointF(this Punkt punkt) {
            return new PointF { X = (float)punkt.X * 0.1f, Y = (float)punkt.Y * 0.1f };
        }

        public static bool ExportujSzkicDoBitmapy(this Szkic szkic)
        {
            Bitmap bitmap = new(500, 300, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Black, 1);

            foreach (ElementRysunku elemSzkicu in szkic.WszystkieElementySzkicu)
            {
                switch (elemSzkicu)
                {
                    case LiniaKonturowa:
                        LiniaKonturowa linia = elemSzkicu as LiniaKonturowa;
                        graphics.DrawLine(pen, linia.StartPoint.PunktToPointF(), linia.EndPoint.PunktToPointF());
                        break;
                    case Otwor:
                        Otwor otwor = elemSzkicu as Otwor;
                        graphics.DrawEllipse(pen, (float)otwor.CenterPoint.X, (float)otwor.CenterPoint.Y, (float)otwor.Dx, (float)otwor.Dy);
                        break;
                    default:
                        break;
                }
            }
            try
            {
                bitmap.Save(@"C:\Temp\Szkic.png");

            }
            catch
            {


            }

            return File.Exists("C:\\Szkic.png");
        }
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
