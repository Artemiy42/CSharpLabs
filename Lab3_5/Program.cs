using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace Lab3_5
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
                        "a.exe [-? | -help] [-v] -o PATH [-e ENC] [-s CHAR] [-f FLDNM1 [-f FLDNM2]...]" +
                        "де\n" +
                        "-help       : отримання цієї справки\n" +
                        "-?          : отримання цієї справки\n" +
                        "-v          : видача помилок в файлі та часу роботи програми\n" +
                        "-o          : вихідний шлях XML файла\n" +
                        "-f FLDNMx   : встановити ім'я вказаного поля (номер:ім'я)\n" +
                        "-e ENC      : кодування стандартного вводу (866, 1251, UTF - 8)\n" +
                        "-s CHAR     : роздільник\n");
                    return 1;
                }
                else if (args[i].ToLower() == "-o")
                {
                    outPath = setString(++i, args, "вихідних шлях");
                }
                else if (args[i].ToLower() == "-v")
                {
                    vFlag = true;
                }
                else if (args[i].ToLower() == "-e")
                {
                    encoding = setString(++i, args, "кодування стандартного вводу");
                }
                else if (args[i].ToLower() == "-s")
                {
                    sep = setChar(++i, args, "роздільник");
                }
                else if (args[i].ToLower() == "-f")
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

            if (outPath == string.Empty)
            {
                Console.WriteLine("Необхідно задачи шлях вихідного XML файлу!");
                return 2;
            }

            if (encoding == "866")
            {
                Console.InputEncoding = Encoding.GetEncoding("cp866");
            }
            else if (encoding == "1251")
            {
                Console.InputEncoding = Encoding.GetEncoding("windows-1251");
            }
            else if (encoding == "UTF-8")
            {
                Console.InputEncoding = Encoding.GetEncoding("utf-8");
            }   
            else if (encoding != string.Empty)
            {
                Console.WriteLine("Задане кодування не підтримується!");
                return 2;
            }

            DateTime start = DateTime.Now;

            XmlDocument xd = new XmlDocument();
            xd.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<Table></Table>");
            XmlNode tbl = xd.DocumentElement;

            XmlNode attr;
            XmlNode rec;
            int recNo = 0;
            string str = null;
            string[] vals;

            while ((str = Console.ReadLine()) != null)
            {
                vals = getRec(str, sep);
                if (vals.Length == 1 && vals[0].Length <= 0) continue;
                recNo++;
                rec = xd.CreateElement("Record");
                attr = xd.CreateNode(XmlNodeType.Attribute, "No", null);
                attr.Value = recNo.ToString();
                rec.Attributes.SetNamedItem(attr);
                
                XmlNode fld;
                for (int i = 0; i < vals.Length; i++)
                {
                    string nm = null;
                    if (fields != null && fields.ContainsKey(i))
                    {
                        nm = fields[i];
                    }
                    else
                    {
                        nm = String.Format("field_{0}", i);
                    }

                    fld = xd.CreateElement(nm);
                    fld.InnerText = vals[i];
                    rec.AppendChild(fld);
                }

                tbl.AppendChild(rec);
            }
                       

            xd.Save(outPath);

            if (vFlag)
            {
                DateTime end = DateTime.Now;
                Console.WriteLine("{0} сек.", (end - start).TotalSeconds);
            }

            return 0;
        }

        static void addField(string field, Dictionary<int, string> fldDict)
        {
            string[] val = field.Split(':');

            try
            {
                int k = int.Parse(val[0]);

                if (fldDict.ContainsKey(k))
                    return;

                fldDict.Add(k, val[1]);
            }
            catch
            {
                Console.Error.WriteLine("wrong parameter: {0}", field);
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
