using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MyAttribute;

namespace LabTap2
{
    class Executer
    {
        private static bool Notify(Exception e)
        {
            return false;
        }

        static void Main(string[] args)
        {
            //Carico Dll
            var a = Assembly.LoadFrom("C:\\Users\\User\\source\\repos\\LabTap2\\MyLibrary\\bin\\Debug\\MyLibrary.dll");
            foreach (var type in a.GetTypes())
            {
                //    Console.WriteLine(type.FullName);
                foreach (var m in type.GetMethods())
                {
                    //  Console.WriteLine(m.ToString());
                }
            }
            //Invoco i metodi nella dll

            var classi = a.GetTypes();

            foreach (var classe in classi)
            {
                //Creo istanza del tipo
                try
                {
                    var _classInstance = Activator.CreateInstance(classe);


                    //Recupero metodi della classe
                    MethodInfo[] methods = classe.GetMethods();
                    //Per ogni metodo
                    foreach (var m in methods)
                    {
                        //Recupero custom attributes
                        var att = m.GetCustomAttributes<ExecuteMeAttribute>();
                        //Per ogni custom attribute
                        foreach (var cust in att)
                        {
                            //Recupero parametro attribute
                            var par = cust.args;
                            //Invoco il metodo con quel parametro
                            m.Invoke(_classInstance, par);

                        }
                        //    Console.WriteLine(m.ToString());
                    }
                }

                catch (System.MissingMethodException e) when (Notify(e))
                {
                }

              // Console.ReadLine();
            }

        }
    }
}
