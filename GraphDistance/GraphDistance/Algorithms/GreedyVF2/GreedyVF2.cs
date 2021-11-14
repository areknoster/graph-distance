using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDistance.Algorithms.GreedyVF2
{
    public class GreedyVf2 : IDistanceFinder
    {
        public static GreedyVf2 CreateGreedyVf2WithInOutRandomCandidates(int attempts = 1)
        {
            return new(
                $"GreedyVf2WithInOutRandomCandidates{attempts}Attempts",
                attempts,
                new InOutRandomOrderCandidatesFactory());
        }

        private readonly CandidatesFinderFactory candidatesFinderFactory;
        private readonly int attempts;

        private GreedyVf2(string name, int attempts, CandidatesFinderFactory candidatesFinderFactory)
        {
            this.attempts = attempts < 1 ? 1 : attempts;
            this.candidatesFinderFactory = candidatesFinderFactory;
            this.Name = name;
        }

        public double FindDistance(Graph graph1, Graph graph2)
        {
            var graphs = new MeasuredGraphs(graph1, graph2);
            List<(int, int)> maxMapping = new List<(int, int)>();
            for (int i = 0; i < attempts; i++)
            {
                var mapping = GreedyFindMaxMapping(graphs);
                if (maxMapping.Count < mapping.Count)
                {
                    maxMapping = mapping;
                }
            }

            graph1.GetInducedSubgraph(maxMapping.Select((t) => t.Item1).ToList()).Print();

            return 1.0 - (double) maxMapping.Count / (double) Math.Max(graph1.Size, graph2.Size);
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