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

        public Dictionary<string, dynamic> subValues = new Dictionary<string, dynamic>();

        /*public Variable(string name, dynamic value)
        {
            this.name = name;
            this.value = value;
        }*/
        public Variable(string name, dynamic value, Type valueType)
        {
            this.name = name;
            this.value = value;
            subValues.Add("type", value.GetType().Name);
            switch (valueType)
            {
                case Type type when type == typeof(List<dynamic>):
                    List<dynamic> list = (List<dynamic>)value;
                    for (int i = 0; i < list.Count; i++)
                    {
                        subValues.Add(i.ToString(), list[i]);
                    }
                    break;
                case Type type when type == typeof(OpenFileDialog):
                    OpenFileDialog dialog = (OpenFileDialog)value;
                    subValues.Add("title", dialog.Title);
                    subValues.Add("path", dialog.FileName);
                    subValues.Add("name", dialog.SafeFileName);
                    break;
            }
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
