﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JScript
{
    class MainLibrary : Libraries.Library
    {
        public static void Var(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "var <name> <value>"); return; }
            if (commandLength > 2) { ExceptionManager.UnexpectedArgs(line); return; }

            dynamic varName = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            dynamic varValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
            if (!(varName is string)) { ExceptionManager.InvalidParameterType(line, varName.GetType().Name, 0, "String"); }
            // varValue puede ser el valor que sea.

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
            // result puede tener el valor que sea.
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
                // message puede ser el valor que sea.
                Console.Write(message);
            }
            Console.ReadKey(true);
        }

        public static void Reassign(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 2) { ExceptionManager.UnexpectedArgs(line); return; }
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "reassign <var_name> <new_value>"); return; }

            dynamic varName = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line, false);
            if (!(varName is string)) { ExceptionManager.InvalidParameterType(line, varName.GetType().Name, 0, "String"); }

            if (!Utilities.VariableExists(varName)) { ExceptionManager.UnknowType(line, varName); return; }
            dynamic newValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
            // newValue puede tener el valor que sea.

            Utilities.FindVariableOfName(varName).value = newValue;
        }

        public static void Exit(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); return; }
            if (commandLength > 0)
            {
                dynamic exitCode = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
                try
                {
                    int code = System.Convert.ToInt32(exitCode);
                    Environment.Exit(code);
                }
                catch { ExceptionManager.InvalidParameterType(line, exitCode.GetType().Name, 0, "Int"); return; }
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

            dynamic library = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(library is string)) { ExceptionManager.InvalidParameterType(line, library.GetType().Name, 0, "String"); }

            switch (library)
            {
                case "input":
                    Init.activeLibraries.Add(new Libraries.InputLib());
                    break;
                case "math":
                    Init.activeLibraries.Add(new Libraries.MathLib());
                    break;
                case "console":
                    Init.activeLibraries.Add(new Libraries.ConsoleLib());
                    break;
                case "convert":
                    Init.activeLibraries.Add(new Libraries.ConvertLib());
                    break;
                case "file":
                    Init.activeLibraries.Add(new Libraries.FileLib());
                    break;
                case "window":
                    Init.activeLibraries.Add(new Libraries.WindowLib());
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
            dynamic label = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(label is string)) { ExceptionManager.InvalidParameterType(line, label.GetType().Name, 0, "String"); return line - 1; }

            if (!Init.labels.ContainsKey(label)) { ExceptionManager.LabelNotFound(line, command[1]); return line - 1; }
            return Init.labels[label];
        }

        public static dynamic Call(string[] command, int line, List<string[]> commands)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 1) { ExceptionManager.SyntaxError(line, "call <func_name>"); return null; }
            dynamic func = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(func is string)) { ExceptionManager.InvalidParameterType(line, func.GetType().Name, 0, "String"); return null; }

            if (Init.functions.ContainsKey(func)) { return Init.ExecuteFunction(commands, Init.functions[command[1]]); }
            return null;
        }

        public static dynamic Return(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength > 1) { ExceptionManager.SyntaxError(line, "return <value>"); return null; }
            if (commandLength < 1) { return null; }
            dynamic value = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            return value;
        }

        public override dynamic ExecuteCommand(string[] command, int line)
        {
            switch (command[0])
            {
                case "call":
                    return MainLibrary.Call(command, line, Init.fileCommands);
            }
            return null;
        }
    }
}