using System;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RTFParser.UnitTest
{
    [TestClass]
    public class RTFParserTest
    {
        [TestMethod]
        public void CompareAgainstMyParser()
        {

            //var richTextBox = new RichTextBox();
            //richTextBox.Rtf = "";
            //var rtfText = string.Join("\r\n", richTextBox.Lines);
            var richTextBox = new RichTextBox();
            richTextBox.Rtf = @"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033{\fonttbl{\f0\fnil\fcharset0 Calibri;}}" +
@"{\*\generator Riched20 10.0.18362}\viewkind4\uc1" +
@"\pard\sa200\sl276\slmult1\f0\fs22\lang9 This is a simple rtf file\par" +
@"}";

            Assert.AreEqual(string.Join("\r\n", richTextBox.Lines), "This is a simple rtf file");
        }

        [TestMethod]
        public void DecodeHexTest()
        {
           Assert.AreEqual("E", RTFParser.DecodeHex("45"));
        }
    }
}
