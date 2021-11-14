using System;
using System.Collections.Generic;
using GraphDistance.Algorithms.Exact;
using Xunit;

namespace GraphDistance.GraphDistanceTests.Algorithms
{
    public class ExactAlgorithmTests
    {
        public static IEnumerable<object[]> FindDistanceData => new List<object[]>
        {
            new object[] { ExampleGraphs.EmptyV, ExampleGraphs.LoopV, 0 },
            new object[] { ExampleGraphs.EmptyV, ExampleGraphs.EmptyV2, 1 },
            new object[] { ExampleGraphs.TwoLoopsV2, ExampleGraphs.EmptyV2, 0 },
            new object[] { ExampleGraphs.TwoLoopsV2, ExampleGraphs.K2, 1 },
            new object[] { ExampleGraphs.K2, ExampleGraphs.K3, 2 },
            new object[] { ExampleGraphs.K2withoutLoops, ExampleGraphs.K2, 0 },
            new object[] { ExampleGraphs.C3directed, ExampleGraphs.C3undirected, 1 },
            new object[] { ExampleGraphs.C3directed, ExampleGraphs.C4directed, 2 },
            new object[] { ExampleGraphs.C3undirected, ExampleGraphs.C4undirected, 2 },
            new object[] { ExampleGraphs.RandomV10, ExampleGraphs.RandomV10, 10 },
            new object[]
            {
                ExampleGraphs.RandomV10SubgraphV5,
                ExampleGraphs.RandomV10SubgraphV5
                    .SwapLabels(0, 2)
                    .SwapLabels(3, 4)
                    .SwapLabels(1, 2),
                5,
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
                5
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
                5
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
                5
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
                5
            },
        };

        [Theory, MemberData(nameof(FindDistanceData))]
        public void Exact_tests(int[,] matrix1, int[,] matrix2, int mcsCount)
        {
            var distanceFinder = new ExactDistanceFinder();

            var distance = distanceFinder.FindDistance(
                CreateGraphFromMatrix(matrix1),
                CreateGraphFromMatrix(matrix2));

            var expectedDistance = 1.0 - (double) mcsCount
                / (double) Math.Max(matrix1.GetLength(0), matrix2.GetLength(0));

            Assert.Equal(expectedDistance, distance);
        }

        private Graph CreateGraphFromMatrix(int [,] matrix)
        {
            var bools = new bool[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    bools[i,j] = matrix[i,j] == 0 ? false : true;
                }
            }

            return new(bools.GetLength(0), bools);
        }
    }
}