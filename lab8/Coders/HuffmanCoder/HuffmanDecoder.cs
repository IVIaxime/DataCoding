using coding.Coders.DictionaryCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding.Coders.HuffmanCoder
{
    public class HuffmanDecoder : DictionaryDecoder
    {
        public HuffmanDecoder(string inputFilePath, string outputFilePath) : base(inputFilePath, outputFilePath) { }
    }
}
