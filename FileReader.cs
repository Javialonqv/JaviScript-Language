using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyOwnLanguageNEW
{
    public class FileReader
    {
        public List<string[]> lines;

        public FileReader(string filePath)
        {
            lines = new List<string[]>();
            string[] file = File.ReadAllLines(filePath);
            foreach (string s in file)
            {
                lines.Add(SplitInput(s));
            }
        }

        string[] SplitInput(string line)
        {
            string[] splited = null;
            var chars = line.ToCharArray();
            bool isQuote = false;
            bool isParenthesis = false;

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '"')
                {
                    isQuote = !isQuote;
                }
                if (chars[i] == '(' || chars[i] == ')') { isParenthesis = !isParenthesis; }
                if (chars[i] == ' ' && !isQuote && !isParenthesis) { chars[i] = '\0'; }
            }
            splited = new string(chars).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            return splited;
        }

        /*string[] SplitInput(string line)
        {
            List<string> splitList = new List<string>();
            bool isInsideQuotes = false;
            StringBuilder currentPart = new StringBuilder();

            foreach (char c in line)
            {
                if (c == '"')
                {
                    isInsideQuotes = !isInsideQuotes;
                    continue; // Saltar al siguiente carácter si es una comilla
                }

                if (c == ' ' && !isInsideQuotes)
                {
                    if (currentPart.Length > 0)
                    {
                        splitList.Add(currentPart.ToString()); // Agregar la parte actual a la lista
                        currentPart.Clear(); // Limpiar para la siguiente parte
                    }
                    continue; // Saltar al siguiente carácter si es un espacio fuera de comillas
                }

                currentPart.Append(c); // Agregar el carácter a la parte actual
            }

            // Agregar la última parte después de salir del bucle
            if (currentPart.Length > 0)
            {
                splitList.Add(currentPart.ToString());
            }

            return splitList.ToArray();
        }*/
    }
}
