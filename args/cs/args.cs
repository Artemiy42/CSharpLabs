using System.IO;
using System.Threading;
using System.Globalization;

using System;
/**
*/


/// \file  args.cs
/// \brief  файл содержит   классы для разбора командной строки
///


//1.04.01 добавил толовер к обеим строчкам
//        метод для восстановления начального значения для флага
//       для целых класс с заданным минимумом и максимумом
	
namespace Args
{

///  Родительский класс для разбора аргументов командной строки
    public class Arg
    {
        public string sNm;      ///<ключ
        public string lNm;      ///<синоним  ключа
        public string sHlp;     ///<паоаметр ключа
        public string lHlp;     ///<описание  ключа
        public bool   required; ///<обязательный ключ - не делать квадратных скобок, по умолчанию - делать
        public bool   show;     ///<показывать ли ключ в строке вызова, по умолчанию показывать.

        public Arg( string sNm     ///<ключ            
                  , string lNm     ///<синоним         
                  , string sHlp    ///<параметр ключа  
                  , string lHlp    ///<описание        
        )
        {
           this.sNm  =   sNm; 
           this.lNm  =   lNm; 
           this.sHlp =  sHlp;
           this.lHlp =  lHlp;
           this.required = false;
           this.show = true;
        }

        ///  функция задает текущей переменной Arg правильное значение
        ///  из списка аргументов командной строки и сдвигает текущий аргумент.
        ///  Используется позднее связывание, так как для разных типов ключей
        ///  выполняются различные преобразования, включая проверку на диапазон допустимых
        ///  значений.

