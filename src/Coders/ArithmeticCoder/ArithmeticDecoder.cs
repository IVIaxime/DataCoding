using coding.Coders.DictionaryCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coding;

namespace coding.Coders.ArithmeticCoder
{
    public class ArithmeticDecoder : DictionaryDecoder
    {
        public ArithmeticDecoder(string inputFilePath, string outputFilePath) : base(inputFilePath, outputFilePath) { }

        public override void Decode()
        {
            byte[] input = ReadFile();
            Dictionary<char, Pair<double, double>> dictionary = ReadDictionary(input);
            string output = "";
            int dicSize = input[0], intervalSize = (input[1] >> 4) + 1, textLen = (input[1] & 15) + 1;
            double number = toBinary(input.Skip(dicSize * (intervalSize + 1) - intervalSize + 2).ToArray(), 1e-20);
            for (int i = 0; i < textLen; ++i)
            {
                KeyValuePair<char, Pair<double, double>> currElem = 
                    dictionary.First(x => x.Value.First <= number && x.Value.Second > number);
                output += currElem.Key;
                number = number / (currElem.Value.Second - currElem.Value.First);
                number -= (int)number;
            }

            WriteFile(output);
        }
        ///decode_arithmetic encoded.txt decoded.txt
        private Dictionary<char, Pair<double, double>> ReadDictionary(byte[] bytes)
        {
            Dictionary<char, Pair<double, double>> dictionary = new Dictionary<char, Pair<double, double>>();

            byte size = bytes[0];
            int intervalSize = (bytes[1] >> 4) + 1;
            double border = toBinary(bytes.Skip(3).Take(intervalSize).ToArray(), 0);
            dictionary.Add(Encoding.GetEncoding(1251).GetChars(new byte[] { bytes[2] })[0],
                    new Pair<double, double>(0, border));
            
            for (int i = 3; i < size * (intervalSize + 1) - intervalSize + 2; i += intervalSize + 1)
            {
                double rightBorder = toBinary(bytes.Skip(i + intervalSize + 1).Take(intervalSize).ToArray(), 0);
                dictionary.Add(Encoding.GetEncoding(1251).GetChars(new byte[] { bytes[i + intervalSize] })[0],
                    new Pair<double, double>(border, rightBorder));
                border = rightBorder;
            }
            dictionary[dictionary.Last().Key].Second = 1;

            return dictionary;
        }
        
        private double toBinary(byte[] bytes, double eps)
        {
            string number = "0,";
            for (int i = 0; i < bytes.Length; ++i)
            {
                string currByte = Calc.ConvertToBase(bytes[i].ToString(), 10, 2, eps);
                number += new string('0', 8 - currByte.Length) + currByte;
            }
            return double.Parse(Calc.ConvertToBase(number, 2, 10, 0));
        }
    }
}
