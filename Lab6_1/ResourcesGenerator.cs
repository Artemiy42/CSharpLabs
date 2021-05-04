using System;
using System.IO;
using System.Resources;
using System.Drawing;
using Args;
using Logger;

namespace Lab6_1
{
    class ResourcesGenerator
    {
        static public ArgFlg hlpF;
        static public ArgFlg dbgF;
        static public ArgFlg vF;
        static public ArgIntMM logLvl;
        static public ArgStr flNm;
        static public ArgStr resx;
        static ResourcesGenerator()
        {
            hlpF = new ArgFlg(false, "?", "help", "to see this help");
            vF = new ArgFlg(false, "v", "verbose", "additional info");
            dbgF = new ArgFlg(false, "d", "debug", "debug mode");
            logLvl = new ArgIntMM(1, "l", "log", "log level", "LLL");
            logLvl.setMin(1);
            logLvl.setMax(8);
            flNm = new ArgStr("none", "f", "file", "data file, if not set - list (recourceNm, fileNm) from stdin", "FLNM");
            resx = new ArgStr(".r.resx", "r", "resx", "resurce file", "RFNM");
        }
        static public void usage()
        {
            Args.Arg.mkVHelp("to make resx file from list of files", "resourceNm", vF
            , hlpF
            , dbgF
            , vF
            , logLvl
            , flNm
            , resx
            );
            Environment.Exit(1);
        }
        /* static Program(){
        var format = new System.Globalization.NumberFormatInfo();
        format.NumberDecimalSeparator = ".";
        } */
        /*[STAThread]
        static void Main(string[] args)
        {
            string rNm = "";
            for (int i = 0; i < args.Length; i++)
            {
                if (hlpF.check(ref i, args))
                    usage();
                else if (dbgF.check(ref i, args))
                    ;
                else if (vF.check(ref i, args))
                    ;
                else if (logLvl.check(ref i, args))
                    ;
                else if (flNm.check(ref i, args))
                    ;
                else if (resx.check(ref i, args))
                    ;
                else
                    rNm = args[i];
            }
            if (rNm == "" && flNm != "none")
                usage();
            DateTime st = DateTime.Now;
            using (LOGGER Logger = new LOGGER(LOGGER.uitoLvl(logLvl)))
            {
                if (vF)
                    Logger.cnslLvl = IMPORTANCELEVEL.Spam;
                string extension;
                Logger.WriteLine(IMPORTANCELEVEL.Stats
                , " resource name/file resx name: ’{0}’/’{1}’", rNm, (string)flNm);
                //ResXResourceSet resSet = new ResXResourceSet(resx);
                Image i;
                byte[] bs;
                ResXResourceWriter rw = //new ResXResourceWriter(resSet);
                new ResXResourceWriter(resx);
                Logger.WriteLine(IMPORTANCELEVEL.Stats
                , "ResXResourceWriter is here! ");
                if (flNm != "none")
                {
                    extension = Path.GetExtension(flNm);
                    if (extension.ToLower() == ".jpg")
                    {
                        i = Image.FromFile(flNm, true);
                        rw.AddResource(rNm, i);
                    }
                    else if (extension.ToLower() == ".png")
                    {
                        i = Image.FromFile(flNm, true);
                        rw.AddResource(rNm, i);
                    }
                    else if (extension.ToLower() == ".bmp")
                    {
                        i = Image.FromFile(flNm, true);
                        rw.AddResource(rNm, i);
                    }
                    else
                    {
                        bs = File.ReadAllBytes(flNm);
                        rw.AddResource(rNm, bs);
                    }
                    Logger.WriteLine(IMPORTANCELEVEL.Stats
                    , "Image from file is here! ");
                }
                else
                {
                    int lineno = 1;
                    for (string str = Console.ReadLine();
                        str != null;
str = Console.ReadLine(), lineno++)
                    {
                        Logger.WriteLine(IMPORTANCELEVEL.Stats
                        , "line no {0}:’{1}’", lineno, str);
                        string[] values = str.Split(new char[] { ' ', '\t' }
                        , StringSplitOptions.RemoveEmptyEntries);
                        if (values.Length > 1)
                        {
                            try
                            {
                                extension = Path.GetExtension(values[1]);
                                if (extension.ToLower() == ".jpg")
                                {
                                    i = Image.FromFile(values[1], true);
                                    rw.AddResource(values[0], i);
                                }
                                else if (extension.ToLower() == ".png")
                                {
                                    i = Image.FromFile(values[1], true);
                                    rw.AddResource(values[0], i);
                                }
                                else if (extension.ToLower() == ".bmp")
                                {
                                    i = Image.FromFile(values[1], true);
                                    rw.AddResource(values[0], i);
                                }
                                else
                                {
                                    bs = File.ReadAllBytes(values[1]);
                                    rw.AddResource(values[0], bs);
                                }
                            }
                            catch
                            {
                                Logger.WriteLine(IMPORTANCELEVEL.Error
                                , "skipped file in line no {0}:’{1}’, length:{2}", lineno, str, values.Length);
                            }
                        }
                        else
                            Logger.WriteLine(IMPORTANCELEVEL.Error
                            , "wrong line no {0}:’{1}’, length:{2}", lineno, str, values.Length);
                    }
                }
                rw.Close();
                DateTime fn = DateTime.Now;
                Logger.WriteLine(IMPORTANCELEVEL.Stats, "time of work with file ’{1}’ is {0} secs"
                , (fn - st).TotalSeconds, (string)flNm);
            }
        }
        */
    }
}