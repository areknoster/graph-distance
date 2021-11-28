using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GraphDistance;
using GraphDistance.Algorithms.Exact;
using GraphDistance.Algorithms.GreedyVF2;

namespace Analyzer
{
    class Program
    {
        
        public static TimeSpan timeout = TimeSpan.FromSeconds(30);

        // public static string ExamplesPath = $"../../../../../Examples/";
        // public static string ResultsPath = $"../../../../../results.csv";
        public static string ExamplesPath = $"../../Examples/";
        public static string ResultsPath = $"../../results.csv";
        public const int Step = 10;
        public const int StepCount = 25;
        
        public static List<double> Densities = new (){ 0.5, 1, 1.5, 3, 5, 10};

        // Uncomment before running makeEXE.bat
        //public static string examplesPath = $"../Examples/";
        
        static void Main(string[] args)
        {
            var analyzer = new Analyzer(timeout, ResultsPath,
                new ExactDistanceFinder(),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(1),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(10),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(100),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(500));

            var examplesPaths = Directory.GetFiles(ExamplesPath, "*.txt");
            var cases = examplesPaths.Select(path =>
            {
                var (G1, G2) = GraphFile.Read(path);
                var desc = Path.GetFileNameWithoutExtension(path);
                return new Case((new MeasuredGraph(G1), new MeasuredGraph(G2)), desc);
            });
            analyzer.AddRange(cases);
            
            var caseSizes = new List<(int, int)>();
            for (int i = 3; i <= 9; i++)
            {
                for (int j = 3; j <= i; j++)
                {
                    caseSizes.Add((i,j));
                }
            }
            
            foreach (var density in Densities)
            {
                foreach (var cs in caseSizes)
                {
                    var g1 = new MeasuredGraph(GraphGenerator.Random(cs.Item1, density));
                    var g2 = new MeasuredGraph(GraphGenerator.Random(cs.Item2, density));
                    analyzer.Add(new Case((g1, g2), "random graphs with same density"));
                }
            }
            

            
            caseSizes.AddRange(Enumerable.Range(1, StepCount).Select(v => (v * Step, v * Step)));
            caseSizes.AddRange(Enumerable.Range(1, StepCount).Select(v => (v * Step, (int) (v * Step * 0.7))));
            caseSizes.AddRange(Enumerable.Range(1, StepCount).Select(v => (v * Step, (int) (v * Step * 0.3))));
            
            foreach (var density in Densities)
            {
                foreach (var cs in caseSizes)
                {
                    var g1 = GraphGenerator.Random(cs.Item1 , density);
                    var g2 = GraphGenerator.Subgraph(GraphGenerator.Shuffle(g1), cs.Item2);
                    var analyzedCase = new Case((new MeasuredGraph(g1), new MeasuredGraph(g2)), "G2 is shuffled subgraph of G1");
                    analyzer.Add(analyzedCase);
                }
            }

            foreach (var cs in caseSizes)
            {
                var g1 = GraphGenerator.CoveringCycle(cs.Item1);
                var g2 = GraphGenerator.CoveringCycle(cs.Item2);
                var analyzedCase = new Case((new MeasuredGraph(g1), new MeasuredGraph(g2)), "hamilton cycle and nothing else");
                analyzer.Add(analyzedCase);
            }

            analyzer.Run();

        }
    }
}