using lab8.Coders.DictionaryCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lab8.DictionaryExtension;

namespace lab8.Coders.HuffmanCoder
{

    public class HuffmanEncoder : DictionaryEncoder
    {
        public HuffmanEncoder(string inputFilePath, string outputFilePath) : base(inputFilePath, outputFilePath) { }


        public override void Encode()
        {
            string input = Encoding.GetEncoding(1251).GetString(ReadFile());
            Dictionary<char, double> possibilities = GetSymbolPossibilities(input);
            possibilities = possibilities.OrderBy(x => x.Value).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
            codesTable = GetCodingTable(possibilities);
            base.Encode();
        }

        public Dictionary<char, string> GetCodingTable(Dictionary<char, double> possibilities)
        {
            Dictionary<Dictionary<char, string>, double> codesTable
                = new Dictionary<Dictionary<char, string>, double>();
            foreach (KeyValuePair<char, double> poss in possibilities)
                codesTable.Add(new Dictionary<char, string> { { poss.Key, "" } }, poss.Value);

            while (codesTable.Count > 1)
            {
                Pair<Dictionary<char, string>, double> last =
                    new Pair<Dictionary<char, string>, double>(codesTable.Last());
                last.First.AddToValue("1");
                codesTable.RenameKey(codesTable.Last().Key, last.First);
                codesTable.Remove(last.First);

                Pair<Dictionary<char, string>, double> prelast =
                    new Pair<Dictionary<char, string>, double>(codesTable.Last());
                prelast.First.AddToValue("0");
                codesTable.RenameKey(codesTable.Last().Key, prelast.First);

                prelast.First.AddRange(last.First);
                codesTable.RenameKey(codesTable.Last().Key, prelast.First);
                codesTable[prelast.First] += last.Second;
                codesTable = codesTable.SortDown();
            }

            return codesTable.Last().Key;
        }

        public override double GetEncodingPrice()
        {
            string input = Encoding.GetEncoding(1251).GetString(ReadFile());
            Dictionary<char, double> possibilities = GetSymbolPossibilities(input);
            possibilities = possibilities.OrderBy(x => x.Value).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
            Dictionary<char, string> codesTable = GetCodingTable(possibilities);

            double price = 0;
            foreach (KeyValuePair<char, double> symbol in possibilities)
                price += symbol.Value * codesTable[symbol.Key].Length;

            return price;
        }
    }
}
