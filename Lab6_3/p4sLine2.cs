using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6_3
{
    public class p4sLine2 : p4sLine
    {
        public mapping mp;
        
        public p4sLine2(rLine l)
        {
            double a, b, c, d;
            
            if (l.getBox(out a, out b, out c, out d))
            {
                mp = new mapping(a, b, c, d);
                // mp.mkZmEqual();
            }
            else
            {
                mp = new mapping();
            }

            MinimumSize = new Size(Convert.ToInt32(mp.w), Convert.ToInt32(mp.h));
            MaximumSize = new Size(Convert.ToInt32(mp.w), Convert.ToInt32(mp.h));
            AutoSize = true;

            if (l == null)
            {
                ;
            }
            else if (l.pnts == null || l.pnts.Length < 1)
            {
                sl = new sLine();
                sl.nm = l.nm;
            }
            else
            {
                sl = new sLine();
                sl.nm = l.nm;
                mkSLine(l);
                Paint += sl._paint;
            }

            mkPan(sl);
        }

        public void mkSLine(rLine rl)
        {
            int x, y;
            
            sl.ps = new Point[rl.pnts.Length];

            for (int i = 0; i < rl.pnts.Length; i++)
            {
                mp.map(rl.pnts[i].X, rl.pnts[i].Y, out x, out y);
                sl.ps[i] = new Point(x, mp.h - y);
            }
        }
    }

    public class p4sLine : Panel
    {
        protected string error;
        protected sLine sl;

        protected void mkPan(sLine l)
        {
            sl = l;

            if (l == null)
            {
                error = "no any line!";
                Paint += _paint;
            }
            else if (l.ps == null || l.ps.Length < 1)
            {
                error = String.Format("Line ’{0}’ is empty!", l.nm);
                Paint += _paint;
            }
            else
            {
                sl = l;
                Paint += sl._paint;
            }


            BackColor = Color.Ivory;
            Dock = DockStyle.Fill;
        }

        public void _paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (Font f = new Font("Times New Roman", 14))
            {
                g.DrawString(error, f, Brushes.Red, 10, 3 * 14);
            }
        }
    }
}
