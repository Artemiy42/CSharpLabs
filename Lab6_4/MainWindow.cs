using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab6_4
{
    class MainWindow : Form
    {
        Point[] points;
        Pen pen;
        Pen pen1;

        public MainWindow()
        {
            pen = new Pen(Brushes.Black, 5);
            pen1 = Pens.Red;
            Paint += Draw_Paint;

            ContextMenuStrip = new ContextMenuStrip();
            Console.WriteLine("ContextMenuStrip version");

            MouseClick += Control1_MouseClick;
            Point[] po = {
                new Point(5,5),
                new Point(50,50),
                new Point(100,5),
                new Point(150, 50),
                new Point(200, 5),
            };

            points = po;
        }

        void ContextMenuStrip_Click(object sender, EventArgs e)
        {
            Console.WriteLine("MenuIten have been clicked");
        }

        void Control1_MouseClick(Object sender, MouseEventArgs e)
        {
            Region r = mkRgn();
            if (r.IsVisible(e.Location))
            {
                ContextMenuStrip.Items.Clear();
                ToolStripMenuItem mi = new ToolStripMenuItem("in Region");
                ContextMenuStrip.Items.Add(mi);
                mi.Click += ContextMenuStrip_Click;
            }
            else
            {
                ContextMenuStrip.Items.Clear();
                ToolStripMenuItem mi = new ToolStripMenuItem("Not in Region");
                ContextMenuStrip.Items.Add(mi);
                mi.Click += ContextMenuStrip_Click;
            }
        }

        public void Draw_Paint(object sender, PaintEventArgs ea)
        {
            Graphics g = ea.Graphics;
            Text = "gdiDrawLine";

            g.DrawLines(pen, points);

            for (int i = 0; i < points.Length - 1; i++)
            {
                g.DrawPolygon(pen1, mkPolygon(points[i], points[i + 1]));
            }
        }

        Region mkRgn()
        {
            GraphicsPath gp = new GraphicsPath();
            int i;
            for (i = 0; i < points.Length - 1; i++)
            {
                gp.AddPolygon(mkPolygon(points[i], points[i + 1]));
            }

            Region r = new Region(gp);
            return r;
        }

        Point[] mkPolygon(Point a, Point b)
        {
            Point[] m = new Point[5];
            m[0] = a;
            m[1] = b;
            m[2] = new Point(b.X + 4, b.Y);
            m[3] = new Point(a.X + 4, a.Y);
            m[4] = a;
            return m;
        }
    }
}
