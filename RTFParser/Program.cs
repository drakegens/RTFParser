using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTFParser
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string rtfResult = GrabRTFString();

            Debug.WriteLine("RTF File: " + rtfResult);

            string plainTextResult = RTFParser.ParseRTFToPlaintext(rtfResult);

            Debug.WriteLine("Result: " + plainTextResult);

            //record db command here



            //The first thing to do is grab the RTF file

            //Then parse the RTF file

            //Then spit out the plaintext into a text file

            //Let's also record each 'request' in a postgres database with result, timestamp, etc...

            //any other functionality?

            //lets add unit tests
        }

        private static string GrabRTFString()
        {
            string fileContents = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "rtf files (*.rtf)|*.rtf";
                openFileDialog.ShowDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContents = reader.ReadToEnd();
                    }
                }
                return fileContents;
            }
        }
    }
}
