namespace GraphDistance.GreedyVF2
{
    internal class MeasuredGraphs
    {
        public MeasuredGraphs(Graph graph1, Graph graph2)
        {
            Graph1 = graph1;
            Graph2 = graph2;
        }

        public Graph Graph1 { get; }
        public Graph Graph2 { get; }
    }
}