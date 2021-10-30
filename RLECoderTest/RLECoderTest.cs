using lab8.Coders.RLECoder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;

namespace RLECoderTest
{
    [TestClass]
    public class RLECoderTest
    {
        [TestMethod]
        public void TestMethodLab8Task1()
        {
            string text = "Крупин Максим Сергеевич, 22.09.2001 года";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();

            RLEDecoder rleDecoder = new RLEDecoder("encoded.txt", "decoded.txt");
            rleDecoder.Decode();
            string decoded = File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251));

            Assert.AreEqual(text, decoded);
        }

        [TestMethod]
        public void TestMethodLab8Task2()
        {
            string text = "ОБОЖЖЕННЫЙ";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();

            RLEDecoder rleDecoder = new RLEDecoder("encoded.txt", "decoded.txt");
            rleDecoder.Decode();
            string decoded = File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251));

            Assert.AreEqual(text, decoded);
        }

        [TestMethod]
        public void TestMethodRepeatedString()
        {
            string text = "АААААААААААААААААААА";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();

            RLEDecoder rleDecoder = new RLEDecoder("encoded.txt", "decoded.txt");
            rleDecoder.Decode();
            string decoded = File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251));

            Assert.AreEqual(text, decoded);
        }

        [TestMethod]
        public void TestMethodAlphabet()
        {
            string text = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();

            RLEDecoder rleDecoder = new RLEDecoder("encoded.txt", "decoded.txt");
            rleDecoder.Decode();
            string decoded = File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251));

            Assert.AreEqual(text, decoded);
        }

        [TestMethod]
        public void TestMethodMorseCode()
        {
            string text = "- .... . .--. .. -.-. - ..- .-. . --- ..-. -.. --- .-. .. .- -. --. .-. .- -.-- -... " +
                "-.-- --- ... -.-. .- .-. .-- .. .-.. -.. . - .... . .--. .-. . ..-. .- -.-. . - .... . .- .-. -" +
                " .. ... - .. ... - .... . -.-. .-. . .- - --- .-. --- ..-. -... . .- ..- - .. ..-. ..- .-.. - .." +
                ".. .. -. --. ... ...... - --- .-. . ...- . .- .-.. .- .-. - .- -. -.. -.-. --- -. -.-. . .- .-.." +
                " - .... . .- .-. - .. ... - .. ... .- .-. - .-... ...-- ----. -.-.-. ... .- .. -- ...... - .... " +
                ". -.-. .-. .. - .. -.-. .. ... .... . .-- .... --- -.-. .- -. - .-. .- -. ... .-.. .- - . .. -. -" +
                " --- .- -. --- - .... . .-. -- .- -. -. . .-. --- .-. .- -. . .-- -- .- - . .-. .. .- .-.. .... " +
                ".. ... .. -- .--. .-. .".Replace(" ", "");
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();

            RLEDecoder rleDecoder = new RLEDecoder("encoded.txt", "decoded.txt");
            rleDecoder.Decode();
            string decoded = File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251));

            Assert.AreEqual(text, decoded);
        }

        [TestMethod]
        public void TestMethodBookEnglishText()
        {
            string text = "In the year 1878 I took my degree of Doctor of Medicine of the" +
                " University of London, and proceeded to Netley to go through the course" +
                " prescribed for surgeons in the army. Having completed my studies there," +
                " I was duly attached to the Fifth Northumberland Fusiliers as Assistant Surgeon.";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();

            RLEDecoder rleDecoder = new RLEDecoder("encoded.txt", "decoded.txt");
            rleDecoder.Decode();
            string decoded = File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251));

            Assert.AreEqual(text, decoded);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestMethodBookRussianTextTooManyNotReapeated()
        {
            string text = "В 1878 году я окончил Лондонский университет, получив звание врача," +
                " и сразу же отправился в Нетли, где прошел специальный курс для военных" +
                " хирургов. После окончания занятий я был назначен ассистентом хирурга в Пятый" +
                " Нортумберлендский стрелковый полк.";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();
        }

        [TestMethod]
        public void TestMethodBookRussianText()
        {
            string text = "Дремотное жужжание пчел, то пробивавших себе дорогу в нескошенной" +
                " высокой траве, то с однообразной настойчивостью кружившихся над пыльными," +
                " золочеными усиками вьющейся лесной мальвы, как будто делали тишину еще более тягостной";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();

            RLEDecoder rleDecoder = new RLEDecoder("encoded.txt", "decoded.txt");
            rleDecoder.Decode();
            string decoded = File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251));

            Assert.AreEqual(text, decoded);
        }

        [TestMethod]
        public void TestMethod127RepeatedSymbols()
        {
            string text = "аааааааааааааааааааааааааааааааааааааааааааааааааааааааааааааааааа" +
                "ааааааааааааааааааааааааааааааааааааааааааааааааааааааааааааа";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();

            RLEDecoder rleDecoder = new RLEDecoder("encoded.txt", "decoded.txt");
            rleDecoder.Decode();
            string decoded = File.ReadAllText("decoded.txt", Encoding.GetEncoding(1251));

            Assert.AreEqual(text, decoded);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestMethod128RepeatedSymbols()
        {
            string text = "аааааааааааааааааааааааааааааааааааааааааааааааааааааааааааа" +
                "аааааааааааааааааааааааааааааааааааааааааааааааааааааааааааааааааааа";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestMethod128NotRepeatedSymbols()
        {
            string text = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяабвгдеёжзийклмнопрстуфхцчшщъыьэюяабвгдеёжзийклмнопрстуфхцчшщъыьэюяабвгдеёжзийклмнопрстуфхцчшщъы";
            File.WriteAllText("text.txt", text, Encoding.GetEncoding(1251));

            RLEEncoder rleEncoder = new RLEEncoder("text.txt", "encoded.txt");
            rleEncoder.Encode();
        }
    }
}
