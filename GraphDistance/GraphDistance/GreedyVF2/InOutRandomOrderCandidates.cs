using System.Collections.Generic;
using System.Linq;

namespace GraphDistance.GreedyVF2
{
    internal class InOutRandomOrderCandidates : ICandidatesFinder
    {
        private SubgraphCandidates graph1Candidates, graph2Candidates;

        public InOutRandomOrderCandidates(MeasuredGraphs graphs)
        {
            graph1Candidates = new SubgraphCandidates(graphs.Graph1);
            graph2Candidates = new SubgraphCandidates(graphs.Graph2);
        }

        public IEnumerable<(int, int)> FindCandidates()
        {
            if (graph1Candidates.OutNeighbours.Count != 0 && graph2Candidates.OutNeighbours.Count != 0)
            {
                var out2Candidate = graph2Candidates.OutNeighbours.First();
                foreach (var graph1OutCandidate in graph1Candidates.OutNeighbours)
                {
                    yield return (graph1OutCandidate, out2Candidate);
                }
            }


            if (graph1Candidates.InNeighbours.Count != 0 && graph2Candidates.InNeighbours.Count != 0)
            {
                var in2Candidate = graph2Candidates.InNeighbours.First();
                foreach (var graph1InCandidate in graph1Candidates.InNeighbours)
                {
                    yield return (graph1InCandidate, in2Candidate);
                }
            }


            if (graph1Candidates.Remaining.Count != 0 && graph2Candidates.Remaining.Count != 0)
            {
                var remaining2Candidate = graph2Candidates.Remaining.First();
                foreach (var graph1RemainingCandidate in graph1Candidates.Remaining)
                {
                    yield return (graph1RemainingCandidate, remaining2Candidate);
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