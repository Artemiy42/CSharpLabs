using System;
using System.IO;
using System.Net;

namespace Lab2_6
{
    internal class Program
    {
        private static bool flagString;
        private static int flagComment;
        private static bool isComment;
        private static bool isLineComment;

        private static StreamWriter file;

        private static bool isMultilineComment;

        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Програма приймає на вхід шлях файлу з коментарями." + 
                                  "Приклад запуску програми: Lab2_6.exe Text.txt");
            }

            string path = args[0];
            
            string[] lines = File.ReadAllLines(path);
            file = new StreamWriter(path);

            for (int i = 0; i < lines.Length; i++)
            {
                CheckString(lines[i]);
            }
        
            file.Close();
        }

        private static void CheckString(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '/':
                        if (!flagString)
                        {
                            if (isMultilineComment)
                            {
                                if (flagComment == 1)
                                {
                                    isMultilineComment = false;
                                }
                            }
                            else
                            {
                                ++flagComment;

                                if (flagComment == 2)
                                {
                                    flagComment = 0;
                                    file.Write('\n');
                                    return;
                                }
                            }
                        }
                        break;
                    case '*':
                        if (!flagString)
                        {
                            if (isMultilineComment)
                            {
                                --flagComment;
                            }
                            else
                            {
                                ++flagComment;
                                
                                if (flagComment == 2)
                                {
                                    isMultilineComment = true;
                                }
                                else
                                {
                                    flagComment = 0;
                                }
                            }
                        }
                        break;
                    case '\"':
                        flagString = !flagString;
                        file.Write(str[i]);
                        break;
                    default:
                        if (!isMultilineComment)
                        {
                            file.Write(str[i]);
                        }
                        break;
                }
            }

            if (!isMultilineComment)
            {
                file.Write('\n');
            }
        }
    }
}