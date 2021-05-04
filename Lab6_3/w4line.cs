using System.Windows.Forms;

namespace Lab6_3
{
    class w4line : Form
    {
        protected rLine rl;
        
        public w4line(rLine l)
        {
            rl = l;
            Padding = new Padding(10);
        }
    }
}
