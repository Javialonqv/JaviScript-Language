using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLanguageNEW.Libraries
{
    class Math : Library
    {
        public static Math math = null;

        public Math()
        {
            math = this;
        }

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
                return System.Math.Sqrt(value);
            }
            else
            {
                ExceptionManager.InvalidType(line, value.GetType().Name);
                return 0;
            }
        }

        public float Random(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "math.random <min_value> <max_value>"); return 0; }

            Random random = new Random();
            float minValue = 0;
            float maxValue = float.MaxValue;

            try { minValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line); }
            catch { ExceptionManager.InvalidType(line, Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line)); }

            try { maxValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line); }
            catch { ExceptionManager.InvalidType(line, Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line)); }

            float randomNumber = minValue + (float)random.NextDouble() * (maxValue - minValue);
            return randomNumber;
        }
        public int RandomInt(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "math.random <min_value> <max_value>"); return 0; }

            Random random = new Random();
            int minValue = 0;
            int maxValue = int.MaxValue;

            try { minValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line); }
            catch { ExceptionManager.InvalidType(line, Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line)); }

            try { maxValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line); }
            catch { ExceptionManager.InvalidType(line, Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line)); }

            int randomNumber = random.Next(minValue, maxValue);
            return randomNumber;
        }
    }
}
