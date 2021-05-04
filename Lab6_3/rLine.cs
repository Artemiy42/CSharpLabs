using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Lab6_3
{
    public class rLine
    {
        public tuple2d[] pnts;
        public string nm;
        static readonly char NumberDecimalSeparator
        = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

        public rLine(string nm, string nordNm = "", string eastNm = "")
        {
            this.nm = nm;
        }

        public bool getBox(out double xMin, out double xMax, out double yMin, out double yMax)
        {
            xMin = double.MaxValue;
            xMax = double.MinValue;
            yMin = double.MaxValue;
            yMax = double.MinValue;
            
            if (pnts != null && pnts.Length > 0)
            {
                for (int i = 0; i < pnts.Length; i++)
                {
                    if (pnts[i].X < xMin)
                        xMin = pnts[i].X;
                    if (pnts[i].X > xMax)
                        xMax = pnts[i].X;
                    if (pnts[i].Y < yMin)
                        yMin = pnts[i].Y;
                    if (pnts[i].Y > yMax)
                        yMax = pnts[i].Y;
                }
            
                return true;
            }

            return false;
        }

        public static tuple2d[] GetPoints(TextReader tr, bool vFlag = false, char decPnt = '.')
        {
            List<tuple2d> ps = new List<tuple2d>();
            string r;
            int lineNo = 0;

            while ((r = tr.ReadLine()) != null)
            {
                lineNo++;
                if (NumberDecimalSeparator != decPnt)
                {
                    r = r.Replace(decPnt, NumberDecimalSeparator);
                }

                string[] numbers = r.Split(' ');
                ps.Add(new tuple2d(double.Parse(numbers[0]), double.Parse(numbers[1])));
            }

            return ps.ToArray();
        }
    }
}
