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
        [STAThread]
        static void Main(string[] args)
        {
            bool isLab5_2 = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-?" || args[i].ToLower() == "-help")
                {
                    Console.WriteLine(
                        "Programma dlya vstavki v formu knopki, nadpisi ili tekstovogo polya." +
                        "a.exe [-? | -help] [-2]" +
                        "де\n" +
                        "-? | -help  : отримання цієї справки\n" +
                        "-2          : запуск віконця з лабораторної 5.2. За замовчуванням запускається віконце з лаболаторної 5.3\n");

                    return;
                }
                else if (args[i].ToLower() == "-2")
                {
                    isLab5_2 = true;
                }
            }

            if (isLab5_2)
            {
                OkCancel w = new OkCancel("Text");
                DialogResult rc = w.ShowDialog();
                Console.WriteLine("result is {0}", rc);
            }
            else
            {
                Field a = new Field("Введіть ім'я користувача", "Admin");
                Field b = new Field("Пароль", "*****");
                Form w = new WindowFields("Введіть ім'я та пароль", a, b);

                DialogResult rc = w.ShowDialog();

                if (rc == DialogResult.OK)
                {
                    Console.WriteLine("Ви ввели пароль ’{0}’ ’{1}’", a.value, b.value);
                }
                else
                    Console.WriteLine("Ви відмовились від вводу");
            }
        }
    }
     
    public class Field
    {
        public string name;
        public string def;
        public string value;

        public Field(string name, string def)
        {
            this.name = name;
            this.def = def;
            value = string.Empty;
        }
    }

    public class WindowFields : OkCancel
    {
        Dictionary<TextBox, Field> fields = null;
        
        public WindowFields(string Title, Field a, Field b) : base(Title)
        {
            fields = new Dictionary<TextBox, Field>(); // создать список пар
            fields.Add(addFld(1, b), b); // добавили второе поле ввода
            fields.Add(addFld(0, a), a); // добавили первое поле ввода
            okButton.Click += _KeyDown;
        }
        public TextBox addFld (int n, Field par) // метод добавляет в окно новое поле ввода
        {
            int h = 32;

            Panel p = new Panel(); // панель содержит метку и поле ввода
            p.Name = string.Format("p{0}", n);
            p.BorderStyle = BorderStyle.FixedSingle;
            p.Size = new Size(1, h);
            p.AutoSize = true;
            p.Dock = DockStyle.Top;
            p.Padding = new Padding(0, 0, 0, 8);
            
            Panel p1 = new Panel(); // панель для высоты
            p1.BorderStyle = BorderStyle.FixedSingle;
            p1.Size = new Size(1, h);
            p1.Dock = DockStyle.Top; /// <--------
            p1.BackColor = Color.Blue;
            p.Controls.Add(p1);
            
            Label l1;
            l1 = new Label();
            l1.Name = string.Format("l{0}", n);
            l1.Size = new Size(172, h); ;
            l1.Text = par.name; // создать новую строку
            l1.Dock = DockStyle.Right;
            // Console.Error.WriteLine ("new field: ’{0}’/’{1}’/’{2}’"
            // , par.name, l1.Text, par.value);
            l1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            p.Controls.Add(l1);
            
            TextBox t1;
            t1 = new TextBox();
            t1.TabIndex = n;
            t1.Name = string.Format("t{0}", n); ;
            t1.Size = new Size(162, h);
            t1.Text = par.def;
            t1.Dock = DockStyle.Right;
            p.Controls.Add(t1);
            
            Controls.Add(p); /// готовое поле ввода
            
            return t1;
        }
        void _KeyDown(object sender, EventArgs e)
        {
            TextBox tb;
            Field f;
            foreach (KeyValuePair<TextBox, Field> Item in fields)
            {
                tb = Item.Key;
                f = Item.Value;
                f.value = tb.Text;
                tb.Text = f.def; /// а теперь восстанавливается значение по умолчанию.
                Console.Error.WriteLine("keyDown: ’{0}’:’{1}’ ", f.name, f.value);
            }
        }

    }

    public class OkCancel : Form
    {
        protected Button okButton;
        protected Button cancleButton;

        public OkCancel(string text)
        {
            Text = text;
            int p = 10;
            Padding = new Padding(p, p, p, p);

            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ControlBox = false;
            AutoScroll = false;
            AutoSize = true;
            Size = new Size(10, 10);

            Panel p0 = new Panel(); // эта панель задает высоту строки
            p0.Size = new Size(1, 32);
            p0.BorderStyle = BorderStyle.Fixed3D;
            p0.BackColor = Color.Green;
            Controls.Add(p0);

            CreateOkButton();

            p0.Size = new Size(112, 32);
            p0.Dock = DockStyle.Right;
            p0.BorderStyle = BorderStyle.Fixed3D;
            p0.BackColor = Color.Red;
            Controls.Add(p0);

            CreateCancleButton();

            DialogResult = DialogResult.Cancel;
            AcceptButton = okButton; // нажатие клавиши Enter как на ок
            CancelButton = cancleButton; // нажатие клавиши Esc КАК НА Cancel
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void CreateOkButton()
        {
            okButton = new Button();
            okButton.Size = new Size(112, 32);
            okButton.Dock = DockStyle.Right; // вправо
            okButton.Text = "Ok";
            okButton.DialogResult = DialogResult.OK;
            Controls.Add(okButton);
        }

        public void CreateCancleButton()
        {
            cancleButton = new Button();
            cancleButton.Size = new Size(112, 32);
            cancleButton.Dock = DockStyle.Right;
            cancleButton.Text = "Cancel";
            cancleButton.DialogResult = DialogResult.Cancel;
            Controls.Add(cancleButton);
        }
    }
}
