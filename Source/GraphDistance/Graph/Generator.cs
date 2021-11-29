using System;
using System.Collections.Generic;

namespace GraphDistance
{
    public class GraphPair
    {
        public GraphPair(Graph g1, Graph g2, string name)
        {
            G1 = g1;
            G2 = g2;
            Name = name;
        }

        public string Name { get; }
        public readonly Graph G1, G2;
    }

    public static class GraphGenerator
    {
        private static readonly Random Rand = new();

        public static Graph Random(int size, double density)
        {
            density = Math.Min(density, size - 1);
            var g = new Graph(size);
            for (int i = 0; i < density * size;)
            {
                var (from, to) = (Rand.Next(size), Rand.Next(size));
                if (!g[from, to])
                {
                    g.AdjacencyMatrix[from, to] = true;
                    i++;
                }
            }

            return g;
        }

        public static Graph CoveringCycle(int size)
        {
            var g = new Graph(size);
            for (int i = 0; i < size - 1; i++)
            {
                g.AdjacencyMatrix[i, i + 1] = true;
            }

            g.AdjacencyMatrix[size - 1, 0] = true;
            return g;
        }

        public static Graph Shuffle(Graph g1)
        {
            var g2 = g1.Copy();
            for (int i = 0; i < g2.Size; i++)
            {
                g2.SwapNodesLabels(i, Rand.Next(g2.Size - 1));
            }

            return g2;
        }

        public static Graph Subgraph(Graph g1, int size)
        {
            if (size > g1.Size)
            {
                throw new Exception("subgraph can't be bigger than graph");
            }

            var g1Shuffled = Shuffle(g1);
            if (size == g1.Size)
            {
                return g1Shuffled;
            }

            var nodes = new List<int>(size);
            for (int i = 0; i < size; i++)
            {
                nodes.Add(i);
            }

            return g1Shuffled.GetInducedSubgraph(nodes);
        }
    }
}