using System;

namespace Lab2_5
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int latitude;
            int longitude;
            
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-?" || args[i] == "/?")
                {
                    Console.WriteLine("");
                }
                else if (args[i] == "-ltt" || args[i] == "/ltt")
                {
                    if (i + 1 < args.Length)
                    {
                        if (!int.TryParse(args[i + 1], out latitude))
                        {
                            Console.WriteLine("Після ключю -ltt повинно йти число!");
                        }
                    }
                }
                else if (args[i] == "-lng" || args[i] == "/lng")
                {
                    if (i + 1 < args.Length)
                    {
                        if (!int.TryParse(args[i + 1], out longitude))
                        {
                            Console.WriteLine("Після ключю -lng повинно йти число!");
                        }
                    }
                }
                
            }
            
            
        }
    }
}