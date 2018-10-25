using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAttribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class ExecuteMeAttribute : Attribute
    {
        public object[] args;

        public ExecuteMeAttribute(params object[] args)
        {
            this.args = args;
        }

    }

}