        /// проверть аргументы командной строки на наличие текущего ключа
        public bool check (
              ref int i    ///<индекс текущего аргумента
            , string [] ps ///<аргументы командной строки
        ){
           bool rc = false;
           if (i< ps.Length) {
             Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU", false);
             Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

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

        ///версия библиотеки
        static public void version (out int major, out int minor, out int build){
             System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly(); 
             System.Version ver = asm.GetName().Version;
             major =  ver.Major;
             minor =  ver.Minor;
             build =  ver.Build;
        }
        /// выдача подсказки
        static public void mkVHelp(string forWhat, string add, bool verbose, params Arg[] ars){

             string version = "";
             System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly(); 
             System.Version ver = asm.GetName().Version;
             version = string.Format("{0}.{1}.{2}", ver.Major.ToString(),
                                        ver.Minor.ToString(), ver.Build.ToString());

           mkVHelp (forWhat, version, add, verbose, ars);
        }
        ///собственно выдача подсказки с версией приложения
        static public void mkVHelp(
               string forWhat        ///<краткое описание приложения
            ,  string ver            ///<версия приложения
            ,  string after            ///<дополнительная часть строки usage 
                                     ///<тут можно нестандартным образом показать ключи 
            ,  bool verbose          ///<добавлять ли список  синонимов в подсказку
            ,  params Arg[] ars
        ){

          string v = "";
          if (ver.Length > 0)
            v = " (ver:"+ver+")";
          if(PeReaderExtensions.IsConsoleApp()){ 
            
            Console.Error.WriteLine(
               Args.Arg.mkHelp(  forWhat+v, after, ars)
            );
            if (verbose)
              for(int i = 0; i < ars.Length; i++)
                 if (ars[i].sNm != ars[i].lNm)
  	             	  Console.Error.WriteLine( 
  	             	     "'{0}' means the same as '{1}'", ars[i].sNm, ars[i].lNm  );

          }
	        else {
	          string foo = Args.Arg.mkHelp(  forWhat+v, after, ars);
            if (verbose)
                for(int i = 0; i < ars.Length; i++)
                   if (ars[i].sNm != ars[i].lNm)
    	             	     foo +=string.Format(
    	             	       "\n'{0}' means the same as '{1}'", ars[i].sNm, ars[i].lNm);
	            System.Windows.Forms.MessageBox.Show(foo,forWhat+v);
          }
        }
        /// генерация текста для подсказки
        static  string mkHelp(string name, string after, params Arg[] ars){
              string rc =  name 
               + "\nusage:\n "
               +  Path.GetFileNameWithoutExtension(
                  System.Windows.Forms.Application.ExecutablePath)+" "
                   // + "(ver:"
                   // + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
                   // +")"
                    ;
               string sHlp="";
               string bHlp="Options \n";
               int i;
               string foo="";
               for (sHlp="",i=0; i<ars.Length; i++){
                  foo = "";
                  if(ars[i].show){
                    foo = "-"+ars[i].sNm + (ars[i].sHlp!=null?" "+ars[i].sHlp:"");
                    if (ars[i].required==false)
                      foo = "["+foo+"]";
                    sHlp += foo+ " ";
                  }
               }
               for (bHlp="\noptions:",i=0; i<ars.Length; i++){
                  foo ="  -"+ ars[i].sNm +(ars[i].sHlp!=null?" "+ars[i].sHlp:"          ") +"\t\t: "
                     +ars[i].longHelp()+ ": "+ars[i].val() ;
                  bHlp += "\n"+foo;
               }
              return rc + sHlp +" " + after+ bHlp;
        }

        public virtual void set(ref int i, string []ps)///<задать значение ключа из текущего аргумента
        {
        }
        ///  функция создает подсказку по  ключу.
        public virtual string longHelp(){
           return lHlp;
        }
        ///  функция переводит в строку значение  ключа.
        public virtual 	string val(){
             return "";
        }
        ///  восстанавливает из строки значение ключа
        public virtual void set(string v){ 
        }

    }



///  ключ логического типа (иначе - флаг)
    public class ArgFlg: Arg    {
        //public 
        bool v;
        //public 
        bool        sv;  //начальное значение

        public ArgFlg( 
           bool v       ///<начальное значение ключа
         , string sNm
         , string lNm
         , string lHlp=null
         , string sHlp=null
        )

          :base( sNm,lNm, sHlp, lHlp){
           this.v = v;
           this.sv = v;
        }

        public void restore(){    ///< восстановить флаг
           v = sv;
        }
        public void toggle(){    ///< переключить флаг
           v =  !sv;
        }

        /// переключение флага, не сдвигает индекс i текущего аргумента.
        public override void set(ref int i, string [] ps){ 
           toggle();
        }
        public override string val(){   
           return v.ToString();
        }
        public override void set(string v){ 
          Boolean.TryParse(v, out this.v);
        }

        public static implicit operator bool (ArgFlg p) {
           return  p.v;
        }

    }

///  ключ  типа строка
    public class ArgStr: Arg    {
        //public 
        string v;
        public  ArgStr( string v, string sNm,string lNm, string lHlp=null, string sHlp=null)
          :base( sNm,lNm, sHlp, lHlp){
           this.v = v;
        }
        public override void set(ref int i, string [] ps){
          i++;
          if (i < ps.Length){
            v =  ps[i];
          }
          else {
             string foo = string.Format("there is no value for {0}/{1} argument", sNm, lNm);

             if(PeReaderExtensions.IsConsoleApp()){ 
               Console.Error.WriteLine(foo);
               Environment.Exit(1);
             }
             else {
          	   System.Windows.Forms.MessageBox.Show(foo,"Error");
               Environment.Exit(1);
             }
          }
        }
        public override string val(){
           return v;
        }
        public override void set(string v){ 
          this.v = v;
        }
        public static implicit operator string (ArgStr p) {
           return  p.v;
        }
    }



///  ключ  типа вещественное число.
///  целая часть числа от дробной отделяется десятичной точкой, а не запятой, как в английской культуре. 
    public class ArgFloat: Arg    {
        //public 
        protected float v;

        public ArgFloat( float v, string sNm, string lNm, string lHlp=null, string sHlp=null)
          :this( ((double) v),  sNm,  lNm,  lHlp=null,  sHlp=null){  }

        public ArgFloat( double v, string sNm, string lNm, string lHlp=null, string sHlp=null)
          :base( sNm,lNm, sHlp, lHlp){
           this.v = (float)v;
        }

        public override void set(ref int i, string [] ps){ /// разбор аргументов командной строки2
          i++;
          if (i < ps.Length){
             try {       
                set(ps[i]);
             } 
             catch {
               string foo = string.Format("wrong value for {0}/{1} argument: {2} "
	                  , sNm, lNm, ps[i]);

               if(PeReaderExtensions.IsConsoleApp()){ 
                 Console.Error.WriteLine(foo);
                 Environment.Exit(1);
               }
               else {
            	   System.Windows.Forms.MessageBox.Show(foo,"Error");
                 Environment.Exit(1);
               }
             }
          }
          else {
               string foo = string.Format("there is no value for {0}/{1} argument", sNm, lNm);
               if(PeReaderExtensions.IsConsoleApp()){ 
                 Console.Error.WriteLine(foo);
                 Environment.Exit(1);
               }
               else {
            	   System.Windows.Forms.MessageBox.Show(foo,"Error");
                 Environment.Exit(1);
               }
          }
        }
        public override string val(){
           return v.ToString();
        }
        public override void set(string v){ 
          Single.TryParse(v, out this.v);
        }

        public static implicit operator float (ArgFloat p) {
           return  p.v;
        }
        public static implicit operator double (ArgFloat p) {
           return  (double)p.v;
        }

        public virtual  void set(double v){ 
          this.v = (float)v;
        }
        public virtual  void set(float v){ 
          this.v = v;
        }
          
    }

///  ключ строчного типа, есть возможность ограничить максимальное и минимальное значение

     public class ArgFloatMM: ArgFloat    {
        bool isMax;
        bool isMin;
        float  max;
        float  min;

        public void setMax(double max){
           this.max = (float)max;
           isMax = true;
        }
        public void setMin(double min){
           this.min = (float)min;
           isMin = true;
        }

        public ArgFloatMM( double v, string sNm, string lNm, string lHlp=null, string sHlp=null)
          :base( v, sNm,lNm, lHlp, sHlp){
            isMax = false;
            isMin = false;
             max = (float)0.0;
             min = (float)0.0;
        }

        public override void set(ref int i, string [] ps){
           base.set(ref i, ps);
           correct();
        }
        void correct(){
           if (isMax && v > max)  v = max;
           if (isMin && v < min)  v = min;
        }
        public  override string longHelp(){
           string rc = "..";
           if (isMax) rc = rc+max.ToString();
           if (isMin) rc = min.ToString()+rc;
           if (rc == "..")
           		return lHlp;
           return lHlp + " ("+rc+")";
        }

        public override void set(string v){ 
          Single.TryParse(v, out this.v);
          correct();
        }
        public override  void set(double v){ 
          base.set(v);
          correct();
        }
        public override  void set(float v){ 
          base.set(v);
          correct();
        }
    }




///  ключ целого типа
    public class ArgInt: Arg    {
        //public 
        protected 
        int v;

        public ArgInt( int v, string sNm, string lNm, string lHlp=null, string sHlp=null)
          :base( sNm,lNm, sHlp, lHlp){
           this.v = v;
        }
        public override void set(ref int i, string [] ps){
          i++;
          if (i < ps.Length){
             try {
             		v = int.Parse(ps[i]);
             } 
             catch {
               string foo = string.Format("wrong value for {0}/{1} argument: {2} "
	                  , sNm, lNm, ps[i]);
               if(PeReaderExtensions.IsConsoleApp()){ 
                 Console.Error.WriteLine(foo);
                 Environment.Exit(1);
               }
               else {
            	   System.Windows.Forms.MessageBox.Show(foo,"Error");
                 Environment.Exit(1);
               }
             }
          }
          else {
               string foo = string.Format("there is no value for {0}/{1} argument", sNm, lNm);
               if(PeReaderExtensions.IsConsoleApp()){ 
                 Console.Error.WriteLine(foo);
                 Environment.Exit(1);
               }
               else {
            	   System.Windows.Forms.MessageBox.Show(foo,"Error");
                 Environment.Exit(1);
               }
          }
        }
        public override string val(){
           return v.ToString();
        }
        public override void set(string v){ 
          int.TryParse(v, out this.v) ;
        }

        public static implicit operator int (ArgInt p) {
           return  p.v;
        }
        public virtual  void set(int v){ 
          this.v = v;
        }
     }


///  ключ целого типа, есть возможность ограничить максимальное и минимальное значение
    public class ArgIntMM: ArgInt    {
        bool isMax;
        bool isMin;
        int  max;
        int  min;

        public void setMax(int max){
           this.max = max;
           isMax = true;
        }
        public void setMin(int min){
           this.min = min;
           isMin = true;
        }

        public ArgIntMM( int v, string sNm, string lNm, string lHlp=null, string sHlp=null)
          :base( v, sNm,lNm, lHlp, sHlp){
            isMax = false;
            isMin = false;
             max = 0;
             min = 0;
        }
        public  override string longHelp(){
           string rc = "..";
           if (isMax) rc = rc+max.ToString();
           if (isMin) rc = min.ToString()+rc;
           if (rc == "..")
           		return lHlp;
           return lHlp + " ("+rc+")";
        }

        public override void set(ref int i, string [] ps){
           base.set(ref i, ps);
           correct();
        }

        public override  void set(int v){ 
          base.set(v);
          correct();
        }
        public override void set(string v){ 
          base.set(v);
          correct();
        }

        void correct(){
           if (isMax && v > max)  v = max;
           if (isMin && v < min)  v = min;
        }
    }
}

