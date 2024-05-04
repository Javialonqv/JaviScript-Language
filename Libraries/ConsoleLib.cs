using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JScript.Libraries
{
    class ConsoleLib : Library
    {
        public override dynamic ExecuteCommand(string[] command, int line)
        {
            switch (command[0])
            {
                case "console.clear":
                    Clear(command, line);
                    return true;
                case "console.fore_color":
                    return ForeColor(command, line);
                case "console.back_color":
                    return BackColor(command, line);
                case "console.title":
                    return Title(command, line);
            }
            return null;
        }

        public void Clear(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 0) { ExceptionManager.UnexpectedArgs(line); }

            Console.Clear();
        }
        public string ForeColor(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            //if (commandLength > 1) { ExceptionManager.SyntaxError(line, "console.fore_color <color>"); }
            if (commandLength < 1) { ExceptionManager.UnexpectedArgs(line); }
            if (commandLength == 1)
            {
                dynamic color = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
                if (!(color is string)) { ExceptionManager.InvalidParameterType(line, color.GetType().Name, 0, "String"); }

                if (Enum.TryParse(color, true, out ConsoleColor consoleColor))
                {
                    Console.ForegroundColor = consoleColor;
                }
                else
                {
                    ExceptionManager.InvalidColorName(line, color);
                }
            }
            return Console.ForegroundColor.ToString();
        }
        public string BackColor(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            //if (commandLength < 1) { ExceptionManager.SyntaxError(line, "console.back_color <color>"); }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); }
            if (commandLength == 1)
            {
                string color = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
                if (!(color is string)) { ExceptionManager.InvalidParameterType(line, color.GetType().Name, 0, "String"); }

                if (Enum.TryParse(color, true, out ConsoleColor consoleColor))
                {
                    Console.BackgroundColor = consoleColor;
                }
                else
                {
                    ExceptionManager.InvalidColorName(line, color);
                }
            }
            return Console.BackgroundColor.ToString();
        }
        public string Title(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            //if (commandLength < 1) { ExceptionManager.SyntaxError(line, "console.title <title>"); }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); }
            if (commandLength == 1)
            {
                string title = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
                if (!(title is string)) { ExceptionManager.InvalidParameterType(line, title.GetType().Name, 0, "String"); }

                Console.Title = title;
            }
            return Console.Title;
        }
    }
}