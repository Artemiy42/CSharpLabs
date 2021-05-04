using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab6_2
{
    public class p4sLine : Control
    {
        string error;
        sLine sl;

        public p4sLine(sLine l)
        {
            mkPan(l);
        }

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
