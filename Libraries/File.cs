using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyOwnLanguageNEW.Libraries
{
    class File : Library
    {
        public override dynamic ExecuteCommand(string[] command, int line)
        {
            switch (command[0])
            {
                case "file.create":
                    Create(command, line);
                    return true;
                case "file.copy":
                    Copy(command, line);
                    return true;
                case "file.move":
                    Move(command, line);
                    return true;
                case "file.exists":
                    return Exists(command, line);
                case "file.read_line":
                    return ReadLine(command, line);
                case "file.write_line":
                    WriteLine(command, line);
                    return true;
            }
            return null;
        }

        public void Create(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "file.create <file_path> [<overwrite>]"); return; }
            if (commandLength > 2) { ExceptionManager.UnexpectedArgs(line); return; }

            dynamic filePath = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(filePath is string)) { ExceptionManager.InvalidParameterType(line, filePath.GetType().Name, 0, "String"); return; }
            dynamic overwrite = false;
            if (commandLength == 2)
            {
                overwrite = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
                if (!(overwrite is bool)) { ExceptionManager.InvalidParameterType(line, overwrite.GetType().Name, 1, "Bool"); }
                return;
            }

            if (!System.IO.File.Exists(filePath) && !overwrite) { ExceptionManager.FileAlreadyExists(line, filePath); return; }
            System.IO.File.Create(filePath, 1);
        }
        public void Copy(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "file.copy <original_file_path> <destination_file_path> [<overwrite>]"); return; }
            if (commandLength > 2) { ExceptionManager.UnexpectedArgs(line); return; }

            dynamic originFilePath = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(originFilePath is string)) { ExceptionManager.InvalidParameterType(line, originFilePath.GetType().Name, 0, "String"); return; }
            dynamic destFilePath = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
            if (!(destFilePath is string)) { ExceptionManager.InvalidParameterType(line, destFilePath.GetType().Name, 1, "String"); return; }
            dynamic dirPath = System.IO.Path.GetDirectoryName(destFilePath);
            dynamic overwrite = false;
            if (commandLength == 3)
            {
                overwrite = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 2, line);
                if (!(overwrite is bool)) { ExceptionManager.InvalidParameterType(line, overwrite.GetType().Name, 1, "Bool"); }
                return;
            }

            if (!System.IO.File.Exists(originFilePath)) { ExceptionManager.FileDoesntExists(line, originFilePath); return; }
            if (!System.IO.Directory.Exists(dirPath)) { ExceptionManager.DirDoesntExists(line, dirPath); return; }
            if (System.IO.File.Exists(destFilePath) && !overwrite) { ExceptionManager.FileAlreadyExists(line, destFilePath); return; }

            System.IO.File.Copy(originFilePath, destFilePath, overwrite);
        }
        public void Move(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "file.move <original_file_path> <destination_file_path>"); return; }
            if (commandLength > 2) { ExceptionManager.UnexpectedArgs(line); return; }

            string originFilePath = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(originFilePath is string)) { ExceptionManager.InvalidParameterType(line, originFilePath.GetType().Name, 0, "String"); return; }
            string destFilePath = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
            if (!(destFilePath is string)) { ExceptionManager.InvalidParameterType(line, destFilePath.GetType().Name, 1, "String"); return; }
            string dirPath = System.IO.Path.GetDirectoryName(destFilePath);

            if (!System.IO.File.Exists(originFilePath)) { ExceptionManager.FileDoesntExists(line, originFilePath); return; }
            if (!System.IO.Directory.Exists(dirPath)) { ExceptionManager.DirDoesntExists(line, dirPath); return; }

            System.IO.File.Move(originFilePath, destFilePath);
        }
        public bool Exists(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "file.exists <file_path>"); return false; }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); return false; }
            dynamic filePath = Utilities.GetCommandParameter(command, 0, line);
            if (!(filePath is string)) { ExceptionManager.InvalidParameterType(line, filePath.GetType().Name, 0, "String"); return false; }
            return System.IO.File.Exists(filePath);
        }
        public string ReadLine(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 2) { ExceptionManager.SyntaxError(line, "file.read_line <file_path> <line>"); return ""; }
            if (commandLength > 2) { ExceptionManager.UnexpectedArgs(line); return ""; }

            dynamic filePath = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(filePath is string)) { ExceptionManager.InvalidParameterType(line, filePath.GetType().Name, 0, "String"); return ""; }
            dynamic fileLine = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
            if (!(fileLine is int)) { ExceptionManager.InvalidParameterType(line, fileLine.GetType().Name, 1, "Int"); return ""; }

            if (!System.IO.File.Exists(filePath)) { ExceptionManager.FileDoesntExists(line, filePath); return ""; }
            string[] fileLines = System.IO.File.ReadAllLines(filePath);
            if (fileLine > fileLines.Length - 1) { return ""; }
            return fileLines[fileLine];
        }
        public void WriteLine(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 3) { ExceptionManager.SyntaxError(line, "file.write_line <file_path> <line> <new_value>"); return; }
            if (commandLength > 3) { ExceptionManager.UnexpectedArgs(line); return; }

            dynamic filePath = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            if (!(filePath is string)) { ExceptionManager.InvalidParameterType(line, filePath.GetType().Name, 0, "String"); return; }
            dynamic fileLine = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 1, line);
            if (!(fileLine is int)) { ExceptionManager.InvalidParameterType(line, fileLine.GetType().Name, 1, "Int"); return; }
            dynamic newValue = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 2, line);
            if (!(newValue is string)) { ExceptionManager.InvalidParameterType(line, newValue.GetType().Name, 2, "String"); return; }

            if (!System.IO.File.Exists(filePath)) { ExceptionManager.FileDoesntExists(line, filePath); return; }
            string[] fileLines = System.IO.File.ReadAllLines(filePath);
            if (fileLine > fileLines.Length - 1) { return; }
            fileLines[fileLine] = newValue;
            System.IO.File.WriteAllLines(filePath, fileLines);
            return;
        }
    }
}
