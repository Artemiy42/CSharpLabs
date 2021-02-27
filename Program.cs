using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Lab5_1
{
    class Program
    {
        static int Main(string[] args)
        {
            string outPath = string.Empty;
            string encoding = string.Empty;
            char sep = ' ';
            bool vFlag = false;
            Dictionary<int, string> fields = new Dictionary<int, string>();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-?" || args[i].ToLower() == "-help")
                {
                    Console.WriteLine(
                        "Programma dlya vstavki v formu knopki, nadpisi ili tekstovogo polya." +
                        "a.exe [-? | -help] [-v] [-x * **] [-y * **] [-w * **] -k <button/label/textbox>" +
                        "де\n" +
                        "-? | -help  : отримання цієї справки\n" +
                        "-v          : видача помилок в файлі та часу роботи програми\n" +
                        "-x          : відступ об'єкту від лівого краю\n" +
                        "-y          : відступ об'єкту від верхнього краю\n" +
                        "-w          : текст на об'єкті\n" +
                        "-k          : вибір об'єкту (button, label ili textbox)\n");
                    
                    return 1;
                }
                else if (args[i].ToLower() == "-v")
                {
                    vFlag = true;
                }
                else if (args[i].ToLower() == "-x")
                {
                    outPath = setString(++i, args, "вихідних шлях");
                }
                else if (args[i].ToLower() == "-y")
                {
                    encoding = setString(++i, args, "кодування стандартного вводу");
                }
                else if (args[i].ToLower() == "-w")
                {
                    sep = setChar(++i, args, "роздільник");
                }
                else if (args[i].ToLower() == "-k")
                {
                    if (++i < args.Length)
                    {
                        addField(args[i], fields);
                    }
                    else
                    {
                        Console.WriteLine($"Значення для параметра поле не задано!");
                        Environment.Exit(2);
                    }
                }
            }
        }
    }

    class wnd : Form
    {
        public
        wnd(int x, int y, int f, string w)
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
            cnt.Top = y; // левого верхнего угла
            cnt.Text = w; // управляющего элемента
            Controls.Add(cnt);
        }
    }
}
