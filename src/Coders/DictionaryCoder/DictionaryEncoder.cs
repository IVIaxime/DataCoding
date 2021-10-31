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
            byte bytePosition = 0;
            List<byte> outputText = new List<byte>();
            outputText.Add(0);
            for (int i = 0; i < str.Length; ++i)
            {
                string codingByte = codesTable[str[i]];
                int splitIndex = Math.Min(8 - bytePosition, codingByte.Length);
                string toCurrByte = codingByte.Substring(0, splitIndex);

                byte currByte = outputText[outputText.Count - 1];
                currByte = WriteByte(toCurrByte, currByte, bytePosition);
                bytePosition += (byte)toCurrByte.Length;
                outputText[outputText.Count - 1] = currByte;

                if (bytePosition > 7)
                {
                    currByte = bytePosition = 0;
                    string toNextByte = codingByte.Substring(splitIndex);
                    currByte = WriteByte(toNextByte, currByte, bytePosition);
                    bytePosition += (byte)toNextByte.Length;
                    outputText.Add(currByte);
                }

            }

            List<byte> outputDictionary = EncodeDictionary(bytePosition);

            List<byte> output = new List<byte>();
            output.AddRange(outputDictionary);
            output.AddRange(outputText);

            WriteFile(output);
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
                byte currCode = (byte)BinaryToDecimal(codesTable.ElementAt(i).Value);
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
                    + BinaryToDecimal(codesTable.ElementAt(i).Value));
                    encodedDictionary[1] |= (byte)((lastByteUsed - 1) >> 1);
                    encodedDictionary[1] |= (byte)((8 - codesTable.ElementAt(i).Value.Length) << 2);
                }
            }
            encodedDictionary.Add((byte)BinaryToDecimal(
                codesTable.ElementAt(codesTable.Count - 1).Value));
            encodedDictionary[encodedDictionary.Count - 1] = (byte)((
                encodedDictionary[encodedDictionary.Count - 1] & 254) | ((lastByteUsed - 1) & 1));
            encodedDictionary.Add(Encoding.GetEncoding(1251).GetBytes(
                new char[] { codesTable.ElementAt(codesTable.Count - 1).Key })[0]);

            return encodedDictionary;
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
    }
}
