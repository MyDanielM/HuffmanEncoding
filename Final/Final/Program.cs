using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Final
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Введи строку:");
            string input = Console.ReadLine();
            input = input.ToLower();


            #region Построение дерева Хаффмана
            HuffmanTree huffmanTree = new HuffmanTree();
            huffmanTree.Build(input);
            #endregion

            #region Вывод буква - частота в строке
            Console.Write("В строке "+"\""+input+"\"");
            Console.WriteLine();
            Dictionary<char, int> symbols = huffmanTree.Frequencies;
            foreach (KeyValuePair<char, int> pair in symbols)
            {
                Console.WriteLine("Символ {0} встречается {1} раз", pair.Key, pair.Value);
            }
            #endregion

            #region Выводим коды символов
            foreach (char symbol in input)
            {
                Console.Write("У символа {0} код = ",symbol);
                BitArray bits = huffmanTree.GetSymbolCode(symbol);
                foreach(bool bit in bits)
                    Console.Write((bit ? 1 : 0) + "");
                Console.WriteLine();
            }
            #endregion

            #region Кодирование строки в биты по дереву Хаффмана
            BitArray encoded = huffmanTree.Encode(input);
            #endregion

            #region Вывод закодированной строки на экран
            Console.Write("Закодировал: ");
            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine();
            #endregion

            #region Раскодирование строки по дереву Хаффмана и вывод её на экран
            string decoded = huffmanTree.Decode(encoded);
            
            //Вывод строки на экран
            Console.WriteLine("Раскодировал: " + decoded);
            #endregion

            Console.ReadKey();
        }
    }
}
