using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding.Coders.RLECoder
{
    public class RLEEncoder : Encoder
    {
        public RLEEncoder(string inputFilePath, string outputFilePath) : base(inputFilePath, outputFilePath) { }


        public override void Encode()
        {
            byte[] input = ReadFile();
            List<byte> output = new List<byte>();
            for (int i = 0; i < input.Length;)
            {
                byte count = 1;
                while (++i < input.Length && input[i] == input[i - 1])
                    ++count;
                if (count == 1)
                {
                    List<byte> sequence = new List<byte>();
                    --i;
                    while (++i < input.Length && input[i] != input[i - 1])
                    {
                        ++count;
                        sequence.Add(input[i - 1]);
                    }
                    if (i == input.Length)
                        sequence.Add(input[i - 1]);
                    else
                    {
                        --i;
                        --count;
                    }

                    output.Add(count);
                    output.AddRange(sequence);
                }
                else
                {
                    output.Add((byte)(128 + count));
                    output.Add(input[i - 1]);
                }

                if (count > 127)
                    throw new FormatException("Слишком много подряд (не)повторяющихся символов");
            }

            WriteFile(output);
        }

        public override double GetEncodingPrice()
        {
            return 8;
        }
    }
}
