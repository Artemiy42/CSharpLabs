using System;
using System.Windows.Forms;

namespace Lab6_2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            sLine ln = new sLine();
            ln.nm = "test line";
            ln.GetPoints(Console.In);
            Application.Run(new w4line(ln));
        }
    }
}
