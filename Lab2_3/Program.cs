using System;

namespace Lab2_3
{
    internal class Program
    {
        private static int number = 0;
        private static string line = string.Empty;
        private static Coordinate coordinate = new Coordinate();

        public static void Main(string[] args)
        {
            ShowMath();

            DrawVariables(number, line, coordinate);
            number = ChangeInt(number);
            line = ChangeString(line);
            coordinate = ChangeStruct(coordinate);
            DrawVariables(number, line, coordinate);
        }

        public static int ChangeInt(int number)
        {
            return ++number;
        }

        public static string ChangeString(string line)
        {
            return line += 'a';
        }

        public static Coordinate ChangeStruct(Coordinate coordinate)
        {
            coordinate.x++;
            coordinate.y++;
            return coordinate;
        }

        public static void DrawVariables(int number, string line, Coordinate coordinate)
        {
            Console.WriteLine("\nnumber = {0}\nline = {1}\ncoordinate = ({2};{3})", number, line, coordinate.x, coordinate.y);
        }

        public static void ShowMath()
        {
            MyMath myMath = new MyMath();

            Console.WriteLine(@"Додавання двох цілих чисел: 5 + 3 = {0}", MyMath.Add(5, 3));
            Console.WriteLine(@"Додавання двох чисел з плаваючою комою: 5.9 + 3.6 = {0}", MyMath.Add(5.9, 3.6));
            Console.WriteLine(@"Множення двох цілих чисел: 5 * 3 = {0}", myMath.Multiply(5, 3));
            Console.WriteLine(@"Множення двох чисел з плаваючою комою: 5.9 * 3.6 = {0}", myMath.Multiply(5.9, 3.6));
        }
    }
    
    public struct Coordinate
    {
        public int x;
        public int y;
    }
    
    public class MyMath
    {
        public static int Add(int first, int second)
        {
            return first + second;
        }

        public static double Add(double first, double second)
        {
            return first + second;
        }

        public int Multiply(int first, int second)
        {
            return first * second;
        }

        public double Multiply(double first, double second)
        {
            return first * second;
        }
    }
    
}