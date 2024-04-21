using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyOwnLanguageNEW
{
    static class ExceptionManager
    {
        public static void UnexpectedArgs(int line)
        {
            ShowException(line, "Unexpected Args", $"ERROR: Unexpected arguments at line {line}.");
        }
        public static void SyntaxError(int line, string syntax)
        {
            ShowException(line, "Syntax Error", $"ERROR: Syntax error at line {line}. Syntax: {syntax}.");
        }
        public static void VariableAlreadyExists(int line, string varName)
        {
            ShowException(line, "Variable Already Exists", $"ERROR: Variable {varName} already exists. Error at line {line}.");
        }
        public static void UnknowCommand(int line, string command)
        {
            ShowException(line, "Unknow Command", $"ERROR: Unknow command {command} at line {line}.");
        }
        public static void UnknowType(int line, string text)
        {
            ShowException(line, "Unknow Type", $"ERROR: Unknow {text} type at line {line}. Maybe a missing type.");
        }
        public static void UnknowOperation(int line, string op)
        {
            ShowException(line, "Unknow Operation", $"ERROR: Unknow {op} operator at line {line}.");
        }
        public static void OperationNOTAllowed(int line, string op)
        {
            ShowException(line, "Operation NOT Allowed", $"ERROR: {op} operation NOT allowed at line {line}.");
        }
        public static void VariableNameNOTAllowed(int line, string varName)
        {
            ShowException(line, "Variable Name NOT Allowed", $"ERROR: The variable name {varName} is NOT allowed at line {line}.");
        }
        public static void InvalidType(int line, string type)
        {
            ShowException(line, "Invalid Type", $"ERROR: Invalid {type} type at line {line}. Maybe a missing library.");
        }
        public static void LibraryNotFound(int line, string library)
        {
            ShowException(line, "Library NOT Found", $"ERROR: {library} library not found at line {line}.");
        }
        public static void ConditionsAreNotBool(int line)
        {
            ShowException(line, "Conditions Are NOT Bool", $"ERROR: If statement conditions NOT return a bool value. Error at line {line}.");
        }
        public static void LabelNotFound(int line, string label)
        {
            ShowException(line, "Label NOT Found", $"ERROR: Label {label} NOT found at line {line}.");
        }
        public static void LabelAlreadyExists(int line, string label)
        {
            ShowException(line, "Label Already Exists", $"ERROR: Label {label} already exists. Error at line {line}.");
        }
        public static void ConversionNotAllowed(int line, dynamic varType, string toConvert)
        {
            ShowException(line, "Conversion NOT Allowed", $"ERROR: Cannot convert type {varType} to type {toConvert}. Error at line {line}.");
        }
        public static void FileDoesntExists(int line, string filePath)
        {
            ShowException(line, "File Doesn't Exists", $"ERROR: File at {filePath} doesn't exists. Error at line {line}.");
        }
        public static void DirDoesntExists(int line, string dirPath)
        {
            ShowException(line, "Directory Doesn't Exists", $"ERROR: Directory at {dirPath} does not exists. Error at line {line}.");
        }
        public static void FileAlreadyExists(int line, string filePath)
        {
            ShowException(line, "File Already Exists", $"ERROR: File at {filePath} already exists. Error at line {line}.");
        }
        public static void InvalidParameterType(int line, string type, int parameter, string neededType)
        {
            ShowException(line, "Invalid Parameter Type", $"ERROR: Invalid {type} type at {parameter} parameter, must be {neededType}." +
                $" Error at line {line}.");

        }
        public static void InvalidColorName(int line, string name)
        {
            ShowException(line, "Invalid Color Name", $"ERROR: The color name {name} is INVALID and doesn't exists.");
        }

        static void ShowException(int line, string title, string message)
        {
            try { throw new Exception(); }
            catch (Exception e) { MessageBox.Show(message + e.StackTrace, title, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
