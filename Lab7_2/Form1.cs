using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Lab7_2
{
    public partial class Form1 : Form
    {
        private OneLinkedList<int> list = new OneLinkedList<int>();

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Видаляємо всі не числа із строки
            textBox2.Text = DeleteAllSymbolExceptDigit(textBox2.Text);
            // Ствимо курсор в кінець тексту
            textBox2.SelectionStart = textBox2.Text.Length;
            textBox2.SelectionLength = 0;
        }

        // Додати елемент до початоку зв'язного списку
        private void button1_Click(object sender, EventArgs e)
        {
            int element;

            if (GetIntFromString(textBox1.Text, out element))
            {
                list.AddFirst(element);
                outTextBox.AppendText($"Зв'язний список після додавання числа {element} до початку списку:");
                PrintNewLine();
                PrintLinkedList(list);
            }
        }

        // Вивести зв'язний список на екран
        private void button2_Click(object sender, EventArgs e)
        {
            if (list.Count == 0)
            {
                outTextBox.AppendText($"Зв'язний список порожній!");
                PrintNewLine();
                return;
            }

            outTextBox.AppendText("Зв'язний список:");
            PrintLinkedList(list);
        }

        // Відсортувати зв'язний список
        private void button3_Click(object sender, EventArgs e)
        {
            if (list.Count == 0)
            {
                outTextBox.AppendText($"Зв'язний список порожній!");
                PrintNewLine();
                return;
            }

            BubbleSorting(list);
            outTextBox.AppendText("Зв'язний список після сортування методом бульбашки: ");
            PrintNewLine();
            PrintLinkedList(list);
        }

        // Додати елемент в кінець зв'язного списку
        private void button4_Click(object sender, EventArgs e)
        {
            int element;
                
            if (GetIntFromString(textBox1.Text, out element))
            {
                list.AddLast(element);
                outTextBox.AppendText($"Зв'язний список після додавання числа {element} до кінуя списку:");
                PrintNewLine();
                PrintLinkedList(list);
            }
        }

        // Створити список із випадковими числами
        private void button5_Click(object sender, EventArgs e)
        {
            int size;

            if (GetIntFromString(textBox2.Text, out size))
            {
                Random random = new Random();

                for (int i = 0; i < size; i++)
                {
                    list.AddLast(random.Next(0, 100));
                }

                outTextBox.AppendText($"Зв'язний список довжиною {size}:");
                PrintNewLine();
                PrintLinkedList(list);
            }
        }

        // Видалити елемент з початку
        private void button7_Click(object sender, EventArgs e)
        {
            if (list.Count == 0)
            {
                outTextBox.AppendText($"Зв'язний список порожній!");
                PrintNewLine();
                return;
            }

            list.RemoveFirst();
            outTextBox.AppendText($"Зв'язний список після видалення першого елемента!");
            PrintLinkedList(list);
        }

        // Видалити елемент з кінця
        private void button6_Click(object sender, EventArgs e)
        {
            if (list.Count == 0)
            {
                outTextBox.AppendText($"Зв'язний список порожній!");
                PrintNewLine();
                return;
            }

            list.RemoveLast();
            outTextBox.AppendText($"Зв'язний список після видалення останнього елемента!");
            PrintLinkedList(list);
        }

        private bool GetIntFromString(string str, out int number)
        {
            try
            {
                number = int.Parse(str);
                return true;        
            }
            catch (Exception exception)
            {
                outTextBox.AppendText($"Будь-ласка введіть елемент списку в діапазоні [0; 2147483647]");
                PrintNewLine();
                number = 0;
                return false;
            }
        }

        private static string DeleteAllSymbolExceptDigit(string dirtyString)
        {
            return new string(dirtyString.Where(Char.IsDigit).ToArray());
        }

        private void PrintNewLine()
        {
            outTextBox.AppendText(Environment.NewLine);
        }

        private void PrintLinkedList(OneLinkedList<int> list)
        {
            foreach (int item in list)
            {
                outTextBox.AppendText(item + " -> ");
            }

            outTextBox.AppendText("null");
            PrintNewLine();
        }

        private void PrintLinkedList<U>(OneLinkedList<U>.Node<U> head)
        {
            OneLinkedList<U>.Node<U> t = head.Clone();

            while (t != null)
            {
                outTextBox.AppendText(t.item + " -> ");
                t = t.next;
            }

            outTextBox.AppendText("null");
            PrintNewLine();
        }

        //Сортировка пузырьком O(n^2)
        public void BubbleSorting<U>(OneLinkedList<U> list) where U : IComparable<U>
        {
            if (list == null)
            {
                return;
            }

            OneLinkedList<U>.Node<U> head = list.head;
            OneLinkedList<U>.Node<U> temp, firstElem, secondElem, forSwap;
            int step = 1;

            bool stop = false;
            while (!stop)
            {
                stop = true;
                firstElem = temp = head;
                secondElem = head.next;

                while (secondElem != null)
                {
                    if (firstElem.item.CompareTo(secondElem.item) > 0) // Если > 0 тогда firstElem > 
                    {
                        if (temp == firstElem)
                        {
                            head = secondElem;
                        }
                        else
                        {
                            temp.next = secondElem;
                        }

                        firstElem.next = secondElem.next;
                        secondElem.next = firstElem;

                        // Меняем местами firstElem и secondElem
                        forSwap = firstElem;
                        firstElem = secondElem;
                        secondElem = forSwap;
                        stop = false;
                    }

                    temp = firstElem;
                    firstElem = firstElem.next;
                    secondElem = secondElem.next;
                }

                outTextBox.AppendText($"Зв'язний список після кроку №{step++}");
                PrintNewLine();
                PrintLinkedList(head);
            }

            list.head = head;
        }
    }
}
