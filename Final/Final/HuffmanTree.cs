﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Final
{
    class HuffmanTree
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

        /// <summary>
        /// Метод для построения дерева Хаффмана по строке
        /// </summary>
        /// <param name="source">Строка</param>
        public void Build(string source)
        {
            
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequencies.ContainsKey(source[i]))
                {
                    Frequencies.Add(source[i], 0);
                }

                Frequencies[source[i]]++;
            }

            foreach (KeyValuePair<char, int> symbol in Frequencies)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                    // Create a parent node by combining the frequencies
                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();

            }

        }

        /// <summary>
        /// Метод кодирования строки по алгоритму Хаффмана
        /// </summary>
        /// <param name="source">Строка для кодирования</param>
        /// <returns>Массив бит</returns>
        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        /// <summary>
        /// Получаем код символа
        /// </summary>
        /// <param name="symbol">Символ</param>
        /// <returns>Массив битов</returns>
        public BitArray GetSymbolCode(char symbol)
        {
            List<bool> encodedSource = new List<bool>();

            List<bool> encodedSymbol = this.Root.Traverse(symbol, new List<bool>());
            encodedSource.AddRange(encodedSymbol);

            BitArray bits = new BitArray(encodedSource.ToArray());
            return bits;
        }

        /// <summary>
        /// Метод декодирования закодированной строки
        /// </summary>
        /// <param name="bits">Массив бит</param>
        /// <returns>Строка</returns>
        public string Decode(BitArray bits)
        {
            Node current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Symbol;
                    current = this.Root;
                }
            }

            return decoded;
        }

        /// <summary>
        /// "Это лист дерева?"
        /// </summary>
        /// <param name="node">Узел</param>
        /// <returns>true||false</returns>
        public bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }
    }
}
