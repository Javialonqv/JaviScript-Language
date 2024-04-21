using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLanguageNEW.Libraries
{
    class Input : Library
    {
        public static Input input = null;

        public Input()
        {
            input = this;
        }

        public override dynamic ExecuteCommand(string[] command, int line)
        {
            switch (command[0])
            {
                case "input.user_input":
                    return UserInput(command, line);
                case "input.user_input_int":
                    return UserInputInt(command, line);
                case "input.user_input_float":
                    return UserInputFloat(command, line);
                case "input.user_key":
                    return UserKeyInput(command, line);
            }
            return null;
        }

        public dynamic UserInput(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 0) { ExceptionManager.UnexpectedArgs(line); }

            return System.Console.ReadLine();
        }
        public dynamic UserInputInt(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 0) { ExceptionManager.UnexpectedArgs(line); }

            string input = System.Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            else
            {
                ExceptionManager.InvalidType(line, "string");
                return null;
            }
        }
        public dynamic UserInputFloat(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 0) { ExceptionManager.UnexpectedArgs(line); }

            string input = System.Console.ReadLine();
            if (float.TryParse(input, out float result))
            {
                return result;
            }
            else
            {
                ExceptionManager.InvalidType(line, "string");
                return null;
            }
        }

        public dynamic UserKeyInput(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 0) { ExceptionManager.UnexpectedArgs(line); }

            return System.Console.ReadKey();
        }
    }
}
