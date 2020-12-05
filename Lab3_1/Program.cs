#pragma warning disable 642

using System;
using System.Windows.Forms;
using Args;

namespace Lab3_1
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ArgFlg sFlag = new ArgFlg(false, "m",  "showMessage",   "to show message");
            ArgFlg vFlag = new ArgFlg(false, "v",  "verbose",   "to show additional information");
            ArgFlg hFlag = new ArgFlg(false, "?",  "help",   "to show usage page");
            
            for (int i = 0; i < args.Length; i++)
            {
                if (hFlag.check(ref i, args))   
                {
                    Arg.mkVHelp("to test command line arguments", "", vFlag, hFlag, vFlag, sFlag); 
                    Environment.Exit(1);
                }
                else if (vFlag.check(ref i, args))
                    ;
                else if (sFlag.check(ref i, args)) 
                    ;
            }

            if ((bool) sFlag == true)
            {
                ShowMessageBox();
            }
            
            Console.WriteLine($"Вы ввели: {0}/{1}/{2}/{3}", (bool) hFlag, (bool) vFlag, (bool) sFlag);
        }

        public static void ShowMessageBox()
        {
            DialogResult result = MessageBox.Show(
                "Ви любите C#?",
                "Хммм...",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
                MessageBox.Show(
                    "Це чудово! =)",
                    "C# <3",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            else
                MessageBox.Show(
                    "Це не чудово. =(",
                    "C# <3",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
