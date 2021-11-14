using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDistance.GreedyVF2
{
    public class GreedyVf2 : IDistanceFinder
    {
        public static GreedyVf2 CreateGreedyVf2WithInOutRandomCandidates()
        {
            return new("GreedyVf2WithInOutRandomCandidates", new InOutRandomOrderCandidatesFactory());
        }

        private readonly CandidatesFinderFactory candidatesFinderFactory;

        private GreedyVf2(string name, CandidatesFinderFactory candidatesFinderFactory)
        {
            this.candidatesFinderFactory = candidatesFinderFactory;
            this.Name = name;
        }


        public double FindDistance(Graph graph1, Graph graph2)
        {
            var graphs = new MeasuredGraphs(graph1, graph2);
            var mapping = GreedyFindMaxMapping(graphs);
            graph1.GetInducedSubgraph(mapping.Select((t) => t.Item1).ToList()).Print();

            return 1.0 - (double) mapping.Count / (double) Math.Max(graph1.Size, graph2.Size);
        }

        public string Name { get; }

        private List<(int, int)> GreedyFindMaxMapping(MeasuredGraphs measuredGraphs)
        {
            var candidatesFinder = candidatesFinderFactory.GetCandidatesFinder(measuredGraphs);
            var subgraphMapping = new SubgraphMapping(measuredGraphs);
            for (var foundCandidate = true; foundCandidate;)
            {
                var candidates = candidatesFinder.FindCandidates();
                foundCandidate = false;
                foreach (var candidate in candidates)
                {
                    if (subgraphMapping.TryAddPair(candidate))
                    {
                        candidatesFinder.AddMatch(candidate);
                        foundCandidate = true;
                        break;
                    }
                }
            }

            return subgraphMapping;
        }
    }
}