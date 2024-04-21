using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLanguageNEW
{
    class Variable
    {
        public string name { get; set; }
        public dynamic value { get; set; }

        public Variable(string name, dynamic value)
        {
            this.name = name;
            this.value = value;
        }

        // TODO: Añadir funciones para cada variable.
        public string GetValueType()
        {
            return value.GetType().Name;
        }
    }
}
