using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyOwnLanguageNEW
{
    class Variable
    {
        public string name { get; set; }
        public dynamic value { get; set; }

        /*public Variable(string name, dynamic value)
        {
            this.name = name;
            this.value = value;
        }*/
        public Variable(string name, dynamic value)
        {
            this.name = name;
            this.value = value;
        }

        public string GetValueType()
        {
            return value.GetType().Name;
        }

        public dynamic GetValueAtIndex(int line, int index)
        {
            if (value.GetType() == typeof(List<dynamic>))
            {
                List<dynamic> list = value;
                if (index > list.Count - 1) { ExceptionManager.ListIndexIsOutsideOfBounds(line, index, list.Count); return null; }
                return list[index];
            }
            else { return null; }
        }
    }
}
