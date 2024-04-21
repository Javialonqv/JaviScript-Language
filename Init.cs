using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyOwnLanguageNEW
{
    static class Init
    {
        public static List<Variable> runtimeVariables = new List<Variable>();
        public static List<Libraries.Library> activeLibraries = new List<Libraries.Library>();
        public static Dictionary<string, int> labels = new Dictionary<string, int>();
        public static Stack<bool> ifBlocks = new Stack<bool>();
        public static Dictionary<string, int> functions = new Dictionary<string, int>();
        static bool inFunc = false;

        [STAThread]
        static void Main(string[] args)
        {
            List<string[]> fileContents = new List<string[]>();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select your code file.";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileContents = new FileReader(dialog.FileName).lines;
                FirstRead(fileContents);
                ExecuteCommands(fileContents);
            }
        }

        // Una primera leia al codigo para poder guardar las etiquetas antes de ejecutar el resto del código:
        static void FirstRead(List<string[]> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                bool readingFunc = false;
                string[] command = commands[i];
                if (command == null || command.Length == 0) { continue; }
                int line = i + 1;
                if (command[0] == "Func" && !readingFunc)
                {
                    if (command.Length > 2) { continue; }
                    if (command.Length < 2) { continue; }
                    readingFunc = true;
                    functions.Add(command[1], i);
                }
                if (command[0] == "EndFunc")
                {
                    if (command.Length > 1) { continue; }
                    readingFunc = false;
                }
                if (command[0].StartsWith("::"))
                {
                    string label = command[0].Substring(2);
                    if (labels.Any(pair => pair.Key == label && pair.Value != i)) { ExceptionManager.LabelAlreadyExists(line, label); return; }
                    if (labels.Any(pair => pair.Key == label && pair.Value == i)) { return; }
                    labels.Add(label, i);
                }
            }
        }

        static void ExecuteCommands(List<string[]> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                string[] command = commands[i];
                if (command == null || command.Length == 0) { continue; }
                if (ifBlocks.Count > 0 && !ifBlocks.Peek())
                {
                    if (command[0] == "EndIf")
                    {
                        ifBlocks.Pop();
                    }
                    else
                    {
                        continue;
                    }
                }
                int line = i + 1;
                // En caso de que la instrucción en realidad esté en una libreria PREVIAMENTE CARGADA:
                dynamic executedByLibrary = null;
                foreach (Libraries.Library library in Init.activeLibraries)
                {
                    // Si es true entonces la instruccion se ejecuto correctamente:
                    if (executedByLibrary == null && !inFunc) { executedByLibrary = library.ExecuteCommand(command, line); }
                }
                if (executedByLibrary == null && !inFunc)
                {
                    switch (command[0])
                    {
                        case "var":
                            MainLibrary.Var(command, line);
                            break;
                        case "print":
                            MainLibrary.Print(command, line, false);
                            break;
                        case "printl":
                            MainLibrary.Print(command, line, true);
                            break;
                        case "pause":
                            MainLibrary.Pause(command, line);
                            break;
                        case "exit":
                            MainLibrary.Exit(command, line);
                            break;
                        case "reassign":
                            MainLibrary.Reassign(command, line);
                            break;
                        case "import":
                            MainLibrary.Import(command, line);
                            break;
                        case "goto":
                            i = MainLibrary.Goto(command, line);
                            break;
                        case "If":
                            MainLibrary.If(command, line);
                            break;
                        case "Func":
                            inFunc = true;
                            break;
                        case "call":
                            MainLibrary.Call(command, line, commands);
                            break;
                        default:
                            if (command[0] == "EndIf" || command[0].StartsWith("#")) { break; }
                            if (command[0].StartsWith("::"))
                            {
                                string label = command[0].Substring(2);
                                if (labels.Any(pair => pair.Key == label && pair.Value != i)) { ExceptionManager.LabelAlreadyExists(line, label); break; }
                                if (labels.Any(pair => pair.Key == label && pair.Value == i)) { break; }
                                labels.Add(label, i);
                                break;
                            }
                            ExceptionManager.UnknowCommand(line, command[0]);
                            break;
                    }
                }
                if (command[0] == "EndFunc") { inFunc = false; }
            }
        }

        public static void ExecuteFunction(List<string[]> commands, int startIndex)
        {
            for (int i = startIndex; i < commands.Count; i++)
            {
                string[] command = commands[i];
                if (command == null || command.Length == 0) { continue; }
                if (ifBlocks.Count > 0 && !ifBlocks.Peek())
                {
                    if (command[0] == "EndIf")
                    {
                        ifBlocks.Pop();
                    }
                    else
                    {
                        continue;
                    }
                }
                int line = i + 1;
                // En caso de que la instrucción en realidad esté en una libreria PREVIAMENTE CARGADA:
                bool executedByLibrary = false;
                foreach (Libraries.Library library in Init.activeLibraries)
                {
                    // Si es true entonces la instruccion se ejecuto correctamente:
                    if (!executedByLibrary) { executedByLibrary = library.ExecuteCommand(command, line); }
                }
                if (!executedByLibrary)
                {
                    switch (command[0])
                    {
                        case "Func":
                            break;
                        case "var":
                            MainLibrary.Var(command, line);
                            break;
                        case "print":
                            MainLibrary.Print(command, line, false);
                            break;
                        case "printl":
                            MainLibrary.Print(command, line, true);
                            break;
                        case "pause":
                            MainLibrary.Pause(command, line);
                            break;
                        case "exit":
                            MainLibrary.Exit(command, line);
                            break;
                        case "reassign":
                            MainLibrary.Reassign(command, line);
                            break;
                        case "import":
                            MainLibrary.Import(command, line);
                            break;
                        case "goto":
                            i = MainLibrary.Goto(command, line);
                            break;
                        case "If":
                            MainLibrary.If(command, line);
                            break;
                        case "EndFunc":
                            return;
                        default:
                            if (command[0] == "EndIf" || command[0].StartsWith("#")) { break; }
                            if (command[0].StartsWith("::"))
                            {
                                string label = command[0].Substring(2);
                                if (labels.Any(pair => pair.Key == label && pair.Value != i)) { ExceptionManager.LabelAlreadyExists(line, label); break; }
                                if (labels.Any(pair => pair.Key == label && pair.Value == i)) { break; }
                                labels.Add(label, i);
                                break;
                            }
                            ExceptionManager.UnknowCommand(line, command[0]);
                            break;
                    }
                }
            }
        }
    }
}
