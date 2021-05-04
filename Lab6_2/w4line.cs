using System.Windows.Forms;

namespace Lab6_2
{
    public class w4line : Form
    {
        private p4sLine pan;

        public w4line(sLine ln)
        {
            Padding = new Padding(10);
            pan = new p4sLine(ln);
            Controls.Add(pan);
        }
    }
}
