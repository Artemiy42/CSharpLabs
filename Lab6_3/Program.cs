using System;
using System.Windows.Forms;

namespace Lab6_3
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            rLine ln = new rLine("test real line");
            ln.pnts = rLine.GetPoints(Console.In, true); 
            Application.Run(new w4rLine(ln));
        }
    }
}
