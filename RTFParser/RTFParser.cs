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

        //public static string ParseRTFToPlainTextWithWinTextForm(string rtf)
        //{

        //}

        public static string ParseRTFToPlainText(string rtf)
        {

            Stack<string> state = new Stack<string>();
            StringBuilder result = new StringBuilder();
            var inCommand = false;
            var inEscape = false;
            var inGroup = false;
            StringBuilder command = new StringBuilder();


            //for validation
            //var richTextBox = new RichTextBox();
            //richTextBox.Rtf = "";
            //var rtfText = string.Join("\r\n", richTextBox.Lines);

            //get rid of control words, control symbols, and groups
            for(int i = 0; i < rtf.Length; i++)
            {
                char character = rtf.ElementAt(i);
                if (!inCommand && !inEscape && !inGroup)
                {
                    switch (character)
                    {
                        case '\\':
                            if (Regex.IsMatch((character + rtf.ElementAt(i + 1).ToString()), "/\\[a-z]+(-?[0-9]+)? ?/"))
                            {
                                inCommand = true;
                            }
                            else
                            {
                                inEscape = true;
                                //it's an escaped character
                            }

                            break;
                        case '{'://beginning of group
                            inGroup = true;
                            break;


                        default:
                            result.Append(character);
                            break;
                            //in a command or an escaped character

                    }
                }
                else if (inCommand)
                {

                    //here is the regex: /\\[a-z]+(-?[0-9]+)? ?/
                    inCommand = Regex.IsMatch(command.ToString(), "/\\[a-z]+(-?[0-9]+)? ?/");

                    //check against regular expression to determine if I can flip inCommand to off
                }
                else if (inEscape)
                {
                    //There are only three escapes that are of general interest: \~ is the escape that indicates a nonbreaking space; \- is an optional hyphen(a.k.a.a hyphenation point); and \_ is a nonbreaking hyphen(that is, a hyphen that that’s not safe for breaking the line after).
                    switch (character)
                    {
                        case '\'':
                            //get the next two characters
                            char[] chars = { rtf.ElementAt(i + 1), rtf.ElementAt(i + 2) };
                            
                            result.Append(DecodeHex(new string(chars)));
                            inEscape = false;
                            i = i + 3;
                            break;
                        //case '~':
                        //    result.Append("~");
                        //    break;
                        //case '-':
                        //    result.Append("-");
                        //    break;
                        //case '_':
                        //    result.Append("_");
                        //    break;
                        //case '*':
                        //    break;
                        default:
                            result.Append(character);
                            inEscape = false;
                            i = i + 2;
                            break;


                    }
                }
                else if (inGroup)
                {
                    if(character == '}')
                    {
                        inGroup = false;
                    }

                }
            }


            return result.ToString();

        }

        public static string DecodeHex(string hex)
        {
            string result = string.Empty;
            for (int i = 0; i < hex.Length; i += 2)
            {
                
               string hs = hex.Substring(i, 2);
                uint decval = System.Convert.ToUInt32(hs, 16);
                char character = System.Convert.ToChar(decval);
                result += character;

            }

            return result;

        }

        private static void ApplyStateToPlainText(string state)
        {

        }

        private static void TakeActionOnPlainTextViaControlAndParameters(string controlAction, List<string> parameters)
        {

        }

        private static string ExtractPlainTextFromGroup(string group)
        {
            return "";
        }

        private static string DetermineEscapedCharacter(string escapedCharacter)
        {
            return "";
        }

        private static string GetCodePage(int value)
        {
            switch (value)
            {
                case 437:
                    return "United States IBM";
                    break;
                case 708:
                    return "Arabic (ASMO 708)";
                    break;
                case 709:
                    return "Arabic (ASMO 449+, BCON V4";
                    break;
                case 710:
                    return "Arabic (transparent Arabic)";
                    break;
                case 711:
                    return "Arabic (Nafitha Enhanced";
                    break;
                case 720:
                    return "";
                    break;
                default:
                    return "";
                    break;
            }
        }


    }
}
