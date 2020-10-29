using System;
using System.Text;

namespace Lab2_4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            PritntCommandArgument(args);
            WorkingWithCharString();

            UTF8ToWin1251("Це якийсь текст ііі");
            Console.WriteLine("Це якийсь текст ііі");

            Console.ReadKey();
        }

        public static void PritntCommandArgument(string[] args)
        {
            StringBuilder allArguments = new StringBuilder();

            for (int i = 0; i < args.Length; i++)
            {
                allArguments.Append(args[i]);
                allArguments.Append("\n");
            }

            Console.WriteLine(allArguments);
        }

        public static void WorkingWithCharString()
        {
            char c = 'A';

            string str = "Some string = ";

            str = str + c; // "Some string = A"

            Console.WriteLine(str);

            StringBuilder stringBuilder = new StringBuilder("text");

            stringBuilder.Append("+"); // "text+"
            stringBuilder.Append("newText"); // "text+newText"

            Console.WriteLine(stringBuilder);
        }

        public static string Win1251ToUTF8(string str)
        {
            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            byte[] win1251Bytes = utf8.GetBytes(str);
            byte[] utf8Bytes = Encoding.Convert(win1251, utf8, win1251Bytes);

            Console.WriteLine(utf8.GetString(utf8Bytes));

            return str;
        }

        public static string UTF8ToWin1251(string str)
        {
            Encoding utf8 = Encoding.GetEncoding("UTF-8");
            Encoding win1251 = Encoding.GetEncoding("Windows-1251");

            byte[] utf8Bytes = win1251.GetBytes(str);
            byte[] win1251Bytes = Encoding.Convert(utf8, win1251, utf8Bytes);

            Console.WriteLine(win1251.GetString(win1251Bytes));

            return str;
        }
    }
}