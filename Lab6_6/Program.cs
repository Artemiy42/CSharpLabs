#define INTERLOCKED
// #define MONITOR
// #define LOCK
// #define LOCKSTATIC

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Lab6_6
{
    public enum IMPORTANCELEVEL
    {
        Debug, // отладочные сообщения
        Warning, // предупреждения - не важные сообщения
        Error, // ошибки приложения - важные сообщения,
        FatalError, // катастрофические ошибки, делающие навозможным функционирвание приложения
    };


    class Program
    {
        static public int max = 5;
        static public int sNumbers = 0;
        public int numbers = 0;

        [STAThread]
        static void Main()
        {
            Program someMemory = new Program();
            TestThread b = new TestThread("ThreadPriority.Lowest ", someMemory);
            TestThread a = new TestThread("ThreadPriority.Highest", someMemory, ThreadPriority.Highest);
            b.t.Join();
            a.t.Join();
            string s;
#if INTERLOCKED
            s = "interlocked";
#elif MONITOR
            s = "monitor ";
#elif LOCK
            s = "lock ";
#elif LOCKSTATIC
            s = "lockStatic ";
#else
#error You have to set at least one macro
#endif
            Console.WriteLine("\n--{0} version, total(nonstatic/static): {1}/{2}\n", s, someMemory.numbers, Program.sNumbers);
            Console.WriteLine("thread ’{0}’ finished with {1} numbers", a.name, a.numbers);
            Console.WriteLine("thread ’{0}’ finished with {1} numbers", b.name, b.numbers);
            Console.WriteLine("\n--{0} version, difference(nonstatic/static): {1}/{2}\n", 
                s, someMemory.numbers - a.numbers - b.numbers, Program.sNumbers - a.numbers - b.numbers);
        }
    }

    class TestThread
    {
        public Thread t = null;
        public int numbers = 0;
        public string name = null;

        public TestThread(string o, Program sharedMemory, ThreadPriority p = ThreadPriority.Lowest)
        {
            name = o;
            t = new Thread(Work);
            t.Priority = p;
            t.Start(sharedMemory);
        }

        public void Work(object o)
        {
            Program mm = (Program) o;
            DateTime st = DateTime.Now;

            for (DateTime cur = DateTime.Now; (cur - st).TotalSeconds < Program.max; cur = DateTime.Now)
            {
#if INTERLOCKED
                Interlocked.Increment(ref mm.numbers);
#elif MONITOR
                Monitor.Enter (mm);
                mm.numbers ++;
                Monitor.Exit (mm);
#elif LOCK
                lock(mm) { // переменная типа класс, а не структуры
                mm.numbers++ ;
                }
#elif LOCKSTATIC
                lock(typeof(Program)) {
                    Program.sNumbers ++;
                }
#endif
                numbers++;
            }
        }
    }

    public class Logger : IDisposable
    {
        private bool dbg = false;
        private StreamWriter streamWriter = null;
        private Queue<string> stringQueue = null;
        private IMPORTANCELEVEL importanceLevel = IMPORTANCELEVEL.Error;
        private Thread log;
        private bool working = false;
        private string filename;

        public Logger(string filename)
        {
            this.filename = filename;
            stringQueue = new Queue<string>();
            streamWriter = new StreamWriter(filename, true);
            working = true;
            log = new Thread(new System.Threading.ThreadStart(LogMessage));
            log.Priority = ThreadPriority.Lowest;
            log.Start();
        }

        public void LogMessage()
        {
            string p = null;

            while (working)
            {
                lock (this)
                {
                    if (stringQueue.Count > 0)
                    {
                        p = (string)stringQueue.Dequeue();
                        streamWriter.WriteLine(p);

                        if (dbg)
                        {
                            streamWriter.Close();
                            streamWriter = new StreamWriter(filename, true);
                        }
                        
                        p = null;
                    }
                    else
                    {
                        Thread.Sleep(10);
                    }
                }
            }
        }

        public void WriteLine(IMPORTANCELEVEL Importance, String Format, params object[] Segments)
        {
            if (working && Importance >= this.importanceLevel)
            {
                String Message = String.Format("[{0}]\t", Importance) + "\t" + string.Format(Format, Segments);

                lock (this)
                {
                    stringQueue.Enqueue(Message);
                }
            }
        }

        public void Dispose()
        {
            string p = null;
            working = false;

            if (log != null)
            {
                log.Join();
            }
            
            while (stringQueue.Count > 0)
            {
                p = stringQueue.Dequeue();
                streamWriter.WriteLine(p);
                p = null;
            }

            streamWriter.Close();
        }
    }
}
