using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDistance
{
    public class Graph
    {
        private readonly int size;
        private readonly AdjacencyMatrix adjacencyMatrix;

        public int Size { get { return size; } }
        public bool[,] AdjacencyMatrix { get { return adjacencyMatrix.Values; } }

        public Graph(int size)
        {
            ValidateSize(size);
            this.size = size;
            adjacencyMatrix = new(size);
        }

        public Graph(bool[,] adjacencyMatrix)
        {
            this.adjacencyMatrix = new(adjacencyMatrix);
            size = adjacencyMatrix.GetLength(0);
        }

        public Graph(int size, bool[,] adjacencyMatrix)
        {
            ValidateGraphStructure(size, adjacencyMatrix.GetLength(0), adjacencyMatrix.GetLength(1));

            this.size = size;
            this.adjacencyMatrix = new(adjacencyMatrix);
        }

        private static void ValidateSize(int size)
        {
            if (size < 0)
            {
                throw new ArgumentException(Errors.Graph.NEGATIVE_SIZE);
            }
        }

        private static void ValidateGraphStructure(int size, int matrixDim1, int matrixDim2)
        {
            if (matrixDim1 != size || matrixDim2 != size)
            {
                throw new ArgumentException(Errors.Graph.INVALIDE_GRAPH_SIZE_OR_MATRIX);
            }
        }

        public bool this[int i, int j]
        {
            get { return adjacencyMatrix[i, j]; }
        }

        public void SwapNodesLabels(int n1, int n2)
        {
            adjacencyMatrix.SwapNodesLabels(n1, n2);
        }

        public List<int> GetSourceIncomingEdgesNodes(int node)
        {
            return adjacencyMatrix.GetSourcesOfIncomingEdges(node);
        }

        public List<int> GetTargetOutgoingEdgesNodes(int node)
        {
            return adjacencyMatrix.GetTargetsOfOutgoingEdges(node);
        }

        public void Print()
        {
            Console.WriteLine($"Graph size: {size}");
            adjacencyMatrix.Print();
        }

        public Graph GetInducedSubgraph(List<int> nodes)
        {
            ValidateNodes(nodes, size);
            nodes.Sort();

            int subgraphSize = nodes.Count;
            bool[,] subgraphAdjacencyMatrix = new bool[subgraphSize, subgraphSize];

            for (int i = 0; i < subgraphSize; i++)
            {
                for (int j = 0; j < subgraphSize; j++)
                {
                    subgraphAdjacencyMatrix[i, j] = this[nodes[i], nodes[j]];
                }
            }

            return new Graph(subgraphSize, subgraphAdjacencyMatrix);
        }

        private static void ValidateNodes(List<int> nodes, int graphSize)
        {
            if (nodes.Any(x => x < 0 || x >= graphSize))
            {
                throw new ArgumentException(Errors.Graph.SUBGRAPH_CREATING_INVALID_NODE_LABEL);
            }
            if (nodes.Distinct().Count() != nodes.Count)
            {
                throw new ArgumentException(Errors.Graph.SUBGRAPH_CREATING_NODE_LABELS_NOT_UNIQUE);
            }
        }
    }
}