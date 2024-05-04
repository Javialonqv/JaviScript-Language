using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JScript.Libraries
{
    class WindowLib : Library
    {
        public override dynamic ExecuteCommand(string[] command, int line)
        {
            switch (command[0])
            {
                case "window.file_dialog":
                    return FileDialog(command, line);
            }
            return null;
        }

        public static dynamic FileDialog(string[] command, int line)
        {
            int commandLength = Utilities.GetParametersNumber(command.Skip(1).ToArray(), line);
            if (commandLength < 1) { ExceptionManager.SyntaxError(line, "window.file_dialog <title>"); return null; }
            if (commandLength > 1) { ExceptionManager.UnexpectedArgs(line); }

            string title = Utilities.GetCommandParameter(command.Skip(1).ToArray(), 0, line);
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = title;
            return dialog;
        }
    }
}
