using System;
using System.IO;
using System.Text;

namespace Lab2_5
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string filePath = @"E:\Programming\C#\Projects\СSharpLabs\Lab2_5\in.txt";
            int latitude = -1;
            int longitude = -1;
            char sep = ' ';
            bool f8 = false;
            bool vFlag = false; 

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-?" || args[i].ToLower() == "-help")
                {
                    Console.WriteLine("a.exe [-? | -help] -ltt N1 -lng N2 [-sep SEP] [-f8]" +
                                      "де" +
                                      "-help    : отримання цієї справки" +
                                      "-?       : отримання цієї справки" +
                                      "-v       : видача помилок в файлі та часу роботи програми" +
                                      "-sep SEP : задати роздільник. За замовчуванням – символ пробілу" +
                                      "-ltt N1  : N1 номер колонки з широтою" +
                                      "-lng N2  : N2 номер колонки з довготою" +
                                      "-f8      : виводити 8 чисел після коми, інакше – 6");
                    return 1;
                }
                else if (args[i].ToLower() == "-ltt")
                {
                    latitude = setInt(++i, args, "широти");
                }
                else if (args[i].ToLower() == "-lng")
                {
                    longitude = setInt(++i, args, "довготи");
                }
                else if (args[i].ToLower() == "-v")
                {
                    vFlag = true;
                }
                else if (args[i].ToLower() == "-sep")
                {
                    sep = setChar(++i, args, "роздільник");
                }
                else if (args[i].ToLower() == "-f8")
                {
                    f8 = true;
                }
            }

            if (longitude < 0)
            {
                Console.WriteLine("Задайте номер стовпчику для ширини за допомогою ключа -ltt!");
                return 2;
            }

            if (latitude < 0)
            {
                Console.WriteLine("Задайте номер стовпчику для довготи за допомогою ключа -lng!");
                return 2;
            }

            if (longitude == latitude)
            {
                Console.WriteLine("Значення номеру стовпчика для широти та довготи повинно бути різним!");
                return 2;
            }
            
            DateTime start = DateTime.Now;
            int errors = 0;
            int lineno = 0;
            string field = "";
            
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (lines.Length < longitude)
                {
                    Console.WriteLine("Введене значення номеру стовпчика для ширини більше кількості рядків в файлі!");
                    return 2;
                }
                
                if (lines.Length < latitude)
                {
                    Console.WriteLine("Введене значення номеру стовпчика для довготи більше кількості рядків в файлі!");
                    return 2;
                }

                field = "Широта";
                string[] words = getRec(lines[longitude - 1], sep);
                double aa = double.Parse(words[0]);
                
                field = "Довгота";
                words = getRec(lines[latitude - 1], sep);
                double bb = double.Parse(words[1]);

                if (f8)
                {
                    Console.WriteLine("{0:f8} {1:f8}", aa, bb);
                }
                else
                {
                    Console.WriteLine("{0:f6} {1:f6}", aa, bb);
                }
            }
            catch (Exception e)
            {
                if (vFlag)
                {
                    Console.Error.WriteLine ("Рядок/Поле: {0}/{1}\nException: '{2}'", lineno, field, e.Message);
                    errors++;
                }
            }

            if (vFlag)
            {
                Console.Error.WriteLine ("Під час обробки файлу було {0} помилок", errors);
                DateTime end = DateTime.Now;
                Console.WriteLine("{0} сек.", (end - start).TotalSeconds);
            }

            return 0;
        }
        
        public static char setChar(int i, string[] args, string par)
        {
            char c = ' ';

            if (i < args.Length)
            {
                if (!char.TryParse(args[i], out c))
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

            return c;
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

        public static string[] getRec(string line, char sep = ' ')
        {
            string[] flds = null;
            char[] seps;
            
            if (line != null)
            {
                int lastP = line.IndexOf('#');
                
                if (lastP >= 0) 
                    line = line.Substring(0, lastP);
                
                line = line.Trim(); 
                if (sep == ' ')
                {
                    seps = new char[2];
                    seps[0] = ' ';
                    seps[1] = '\t';
                    flds = line.Split(seps, StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    seps = new char[1];
                    seps[0] = sep; 
                    flds = line.Split(seps);
                }
            }

            return flds;
        }
    }
}