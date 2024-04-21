using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnLanguageNEW
{
    static class MainLibrary
    {
        public static void Var(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "var <name> <value>"); return; }
            if (commandLength > 2) { ExceptionManager.UnexpectedArgs(line); return; }

            //string varName = command[1];
            //dynamic varValue = Utilities.ConcatenateValues(command.Skip(2).ToArray(), line);

            string varName = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            dynamic varValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
            if (Init.runtimeVariables.Find(v => v.name == varName) != null) { ExceptionManager.VariableAlreadyExists(line, varName); return; }
            if (varName.Contains(".")) { ExceptionManager.VariableNameNOTAllowed(line, varName); }
            if (int.TryParse(varName.Substring(0, 1), out int i)) { ExceptionManager.VariableNameNOTAllowed(line, varName); }
            Variable var = new Variable(varName, varValue);
            Init.runtimeVariables.Add(var);
        }

        public static void Print(string[] command, int line, bool printLine)
        {
            string[] temp = command.Skip(1).ToArray();
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "print <values>"); return; }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); return; }

            dynamic result = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (printLine) { Console.WriteLine(result); }
            else { Console.Write(result); }
        }

        public static void Pause(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); return; }
            if (commandLength == 1)
            {
                dynamic message = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
                Console.Write(message);
            }
            Console.ReadKey(true);
        }

        public static void Reassign(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 2) { ExceptionManager.UnexpectedArgs(line); return; }
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "reassign <var_name> <new_value>"); return; }

            string varName = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line, false);
            if (Init.runtimeVariables.Find(v => v.name == varName) == null) { ExceptionManager.UnknowType(line, varName); return; }
            dynamic newValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);

            Init.runtimeVariables.Find(v => v.name == varName).value = newValue;
        }

        public static void Exit(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); return; }
            if (commandLength > 0)
            {
                if (int.TryParse(Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line), out int exitCode))
                {
                    Environment.Exit(exitCode);
                }
                else { ExceptionManager.InvalidType(line, command[2]); return; }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        public static void Import(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); return; }
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "import <library>"); return; }

            string library = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            switch (library)
            {
                case "input":
                    Init.activeLibraries.Add(new Libraries.Input());
                    break;
                case "math":
                    Init.activeLibraries.Add(new Libraries.Math());
                    break;
                case "console":
                    Init.activeLibraries.Add(new Libraries.Console());
                    break;
                case "convert":
                    Init.activeLibraries.Add(new Libraries.Convert());
                    break;
                case "file":
                    Init.activeLibraries.Add(new Libraries.File());
                    break;
                default:
                    ExceptionManager.LibraryNotFound(line, library);
                    break;
            }
        }

        public static void If(string[] command, int line)
        {
            if (command.Length < 2) { ExceptionManager.SyntaxError(line, "If <conditions>"); return; }
            //dynamic executeIfBlock = Utilities.ConcatenateValues(command.Skip(1).ToArray(), line);
            dynamic executeIfBlock = Utilities.EvaluateIfConditions(line, command);
            if (executeIfBlock is bool)
            {
                Init.ifBlocks.Push(executeIfBlock);
            }
            else
            {
                ExceptionManager.ConditionsAreNotBool(line);
            }
        }

        public static int Goto(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "goto <label_name>"); return line - 1; }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); return line - 1; }
            if (!Init.labels.ContainsKey(command[1])) { ExceptionManager.LabelNotFound(line, command[1]); return line - 1; }
            return Init.labels[command[1]];
        }

        public static void Call(string[] command, int line, List<string[]> commands)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 1) { ExceptionManager.SyntaxError(line, "call <func_name>"); return; }
            if (Init.functions.ContainsKey(command[1])) { Init.ExecuteFunction(commands, Init.functions[command[1]]); }
        }
    }
}