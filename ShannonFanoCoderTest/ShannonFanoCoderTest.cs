using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using coding.Coders;
using System.IO;
using System.Text;
using coding.Coders.ShannonFanoCoder;

namespace ShannonFanoCoderTest
{
    [TestClass]
    public class ShannonFanoCoderTest
    {
        [TestMethod]
        public void TestMethodFIO()
        {
            string text = "Крупин Максим Сергеевич, 22.09.2001 года";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ShannonFanoEncoder encoder = new ShannonFanoEncoder("text.txt", "encoded.txt");
            encoder.Encode();
            ShannonFanoDecoder decoder = new ShannonFanoDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodFIOEng()
        {
            string text = "Krupin Maxim Sergeevich, 09.22.2001 year";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ShannonFanoEncoder encoder = new ShannonFanoEncoder("text.txt", "encoded.txt");
            encoder.Encode();
            ShannonFanoDecoder decoder = new ShannonFanoDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodAlphabet()
        {
            string text = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяabcdefghijklmnopqrstuvwxyz";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ShannonFanoEncoder encoder = new ShannonFanoEncoder("text.txt", "encoded.txt");
            encoder.Encode();
            ShannonFanoDecoder decoder = new ShannonFanoDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodLab2Text()
        {
            string text = "общая технологическая схема изготовления сплавного транзистора" +
                " напоминает схему изготовления диода, за исключением того, что в" +
                " полупроводниковую пластинку производят вплавлению двух навесок примесей" +
                " с двух сторон. вырезанные из монокристалла германия или кремния пластинки" +
                " шлифуют и травят до необходимой толщины.";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ShannonFanoEncoder encoder = new ShannonFanoEncoder("text.txt", "encoded.txt");
            encoder.Encode();
            ShannonFanoDecoder decoder = new ShannonFanoDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodTextEnglish()
        {
            string text = "in the year 1878 i took my degree of doctor of medicine of the" +
                " university of london, and proceeded to netley to go through the course" +
                " prescribed for surgeons in the army. having completed my studies there," +
                " i was duly attached to the fifth northumberland fusiliers as assistant surgeon.";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ShannonFanoEncoder encoder = new ShannonFanoEncoder("text.txt", "encoded.txt");
            encoder.Encode();
            ShannonFanoDecoder decoder = new ShannonFanoDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodRepeatingString1()
        {
            string text = "аааааааааааааааааааа";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ShannonFanoEncoder encoder = new ShannonFanoEncoder("text.txt", "encoded.txt");
            encoder.Encode();
            ShannonFanoDecoder decoder = new ShannonFanoDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodRepeatingString2()
        {
            string text = "абабабабабабабабабабабабабабабабабабабаб";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ShannonFanoEncoder encoder = new ShannonFanoEncoder("text.txt", "encoded.txt");
            encoder.Encode();
            ShannonFanoDecoder decoder = new ShannonFanoDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }

        [TestMethod]
        public void TestMethodRepeatingString3()
        {
            string text = "абвабвабвабвабвабвабвабвабвабвабвабва";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));
            ShannonFanoEncoder encoder = new ShannonFanoEncoder("text.txt", "encoded.txt");
            encoder.Encode();
            ShannonFanoDecoder decoder = new ShannonFanoDecoder("encoded.txt", "decoded.txt");
            decoder.Decode();
            Assert.AreEqual(text, File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251)));
        }
    }
}
