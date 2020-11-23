using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Args;

namespace test
{
    class set{
        static public ArgFlg  hlpF ;
        static public ArgFlg  vF ;

        static public ArgStr  Reg ;
        static public ArgStr  Env ;
        static public ArgStr  Mod ;

        static  set (){
           hlpF   =  new ArgFlg(false, "?","help",    "to see this help");
           vF     =  new ArgFlg(false, "v",  "verbose", "additional info");

           Reg   =  new ArgStr("RUS", "r", "Region", "region blablabla", "RRR");
           Env   =  new ArgStr("DEV", "e", "Environment", "environment blablabla", "EEE");
           Mod   =  new ArgStr("", "m", "Modules", "list of modules for this process", "MLIST");
        }


        static public  void usage(){
           Args.Arg.mkVHelp("to show of qu-qu example", "nothing to talk about"
                ,vF
                ,hlpF
                ,vF
                ,Reg
                ,Env
                ,Mod
                );
           Environment.Exit(1);
        }
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
           for (int i = 0; i<args.Length; i++){
             if (set.hlpF.check(ref i, args))
               set.usage();
             else if (set.vF.check(ref i, args))
               ;
             else if (set.Reg.check(ref i, args))
               ;
             else if (set.Env.check(ref i, args))
               ;
             else if (set.Mod.check(ref i, args))
               ;
           }

           Console.WriteLine("I have to work in '{0}' region and '{1}' environment,\n with '{2}' modules "
             , set.Reg.v, set.Env.v, set.Mod.v);
        }
    }                
}
