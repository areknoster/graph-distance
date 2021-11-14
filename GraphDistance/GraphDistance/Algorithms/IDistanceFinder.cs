namespace GraphDistance
{
    public interface IDistanceFinder
    {
        double FindDistance(Graph graph1, Graph graph2);
        string Name { get; }
    }
}