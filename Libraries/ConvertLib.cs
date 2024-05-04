using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JScript.Libraries
{
    class ConvertLib : Library
    {
        public override dynamic ExecuteCommand(string[] command, int line)
        {
            switch (command[0])
            {
                case "convert.to_string":
                    return ConvertToString(command, line);
                case "convert.to_int":
                    return ConvertToInt(command, line);
                case "convert.to_float":
                    return ConvertToFloat(command, line);
                case "convert.to_bool":
                    return ConvertToBool(command, line);
            }
            return null;
        }

        public string ConvertToString(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "convert.to_string <value>"); }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); }

            dynamic value = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line, false).ToString();

            return Utilities.GetValue(line, value).ToString();
        }
        public int ConvertToInt(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "convert.to_int <var_name>"); }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); }

            dynamic value = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line, false).ToString();
            try
            {
                return Convert.ToInt32(Utilities.GetValue(line, value));
            }
            catch { ExceptionManager.ConversionNotAllowed(line, Utilities.GetValue(line, value).GetType().Name, "int"); return 0; }
        }
        public float ConvertToFloat(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "convert.to_float <var_name>"); }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); }

            dynamic value = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line, false).ToString();
            try
            {
                return Convert.ToSingle(Utilities.GetValue(line, value));
            }
            catch { ExceptionManager.ConversionNotAllowed(line, Utilities.GetValue(line, value).GetType().Name, "float"); return 0; }
        }
        public bool ConvertToBool(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "convert.to_bool <var_name>"); }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); }

            dynamic value = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line, false).ToString();
            try
            {
                return Convert.ToBoolean(Utilities.GetValue(line, value));
            }
            catch { ExceptionManager.ConversionNotAllowed(line, Utilities.GetValue(line, value).GetType().Name, "bool"); return false; }
        }

        /*public bool TryConvertToString(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "convert.try_to_string <var_name>"); }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); }

            string varName = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line, false);
            if (!Utilities.VariableExists(varName)) { ExceptionManager.UnknowType(line, varName); return; }
            Variable var = Utilities.FindVariableOfName(varName);
            try
            {
                System.Convert.ToString(var.value);
            }
            catch { return false; }
            return true;
        }*/
    }
}
