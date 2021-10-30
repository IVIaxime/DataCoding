using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using lab8.Coders;
using lab8.Coders.ShannonFanoCoder;
using lab8.Coders.DictionaryCoder;
using lab8.Coders.RLECoder;
using lab8.CommandHadler;
using lab8.Coders.HuffmanCoder;

namespace lab8
{
    class Program
    {
        public static void PrintCommands(List<Command> commands)
        {
            foreach (Command command in commands)
            {
                Console.Write(command.CommandName);

                if (command.ArgsDescription.Count != 0)
                {
                    foreach (string arg in command.ArgsDescription)
                        Console.Write(" " + arg);
                }

                Console.WriteLine(" - " + command.Description);
            }
        }

        static void EncodeShannonFano(string inputFilePath, string outputFilePath)
        {
            ShannonFanoEncoder coder = new ShannonFanoEncoder(inputFilePath, outputFilePath);
            coder.Encode();
            Console.WriteLine("Сообщение закодировано\nЦена кодирования: {0 :F2}", coder.GetEncodingPrice());
            Console.WriteLine("Степень сжатия: {0 :F2} %", coder.GetCompressionCoeff());
        }

        static void DecodeShannonFano(string inputFilePath, string outputFilePath)
        {
            ShannonFanoDecoder decoder = new ShannonFanoDecoder(inputFilePath, outputFilePath);
            decoder.Decode();
            Console.WriteLine("Сообщение декодировано");
        }

        static void EncodeRLE(string inputFilePath, string outputFilePath)
        {
            RLEEncoder rleEncoder = new RLEEncoder(inputFilePath, outputFilePath);
            rleEncoder.Encode();
            Console.WriteLine("Сообщение закодировано");
            Console.WriteLine("Степень сжатия: {0 :F2} %\n", rleEncoder.GetCompressionCoeff());
        }

        static void DecodeRLE(string inputFilePath, string outputFilePath)
        {
            RLEDecoder rleDecoder = new RLEDecoder(inputFilePath, outputFilePath);
            rleDecoder.Decode();
            Console.WriteLine("Сообщение декодировано");
        }

        static void EncodeHuffman(string inputFilePath, string outputFilePath)
        {
            HuffmanEncoder huffmanEncoder = new HuffmanEncoder(inputFilePath, outputFilePath);
            huffmanEncoder.Encode();
            Console.WriteLine("Сообщение закодировано\nЦена кодирования: {0 :F2}", huffmanEncoder.GetEncodingPrice());
            Console.WriteLine("Степень сжатия: {0 :F2} %\n", huffmanEncoder.GetCompressionCoeff());
        }

        static void DecodeHuffman(string inputFilePath, string outputFilePath)
        {
            HuffmanDecoder rleEncoder = new HuffmanDecoder(inputFilePath, outputFilePath);
            rleEncoder.Decode();
            Console.WriteLine("Сообщение декодировано");
        }


        static void Main(string[] args)
        {
            Commander commander = new Commander();

            commander.AddCommand(new Command("/quit",
                "выйти из программы",
                commandArgs => Environment.Exit(0)));

            commander.AddCommand(new Command("/help",
                "вывести список команд",
                commandsArgs => PrintCommands(commander.GetCommands())));

            commander.AddCommand(new Command("/encode_shannon",
                "закодировать сообщение методом Шеннона-Фано",
                new List<string>() { "<путь к файлу с текстом, который нужно закодировать>",
                    "<путь к файлу, в который записать закодированный текст>" },
                commandArgs => EncodeShannonFano(commandArgs[0], commandArgs[1])));

            commander.AddCommand(new Command("/decode_shannon",
                "декодировать сообщение методом Шеннона-Фано",
                new List<string>() { "<путь к файлу с текстом, который нужно декодировать>",
                    "<путь к файлу, в который записать декодированный текст>" },
                commandArgs => DecodeShannonFano(commandArgs[0], commandArgs[1])));

            commander.AddCommand(new Command("/encode_rle",
                "закодировать сообщение методом RLE",
                new List<string>() { "<путь к файлу с текстом, который нужно закодировать>",
                    "<путь к файлу, в который записать закодированный текст>" },
                commandArgs => EncodeRLE(commandArgs[0], commandArgs[1])));

            commander.AddCommand(new Command("/decode_rle",
                "декодировать сообщение методом RLE",
                new List<string>() { "<путь к файлу с текстом, который нужно декодировать>",
                    "<путь к файлу, в который записать декодированный текст>" },
                commandArgs => DecodeRLE(commandArgs[0], commandArgs[1])));

            commander.AddCommand(new Command("/encode_huffman",
                "закодировать сообщение методом Хаффмана",
                new List<string>() { "<путь к файлу с текстом, который нужно закодировать>",
                    "<путь к файлу, в который записать закодированный текст>" },
                commandArgs => EncodeHuffman(commandArgs[0], commandArgs[1])));

            commander.AddCommand(new Command("/decode_huffman",
                "декодировать сообщение методом Хаффмана",
                new List<string>() { "<путь к файлу с текстом, который нужно декодировать>",
                    "<путь к файлу, в который записать декодированный текст>" },
                commandArgs => DecodeHuffman(commandArgs[0], commandArgs[1])));


            Console.WriteLine("Введите /help для вывода списка команд");
            while (true)
            {
                Console.Write("Введите команду: ");
                try
                {
                    commander.ExecuteCommand(Console.ReadLine());
                }
                catch (Exception exc)
                {
                    Console.WriteLine("Ошибка: " + exc.Message.ToString());
                }
            }
        }
    }
}
