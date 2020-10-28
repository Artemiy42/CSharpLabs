using System;

namespace Lab2_2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Введіть кінець діапазона від 2 до : ");

            int maxNumber = int.Parse(Console.ReadLine());

            DateTime start = DateTime.Now;

            long[] array = new long[maxNumber + 1];

            for (int i = 0; i < maxNumber + 1; i++)
                array[i] = i;

            for (long p = 2; p < maxNumber + 1; p++)
            {
                if (array[p] != 0)
                {
                    for (long j = p * p; j < maxNumber + 1; j += p)
                    {
                        array[j] = 0;
                    }
                }
            }

            int freeNumber = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != 0)
                {
                    array[freeNumber] = array[i];
                    array[i] = 0;
                    
                    while (array[freeNumber] != 0)
                    {
                        freeNumber++;
                    } 
                }
            }

            Array.Resize(ref array, freeNumber);

            for (long i = 0; i < array.Length; i++)
            {
                Console.Write("{0} ", array[i]);
            }

            DateTime end = DateTime.Now;
            Console.WriteLine("\n{0} сек.", (end - start).TotalSeconds);
        }
    }
}