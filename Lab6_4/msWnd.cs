using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab6_4
{
    class msWnd : w4rLine
    {
        Boolean mouseDown;
        PointF MousePoint1;

        public msWnd(rLine l) : base(l)
        {
            mouseDown = false;
            pan.MouseDown += Form1_MouseDown;
            pan.MouseUp += Form1_MouseUp;
            pan.MouseMove += Form1_MouseMove;
        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MousePoint1 = e.Location;
            mouseDown = true;
        }
        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        
        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                PointF MousePoint2 = e.Location;
                pan.mp.moveEastWest((int)((MousePoint1.X - MousePoint2.X) * 100) / ClientSize.Width);
                pan.mp.moveNordSouth(-(((int)(MousePoint1.Y - MousePoint2.Y)) * 100) / ClientSize.Height);
                pan.mkSLine(rl); 
                pan.Invalidate();
                MousePoint1 = e.Location;
            }
        }
    }
}
