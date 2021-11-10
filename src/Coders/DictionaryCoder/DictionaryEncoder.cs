using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coding.DictionaryExtension;

namespace coding.Coders.DictionaryCoder
{
    public abstract class DictionaryEncoder : Encoder
    {
        protected Dictionary<char, string> codesTable;


        public DictionaryEncoder(string inputFilePath, string outputFilePath)
            : base(inputFilePath, outputFilePath) { }

        public DictionaryEncoder(Dictionary<char, string> codesTable, string inputFilePath, string outputFilePath)
            : base(inputFilePath, outputFilePath)
        {
            if (codesTable == null)
                throw new ArgumentNullException();
            this.codesTable = codesTable;
        }

        public override void Encode()
        {
            codesTable = codesTable.OrderBy(x => x.Value.Length).ThenBy(x => x.Value.Last()).ToDictionary(pair => pair.Key, pair => pair.Value);
            char key = codesTable.GetZeroesValue();
            Dictionary<char, string> newCodesTable = new Dictionary<char, string> { { key, codesTable[key] } };
            codesTable.Remove(key);
            newCodesTable.AddRange(codesTable);
            codesTable = newCodesTable;

            string str = Encoding.GetEncoding(1251).GetString(ReadFile());
            byte bytePosition;
            List<byte> outputText = encodeBytes(str, out bytePosition);

            List<byte> outputDictionary = EncodeDictionary(bytePosition);

            List<byte> output = new List<byte>();
            output.AddRange(outputDictionary);
            output.AddRange(outputText);

            WriteFile(output);
        }

        protected List<byte> encodeBytes(string bytes, out byte bytePosition)
        {
            bytePosition = 0;
            List<byte> outputText = new List<byte>();
            outputText.Add(0);
            for (int i = 0; i < bytes.Length; ++i)
            {
                string codingByte = codesTable[bytes[i]];
                int splitIndex = Math.Min(8 - bytePosition, codingByte.Length);
                string toCurrByte = codingByte.Substring(0, splitIndex);

                byte currByte = outputText[outputText.Count - 1];
                currByte = WriteByte(toCurrByte, currByte, bytePosition);
                bytePosition += (byte)toCurrByte.Length;
                outputText[outputText.Count - 1] = currByte;

                if (bytePosition > 7 && toCurrByte != codingByte)
                {
                    currByte = bytePosition = 0;
                    string toNextByte = codingByte.Substring(splitIndex);
                    currByte = WriteByte(toNextByte, currByte, bytePosition);
                    bytePosition += (byte)toNextByte.Length;
                    outputText.Add(currByte);
                }

            }

            return outputText;
        }

        private List<byte> EncodeDictionary(byte lastByteUsed)
        {
            List<byte> encodedDictionary = new List<byte>();
            encodedDictionary.Add((byte)codesTable.Count);
            if (codesTable.Count == 1)
            {
                encodedDictionary.Add((byte)(lastByteUsed - 1));
                encodedDictionary.Add(Encoding.GetEncoding(1251).GetBytes(new char[] { codesTable.ElementAt(0).Key })[0]);
                return encodedDictionary;
            }

            int maxLength = -1;
            for (int i = 0; i < codesTable.Count - 1; ++i)
            {
                byte currCode = byte.Parse(Calc.ConvertToBase(codesTable.ElementAt(i).Value, 2, 10, 1e-10));
                if (currCode > 127)
                    maxLength = 8;
                if (maxLength != 8)
                    currCode += (byte)((8 - codesTable.ElementAt(i + 1).Value.Length)
                        << codesTable[codesTable.ElementAt(i).Key].Length);
                encodedDictionary.Add(currCode);
                encodedDictionary.Add(Encoding.GetEncoding(1251).GetBytes(new char[] { codesTable.ElementAt(i).Key })[0]);
                if (i == 0)
                {
                    encodedDictionary[1] = (byte)(((8 - codesTable.ElementAt(i + 1).Value.Length) << 5)
                    + int.Parse(Calc.ConvertToBase(codesTable.ElementAt(i).Value, 2, 10, 1e-10)));
                    encodedDictionary[1] |= (byte)((lastByteUsed - 1) >> 1);
                    encodedDictionary[1] |= (byte)((8 - codesTable.ElementAt(i).Value.Length) << 2);
                }
            }
            encodedDictionary.Add(byte.Parse(Calc.ConvertToBase(
                codesTable.ElementAt(codesTable.Count - 1).Value, 2, 10, 1e-10)));
            encodedDictionary[encodedDictionary.Count - 1] = (byte)((
                encodedDictionary[encodedDictionary.Count - 1] & 254) | ((lastByteUsed - 1) & 1));
            encodedDictionary.Add(Encoding.GetEncoding(1251).GetBytes(
                new char[] { codesTable.ElementAt(codesTable.Count - 1).Key })[0]);

            return encodedDictionary;
        }

        protected List<byte> WriteAllBytes(string bytes)
        {
            List<byte> res = new List<byte>();
            for (int i = 0; i < bytes.Length; i += 8)
                res.Add(WriteByte(bytes.Substring(i, Math.Min(8, bytes.Length - i)), 0, 0));
            return res;
        }

        private byte WriteByte(string strByte, byte codingByte, byte position)
        {
            int start = position;
            int size = start + strByte.Length;
            for (; position < size; ++position)
                codingByte |= (byte)(int.Parse(strByte[position - start].ToString())
                    << (8 - position - 1));

            return codingByte;
        }

        protected Dictionary<char, double> GetSymbolPossibilities(string text)
        {
            Dictionary<char, int> charsCount = GetSymbolsCount(text);
            Dictionary<char, double> possibilities = new Dictionary<char, double>();

            foreach (KeyValuePair<char, int> charCount in charsCount)
                possibilities.Add(charCount.Key, (double)charCount.Value / text.Length);

            return possibilities;
        }

        private Dictionary<char, int> GetSymbolsCount(string text)
        {
            Dictionary<char, int> charsCount = new Dictionary<char, int>();

            for (int i = 0; i < text.Length; ++i)
            {
                if (charsCount.ContainsKey(text[i]))
                    ++charsCount[text[i]];
                else
                    charsCount.Add(text[i], 1);
            }

            return charsCount;
        }
    }
}
