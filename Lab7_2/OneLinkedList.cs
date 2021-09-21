using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab7_2
{
    // Однозв'язний список
    public class OneLinkedList<T> : IEnumerable<T>
    {
        internal Node<T> head; // Головний/перший елемент
        internal Node<T> tail; // Останій/хвостовий елемент
        internal int count;    // Кількість елементів в списку

        // Додання елемента в початок
        public void AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
            {
                tail = node;
            }
            else
            {
                node.next = head;
            }

            head = node;
            count++;
        }

        // Додання елемента в кінець
        public void AddLast(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
            {
                head = node;
            }
            else
            {
                tail.next = node;
            }

            tail = node;
            count++;
        }

        public void RemoveFirst()
        {
            if (head == null)
            {
                return;
            }

            head = head.next;

            if (head == null)
            {
                tail = null;
            }

            count--;
        }

        public void RemoveLast()
        {
            if (head == null)
            {
                return;
            }

            if (head == tail)
            {
                head = tail = null;
                count--;
                return;
            }

            Node<T> current = head;

            while (current.next.next != null)
            {
                current = current.next;
            }

            current.next = null;
            tail = current;
            count--;
        }

        // Видаление елемента
        public bool Remove(T data)
        {
            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.item.Equals(data))
                {
                    // Якщо вузод в середині або в кінці
                    if (previous != null)
                    {
                        // Прибераемо вузол current, тепер previous посилаеться на на current,
                        // а на current.next
                        previous.next = current.next;

                        // Якщо current.next не встановлено, тоді вузел останній,
                        // змінюємо змінну tail
                        if (current.next == null)
                            tail = previous;
                    }
                    else
                    {
                        // Якщо видаляється першний елемент
                        // змінюємо значення head
                        head = head.next;

                        // Якщо після видалення список пустий, встановлюємо tail в null
                        if (head == null)
                        {
                            tail = null;
                        }    
                    }

                    count--;
                    return true;
                }

                previous = current;
                current = current.next;
            }

            return false;
        }
    
        // Довжина списку
        public int Count { get { return count; } }

        // Повертає true, якщо список пустий
        public bool IsEmpty { get { return count == 0; } }

        // Очистка списка
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        // Повертає true, якщо список має хоча б один елемент
        public bool Contains(T data)
        {
            Node<T> current = head;
            
            while (current != null)
            {
                if (current.item.Equals(data))
                {
                    return true;
                }

                current = current.next;
            }
            
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head.Clone();
            while (current != null)
            {
                yield return current.item;
                current = current.next;
            }
        }

        public class Node<T>
        {
            public Node<T> next;
            public T item;

            public Node(T value)
            {
                item = value;
            }

            public Node<T> Clone()
            {
                Node<T> newNode = new Node<T>(item);
                newNode.next = next;
                return newNode;
            }
        }
    }
}
