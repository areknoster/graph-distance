using System.Collections.Generic;

namespace GraphDistance.GreedyVF2
{
    internal interface ICandidatesFinder
    {
        IEnumerable<(int, int)> FindCandidates();
        void AddMatch((int, int) match);
    }

    internal abstract class CandidatesFinderFactory
    {
        public abstract ICandidatesFinder GetCandidatesFinder(MeasuredGraphs measuredGraphs);
    }
}