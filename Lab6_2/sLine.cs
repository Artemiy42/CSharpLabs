using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Lab6_2
{
    public class sLine
    {
        public string nm = "line";
        public Point[] ps; 
        public Pen pen = Pens.Black;
        public bool mkPolygon = false;
        public bool mkLine = true;

        public void _paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (ps != null && ps.Length > 1)
            {
                if (mkPolygon)
                    g.DrawPolygon(pen, ps); // замкнутая линия
                else
                    g.DrawLines(pen, ps);
            }
            else
            {
                string text = String.Format("line ’{0}’ is empty!", nm);
                using (Font f = new Font("Times New Roman", 14))
                {
                    g.DrawString(text, f
                    , Brushes.Red
                    , 20, 3 * 14
                    );
                }
            }
        }

        public Point[] GetPoints(TextReader textReader)
        {
            List<Point> points = new List<Point>();

            string line;
            Console.WriteLine("Start Get points");

            while ((line = textReader.ReadLine()) != null)
            {
                string[] numbers = line.Split(' ');

                points.Add(new Point(int.Parse(numbers[0]), int.Parse(numbers[1])));

                Console.WriteLine(line);
            }

            ps = points.ToArray();
            return ps;
        }
    }
}
