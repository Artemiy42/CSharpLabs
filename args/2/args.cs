using System.IO;
using System;
	
namespace Args
{
    public class Arg
    {
        public string sNm;
        public string lNm;
        public string sHlp;
        public string lHlp;
        public bool   required; 
        public bool   show; 

        public virtual string val(){
             return "";
        }
        public virtual void set(ref int i, string []ps){
        }
        public bool check (ref int i , string [] ps){
           bool rc = false;
           if (i< ps.Length) {
             string p = ps[i];
             if (p[0]== '-' || p[0]=='/') {
                p = p.Substring(1).ToLower();
//                   Console.Error.WriteLine("--- lnm/p: '{0}/{1}'", lNm, p );
                if (p == sNm.ToLower() || p == lNm.ToLower()) {
                   set (ref i, ps);
                   rc = true;
                }
             }
           }
           return rc;
        }
        public Arg( string sNm, string lNm, string sHlp, string lHlp  )
        {
           this.sNm  =   sNm; 
           this.lNm  =   lNm; 
           this.sHlp =  sHlp;
           this.lHlp =  lHlp;
           this.required = false;
           this.show = true;
        }
        static public void mkVHelp(string name, string after, bool verbose, params Arg[] ars){
          Console.Error.WriteLine(
             Args.Arg.mkHelp(  name, after, ars)
          );
          if (verbose)
            for(int i = 0; i < ars.Length; i++)
               if (ars[i].sNm != ars[i].lNm)
	             	  Console.Error.WriteLine( 
	             	     "'{0}' means the same as '{1}'", ars[i].sNm, ars[i].lNm  );

        }
        static public string mkHelp(string name, string after, params Arg[] ars){
              string rc =  name 
               + "\nusage:\n"
               +  Path.GetFileNameWithoutExtension(
                  System.Windows.Forms.Application.ExecutablePath)+ " ";
               string sHlp="";
               string bHlp="Options \n";
               int i;
               string foo="";
               for (sHlp="",i=0; i<ars.Length; i++){
                  foo = "";
                  if(ars[i].show){
                    foo = "-"+ars[i].sNm + (ars[i].sHlp!=null?" "+ars[i].sHlp:"");
                    if (ars[i].required==false)
                      foo = "["+foo+"] ";
                    sHlp += foo;
                  }
               }
               for (bHlp="\noptions:",i=0; i<ars.Length; i++){
                  foo ="  -"+ ars[i].sNm +(ars[i].sHlp!=null?" "+ars[i].sHlp:"          ") +"\t\t: "
                     +ars[i].lHlp+ " ("+ars[i].val()+")" ;
                  bHlp += "\n"+foo;
               }
              return rc + sHlp +" " + after+ bHlp;

        }
    }


    public class ArgFlg: Arg    {
        public bool v;
        public bool        sv;  //начальное значение

        public ArgFlg( bool v, string sNm,string lNm, string lHlp=null, string sHlp=null)

          :base( sNm,lNm, sHlp, lHlp){
           this.v = v;
           this.sv = v;
        }

        public override string val(){
           return v.ToString();
        }
        public override void set(ref int i, string [] ps){
           v = ! sv;
        }
        public static implicit operator bool (ArgFlg p) {
           return  p.v;
        }
    }


    public class ArgInt: Arg    {
        public int v;

        public ArgInt( int v, string sNm, string lNm, string lHlp=null, string sHlp=null)
          :base( sNm,lNm, sHlp, lHlp){
           this.v = v;
        }
        public override string val(){
           return v.ToString();
        }
        public override void set(ref int i, string [] ps){
          i++;
          if (i < ps.Length){
             try {
             		v = int.Parse(ps[i]);
             } 
             catch {
	             Console.Error.WriteLine("wrong value for {0}/{1} argument: {2} "
	                  , sNm, lNm, ps[i]);
               Environment.Exit(1);
             }
          }
          else {
             Console.Error.WriteLine("there is no value for {0}/{1} argument", sNm, lNm);
             Environment.Exit(1);
          }
        }
        public static implicit operator int (ArgInt p) {
           return  p.v;
        }
     }

    public class ArgFloat: Arg    {
        public float v;

        public ArgFloat( float v, string sNm, string lNm, string lHlp=null, string sHlp=null)
          :this( ((double) v),  sNm,  lNm,  lHlp=null,  sHlp=null){  }

        public ArgFloat( double v, string sNm, string lNm, string lHlp=null, string sHlp=null)
          :base( sNm,lNm, sHlp, lHlp){
           this.v = (float)v;
        }
        public override string val(){
           return v.ToString();
        }
        public override void set(ref int i, string [] ps){
          i++;
          if (i < ps.Length){
             try {
             		v = float.Parse(ps[i]);
             } 
             catch {
	             Console.Error.WriteLine("wrong value for {0}/{1} argument: {2} "
	                  , sNm, lNm, ps[i]);
               Environment.Exit(1);
             }
          }
          else {
             Console.Error.WriteLine("there is no value for {0}/{1} argument", sNm, lNm);
             Environment.Exit(1);
          }
        }
        public static implicit operator float (ArgFloat p) {
           return  p.v;
        }
    }

    public class ArgStr: Arg    {
        public string v;
        public  ArgStr( string v, string sNm,string lNm, string lHlp=null, string sHlp=null)
          :base( sNm,lNm, sHlp, lHlp){
           this.v = v;
        }
        public override string val(){
           return v.ToString();
        }
        public override void set(ref int i, string [] ps){
          i++;
          if (i < ps.Length){
            v = ps[i];
          }
          else {
             Console.Error.WriteLine("there is no value for {0}/{1} argument", sNm, lNm);
             Environment.Exit(1);
          }
        }
        public static implicit operator string (ArgStr p) {
           return  p.v;
        }
    }
}

