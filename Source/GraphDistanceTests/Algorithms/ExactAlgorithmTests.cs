using GraphDistance.Algorithms.Exact;
using System;
using System.Collections.Generic;
using Xunit;

namespace GraphDistance.GraphDistanceTests.Algorithms
{
    public class ExactAlgorithmTests
    {
        public static IEnumerable<object[]> FindDistanceData => new List<object[]>
        {
            new object[] { ExampleGraphs.EmptyV, ExampleGraphs.LoopV, 0, new List<(int G1, int G2)>
            { } },
            new object[] { ExampleGraphs.EmptyV, ExampleGraphs.EmptyV2, 1, new List<(int G1, int G2)>
            { (0, 0) } },
            new object[] { ExampleGraphs.TwoLoopsV2, ExampleGraphs.EmptyV2, 0, new List<(int G1, int G2)>
            { } },
            new object[] { ExampleGraphs.TwoLoopsV2, ExampleGraphs.K2, 1, new List<(int G1, int G2)>
            { (0, 0) } },
            new object[] { ExampleGraphs.K2, ExampleGraphs.K3, 2, new List<(int G1, int G2)>
            { (0, 0), (1, 1) } },
            new object[] { ExampleGraphs.K2withoutLoops, ExampleGraphs.K2, 0, new List<(int G1, int G2)>
            { } },
            new object[] { ExampleGraphs.C3directed, ExampleGraphs.C3undirected, 1, new List<(int G1, int G2)>
            { (0, 0) } },
            new object[] { ExampleGraphs.C3directed, ExampleGraphs.C4directed, 2, new List<(int G1, int G2)>
            { (0, 0), (1, 1) } },
            new object[] { ExampleGraphs.C3undirected, ExampleGraphs.C4undirected, 2, new List<(int G1, int G2)>
            { (0, 0), (1, 1) } },
            new object[] { ExampleGraphs.RandomV10, ExampleGraphs.RandomV10, 10, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3), (4, 4), (5, 5), (6, 6), (7, 7), (8, 8), (9, 9) } },
            new object[]
            {
                ExampleGraphs.RandomV10SubgraphV5,
                ExampleGraphs.RandomV10SubgraphV5
                    .SwapLabels(0, 2)
                    .SwapLabels(3, 4)
                    .SwapLabels(1, 2),
                5,
                new List<(int G1, int G2)>
                { (0, 1), (1, 2), (2, 0), (3, 4), (4, 3) }
            },
            new object[]
            {
                ExampleGraphs.RandomV10
                    .SwapLabels(2, 4)
                    .SwapLabels(3, 8)
                    .SwapLabels(5, 9),
                ExampleGraphs.RandomV10SubgraphV5
                    .SwapLabels(0, 1)
                    .SwapLabels(2, 3),
                5,
                new List<(int G1, int G2)>
                { (0, 1), (1, 0), (2, 4), (4, 3), (8, 2) }
            },
            new object[]
            {
                ExampleGraphs.RandomV10SubgraphV5
                    .SwapLabels(0, 2)
                    .SwapLabels(1, 3),
                ExampleGraphs.RandomV10
                    .SwapLabels(2, 6)
                    .SwapLabels(3, 7)
                    .SwapLabels(5, 8),
                5,
                new List<(int G1, int G2)>
                { (0, 6), (1, 7), (2, 0), (3, 1), (4, 4) }
            },
            new object[]
            {
                ExampleGraphs.RandomV10
                    .SwapLabels(1, 5)
                    .SwapLabels(2, 6)
                    .SwapLabels(3, 8),
                ExampleGraphs.RandomV10Subgraph5WithExtraV6
                    .SwapLabels(0, 2)
                    .SwapLabels(1, 4)
                    .SwapLabels(3, 5),
                5,
                new List<(int G1, int G2)>
                { (0, 2), (4, 1), (5, 4), (6, 0), (8, 5) }
            },
            new object[]
            {
                ExampleGraphs.RandomV10Subgraph5WithExtraV6
                    .SwapLabels(0, 2)
                    .SwapLabels(1, 4)
                    .SwapLabels(3, 5),
                ExampleGraphs.RandomV10
                    .SwapLabels(1, 5)
                    .SwapLabels(2, 6)
                    .SwapLabels(3, 8),
                5,
                new List<(int G1, int G2)>
                { (0, 6), (1, 4), (2, 0), (4, 5), (5, 8) }
            },
        };

        public static IEnumerable<object[]> FindDistanceDataFromExamples => new List<object[]>
        {
            // 01.G5_G5_subgraph5_the-same-graphs
            new object[] { GraphsFromExamples.G5, GraphsFromExamples.G5, 5, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3), (4, 4) }  },
            // 02.G5_G5_subgraph5_the-same-graphs-with-swapped-verticles
            new object[] { GraphsFromExamples.G5, GraphsFromExamples.G5_swapped, 5, new List<(int G1, int G2)>
            { (0, 4), (1, 2), (2, 1), (3, 3), (4, 0) } },
            // 03.G5_G5_subgraph4
            new object[] { GraphsFromExamples.G5, GraphsFromExamples.G5_1Vdifferent, 4, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3) } },
            // 04.G5_G5_subgraph3
            new object[] { GraphsFromExamples.G5, GraphsFromExamples.G5_2Vdifferent, 3, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (4, 2) } },
            // 05.G5_G5_subgraph1
            new object[] { GraphsFromExamples.G5, GraphsFromExamples.G5_4Vdifferent, 1, new List<(int G1, int G2)>
            { (2, 0) } },
            // 06.G5_G7_two-extra-isolated-v
            new object[] { GraphsFromExamples.G5, GraphsFromExamples.G7_with_2_extra_isolated, 5, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3), (4, 4) } },
            // 07.G8_G5_subgraph5
            new object[] { GraphsFromExamples.G8, GraphsFromExamples.G5, 5, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3), (4, 4) } },
            // 08.G8_G5_subgraph5-with-swapped-verticles
            new object[] { GraphsFromExamples.G8, GraphsFromExamples.G5_swapped, 5, new List<(int G1, int G2)>
            { (0, 4), (1, 2), (2, 1), (3, 3), (4, 0) } },
            // 09.G8_G5_subgraph4
            new object[] { GraphsFromExamples.G8, GraphsFromExamples.G5_1Vdifferent, 4, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3) } },
            // 10.C5_C5_both-directed
            new object[] { GraphsFromExamples.C5directed, GraphsFromExamples.C5directed, 5, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3), (4, 4) } },
            // 11.C5_C6_both-directed
            new object[] { GraphsFromExamples.C5directed, GraphsFromExamples.C6directed, 4, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3) } },
            // 12.C5_C5_both-undirected
            new object[] { GraphsFromExamples.C5undirected, GraphsFromExamples.C5undirected, 5, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3), (4, 4) } },
            // 13.C5_C6_both-undirected
            new object[] { GraphsFromExamples.C5undirected, GraphsFromExamples.C6undirected, 4, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3) } },
            // 14.C5_C5_directed-undirected
            new object[] { GraphsFromExamples.C5directed, GraphsFromExamples.C5undirected, 2, new List<(int G1, int G2)>
            { (0, 0), (2, 2) } },
            // 15.K5_K5
            new object[] { GraphsFromExamples.K5, GraphsFromExamples.K5, 5, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3), (4, 4) } },
            // 16.K5_K6
            new object[] { GraphsFromExamples.K5, GraphsFromExamples.K6, 5, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3), (4, 4) } },
            // 17.K5_G5_isolated
            new object[] { GraphsFromExamples.K5, GraphsFromExamples.G5_isolated, 1, new List<(int G1, int G2)>
            { (0, 0) } },
            // 18.G5_G6_both_isolated
            new object[] { GraphsFromExamples.G5_isolated, GraphsFromExamples.G6_isolated, 5, new List<(int G1, int G2)>
            { (0, 0), (1, 1), (2, 2), (3, 3), (4, 4) } },
            // 19.G5_G5_with_and_without_loops
            new object[] { GraphsFromExamples.G5_with_loops, GraphsFromExamples.G5_without_loops, 0, new List<(int G1, int G2)>
            { } },
        };

        [Theory, MemberData(nameof(FindDistanceData))]
        public void Exact_tests(int[,] matrix1, int[,] matrix2, int mcsCount, List<(int G1, int G2)> expectedVertices)
        {
            var distanceFinder = new ExactDistanceFinder();

            var (Distance, Mapping) = distanceFinder.FindDistance(
                CreateGraphFromMatrix(matrix1),
                CreateGraphFromMatrix(matrix2));

            var expectedDistance = 1.0 - mcsCount
                / (double)Math.Max(matrix1.GetLength(0), matrix2.GetLength(0));

            Assert.Equal(expectedDistance, Distance);
            Assert.Equal(expectedVertices.Count, Mapping.Count);
            for (int i = 0; i < expectedVertices.Count; i++)
            {
                Assert.Equal(expectedVertices[i].G1, Mapping[i].G1);
                Assert.Equal(expectedVertices[i].G2, Mapping[i].G2);
            }
        }

        [Theory, MemberData(nameof(FindDistanceDataFromExamples))]
        public void Exact_from_examples_tests(int[,] matrix1, int[,] matrix2, int mcsCount, List<(int G1, int G2)> expectedVertices)
        {
            var distanceFinder = new ExactDistanceFinder();

            var (Distance, Mapping) = distanceFinder.FindDistance(
                CreateGraphFromMatrix(matrix1),
                CreateGraphFromMatrix(matrix2));

            var expectedDistance = 1.0 - mcsCount
                / (double)Math.Max(matrix1.GetLength(0), matrix2.GetLength(0));

            Assert.Equal(expectedDistance, Distance);
            Assert.Equal(expectedVertices.Count, Mapping.Count);
            for (int i = 0; i < expectedVertices.Count; i++)
            {
                Assert.Equal(expectedVertices[i].G1, Mapping[i].G1);
                Assert.Equal(expectedVertices[i].G2, Mapping[i].G2);
            }
        }

        private static Graph CreateGraphFromMatrix(int[,] matrix)
        {
            var bools = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    bools[i, j] = (matrix[i, j] != 0);
                }
            }

            return new(bools.GetLength(0), bools);
        }
    }
}