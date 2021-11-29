using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDistance.Algorithms.Exact
{
    public class ExactDistanceFinder : IDistanceFinder
    {
        public string Name => "Exact";

        public (double Distance, List<(int G1, int G2)> Mapping) FindDistance(Graph graph1, Graph graph2)
        {
            var (G1Vertices, G2Vertices) = GetMCSVertices(graph1, graph2, 0, new(), new());

            if (G1Vertices.Count != G2Vertices.Count)
            {
                throw new InvalidOperationException("Invalid exact algorithm result");
            }

            var mapping = new List<(int G1, int G2)>();
            for (int i = 0; i < G1Vertices.Count; i++)
            {
                mapping.Add((G1Vertices[i], G2Vertices[i]));
            }

            return (1.0 - G1Vertices.Count / (double)Math.Max(graph1.Size, graph2.Size), mapping);
        }

        private (List<int> G1Vertices, List<int> G2Vertices) GetMCSVertices(
            Graph graph1,
            Graph graph2,
            int indexG1,
            List<int> consideredG1Vertices,
            List<int> correspondingG2Vertices)
        {
            if (indexG1 == graph1.Size)
            {
                return (consideredG1Vertices.ToList(), correspondingG2Vertices.ToList());
            }

            var (G1Vertices, G2Vertices) = GetMCSVertices(
                graph1,
                graph2,
                indexG1 + 1,
                consideredG1Vertices,
                correspondingG2Vertices);

            consideredG1Vertices.Add(indexG1);
            (List<int> G1Vertices, List<int> G2Vertices) candidate2 = (new List<int>(), new List<int>());
            var (IsCommon, vertices) = IsCommonSubgraph(graph1, graph2, consideredG1Vertices);
            if (IsCommon)
            {
                candidate2 = GetMCSVertices(
                    graph1,
                    graph2,
                    indexG1 + 1,
                    consideredG1Vertices,
                    vertices);
            }

            consideredG1Vertices.RemoveAt(consideredG1Vertices.Count - 1);

            return G1Vertices.Count > candidate2.G1Vertices.Count
                ? (G1Vertices, G2Vertices)
                : (candidate2.G1Vertices, candidate2.G2Vertices);
        }

        private static (bool IsCommon, List<int> vertices) IsCommonSubgraph(
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
                    return (true, graph2Vertices.ToList());
                }
            }

            return (false, new());
        }

        private static bool AreInducedGraphsTheSame(
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