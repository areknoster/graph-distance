using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphDistance
{
    public static class GraphFile
    {
        public static (Graph G1, Graph G2) Read(string filePath)
        {
            ValidateFilePath(filePath);

            using (StreamReader file = new(filePath))
            {
                int size1 = ReadSize(file);
                bool[,] adjacencyMatrix1 = ReadAdjacencyMatrix(file, size1);

                int size2 = ReadSize(file);
                bool[,] adjacencyMatrix2 = ReadAdjacencyMatrix(file, size2);

                file.Close();
                return (new Graph(size1, adjacencyMatrix1), new Graph(size2, adjacencyMatrix2));
            }
        }

        private static void ValidateFilePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException(Errors.GraphFile.INVALID_PATH);
            }

            if (Path.GetExtension(filePath) != ".txt")
            {
                throw new ArgumentException(Errors.GraphFile.FILE_EXTENSION_NOT_TXT);
            }
        }

        private static int ReadSize(StreamReader streamReader)
        {
            bool success = int.TryParse(streamReader.ReadLine(), out int size);

            if (!success)
            {
                throw new Exception(Errors.GraphFile.CANNOT_READ_SIZE);
            }

            return size;
        }

        private static bool[,] ReadAdjacencyMatrix(StreamReader streamReader, int validSize)
        {
            List<string[]> rows = new();
            for (int i = validSize; i > 0; i--)
            {
                string row = streamReader.ReadLine();

                if (row == null)
                {
                    break;
                }

                rows.Add(row.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            }

            if (rows.Count != validSize || rows.Any(x => x.Length != validSize))
            {
                throw new Exception(Errors.GraphFile.MATRIX_NOT_SQUARE);
            }

            bool[,] adjacencyMatrix = new bool[validSize, validSize];

            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < rows[i].Length; j++)
                {
                    bool success = int.TryParse(rows[i][j], out int value);

                    if (!success)
                    {
                        throw new Exception(Errors.GraphFile.CANNOT_READ_MATRIX);
                    }

                    if (value != 0 && value != 1)
                    {
                        throw new Exception(Errors.GraphFile.MATRIX_NOT_BINARY);
                    }

                    adjacencyMatrix[i, j] = Convert.ToBoolean(value);
                }
            }

            return adjacencyMatrix;
        }

        public static void Write(Graph graph, string fileName)
        {
            using (StreamWriter file = new(fileName, false))
            {
                file.WriteLine(graph.Size);

                for (int i = 0; i < graph.AdjacencyMatrix.GetLength(0); i++)
                {
                    string line = string.Join(" ", Enumerable.Range(0, graph.AdjacencyMatrix.GetLength(1))
                        .Select(j => Convert.ToInt32(graph[i, j])));
                    file.WriteLine(line);
                }

                file.Close();
            }
        }
    }
}