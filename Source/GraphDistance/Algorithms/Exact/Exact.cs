using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDistance.Algorithms.Exact
{
    public class ExactDistanceFinder : IDistanceFinder
    {
        public string Name => "ExactDistanceFinder";

        public (double Distance, List<(int G1, int G2)> Mapping) FindDistance(Graph graph1, Graph graph2)
        {
            var result = GetMCSVertices(graph1, graph2, 0, new(), new());

            if (result.G1Vertices.Count != result.G2Vertices.Count)
            {
                throw new InvalidOperationException("Invalid exact algorithm result");
            }

            var mapping = new List<(int G1, int G2)>();
            for (int i = 0; i < result.G1Vertices.Count; i++)
            {
                mapping.Add((result.G1Vertices[i], result.G2Vertices[i]));
            }

            return (1.0 - result.G1Vertices.Count / (double)Math.Max(graph1.Size, graph2.Size), mapping);
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

            var candidate1 = GetMCSVertices(
                graph1,
                graph2,
                indexG1 + 1,
                consideredG1Vertices,
                correspondingG2Vertices);

            consideredG1Vertices.Add(indexG1);
            (List<int> G1Vertices, List<int> G2Vertices) candidate2 = (new List<int>(), new List<int>());
            var commonSubgraphResult = IsCommonSubgraph(graph1, graph2, consideredG1Vertices);
            if (commonSubgraphResult.IsCommon)
            {
                candidate2 = GetMCSVertices(
                    graph1,
                    graph2,
                    indexG1 + 1,
                    consideredG1Vertices,
                    commonSubgraphResult.vertices);
            }

            consideredG1Vertices.RemoveAt(consideredG1Vertices.Count - 1);

            return candidate1.G1Vertices.Count > candidate2.G1Vertices.Count
                ? (candidate1.G1Vertices, candidate1.G2Vertices)
                : (candidate2.G1Vertices, candidate2.G2Vertices);
        }

        private (bool IsCommon, List<int> vertices) IsCommonSubgraph(
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