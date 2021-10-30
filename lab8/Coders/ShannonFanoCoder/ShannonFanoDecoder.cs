using lab8.Coders.DictionaryCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Coders.ShannonFanoCoder
{
    public class ShannonFanoDecoder : DictionaryDecoder
    {
        public ShannonFanoDecoder(string inputFilePath, string outputFilePath)
            : base(inputFilePath, outputFilePath) { }
    }
}
