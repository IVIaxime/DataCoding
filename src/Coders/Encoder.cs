using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding.Coders
{
    public abstract class Encoder : Coder<List<byte>, byte[]>, IEncodable
    {
        protected Encoder(string inputFilePath, string outputFilePath) : base(inputFilePath, outputFilePath) { }

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
