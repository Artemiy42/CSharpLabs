using System;

namespace Lab6_3
{
    public class mapping
    {
        int l, r, t, b;
        bool isWellformed = false;
        public double xMin, xMax, yMin, yMax;

        public mapping() : this(0.0, 1.0, 0.0, 1.0)
        {
            this.l = 0;
            this.b = 0;
            this.r = 255;
            this.t = 255;
        }

        public mapping(double x, double X, double y, double Y)
        {
            xMin = x;
            xMax = X;
            yMin = y;
            yMax = Y;
            
            if (xMin == xMax)
            {
                xMax = xMin + 0.01;
            }

            if (yMin == yMax)
            {
                yMax = yMin + 0.01;
            }

            this.l = 1;
            this.b = 1;
            this.r = 400;
            this.t = 300;
            isWellformed = true;
        }
        
        public int w
        {
            get { return r + l; } 
        }
     
        public int h
        {
            get { return t + b; } 
        }

        public void map(double X, double Y, out int x, out int y)
        {
            if (isWellformed)
            {
                x = Convert.ToInt32(Math.Round((X - xMin) / (xMax - xMin) * (r - l) + l));
                y = Convert.ToInt32(Math.Round((Y - yMin) / (yMax - yMin) * (t - b) + b));
            }
            else
            {
                x = 0; y = 0;
            }
        }

        public void moveNordSouth(int shift)
        {
            if (isWellformed)
            {
                double percent = (yMax - yMin) / 100;
                yMax += percent * shift;
                yMin += percent * shift;
            }
        }

        public void moveEastWest(int shift)
        {
            if (isWellformed)
            {
                double percent = (xMax - xMin) / 100;
                xMax += percent * shift;
                xMin += percent * shift;
            }
        }

        public void zoom(int shift)
        {
            if (isWellformed)
            {
                double percent = (xMax - xMin) / 100;
                yMax += percent * shift;
                yMin -= percent * shift;
                xMax += percent * shift;
                xMin -= percent * shift;
            }
        }
    }
}

