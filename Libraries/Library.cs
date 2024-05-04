using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JScript.Libraries
{
    class Library
    {
        public virtual dynamic ExecuteCommand(string[] command, int line)
        {
            return null;
        }
        /*public virtual bool ExecuteCommand(string[] command, int line)
        {
            return false;
        }*/
    }
}
