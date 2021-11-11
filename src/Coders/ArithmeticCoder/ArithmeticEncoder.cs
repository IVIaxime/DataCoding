using coding;
using coding.Coders;
using coding.Coders.DictionaryCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding.Coders.ArithmeticCoder
{
    public class ArithmeticEncoder : DictionaryEncoder
    {
        public ArithmeticEncoder(string inputFilePath, string outputFilePath)
            : base(inputFilePath, outputFilePath) { }


        public override void Encode()
        {
            string input = Encoding.GetEncoding(1251).GetString(ReadFile());
            if (input.Length > 16)
                throw new FormatException("Текст должен быть не длиннее 16-ти символов");
            Dictionary<char, double> possibilities = GetSymbolPossibilities(input);
            possibilities = possibilities.OrderBy(x => x.Value).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
            Dictionary<char, Pair<double, double>> intervals = getIntervals(possibilities);

            Pair<double, double> lastBorders = createTable(input, intervals);
            double average = (lastBorders.First + lastBorders.Second) / 2;
            double eps = lastBorders.Second - average;
            string text = Calc.ConvertToBase(average.ToString(), 10, 2, eps / 2).Substring(2);
            List<byte> encodedText = WriteAllBytes(text);
            List<byte> encodedDictionary = EncodeDictionary(intervals, eps, input.Length);
            List<byte> output = new List<byte>();
            output.AddRange(encodedDictionary);
            output.AddRange(encodedText);

            WriteFile(output);
        }

        private Pair<double, double> createTable(string input, Dictionary<char, Pair<double, double>> intervals)
        {
            double interval, lowerBorder = 0, upperBorder = 1;

            foreach (char symbol in input)
            {
                interval = upperBorder - lowerBorder;
                upperBorder = lowerBorder + interval * intervals[symbol].Second;
                lowerBorder = lowerBorder + interval * intervals[symbol].First;
            }

            return new Pair<double, double>(lowerBorder, upperBorder);
        }

        private List<byte> EncodeDictionary(Dictionary<char, Pair<double, double>> intervals, double eps, int textLen)
        {
            List<byte> output = new List<byte>();

            Dictionary<char, string> encodedIntervals = new Dictionary<char, string>();
            foreach (KeyValuePair<char, Pair<double, double>> interval in intervals)
                encodedIntervals.Add(interval.Key, Calc.ConvertToBase(interval.Value.First.ToString(), 10, 2, eps / 40));
            byte intervalSize = (byte)Math.Ceiling(encodedIntervals.Max(x => (x.Value.Length - 2)) / 8d);

            output.Add((byte)intervals.Count);
            output.Add((byte)(textLen - 1 + ((intervalSize - 1) << 4)));
            output.Add(Encoding.GetEncoding(1251).GetBytes(new char[] { encodedIntervals.ElementAt(0).Key })[0]);
            encodedIntervals.Remove(encodedIntervals.ElementAt(0).Key);

            foreach (KeyValuePair<char, string> interval in encodedIntervals)
            {
                List<byte> bytes = WriteAllBytes(interval.Value.Substring(2));
                while (bytes.Count < intervalSize)
                    bytes.Add(0);
                bytes.Add(Encoding.GetEncoding(1251).GetBytes(new char[] { interval.Key })[0]);
                output.AddRange(bytes);
            }

            return output;
        }

        Dictionary<char, Pair<double, double>> getIntervals(Dictionary<char, double> possibilities)
        {
            Dictionary<char, Pair<double, double>> intervals = new Dictionary<char, Pair<double, double>>();

            double prevPoss = 0;
            for (int i = 0; i < possibilities.Count; ++i)
            {
                KeyValuePair<char, double> currPossibility = possibilities.ElementAt(i);
                intervals.Add(currPossibility.Key, new Pair<double, double>(prevPoss, prevPoss + currPossibility.Value));
                prevPoss += currPossibility.Value;
            }

            return intervals;
        }
        
        public override double GetEncodingPrice()
        {
            string input = Encoding.GetEncoding(1251).GetString(ReadFile());
            Dictionary<char, double> possibilities = GetSymbolPossibilities(input);
            possibilities = possibilities.OrderBy(x => x.Value).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
            Dictionary<char, Pair<double, double>> intervals = getIntervals(possibilities);

            Pair<double, double> lastBorders = createTable(input, intervals);
            double average = (lastBorders.First + lastBorders.Second) / 2;
            double eps = lastBorders.Second - average;
            string text = Calc.ConvertToBase(average.ToString(), 10, 2, eps).Substring(2);

            return (double)text.Length / input.Length;
        }
    }
}
