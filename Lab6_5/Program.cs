using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Lab6_5
{
    static class Program
    {
        [STAThread]
        static int Main(string[] ars)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create("http://somesite.com/myfile.txt");
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }

            response.Close();
            Console.WriteLine("Запрос выполнен");

            return 0;
        }
    }
}
