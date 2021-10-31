using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding.Coders.DictionaryCoder
{
    public abstract class DictionaryDecoder : Decoder
    {
        public DictionaryDecoder(string inputFilePath, string outputFilePath)
            : base(inputFilePath, outputFilePath) { }


        public override void Decode()
        {
            byte lastUsedBits;
            byte[] bytes = ReadFile();
            Dictionary<string, char> dictionary = ReadDictionary(bytes, out lastUsedBits);

            string decoded = "";
            string currSymbol = "";
            for (int i = dictionary.Count * 2 + 1; i < bytes.Length - 1; ++i)
            {
                for (int j = 7; j >= 0; --j)
                {
                    currSymbol += ((bytes[i] & (1 << j)) != 0 ? 1 : 0).ToString();
                    if (dictionary.ContainsKey(currSymbol))
                    {
                        decoded += dictionary[currSymbol];
                        currSymbol = "";
                    }
                }
            }

            for (int j = 7; j >= 8 - lastUsedBits - 1; --j)
            {
                currSymbol += ((bytes[bytes.Length - 1] & (1 << j)) != 0 ? 1 : 0).ToString();
                if (dictionary.ContainsKey(currSymbol))
                {
                    decoded += dictionary[currSymbol];
                    currSymbol = "";
                }
            }

            WriteFile(decoded);
        }

        private Dictionary<string, char> ReadDictionary(byte[] bytes, out byte lastUnusedBits)
        {
            Dictionary<string, char> dictionary = new Dictionary<string, char>();

            int dicSize = bytes[0];
            if (dicSize == 1)
            {
                lastUnusedBits = (byte)(bytes[1] & 7);
                dictionary.Add("0", Encoding.GetEncoding(1251).GetChars(new byte[] { bytes[2] })[0]);
                return dictionary;
            }

            lastUnusedBits = (byte)(bytes[1] & 3);
            bytes[1] &= 252;
            int nextUnusedBits = (bytes[1] & 28) >> 2;
            bytes[1] &= 227;
            string key = new string('0', 8 - nextUnusedBits);
            dictionary.Add(key, Encoding.GetEncoding(1251).GetChars(new byte[] { bytes[2] })[0]);
            nextUnusedBits = (bytes[1] & 224) >> 5;

            for (int i = 3; i < dicSize * 2 - 1; i += 2)
            {
                key = string.Join("", DecimalToBinary(bytes[i] & (byte)(255 >> nextUnusedBits)));
                key = new string('0', 8 - nextUnusedBits - key.Length) + key;
                dictionary.Add(key, Encoding.GetEncoding(1251).GetChars(new byte[] { bytes[i + 1] })[0]);
                nextUnusedBits = bytes[i] >> (8 - nextUnusedBits);
            }
            lastUnusedBits = (byte)((lastUnusedBits << 1) | (bytes[dicSize * 2 - 1] & 1));
            bytes[dicSize * 2 - 1] |= 1;
            key = string.Join("", DecimalToBinary(bytes[dicSize * 2 - 1] & (byte)(255 >> nextUnusedBits)));
            key = new string('0', 8 - nextUnusedBits - key.Length) + key;
            dictionary.Add(key, Encoding.GetEncoding(1251).GetChars(new byte[] { bytes[dicSize * 2] })[0]);

            return dictionary;
        }
    }
}
