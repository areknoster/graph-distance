using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphDistance.GreedyVF2
{
    internal class InOutRandomOrderCandidates : ICandidatesFinder
    {
        private SubgraphCandidates graph1Candidates, graph2Candidates;
        private Random random;

        public InOutRandomOrderCandidates(MeasuredGraphs graphs)
        {
            graph1Candidates = new SubgraphCandidates(graphs.Graph1);
            graph2Candidates = new SubgraphCandidates(graphs.Graph2);
            random = new Random();
        }

        public IEnumerable<(int, int)> FindCandidates()
        {
            var CandidatesPairOrderedSet = new (HashSet<int>, HashSet<int>)[]
            {
                (graph1Candidates.OutNeighbours, graph2Candidates.OutNeighbours),
                (graph1Candidates.InNeighbours, graph2Candidates.InNeighbours),
                (graph1Candidates.Remaining, graph2Candidates.Remaining),
            };
            
            foreach (var nodesPairsSpace in CandidatesPairOrderedSet)
            {
                if (nodesPairsSpace.Item1.Count != 0 && nodesPairsSpace.Item2.Count != 0)
                {
                    var secondSetCandidate = nodesPairsSpace.Item2.Skip(random.Next(nodesPairsSpace.Item2.Count - 1)).First();
                    foreach (var firstSetCandidate in nodesPairsSpace.Item1)
                    {
                        yield return (firstSetCandidate, secondSetCandidate);
                    }
                }
            }
        }

        public void AddMatch((int, int) match)
        {
            graph1Candidates.AddNodeToSubgraph(match.Item1);
            graph2Candidates.AddNodeToSubgraph(match.Item2);
        }
    }

    internal class InOutRandomOrderCandidatesFactory : CandidatesFinderFactory
    {
        public override ICandidatesFinder GetCandidatesFinder(MeasuredGraphs measuredGraphs)
        {
            return new InOutRandomOrderCandidates(measuredGraphs);
        }
    }

    internal class SubgraphCandidates
    {
        private Graph graph;
        public HashSet<int> OutNeighbours { get; }
        public HashSet<int> InNeighbours { get; }
        public HashSet<int> Remaining { get; }

        public SubgraphCandidates(Graph graph)
        {
            this.graph = graph;

            OutNeighbours = new HashSet<int>();
            InNeighbours = new HashSet<int>();
            Remaining = new HashSet<int>(graph.Size);
            for (int i = 0; i < graph.Size; i++)
            {
                Remaining.Add(i);
            }
        }

        public void AddNodeToSubgraph(int n)
        {
            OutNeighbours.Remove(n);
            InNeighbours.Remove(n);
            Remaining.Remove(n);
            Remaining.RemoveWhere((r) =>
            {
                if (graph[n, r])
                {
                    OutNeighbours.Add(r);
                    return true;
                }

                if (graph[r, n])
                {
                    InNeighbours.Add(r);
                    return true;
                }

                return false;
            });
        }
    }
}