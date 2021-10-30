using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace lab8.Coders
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


        protected int BinaryToDecimal(string binaryNum)
        {
            int decNum = 0;

            for (int i = binaryNum.Length - 1; i >= 0; --i)
                decNum += int.Parse(binaryNum[i].ToString()) * (int)Math.Pow(2, binaryNum.Length - i - 1);

            return decNum;
        }

        protected byte[] DecimalToBinary(int decNumber)
        {
            if (decNumber == 0)
                return new byte[] { 0 };
            int binNumSize = (int)Math.Log(decNumber, 2) + 1;
            byte[] binaryNumber = new byte[binNumSize];

            for (int i = 0; i < binNumSize; ++i, decNumber >>= 1)
                binaryNumber[binNumSize - i - 1] = (byte)(decNumber & 1);

            return binaryNumber;
        }


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
