using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7_1
{
    public partial class Form1 : Form
    {
        public enum SortingMethod
        { 
            Insert,
            Shella,
            Bubble
        }

        private int[] array;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Видаляємо всі не числа із строки
            textBox1.Text = DeleteAllSymbolExceptDigit(textBox1.Text);
            // Ствимо курсор в кінець тексту
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int arraySize = int.Parse(textBox1.Text);

                array = new int[arraySize];
                FillArrayRandomNumbers(ref array, 100);

                outTextBox.AppendText($"Створений масив довжиной: {arraySize}");
                PrintNewLine();

                if (checkBox2.Checked)
                {
                    PrintArray(array);
                }

                button2.Enabled = true;
            }
            catch(Exception exception)
            {
                outTextBox.AppendText($"Будь-ласка введіть розмір масиву в діапазоні [0; 2147483647]");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SortingMethod currentSortingMethod;

            if (radioButton1.Checked)
                currentSortingMethod = SortingMethod.Insert;
            else if (radioButton2.Checked)
                currentSortingMethod = SortingMethod.Shella;
            else
                currentSortingMethod = SortingMethod.Bubble;

            outTextBox.AppendText("Початок сортування методом" + GetSortingName(currentSortingMethod));
            PrintNewLine();

            Stopwatch watch = Stopwatch.StartNew();

            if (currentSortingMethod == SortingMethod.Insert)
            {
                InsertSorting((int[]) array.Clone());
            }
            else if (currentSortingMethod == SortingMethod.Shella)
            {
                ShellaSorting((int[])array.Clone());
            }    
            else
            {
                BubbleSorting((int[])array.Clone());
            }

            watch.Stop();
            long elapsedMs = watch.ElapsedMilliseconds;

            outTextBox.AppendText("Кінець сортування методом" + GetSortingName(currentSortingMethod));
            PrintNewLine();
            outTextBox.AppendText($"Масив було відсортовано за {elapsedMs / 1000f} сек.");
            PrintNewLine();
        }

        private string GetSortingName(SortingMethod sortingMethod)
        {
            switch (sortingMethod)
            {
                case SortingMethod.Insert:
                    return "вставка";
                case SortingMethod.Shella:
                    return "Шелла";
                case SortingMethod.Bubble:
                    return "бульбашки";
            }

            return "";
        }

        private int[] InsertSorting(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i;
                
                while (j > 0 && key < array[j - 1])
                {
                    Swap(ref array[j - 1], ref array[j]);
                    j--;
                }

                if (checkBox1.Checked)
                {
                    outTextBox.AppendText($"Крок №{i}");
                    PrintNewLine();
                    PrintArray(array);
                }
            }

            return array;
        }

        private int[] ShellaSorting(int[] array)
        {
            int step = 1;
            int halfSize = array.Length / 2;
            
            while (halfSize >= 1)
            {
                for (int i = halfSize; i < array.Length; i++)
                {
                    int j = i;
                    
                    while ((j >= halfSize) && (array[j - halfSize] > array[j]))
                    {
                        Swap(ref array[j], ref array[j - halfSize]);
                        j = j - halfSize;
                    }
                }

                halfSize = halfSize / 2;

                if (checkBox1.Checked)
                {
                    outTextBox.AppendText($"Крок №{step++}");
                    PrintNewLine();
                    PrintArray(array);
                }
            }

            return array;
        }

        // Сортування бульбашкою
        private int[] BubbleSorting(int[] array)
        {
            int len = array.Length;
            for (int i = 1; i < len; i++)
            {
                for (int j = 0; j < len - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }

                }
                
                if (checkBox1.Checked)
                {
                    outTextBox.AppendText($"Крок №{i}");
                    PrintNewLine();
                    PrintArray(array);
                }
            }

            return array;
        }

        private static void Swap(ref int e1, ref int e2)
        {
            int temp = e1;
            e1 = e2;
            e2 = temp;
        }

        private static string DeleteAllSymbolExceptDigit(string dirtyString)
        {
            return new string(dirtyString.Where(Char.IsDigit).ToArray());
        }

        private static void FillArrayRandomNumbers(ref int[] array, int maxValue)
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next() % maxValue + 1;
            }
        }

        private void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0)
                {
                    outTextBox.AppendText(", ");
                }

                outTextBox.AppendText($"{array[i]}");                
            }

            PrintNewLine();
        }


        private void PrintNewLine()
        {
            outTextBox.AppendText(Environment.NewLine);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            outTextBox.Clear();
        }
    }
}
