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
    }
}