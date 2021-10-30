using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Coders.RLECoder
{
    public class RLEDecoder : Decoder
    {
        public RLEDecoder(string inputFilePath, string outputFilePath) : base(inputFilePath, outputFilePath) { }


        public override void Decode()
        {
            byte[] input = ReadFile();
            List<byte> output = new List<byte>();
            for (int i = 0; i < input.Length;)
            {
                if ((input[i] & 128) != 0)
                {
                    output.AddRange(Enumerable.Repeat(input[i + 1], input[i] - 128));
                    i += 2;
                }
                else
                {
                    output.AddRange(input.Skip(i + 1).Take(input[i]));
                    i += input[i] + 1;
                }
            }

            WriteFile(Encoding.GetEncoding(1251).GetString(output.ToArray()));
        }
    }
}
