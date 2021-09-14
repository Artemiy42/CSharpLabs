using System;

namespace Exponent
{
    class Program
    {
        static void Main(string[] args)
        {
            // Входная матрица A, заполнять ее можешь как хочешь, это один из методов иницализовать массив
            double[,] A = { { 2, 2, 2 }, { 2, 2, 2 }, { 2, 2, 2 } };
            // Вызываем функцию Exp передавая матрицу A, параметр t и значение n - количество итераций
            double[,] exp = Exp(A, 1.5, 5);

            // Это просто вывод массива в консоль, думаю тебе это не пригодится, но тут в принципе ничего сложного нету
            int rExp = A.GetLength(0); // Получаем длинну масива
            int cExp = A.GetLength(1); // Получаем ширину масива

            // Проходимся по всему массиву и выводим все данные на экран
            for (int i = 0; i < rExp; i++)
            {
                for (int j = 0; j < cExp; j++)
                {
                    Console.Write(exp[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        // Функцию которая считает экспоненту
        public static double[,] Exp(double[,] A, double t, int n)
        {
            // Делаем exp единичной матрицей 3 на 3
            double[,] exp = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };

            // Повторяем действия n раз
            for (int i = 1; i < n; i++)
            {
                // Я бы мог записать все одной строчкой, но тогда читаемость кода была бы на 0, потому сохраняю промежуточные данные в переменных mult и divis
                // Этой строчкой мы получаем верхную часть уравнения, а именно A^i*t^i (у тебя там в уравнении k, но суть от того не меняется)
                // Функция Math.Pow(t, i) берет число t в степень i и возращает возведенное число
                // Функция PowMatrix(A, i) возводит матрцу A в степерь i и возращает возведенную матрицу
                // MultiplyMatrix(A, b) умножает матрицу на число, так как PowMatrix вернет матрицу, а Math.Pow вернет число
                double[,] mult = MultiplyMatrix(PowMatrix(A, i), Math.Pow(t, i));

                // Следующая строчка отображает следующуя часть уравнения, A^i*t^i / i!
                // Factorial(i) возращает факториал числа i
                // DivisionMatrix(mult, fact) выполняет деление матрици на число
                double[,] divis = DivisionMatrix(mult, Factorial(i));

                // AddMatrix выполняет сумирывание матриц
                // Подсчитав  A^i*t^i / i! прибовляем его к старому значению exp и сохраняем в переменной exp
                exp = AddMatrix(exp, divis);
            }


            return exp;
        }

        // Функция возращающая факториал числа number
        public static int Factorial(int number)
        {
            int fact = 1;

            for (int i = 2; i <= number; i++)
            {
                fact *= i;
            }

            return fact;
        }

        // Функция возводит матрицу A в степерь step (A^step)
        public static double[,] PowMatrix(double[,] A, int step)
        {
            double[,] B = A;

            for (int i = 0; i < step; i++)
            {
                B = MultiplyMatrix(A, B);
            }

            return B;
        }

        // Функция сумирует матрицу A и B (A + B)
        public static double[,] AddMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);

            for (int i = 0; i < rA; i++)
            {
                for (int j = 0; j < cA; j++)
                {
                    A[i, j] += B[i, j];
                }
            }

            return A;
        }

        // Функция делит матрицу A на число b (A / b)
        public static double[,] DivisionMatrix(double[,] A, double b)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);

            for (int i = 0; i < rA; i++)
            {
                for (int j = 0; j < cA; j++)
                {
                    A[i, j] /= b;
                }
            }

            return A;
        }

        // Функция умножает матрицу A на число b (A * b)
        public static double[,] MultiplyMatrix(double[,] A, double b)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);

            for (int i = 0; i < rA; i++)
            {
                for (int j = 0; j < cA; j++)
                {
                    A[i, j] *= b;
                }
            }

            return A;
        }

        // Функция умножает матрицу A на матрицу B (A * B)
        public static double[,] MultiplyMatrix(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);

            double temp = 0;
            double[,] C = new double[rA, cB];

            if (cA != rB)
            {
                throw new Exception("matrik can't be multiplied !!");
            }
            else
            {
                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        C[i, j] = temp;
                    }
                }
                return C;
            }
        }
    }
}
