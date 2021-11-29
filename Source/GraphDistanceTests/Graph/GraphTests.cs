using System;
using System.Collections.Generic;
using Xunit;

namespace GraphDistance.Tests
{
    public class GraphTests
    {
        private readonly int negativeSize = -1;

        private readonly int validSize = 3;
        private readonly bool[,] validSizeEmptyMatrix = new bool[3, 3];

        private readonly bool[,] invalidMatrix = new bool[2, 3]
        {
            { false, true, false },
            { true, false, true },
        };

        private readonly bool[,] validMatrix = new bool[3, 3]
        {
            { false, true, false },
            { true, false, true },
            { true, false, false },
        };

        private readonly List<int> invalidNodes = new() { 0, 3 };
        private readonly List<int> nonuniqueNodes = new() { 0, 0 };
        private readonly List<int> validNodes = new() { 0, 1 };
        private readonly Graph subGraph = new(new bool[2, 2]
        {
            { false, true },
            { true, false },
        });

        [Fact(DisplayName = "Constructor with negative size")]
        public void GraphTest11()
        {
            var exception = Assert.Throws<ArgumentException>(
                () => { Graph graph = new(negativeSize); });
            Assert.Equal(Errors.Graph.NEGATIVE_SIZE, exception.Message);
        }

        [Fact(DisplayName = "Constructor with valid size")]
        public void GraphTest12()
        {
            Graph graph = new(validSize);
            Assert.Equal(validSize, graph.Size);
            Assert.Equal(validSizeEmptyMatrix, graph.AdjacencyMatrix);
        }

        [Fact(DisplayName = "Constructor with invalid matrix")]
        public void GraphTest21()
        {
            var exception = Assert.Throws<ArgumentException>(
                () => { Graph graph = new(invalidMatrix); });
            Assert.Equal(Errors.AdjacencyMatrix.DIMENSIONS_NOT_EQUAL, exception.Message);
        }

        [Fact(DisplayName = "Constructor with valid matrix")]
        public void GraphTest22()
        {
            Graph graph = new(validMatrix);
            Assert.Equal(validMatrix.GetLength(0), graph.Size);
            Assert.Equal(validMatrix, graph.AdjacencyMatrix);
        }

        [Fact(DisplayName = "Constructor with invalid size or matrix")]
        public void GraphTest31()
        {
            var exception = Assert.Throws<ArgumentException>(
                () => { Graph graph = new(validSize, invalidMatrix); });
            Assert.Equal(Errors.Graph.INVALIDE_GRAPH_SIZE_OR_MATRIX, exception.Message);
        }

        [Fact(DisplayName = "Constructor with valid size and matrix")]
        public void GraphTest32()
        {
            Graph graph = new(validSize, validMatrix);
            Assert.Equal(validSize, graph.Size);
            Assert.Equal(validMatrix, graph.AdjacencyMatrix);
        }

        [Fact(DisplayName = "Subgraph induced by invalid nodes")]
        public void GetInducedSubgraphTest1()
        {
            Graph graph = new(validMatrix);
            var exception = Assert.Throws<ArgumentException>(
                () => { var result = graph.GetInducedSubgraph(invalidNodes); });
            Assert.Equal(Errors.Graph.SUBGRAPH_CREATING_INVALID_NODE_LABEL, exception.Message);
        }

        [Fact(DisplayName = "Subgraph induced by nonunique nodes")]
        public void GetInducedSubgraphTest2()
        {
            Graph graph = new(validMatrix);
            var exception = Assert.Throws<ArgumentException>(
                () => { var result = graph.GetInducedSubgraph(nonuniqueNodes); });
            Assert.Equal(Errors.Graph.SUBGRAPH_CREATING_NODE_LABELS_NOT_UNIQUE, exception.Message);
        }

        [Fact(DisplayName = "Subgraph induced by valid nodes")]
        public void GetInducedSubgraphTest3()
        {
            Graph graph = new(validMatrix);
            var result = graph.GetInducedSubgraph(validNodes);
            Assert.Equal(subGraph.Size, result.Size);
            Assert.Equal(subGraph.AdjacencyMatrix, result.AdjacencyMatrix);
        }
    }
}