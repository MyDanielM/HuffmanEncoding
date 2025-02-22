﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    class Node
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public List<bool> Code { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }

        /// <summary>
        /// Метод прохода по дереву по условию алгоритма Хаффмана
        /// </summary>
        /// <param name="symbol">Символ</param>
        /// <param name="data">Массив бит для символа</param>
        /// <returns></returns>
        public List<bool> Traverse(char symbol, List<bool> data)
        {
            // Leaf
            if (Right == null && Left == null)
            {
                if (symbol.Equals(this.Symbol))
                {
                    Code = data;
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);

                    left = Left.Traverse(symbol, leftPath);
                }

                if (Right != null)
                {
                    List<bool> rightPath = new List<bool>();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = Right.Traverse(symbol, rightPath);
                }

                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }
    }
}
