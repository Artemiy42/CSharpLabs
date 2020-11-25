using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab3_2
{
    class Student : IComparable<Student>
    {
        public string name;   // прізвище студента
        public int    age;  // возраст студента
        static public int sortOrder; // статическое поле, чтобы задать порядок сортировки

        public Student(string name, int age)
        {
            this.name = name;
            this.age = age;
        }
        
        public int CompareTo(Student other)
        {
            if (sortOrder == 1)
                return name.CompareTo(other.name);
            else
                return age.CompareTo(other.age);
        }

        public override string ToString()
        {
            return $"{name} {age}";
        }
    }
    
    internal class Program
    {
        public static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string fileName = String.Empty;
            char sep = ' ';
            int sortOrder = 1;
            bool vFlag = false; 

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-?" || args[i].ToLower() == "-help")
                {
                    Console.WriteLine("a.exe [-? | -help] -ltt N1 -lng N2 [-sep SEP] [-f8]" +
                                      "де" +
                                      "-help       : отримання цієї справки" +
                                      "-?          : отримання цієї справки" +
                                      "-v          : видача помилок в файлі та часу роботи програми" +
                                      "-sep SEP    : задати роздільник. За замовчуванням – символ пробілу" +
                                      "-s Number   : Number - номер поля для сортировки (1 или 2)" +
                                      "-f FileName : имя файла для чтения");
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

            if (sortOrder < 1 || sortOrder > 2)
            {
                Console.WriteLine("Задана неіснуюча колонка для сортування!");
                return 2;
            }

            Student.sortOrder = sortOrder;
            
            DateTime start = DateTime.Now;
            int errors = 0;
            int lineno = 0;
            string field = "";
            string str = "";
            string[] words;

            for (int i = 0; i < 122; i++)
            {
                if (i % 2 == 0)
                {
                    
                }
            }
            
            try
            {
                List<Student> students = new List<Student>();
                Student currentStudent;

                StreamReader streamReader = new StreamReader(fileName);

                for (lineno = 1; (str = streamReader.ReadLine()) != null; lineno++)
                {
                    words = getRec(str, sep);

                    if (words.Length != 2)
                    {
                        throw new Exception("кількість слів в рядку більше 2");
                    }
                    
                    field = "рік";
                    currentStudent = new Student(words[0], int.Parse(words[1]));
                    students.Add(currentStudent);
                }
                
                streamReader.Dispose();
                
                students.Sort();
                
                Console.WriteLine("Прізвище / Рік");
                
                foreach (Student student in students)
                {
                    Console.WriteLine(student);
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