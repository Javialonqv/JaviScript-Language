using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JScript.Libraries
{
    class MathLib : Library
    {
        public override dynamic ExecuteCommand(string[] command, int line)
        {
            switch (command[0])
            {
                case "math.sqrt":
                    return Sqrt(command, line);
                case "math.random":
                    return Random(command, line);
                case "math.random_int":
                    return RandomInt(command, line);
            }
            return null;
        }

        public float Sqrt(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "math.sqrt <value>"); return 0; }

            dynamic value = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (value is int || value is float)
            {
                return Math.Sqrt(value);
            }
            else
            {
                ExceptionManager.InvalidParameterType(line, value.GetType().Name, 0, "Int or Float");
                return 0;
            }
        }

        public float Random(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "math.random <min_value> <max_value>"); return 0; }

            Random random = new Random();
            dynamic minValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(minValue is int || minValue is float)) { ExceptionManager.InvalidParameterType(line, minValue.GetType().Name, 0, "Int or Float"); }
            dynamic maxValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
            if (!(maxValue is int || maxValue is float)) { ExceptionManager.InvalidParameterType(line, maxValue.GetType().Name, 1, "Int or Float"); }

            float randomNumber = minValue + (float)random.NextDouble() * (maxValue - minValue);
            return randomNumber;
        }
        public int RandomInt(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "math.random <min_value> <max_value>"); return 0; }

            Random random = new Random();
            dynamic minValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(minValue is int)) { ExceptionManager.InvalidParameterType(line, minValue.GetType().Name, 0, "Int"); }
            dynamic maxValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
            if (!(maxValue is int)) { ExceptionManager.InvalidParameterType(line, maxValue.GetType().Name, 0, "Int"); }

            int randomNumber = random.Next(minValue, maxValue);
            return randomNumber;
        }
    }
}
