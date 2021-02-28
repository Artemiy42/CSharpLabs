using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5_2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            OkCancel w = new OkCancel();
            DialogResult rc = w.ShowDialog();
            Console.WriteLine("result is {0}", rc);
        }
    }

    public class OkCancel : Form
    {
        Button okButton;
        Button cancleButton;
        int p = 4;

        public OkCancel()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            AutoScroll = false;
            Padding = new Padding(p, p, p, p);
            AutoSize = true;

            Panel p0 = new Panel(); // эта панель задает высоту строки
            p0.Size = new Size(1, 32);
            p0.Dock = DockStyle.Top;
            Controls.Add(p0);

            CreateOkButton();

            Panel p1 = new Panel(); // эта панель задает расстояние между кнопками
            p1.Size = new Size(112, 32);
            p1.Dock = DockStyle.Right;
            Controls.Add(p1);

            CreateCancleButton();

            DialogResult = DialogResult.Cancel;
            AcceptButton = okButton; // нажатие клавиши Enter как на ок
            CancelButton = cancleButton; // нажатие клавиши Esc КАК НА Cancel*/

            Controls.Add(okButton);
            Controls.Add(cancleButton);
        }

        public void CreateOkButton()
        {
            okButton = new Button();
            okButton.Size = new Size(112, 32);
            okButton.Dock = DockStyle.Right; // вправо
            okButton.Text = "Ok";
            okButton.DialogResult = DialogResult.OK;
        }

        public void CreateCancleButton()
        {
            cancleButton = new Button();
            cancleButton.Size = new Size(112, 32);
            cancleButton.Dock = DockStyle.Right;
            cancleButton.Text = "Cancel";
            cancleButton.DialogResult = DialogResult.Cancel;
        }
    }
}
