namespace Lab6_3
{
    public class tuple2d
    { 
        public double X; 
        public double Y; 

        public tuple2d(double x = 0.0, double y = 0.0)
        {
            X = x;
            Y = y;
        }
        
        public tuple2d(string x, string y)
        {
            X = double.Parse(x);
            Y = double.Parse(y);
        }
    }
}
