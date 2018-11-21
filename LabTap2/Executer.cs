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
       static void Main(string[] args)
        {
            //Carico Dll
            var a = Assembly.LoadFrom("C:\\Users\\User\\source\\repos\\LabTap2\\MyLibrary\\bin\\Debug\\MyLibrary.dll");
           //Ottengo classi nell'assembly
            var types = a.GetTypes();

            foreach (var _class in types)
            {
                if (_class.IsClass)
                {
                    try
                    {
                        //Ottengo metodi della classe
                        MethodInfo[] classMethods = _class.GetMethods();
                        //Per ogni metodo
                        foreach (var method in classMethods)
                        {
                            //Ottengo custom attributes del metodo
                            var customAttributes = method.GetCustomAttributes<ExecuteMeAttribute>();
                            //Per ogni custom attribute
                            foreach (var attribute in customAttributes)
                            {
                                //Recupero parametri del custom attribute
                                var attributeParameters = attribute.AttrArgs;
                                //Controllo correttezza parametri
                                ParametersCheck(method, attributeParameters);
                                //Creo istanza della classe
                                //Solleva MissingMethodException se la classe non ha costruttore di default
                                var classInstance = Activator.CreateInstance(_class);
                                //Invoco il metodo con quei parametri
                                method.Invoke(classInstance, attributeParameters);

                            }
                        }
                    }

                    //Costruttore di default non presente
                    catch (MissingMethodException e)
                    {
                        Console.WriteLine("Cotruttore di default non presente per la classe, " +
                                          "passo alla classe successiva");
                    }

                    //Errore nei parametri
                    catch (ParametersErrorException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                Console.ReadLine();
            }
        }
       
        //Metodo controllo correttezza parametri da Custom Attribute a Metodo invocato
        private static void ParametersCheck(MethodInfo m, object[] attributePar)
        {
            ParameterInfo[] methodPar = m.GetParameters();
            if (attributePar.Length != methodPar.Length)
                throw new ParametersErrorException("Errore parametri per il metodo " 
                    + m.DeclaringType + "." + m.Name);

            int i = 0;
            foreach (var p in methodPar)
            {
                if (p.ParameterType != attributePar[i].GetType())
                    throw new ParametersErrorException("Errore parametri per il metodo "
                        + m.DeclaringType + "." + m.Name);
                i++;
            }

           }
    }

    public class ParametersErrorException : Exception
    {
        public ParametersErrorException(string message)
            : base(message)
        {

        }
    }
}
