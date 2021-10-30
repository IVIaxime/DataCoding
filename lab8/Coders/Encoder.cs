using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Coders
{
    public abstract class Encoder : Coder<List<byte>, byte[]>, IEncodable
    {
        protected Encoder(string inputFilePath, string outputFilePath) : base(inputFilePath, outputFilePath) { }


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

        public double GetCompressionCoeff()
        {
            return (1 - (double)File.ReadAllBytes(outputFilePath).Length / File.ReadAllBytes(inputFilePath).Length) * 100;
        }

        public override void WriteFile(List<byte> encodedText)
        {
            File.WriteAllBytes(outputFilePath, encodedText.ToArray());
        }

        public override byte[] ReadFile()
        {
            return File.ReadAllBytes(inputFilePath);
        }

        public abstract void Encode();
        public abstract double GetEncodingPrice();
    }
}
