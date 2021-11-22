using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDistance.Algorithms.Exact
{
    public class ExactDistanceFinder : IDistanceFinder
    {
        public string Name => "ExactDistanceFinder";

        public double FindDistance(Graph graph1, Graph graph2)
        {
            var mcsVertices = GetMCSVertices(graph1, graph2, 0, new());

            Console.WriteLine("--> Result subgraph:");
            graph1.GetInducedSubgraph(mcsVertices).Print();

            return 1.0 - mcsVertices.Count / (double)Math.Max(graph1.Size, graph2.Size);
        }

        private List<int> GetMCSVertices(
            Graph graph1,
            Graph graph2,
            int index,
            List<int> consideredVertices)
        {
            if (index == graph1.Size)
            {
                return consideredVertices.ToList();
            }

            var candidateVertices1 = GetMCSVertices(
                graph1,
                graph2,
                index + 1,
                consideredVertices);

            consideredVertices.Add(index);
            var candidateVertices2 = new List<int>();
            if (IsCommonSubgraph(graph1, graph2, consideredVertices))
            {
                candidateVertices2 = GetMCSVertices(
                    graph1,
                    graph2,
                    index + 1,
                    consideredVertices);
            }

            consideredVertices.RemoveAt(consideredVertices.Count - 1);

            return candidateVertices1.Count > candidateVertices2.Count
                ? candidateVertices1
                : candidateVertices2;
        }

        private bool IsCommonSubgraph(
            Graph graph1,
            Graph graph2,
            List<int> graph1Vertices)
        {
            var permutations = GetPermutations(Enumerable.Range(0, graph2.Size), graph1Vertices.Count);
            foreach (var graph2Vertices in permutations)
            {
                // Arrays for optimalization
                if (AreInducedGraphsTheSame(graph1, graph2, graph1Vertices.ToArray(), graph2Vertices.ToArray()))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AreInducedGraphsTheSame(
            Graph graph1,
            Graph graph2,
            int[] graph1Vertices,
            int[] graph2Vertices)
        {
            for (int i = 0; i < graph1Vertices.Length; i++)
            {
                for (int j = 0; j < graph1Vertices.Length; j++)
                {
                    if (graph1[graph1Vertices[i], graph1Vertices[j]] != graph2[graph2Vertices[i], graph2Vertices[j]])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetPermutations(list, length - 1)
                .SelectMany(t => list
                    .Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}