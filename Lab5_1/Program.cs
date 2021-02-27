using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab5_1
{
    static class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            int leftOffset = 0;
            int topOffset = 0;
            string text = string.Empty;
            string controlObject = string.Empty;
            Dictionary<string, int> availableOjects = new Dictionary<string, int>() { { "button", 1 }, { "label", 2 }, { "textbox", 3 } };
            bool vFlag = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-?" || args[i].ToLower() == "-help")
                {
                    Console.WriteLine(
                        "Programma dlya vstavki v formu knopki, nadpisi ili tekstovogo polya." +
                        "a.exe [-? | -help] [-v] [-x N] [-y N] [-w T] -k <button/label/textbox>" +
                        "де\n" +
                        "-? | -help  : отримання цієї справки\n" +
                        "-v          : видача помилок в файлі та часу роботи програми\n" +
                        "-x          : відступ об'єкту від лівого краю. N - число\n" +
                        "-y          : відступ об'єкту від верхнього краю. N - число\n" +
                        "-w          : текст на об'єкті. T- текст\n" +
                        "-k          : вибір об'єкту (button, label ili textbox)\n");

                    return 1;
                }
                else if (args[i].ToLower() == "-v")
                {
                    vFlag = true;
                }
                else if (args[i].ToLower() == "-x")
                {
                    leftOffset = setInt(++i, args, "відступ від лівого краю");
                }
                else if (args[i].ToLower() == "-y")
                {
                    topOffset = setInt(++i, args, "відступ від верхнього краю");
                }
                else if (args[i].ToLower() == "-w")
                {
                    text = setString(++i, args, "текст на об'єкті");
                }
                else if (args[i].ToLower() == "-k")
                {
                    controlObject = setString(++i, args, "вибір об'єкту");
                }
            }

            if (!availableOjects.ContainsKey(controlObject))
            {
                Console.WriteLine($"Об'єкт {0} не підтримується, будь-ласка відкрийте справку та перегляньте доступні об'єкти\n", controlObject);
                return 1;
            }

            Window mainWindow = new Window(leftOffset, topOffset, availableOjects[controlObject], text);
            Application.Run(mainWindow);

            return 0;
        }

        public static int setInt(int i, string[] args, string par)
        {
            int ret = 0;

            if (i < args.Length)
            {
                if (!int.TryParse(args[i], out ret))
                {
                    Console.WriteLine($"Значення для параметра {0} не правильного типу!", par);
                    Environment.Exit(2);
                }
            }
            else
            {
                Console.WriteLine($"Значення для параметра {0} не задано!", par);
                Environment.Exit(2);
            }

            return ret;
        }

        public static string setString(int i, string[] args, string par)
        {
            string ret = string.Empty;

            if (i < args.Length)
            {
                ret = args[i];
            }
            else
            {
                Console.WriteLine($"Значення для параметра {0} не задано!", par);
                Environment.Exit(2);
            }

            return ret;
        }
    }

    class Window : Form
    {
        public Window(int x, int y, int f, string w)
        {
            this.Width = 300; // изменение размера окна
            this.Height = 200;
            
            Control cnt;
            
            if (f == 1)
                cnt = new Button();
            else if (f == 2)
                cnt = new Label();
            else if (f == 3)
                cnt = new TextBox();
            else
                return;
            
            // cnt.Location = new Point(x, y);
            cnt.Left = x; // изменение координат
            cnt.Top = y;  // левого верхнего угла
            cnt.Text = w; // управляющего элемента
            
            Controls.Add(cnt);
        }
    }
}
