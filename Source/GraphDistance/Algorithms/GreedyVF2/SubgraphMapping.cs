using System.Collections.Generic;

namespace GraphDistance.Algorithms.GreedyVF2
{
    internal class SubgraphMapping : List<(int, int)>
    {
        private readonly MeasuredGraphs graphs;

        public SubgraphMapping(MeasuredGraphs graphs)
        {
            this.graphs = graphs;
        }

        public bool TryAddPair((int, int) matchToCheck)
        {
            if (graphs.Graph1[matchToCheck.Item1, matchToCheck.Item1] !=
                graphs.Graph2[matchToCheck.Item2, matchToCheck.Item2])
            {
                return false;
            }

            foreach (var match in this)
            {
                if ((graphs.Graph1[match.Item1, matchToCheck.Item1] !=
                    graphs.Graph2[match.Item2, matchToCheck.Item2])
                    || (graphs.Graph1[matchToCheck.Item1, match.Item1] !=
                    graphs.Graph2[matchToCheck.Item2, match.Item2]))
                {
                    return false;
                }
            }

            Add(matchToCheck);
            return true;
        }
    }
}