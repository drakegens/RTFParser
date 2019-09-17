using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace RTFParser
{
    public static class RTFParser
    {
        
        public static string ParseRTFToPlaintext(string rtf)
        {
            StringBuilder result = new StringBuilder();
            var regularExpression = @"\\[a-z]+(-?[0-9]+)? ?";
            string command = string.Empty;
            string tempCommand = string.Empty;
            var inCommand = false;
            var inSpecialGroup = false;
            var inExtensibleRTF = false;
            Stack<string> commands = new Stack<string>();

            for (int i = 1; i < rtf.Length; i++)
            {
                inSpecialGroup = determineIfInSpecialGroup(commands);
                char character = rtf.ElementAt(i);
                if (!inCommand && !inSpecialGroup && !inExtensibleRTF)
                {
                    switch (character)
                    {
                        case '\\':
                            if (Regex.IsMatch((character + rtf.ElementAt(i + 1).ToString()), regularExpression))
                            {
                                inCommand = true;
                                command += character;
                            }
                            else if (rtf.ElementAt(i + 1) == '*')
                            {
                                inExtensibleRTF = true;  
                            }
                            else
                            {
                                //inescape
                                //handle special cases of escapes too
                            }
                            break;
                        case '{':
                            break;
                        case '}':
                            break;
                        default:
                            //if most recent command is a \fonttbl or \colortbl or \*\command construct... ignore until i hit a }
                            result.Append(character);
                            break;
                    }
                }
                else if (inCommand)
                {
                        command += character;
                        tempCommand = Regex.Replace(command, regularExpression, "");

                        if (tempCommand.Length > 0)
                        {
                            inCommand = false;
                            commands.Push(command.Substring(0, command.Length - 1));
                            command = string.Empty;
                            i--;
                        }
                }
                else if (inSpecialGroup)
                {
                    if(character == '}')
                    {
                        inSpecialGroup = false;
                        commands.Pop();
                    }
                }
                else if (inExtensibleRTF)
                {
                    if (character == '}')
                    {
                        inExtensibleRTF = false;
                    }
                }
            }
            return result.ToString();
        }

        private static bool determineIfInSpecialGroup(Stack<string> commands)
        {
            if (commands.Count > 0) { 
            if (commands.Peek() == "\\fonttbl" || commands.Peek() == "\\colortbl") {

                return true;
            }
            }
            return false;
        }

    }
}
