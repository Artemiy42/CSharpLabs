using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab3_4
{
    abstract class Converter : IComparable<Converter>
    {
        public static int sortOrder = 1;
        protected static string[] sep = {" ", "\t"};
        public abstract Converter str2rec(string rec);
        public abstract void rec2bin(BinaryWriter bw);
        public abstract Converter bin2rec(BinaryReader br);

        public virtual int CompareTo(Converter other)
        {
            return 0;
        }
    }

    class int2 : Converter
    {
        int a;
        int b;

        public int2() : this(0, 0)
        {
        }

        public int2(int x, int y)
        {
            a = x;
            b = y;
        }

        public override int CompareTo(Converter other)
        {
            int res;
            
            if (sortOrder == 2)
            {
                res = b.CompareTo(((int2) other).b);
            }
            else
            {
                res = a.CompareTo(((int2) other).a);
            }

            return res;
        }

        public override Converter str2rec(string rec)
        {
            string[] flds = rec.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (flds.Length >= 2)
            {
                a = int.Parse(flds[0]);
                b = int.Parse(flds[1]);
            }

            return new int2(a, b);
        }

        public override Converter bin2rec(BinaryReader br)
        {
            a = br.ReadInt32();
            b = br.ReadInt32();
            
            return new int2(a, b);
        }

        public override void rec2bin(BinaryWriter bw)
        {
            bw.Write(a);
            bw.Write(b);
        }

        public override string ToString()
        {
            return $"{a} {b}";
        }
    }
    
    class int3 : Converter
    {
        int a;
        int b;
        int c;

        public int3() : this(0, 0, 0)
        {
        }

        public int3(int x, int y, int z)
        {
            a = x;
            b = y;
            c = z;
        }

        public override int CompareTo(Converter other)
        {
            int res;
            
            if (sortOrder == 2)
            {
                res = b.CompareTo(((int3) other).b);
            }
            else if (sortOrder == 3)
            {
                res = c.CompareTo(((int3) other).c);
            }
            else
            {
                res = a.CompareTo(((int3) other).a);
            }

            return res;
        }

        public override Converter str2rec(string rec)
        {
            string[] flds = rec.Split(sep, StringSplitOptions.RemoveEmptyEntries);

            if (flds.Length >= 3)
            {
                a = int.Parse(flds[0]);
                b = int.Parse(flds[1]);
                c = int.Parse(flds[2]);
            }

            return new int3(a, b, c);
        }
        
        public override Converter bin2rec(BinaryReader br)
        {
            a = br.ReadInt32();
            b = br.ReadInt32();
            c = br.ReadInt32();
            
            return new int3(a, b, c);
        }

        public override void rec2bin(BinaryWriter bw)
        {
            bw.Write(a);
            bw.Write(b);
            bw.Write(c);
        }
        
        public override string ToString()
        {
            return $"{a} {b} {c}";
        }
    }

    class double2 : Converter
    {
        double a;
        double b;

        public double2() : this(0.0, 0.0)
        {
        }

        public double2(double x, double y)
        {
            a = x;
            b = y;
        }

        public override int CompareTo(Converter other)
        {
            int res;
            
            if (sortOrder == 2)
            {
                res = b.CompareTo(((double2) other).b);
            }
            else
            {
                res = a.CompareTo(((double2) other).a);
            }

            return res;
        }

        public override Converter str2rec(string rec)
        {
            string[] flds = rec.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            
            if (flds.Length >= 2)
            {
                a = double.Parse(flds[0]);
                b = double.Parse(flds[1]);
            }

            return new double2(a, b);
        }
        
        public override Converter bin2rec(BinaryReader br)
        {
            a = br.ReadDouble();
            b = br.ReadDouble();
            
            return new double2(a, b);
        }

        public override void rec2bin(BinaryWriter bw)
        {
            bw.Write(a);
            bw.Write(b);
        }
        
        public override string ToString()
        {
            return $"{a} {b}";
        }
    }
    
    class double3 : Converter
    {
        double a;
        double b;
        double c;

        public double3() : this(0.0, 0.0, 0.0)
        {
        }

        public double3(double x, double y, double z)
        {
            a = x;
            b = y;
            c = z;
        }

        public override int CompareTo(Converter other)
        {
            int res;
            
            if (sortOrder == 2)
            {
                res = b.CompareTo(((double3) other).b);
            }
            else if (sortOrder == 3)
            {
                res = c.CompareTo(((double3) other).c);
            }
            else
            {
                res = a.CompareTo(((double3) other).a);
            }

            return res;
        }

        public override Converter str2rec(string rec)
        {
            string[] flds = rec.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            
            if (flds.Length >= 3)
            {
                a = double.Parse(flds[0]);
                b = double.Parse(flds[1]);
                c = double.Parse(flds[2]);
            }

            return new double3(a, b, c);
        }
        
        public override Converter bin2rec(BinaryReader br)
        {
            a = br.ReadDouble();
            b = br.ReadDouble();
            c = br.ReadDouble();

            return new double3(a, b, c);
        }

        public override void rec2bin(BinaryWriter bw)
        {
            bw.Write(a);
            bw.Write(b);
            bw.Write(c);
        }
        
        public override string ToString()
        {
            return $"{a} {b} {c}";
        }
    }

    class Program
    {
        static int Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            
            string fileName = String.Empty;
            char sep = ' ';
            int sortOrder = 1;
            int convertOrder = 0;
            bool vSort = false;
            bool vDouble = false;
            bool v3Column = false;
            bool vFlag = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-?" || args[i].ToLower() == "-help")
                {
                    Console.WriteLine(
                        "a.exe [-? | -help] [-v] [-sort] [-s Number] [-sep SEP] [-3] [-double] {-txt2bin | -bin2txt} -f fileName\n" +
                        "де\n" +
                        "-help       : отримання цієї справки\n" +
                        "-?          : отримання цієї справки\n" +
                        "-v          : видача помилок в файлі та часу роботи програми\n" +
                        "-sort       : сортувати зчитані дані\n" +
                        "-s Number   : Number - номер поля для сортування (за замовчуванням по 1 колонці)\n" +
                        "-sep SEP    : задати роздільник. За замовчуванням – символ пробілу\n" +
                        "-txt2bin    : направлення конвертації - із стандартоного вводу в двійковий\n" +
                        "-bin2txt    : направлення конвертації - із двійкового в стандартний вивід\n" +
                        "-3          : друк записів в 3 колонки (за замовчування друкується 2)\n" +
                        "-double     : друк запісів в форматі з плавоючою комою (за замовчуванням дукуються цілі)\n" +
                        "-f FileName : имя файла для чтения\n");
                    return 1;
                }
                else if (args[i].ToLower() == "-s")
                {
                    sortOrder = setInt(++i, args, "номер");
                }
                else if (args[i].ToLower() == "-f")
                {
                    fileName = setString(++i, args, "ім\'я файла");
                }
                else if (args[i].ToLower() == "-v")
                {
                    vFlag = true;
                }
                else if (args[i].ToLower() == "-sort")
                {
                    vSort = true;
                }
                else if (args[i].ToLower() == "-3")
                {
                    v3Column = true;
                }
                else if (args[i].ToLower() == "-double")
                {
                    vDouble = true;
                }
                else if (args[i].ToLower() == "-bin2txt")
                {
                    if (convertOrder != 0)
                    {
                        Console.WriteLine("Напрямок конвертації вже визначений!");
                        return 2;
                    }

                    convertOrder = 2;
                }
                else if (args[i].ToLower() == "-txt2bin")
                {
                    if (convertOrder != 0)
                    {
                        Console.WriteLine("Напрямок конвертації вже визначений!");
                        return 2;
                    }

                    convertOrder = 1;
                }
                else if (args[i].ToLower() == "-sep")
                {
                    sep = setChar(++i, args, "роздільник");
                }
            }

            if (fileName == String.Empty)
            {
                Console.WriteLine("Задайте ім\'я файлу для читання за допомогою ключа -f !");
                return 2;
            }

            if (sortOrder < 1 || sortOrder > 3)
            {
                Console.WriteLine("Задана неіснуюча колонка для сортування!");
                return 2;
            }

            if (convertOrder == 0)
            {
                Console.WriteLine("Не визначений напрямок конвертації!");
                return 2;
            }

            Converter.sortOrder = sortOrder;
            DateTime start = DateTime.Now;
            Converter rec;

            if (vDouble)
            {
                if (v3Column)
                {
                    rec = new double3();
                }
                else
                {
                    rec = new double2();
                }
            }
            else
            {
                if (v3Column)
                {
                    rec = new int3();
                }
                else
                {
                    rec = new int2();
                }
            }

            if (convertOrder == 1)
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Create)))
                {
                    txt2bin(Console.In, bw, vSort, rec);
                }
            }
            else
            {
                using (BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    binToTxt(Console.Out, br, vSort, rec);
                }
            }
            
            if (vFlag)
            {
                DateTime end = DateTime.Now;
                Console.WriteLine("{0} сек.", (end - start).TotalSeconds);
            }

            return 0;
        }

        public static void binToTxt(TextWriter tw, BinaryReader br, bool sort, Converter rec)
        {
            List<Converter> ls = new List<Converter>();

            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                ls.Add(rec.bin2rec(br));
            }
            
            if (sort)
            {
                ls.Sort();
            }
            
            for (int i = 0; i < ls.Count; i++)
            {
                tw.WriteLine(ls[i]);
            }
        }

        public static void txt2bin(TextReader tr, BinaryWriter bw, bool sort, Converter rec)
        {
            string r;
            List<Converter> ls = new List<Converter>();
            
            while ((r = tr.ReadLine()) != null)
            {
                ls.Add(rec.str2rec(r));
            }

            if (sort)
            {
                ls.Sort();
            }
            
            for (int i = 0; i < ls.Count; i++)
            {
                ls[i].rec2bin(bw);
            }
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