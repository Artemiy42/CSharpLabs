using System;
using System.Windows.Forms;

namespace Lab3_3
{
    class Window : Form
    {
        private static DialogResult result;

        public Window(string windowName)
        {
            Text = windowName;
            FormClosing += FormClossing;
        }

        [STAThread]
        static void Main()
        {
            Window window1 = new Window("Головне вікно");

            result = MessageBox.Show("Ви маєте парний номер залікової книги?", 
                "Питання", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);
            Application.Run(window1);
        }

        private void FormClossing(object sender, FormClosingEventArgs args) 
        {
            if (args.CloseReason == CloseReason.UserClosing)
            { 
                if (result == DialogResult.Yes)
                {
                    ShowQuestionBox(args);
                }
                else if (result == DialogResult.No)
                {
                    ShowErrorBox(args);
                }
            }
        }

        private void ShowQuestionBox(FormClosingEventArgs args)
        {
            DialogResult resul = MessageBox.Show("Ви впевнені?", "", MessageBoxButtons.YesNo);

            if (resul == DialogResult.Yes)
            {
                args.Cancel = false;
            }
            else
            {
                args.Cancel = true;
            }
        }

        private void ShowErrorBox(FormClosingEventArgs args)
        {
            DialogResult resul = MessageBox.Show("Ви впевнені?", "", MessageBoxButtons.OKCancel);

            if (resul == DialogResult.OK)
            {
                args.Cancel = false;
            }
            else
            {
                args.Cancel = true;
            }
        }
    }
}
