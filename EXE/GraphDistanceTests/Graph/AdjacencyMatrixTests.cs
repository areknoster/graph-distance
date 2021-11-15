using System;
using System.Collections.Generic;
using Xunit;

namespace GraphDistance.Tests
{
    public class AdjacencyMatrixTests
    {
        private readonly int negativeSize = -1;

        private readonly int zeroSize = 0;
        private readonly bool[,] zeroSizeMatrix = new bool[0, 0];

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

        private readonly bool[,] validMatrixAfterSwappingNodesLabels = new bool[3, 3]
        {
            { false, false, true },
            { true, false, false },
            { true, true, false },
        };

        [Fact(DisplayName = "Constructor with negative size")]
        public void AdjacencyMatrixTest11()
        {
            var exception = Assert.Throws<ArgumentException>(
                () => { AdjacencyMatrix adjacencyMatrix = new(negativeSize); });
            Assert.Equal(Errors.AdjacencyMatrix.NEGATIVE_SIZE, exception.Message);
        }

        [Fact(DisplayName = "Constructor with zero size")]
        public void AdjacencyMatrixTest12()
        {
            AdjacencyMatrix adjacencyMatrix = new(zeroSize);
            Assert.Equal(zeroSizeMatrix, adjacencyMatrix.Values);
        }

        [Fact(DisplayName = "Constructor with valid size")]
        public void AdjacencyMatrixTest13()
        {
            AdjacencyMatrix adjacencyMatrix = new(validSize);
            Assert.Equal(validSizeEmptyMatrix, adjacencyMatrix.Values);
        }

        [Fact(DisplayName = "Constructor with invalid matrix")]
        public void AdjacencyMatrixTest21()
        {
            var exception = Assert.Throws<ArgumentException>(
                () => { AdjacencyMatrix adjacencyMatrix = new(invalidMatrix); });
            Assert.Equal(Errors.AdjacencyMatrix.DIMENSIONS_NOT_EQUAL, exception.Message);
        }

        [Fact(DisplayName = "Constructor with valid matrix")]
        public void AdjacencyMatrixTest22()
        {
            AdjacencyMatrix adjacencyMatrix = new(validMatrix);
            Assert.Equal(validMatrix, adjacencyMatrix.Values);
        }

        [Fact(DisplayName = "Swapping invalid nodes")]
        public void SwapNodesLabelsTest1()
        {
            AdjacencyMatrix adjacencyMatrix = new(validMatrix);
            var exception = Assert.Throws<ArgumentException>(
                () => { adjacencyMatrix.SwapNodesLabels(2, 3); });
            Assert.Equal(Errors.AdjacencyMatrix.INVALID_NODE_LABEL, exception.Message);
        }

        [Fact(DisplayName = "Swapping valid nodes")]
        public void SwapNodesLabelsTest2()
        {
            AdjacencyMatrix adjacencyMatrix = new(validMatrix);
            adjacencyMatrix.SwapNodesLabels(1, 2);
            Assert.Equal(validMatrixAfterSwappingNodesLabels, adjacencyMatrix.Values);
        }

        [Fact(DisplayName = "Getting sources of incoming edges from invalid node")]
        public void GetSourcesOfIncomingEdgesNodesTest1()
        {
            AdjacencyMatrix adjacencyMatrix = new(validMatrix);
            var exception = Assert.Throws<ArgumentException>(
                () => { var result = adjacencyMatrix.GetTargetsOfOutgoingEdges(3); });
            Assert.Equal(Errors.AdjacencyMatrix.INVALID_NODE_LABEL, exception.Message);
        }

        [Fact(DisplayName = "Getting sources of incoming edges from valid node")]
        public void GetSourcesOfIncomingEdgesNodesTest2()
        {
            AdjacencyMatrix adjacencyMatrix = new(validMatrix);
            var result = adjacencyMatrix.GetSourcesOfIncomingEdges(0);
            Assert.Equal(new List<int>() { 1, 2 }, result);
        }

        [Fact(DisplayName = "Getting targets of outgoing edges from invalid node")]
        public void GetTargetsOfOutgoingEdgesNodesTest1()
        {
            AdjacencyMatrix adjacencyMatrix = new(validMatrix);
            var exception = Assert.Throws<ArgumentException>(
                () => { var result = adjacencyMatrix.GetTargetsOfOutgoingEdges(3); });
            Assert.Equal(Errors.AdjacencyMatrix.INVALID_NODE_LABEL, exception.Message);
        }

        [Fact(DisplayName = "Getting targets of outgoing edges from valid node")]
        public void GetTargetsOfOutgoingEdgesNodesTest2()
        {
            AdjacencyMatrix adjacencyMatrix = new(validMatrix);
            var result = adjacencyMatrix.GetTargetsOfOutgoingEdges(1);
            Assert.Equal(new List<int>() { 0, 2 }, result);
        }
    }
}