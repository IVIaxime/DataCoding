using lab8.Coders.DictionaryCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Coders.ShannonFanoCoder
{
    public class ShannonFanoEncoder : DictionaryEncoder
    {
        public ShannonFanoEncoder(string inputFilePath, string outputFilePath)
            : base(inputFilePath, outputFilePath) { }

        public override void Encode()
        {
            string input = Encoding.GetEncoding(1251).GetString(ReadFile());
            Dictionary<char, double> possibilities = GetSymbolPossibilities(input);
            possibilities = possibilities.OrderBy(x => x.Value).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
            codesTable = new Dictionary<char, string>();
            GetCodingTable(possibilities, codesTable);
            base.Encode();
        }

        public override double GetEncodingPrice()
        {
            string input = Encoding.GetEncoding(1251).GetString(ReadFile());
            Dictionary<char, double> possibilities = GetSymbolPossibilities(input);
            possibilities = possibilities.OrderBy(x => x.Value).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
            Dictionary<char, string> codesTable = new Dictionary<char, string>();
            GetCodingTable(possibilities, codesTable);

            double price = 0;
            foreach (KeyValuePair<char, double> symbol in possibilities)
                price += symbol.Value * codesTable[symbol.Key].Length;

            return price;
        }

        private void GetCodingTable(Dictionary<char, double> possibilities, Dictionary<char, string> codes)
        {
            if (possibilities.Count == 1)
            {
                codes.Add(possibilities.First().Key, "0");
                return;
            }
            foreach (KeyValuePair<char, double> pair in possibilities)
                codes.Add(pair.Key, "");

            GetCodingTable(possibilities, codes, 0, possibilities.Count, "");
        }

        private void GetCodingTable(Dictionary<char, double> possibilities, Dictionary<char, string> codes,
            int begin, int end, string symbol = "")
        {
            double sum = 0;
            double mean = Sum(possibilities, begin, end) / 2;
            int meanIndex = -1;
            for (int i = begin; i < end; sum += possibilities.ElementAt(i).Value, ++i)
            {
                if (mean >= sum && mean <= sum + possibilities.ElementAt(i).Value)
                {
                    if (mean - sum < sum + possibilities.ElementAt(i).Value - mean)
                        meanIndex = i;
                    else
                        meanIndex = i + 1;
                }
                codes[possibilities.ElementAt(i).Key] += symbol;
            }

            if (end - begin > 1)
            {
                GetCodingTable(possibilities, codes, begin, meanIndex, "0");
                GetCodingTable(possibilities, codes, meanIndex, end, "1");
            }
        }

        private double Sum(Dictionary<char, double> possibilities, int begin, int end)
        {
            double sum = 0;

            for (int i = begin; i < end; ++i)
                sum += possibilities.ElementAt(i).Value;

            return sum;
        }

    }
}
