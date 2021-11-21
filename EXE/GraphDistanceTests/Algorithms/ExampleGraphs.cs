namespace GraphDistance.GraphDistanceTests.Algorithms
{
    public static class ExampleGraphs
    {
        public static readonly int[,] EmptyV = new int[1, 1]
        {
            { 0 },
        };

        public static readonly int[,] LoopV = new int[1, 1]
        {
            { 1 },
        };

        public static readonly int[,] EmptyV2 = new int[2, 2]
        {
            { 0, 0 },
            { 0, 0 },
        };

        public static readonly int[,] OneLoopV2 = new int[2, 2]
        {
            { 1, 0 },
            { 0, 0 },
        };

        public static readonly int[,] TwoLoopsV2 = new int[2, 2]
        {
            { 1, 0 },
            { 0, 1 },
        };

        public static readonly int[,] K2 = new int[2, 2]
        {
            { 1, 1 },
            { 1, 1 },
        };

        public static readonly int[,] K2withoutLoops = new int[2, 2]
        {
            { 0, 1 },
            { 1, 0 },
        };

        public static readonly int[,] C3directed = new int[3, 3]
        {
            { 0, 1, 0 },
            { 0, 0, 1 },
            { 1, 0, 0 },
        };

        public static readonly int[,] C3undirected = new int[3, 3]
        {
            { 0, 1, 1 },
            { 1, 0, 1 },
            { 1, 1, 0 },
        };

        public static readonly int[,] K3 = new int[3, 3]
        {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 },
        };

        public static readonly int[,] C4directed = new int[4, 4]
        {
            { 0, 1, 0, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 0, 1 },
            { 1, 0, 0, 0 },
        };

        public static readonly int[,] C4undirected = new int[4, 4]
        {
            { 0, 1, 0, 1 },
            { 1, 0, 1, 0 },
            { 0, 1, 0, 1 },
            { 1, 0, 1, 0 },
        };

        public static readonly int[,] RandomV10 = new int[10, 10]
        {
            { 1, 1, 0, 1, 1, 0, 1, 0, 0, 1 },
            { 1, 1, 1, 0, 1, 0, 0, 0, 1, 1 },
            { 0, 0, 0, 1, 0, 1, 1, 0, 0, 1 },
            { 1, 0, 1, 1, 0, 0, 1, 1, 0, 1 },
            { 0, 1, 0, 0, 1, 0, 1, 1, 1, 0 },
            { 1, 0, 1, 0, 0, 1, 0, 1, 0, 1 },
            { 0, 0, 0, 1, 1, 0, 0, 0, 1, 0 },
            { 0, 1, 1, 0, 1, 0, 1, 1, 0, 1 },
            { 0, 1, 1, 1, 1, 1, 0, 0, 1, 0 },
            { 1, 0, 1, 0, 0, 0, 0, 1, 1, 1 },
        };

        public static readonly int[,] RandomV10SubgraphV5 = new int[5, 5]
        {
            { 1, 1, 0, 1, 1 },
            { 1, 1, 1, 0, 1 },
            { 0, 0, 0, 1, 0 },
            { 1, 0, 1, 1, 0 },
            { 0, 1, 0, 0, 1 },
        };

        public static readonly int[,] RandomV10Subgraph5WithExtraV6 = new int[6, 6]
        {
            { 1, 1, 0, 1, 1, 1 },
            { 1, 1, 1, 0, 1, 1 },
            { 0, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 1, 0, 1 },
            { 0, 1, 0, 0, 1, 1 },
            { 1, 1, 1, 1, 1, 1 },
        };
    }

    public static class GraphsFromExamples
    {
        public static readonly int[,] G5 = new int[5, 5]
        {
            { 1, 1, 0, 1, 1 },
            { 1, 1, 1, 0, 1 },
            { 0, 0, 0, 1, 0 },
            { 1, 0, 1, 1, 0 },
            { 0, 1, 0, 0, 1 },
        };

        public static readonly int[,] G5_swapped = new int[5, 5]
        {
            { 1, 0, 1, 0, 0 },
            { 0, 0, 0, 1, 0 },
            { 1, 1, 1, 0, 1 },
            { 0, 1, 0, 1, 1 },
            { 1, 0, 1, 1, 1 },
        };

        public static readonly int[,] G5_1Vdifferent = new int[5, 5]
        {
            { 1, 1, 0, 1, 0 },
            { 1, 1, 1, 0, 1 },
            { 0, 0, 0, 1, 1 },
            { 1, 0, 1, 1, 0 },
            { 0, 1, 1, 0, 0 },
        };

        public static readonly int[,] G5_2Vdifferent = new int[5, 5]
        {
            { 1, 1, 1, 0, 1 },
            { 1, 1, 1, 0, 1 },
            { 0, 1, 1, 1, 1 },
            { 1, 1, 1, 0, 0 },
            { 0, 1, 0, 1, 1 },
        };

        public static readonly int[,] G5_4Vdifferent = new int[5, 5]
        {
            { 0, 0, 0, 0, 0 },
            { 1, 0, 1, 1, 0 },
            { 0, 0, 0, 1, 0 },
            { 1, 1, 1, 0, 1 },
            { 0, 0, 0, 1, 0 },
        };

        public static readonly int[,] G5_5Vdifferent = new int[5, 5]
        {
            { 0, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1 },
            { 1, 0, 1, 1, 1 },
            { 1, 1, 1, 0, 1 },
            { 1, 1, 0, 1, 0 },
        };

        public static readonly int[,] G8 = new int[8, 8]
        {
            { 1, 1, 0, 1, 1, 0, 1, 0 },
            { 1, 1, 1, 0, 1, 1, 1, 1 },
            { 0, 0, 0, 1, 0, 1, 0, 0 },
            { 1, 0, 1, 1, 0, 0, 0, 1 },
            { 0, 1, 0, 0, 1, 1, 1, 0 },
            { 1, 1, 0, 1, 0, 0, 0, 1 },
            { 0, 0, 1, 0, 0, 1, 1, 0 },
            { 1, 0, 0, 1, 0, 1, 0, 1 },
        };

        public static readonly int[,] C5directed = new int[5, 5]
        {
            { 0, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 0 },
            { 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0 },
        };

        public static readonly int[,] C5undirected = new int[5, 5]
        {
            { 0, 1, 0, 0, 1 },
            { 1, 0, 1, 0, 0 },
            { 0, 1, 0, 1, 0 },
            { 0, 0, 1, 0, 1 },
            { 1, 0, 0, 1, 0 },
        };

        public static readonly int[,] C6directed = new int[6, 6]
        {
            { 0, 1, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0 },
        };

        public static readonly int[,] C6undirected = new int[6, 6]
        {
            { 0, 1, 0, 0, 0, 1 },
            { 1, 0, 1, 0, 0, 0 },
            { 0, 1, 0, 1, 0, 0 },
            { 0, 0, 1, 0, 1, 0 },
            { 0, 0, 0, 1, 0, 1 },
            { 1, 0, 0, 0, 1, 0 },
        };

        public static readonly int[,] K5 = new int[5, 5]
        {
            { 0, 1, 1, 1, 1 },
            { 1, 0, 1, 1, 1 },
            { 1, 1, 0, 1, 1 },
            { 1, 1, 1, 0, 1 },
            { 1, 1, 1, 1, 0 },
        };

        public static readonly int[,] K6 = new int[6, 6]
        {
            { 0, 1, 1, 1, 1, 1 },
            { 1, 0, 1, 1, 1, 1 },
            { 1, 1, 0, 1, 1, 1 },
            { 1, 1, 1, 0, 1, 1 },
            { 1, 1, 1, 1, 0, 1 },
            { 1, 1, 1, 1, 1, 0 },
        };

        public static readonly int[,] G5_isolated = new int[5, 5]
        {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
        };

        public static readonly int[,] G6_isolated = new int[6, 6]
        {
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0 },
        };

        public static readonly int[,] G7_with_2_extra_isolated = new int[7, 7]
        {
            { 1, 1, 0, 1, 1, 0, 0 },
            { 1, 1, 1, 0, 1, 0, 0 },
            { 0, 0, 0, 1, 0, 0, 0 },
            { 1, 0, 1, 1, 0, 0, 0 },
            { 0, 1, 0, 0, 1, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };

        public static readonly int[,] G5_with_loops = new int[5, 5]
        {
            { 1, 1, 0, 1, 1 },
            { 1, 1, 1, 0, 1 },
            { 0, 0, 1, 1, 0 },
            { 1, 0, 1, 1, 0 },
            { 0, 1, 0, 0, 1 },
        };

        public static readonly int[,] G5_without_loops = new int[5, 5]
        {
            { 0, 1, 0, 1, 1 },
            { 1, 0, 1, 0, 1 },
            { 0, 0, 0, 1, 0 },
            { 1, 0, 1, 0, 0 },
            { 0, 1, 0, 0, 0 },
        };
    }
}