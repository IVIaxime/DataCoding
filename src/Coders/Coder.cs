using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace coding.Coders
{
    public abstract class Coder<T1, T2>
    {
        protected string inputFilePath, outputFilePath;


        protected Coder(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }


        public abstract void WriteFile(T1 encodedData);

        public abstract T2 ReadFile();


        public string InputFilePath
        {
            get
            {
                return inputFilePath;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                inputFilePath = value;
            }
        }

        public string OutputFilePath
        {
            get
            {
                return outputFilePath;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                outputFilePath = value;
            }
        }
    }
}
