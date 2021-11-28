using System.Collections.Generic;

namespace GraphDistance
{
    public interface IDistanceFinder
    {
        (double Distance, List<(int G1, int G2)> Mapping) FindDistance(Graph graph1, Graph graph2);
        string Name { get; }
    }
}