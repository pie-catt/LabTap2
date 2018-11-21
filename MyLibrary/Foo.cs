using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAttribute;

namespace MyLibrary
{
    public class Foo
    {
        [ExecuteMe]
        public void M1()

        {
            Console.WriteLine("Classe " + this.GetType().Name + " metodo M1");
        }

        [ExecuteMe(45)]
        [ExecuteMe(0)]
        [ExecuteMe(3)]
        public void M2(int a)
        {
            Console.WriteLine("Classe " + this.GetType().Name + " metodo M2 a={0}", a);
        }

        [ExecuteMe("hello", "reflection")]
        public void M3(string s1, string s2)
        {
            Console.WriteLine("Classe " + this.GetType().Name + " metodo M3 s1={0} s2={1}", s1, s2);
        }
    }

    //Classe senza costruttore di default
   public class Foo2
    {
        public Foo2(int a)
        {
            
        }

        [ExecuteMe]
        public void M1()
        {
            Console.WriteLine("Classe " + this.GetType().Name + " metodo M1");
        }
    }

    public class Foo3
    {

        [ExecuteMe]
        public void M1()

        {
            Console.WriteLine("Classe " + this.GetType().Name + " metodo M1");
        }

    }

    //Classe con errore sui parametri del metodo
    public class Foo4
    {
        [ExecuteMe("tre")]
        public void M1(int a)
        {
            Console.WriteLine("Classe " + this.GetType().Name + " metodo M1, a = {0}", a);
        }
    }

    public class Foo5
    {
        [ExecuteMe]
        public void M1024()
        {
            Console.WriteLine("Classe " + this.GetType().Name + " M1024");
        }
    }


}
