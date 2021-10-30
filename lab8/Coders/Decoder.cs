using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Coders
{
    public abstract class Decoder : Coder<string, byte[]>, IDecodable
    {
        protected Decoder(string inputFilePath, string outputFilePath) : base(inputFilePath, outputFilePath) { }

        public override void WriteFile(string text)
        {
            File.WriteAllText(outputFilePath, text, Encoding.GetEncoding(1251));
        }

        public override byte[] ReadFile()
        {
            return File.ReadAllBytes(inputFilePath);
        }

        public abstract void Decode();
    }
}
