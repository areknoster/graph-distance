﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphDistance
{
    public class AdjacencyMatrix
    {
        private readonly bool[,] values;

        public bool[,] Values
        {
            get { return values; }
        }

        public AdjacencyMatrix(int size)
        {
            ValidateSize(size);
            values = new bool[size, size];
        }

        public AdjacencyMatrix(bool[,] values)
        {
            ValidateAdjacencyMatrix(values);
            this.values = values;
        }

        private static void ValidateSize(int size)
        {
            if (size < 0)
            {
                throw new ArgumentException(Errors.AdjacencyMatrix.NEGATIVE_SIZE);
            }
        }

        private static void ValidateAdjacencyMatrix(bool[,] values)
        {
            if (values.GetLength(0) != values.GetLength(1))
            {
                throw new ArgumentException(Errors.AdjacencyMatrix.DIMENSIONS_NOT_EQUAL);
            }
        }

        public bool this[int i, int j]
        {
            get { return values[i, j]; }
            set { values[i, j] = value; }
        }

        public void SwapNodesLabels(int n1, int n2)
        {
            ValidateNodeLabel(n1);
            ValidateNodeLabel(n2);

            for (int i = 0; i < values.GetLength(0); i++)
            {
                bool tmp = this[i, n1];
                this[i, n1] = this[i, n2];
                this[i, n2] = tmp;
            }

            for (int i = 0; i < values.GetLength(1); i++)
            {
                bool tmp = this[n1, i];
                this[n1, i] = this[n2, i];
                this[n2, i] = tmp;
            }
        }

        public List<int> GetSourcesOfIncomingEdges(int node)
        {
            ValidateNodeLabel(node);
            return Enumerable.Range(0, values.GetLength(0)).Where(i => this[i, node]).ToList();
        }

        public List<int> GetTargetsOfOutgoingEdges(int node)
        {
            ValidateNodeLabel(node);
            return Enumerable.Range(0, values.GetLength(1)).Where(i => this[node, i]).ToList();
        }

        private void ValidateNodeLabel(int n)
        {
            if (n < 0 || n >= values.GetLength(0))
            {
                throw new ArgumentException(Errors.AdjacencyMatrix.INVALID_NODE_LABEL);
            }
        }

        public void Print()
        {
            Console.WriteLine("Adjacency matrix:");
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    Console.Write($"{Convert.ToInt32(this[i, j])} ");
                }

                Console.Write("\n");
            }
        }

        public List<string> GetPrintLines(List<int> indexes)
        {
            var indexesCount = indexes.Count;
            var lines = new List<string>
            {
                $"Adjacency matrix ({indexesCount}x{indexesCount}) with indexes:"
            };

            int maxIndexLength = indexes.Max().ToString().Length;

            var sb = new StringBuilder();
            sb.Append(new string(' ', maxIndexLength + 1) + "|");
            for (int i = 0; i < indexesCount; i++)
            {
                sb.Append(string.Format($" {{0,{maxIndexLength}}}", indexes[i]));
            }
            lines.Add(sb.ToString());
            sb.Clear();

            sb.Append(new string(
                '-', maxIndexLength + 1)
                + "+"
                + new string('-', (maxIndexLength + 1) * indexesCount));
            lines.Add(sb.ToString());
            sb.Clear();

            for (int i = 0; i < indexesCount; i++)
            {
                sb.Append(string.Format($"{{0,{maxIndexLength}}} |", indexes[i]));
                for (int j = 0; j < indexesCount; j++)
                {
                    sb.Append(string.Format($" {{0,{maxIndexLength}}}", Convert.ToInt32(this[indexes[i], indexes[j]])));
                }

                lines.Add(sb.ToString());
                sb.Clear();
            }

            return lines;
        }
    }
}