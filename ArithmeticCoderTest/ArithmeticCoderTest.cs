using coding.Coders.ArithmeticCoder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace ArithmeticCoderTest
{
    [TestClass]
    public class ArithmeticCoderTest
    {
        [TestMethod]
        public void TestMethodFIORus()
        {
            string text = "Крупин Максим";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ArithmeticEncoder encoder = new ArithmeticEncoder("text.txt", "encoded.txt");
            encoder.Encode();

            ArithmeticDecoder decoder = new ArithmeticDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodFIOEng()
        {
            string text = "Krupin Maxim";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ArithmeticEncoder encoder = new ArithmeticEncoder("text.txt", "encoded.txt");
            encoder.Encode();

            ArithmeticDecoder decoder = new ArithmeticDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodLab11_1()
        {
            string text = "золото";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ArithmeticEncoder encoder = new ArithmeticEncoder("text.txt", "encoded.txt");
            encoder.Encode();

            ArithmeticDecoder decoder = new ArithmeticDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodLab11_2()
        {
            string text = "миссия";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ArithmeticEncoder encoder = new ArithmeticEncoder("text.txt", "encoded.txt");
            encoder.Encode();

            ArithmeticDecoder decoder = new ArithmeticDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodLab11_3()
        {
            string text = "свисток";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ArithmeticEncoder encoder = new ArithmeticEncoder("text.txt", "encoded.txt");
            encoder.Encode();

            ArithmeticDecoder decoder = new ArithmeticDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodText1()
        {
            string text = "мама мыла раму";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ArithmeticEncoder encoder = new ArithmeticEncoder("text.txt", "encoded.txt");
            encoder.Encode();

            ArithmeticDecoder decoder = new ArithmeticDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodText2()
        {
            string text = "кодирование";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ArithmeticEncoder encoder = new ArithmeticEncoder("text.txt", "encoded.txt");
            encoder.Encode();

            ArithmeticDecoder decoder = new ArithmeticDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodRepeating1()
        {
            string text = "аааааааааа";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ArithmeticEncoder encoder = new ArithmeticEncoder("text.txt", "encoded.txt");
            encoder.Encode();

            ArithmeticDecoder decoder = new ArithmeticDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodRepeating2()
        {
            string text = "абабабабаб";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ArithmeticEncoder encoder = new ArithmeticEncoder("text.txt", "encoded.txt");
            encoder.Encode();

            ArithmeticDecoder decoder = new ArithmeticDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }
    }
}
