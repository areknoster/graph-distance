using GraphDistance.Algorithms.Exact;
using GraphDistance.Algorithms.GreedyVF2;
using System;
using System.Collections.Generic;

namespace GraphDistance
{
    internal class Program
    {
        public static string examplesPath = $"../../../../../Examples/";

        private static void Main(string[] args)
        {
            var comparer = new AlgorithmsComparer(
                TimeSpan.FromSeconds(10),
                new ExactDistanceFinder(),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(attempts: 10),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(attempts: 500));

            Start(comparer);
        }

        private static void Start(AlgorithmsComparer comparer)
        {
            Console.WriteLine("=================");
            Console.WriteLine("Graph distance");
            Console.WriteLine("=================");

            while (true)
            {
                switch (GetMode())
                {
                    case "1":
                        CalculateOwnData(comparer);
                        break;
                    case "2":
                        CalculateExamples(comparer);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid mode selected. Exiting...");
                        return;
                }
            }
        }

        private static string GetMode()
        {
            Console.WriteLine("Select mode:");
            Console.WriteLine("1 - your own data");
            Console.WriteLine("2 - example data");
            Console.WriteLine("0 - quit application");

            return Console.ReadLine();
        }

        private static void CalculateOwnData(AlgorithmsComparer comparer)
        {
            Console.WriteLine();
            Console.WriteLine("Type your own data file absolute path:");

            var absolutePath = Console.ReadLine();

            try
            {
                ConsiderExample(absolutePath, comparer);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }
        }

        private static void CalculateExamples(AlgorithmsComparer comparer)
        {
            var examples = new List<string>()
            {
                "1. G5_G5_subgraph5_the-same-graphs",
                "2. G5_G5_subgraph5_the-same-graphs-with-swapped-verticles",
                "3. G5_G5_subgraph4",
                "4. G5_G5_subgraph3",
                "5. G5_G5_subgraph1",
                "6. G5_G7_two-extra-isolated-v",
                "7. G8_G5_subgraph5",
                "8. G8_G5_subgraph5-with-swapped-verticles",
                "9. G8_G5_subgraph4",
                "10. C5_C5_both-directed",
                "11. C5_C6_both-directed",
                "12. C5_C5_both-undirected",
                "13. C5_C6_both-undirected",
                "14. C5_C5_directed-undirected",
                "15. K5_K5",
                "16. K5_K6",
                "17. K5_G5_isolated",
                "18. G5_G6_both_isolated",
                "19. G5_G5_with_and_without_loops",
                "20. G9_G10_random",
                "21. G10_G15_random",
                "22. G35_G40_random"
            };

            try
            {
                int exampleToConsider = GetExampleToConsider(examples.ToArray());

                while (exampleToConsider > 0)
                {
                    ConsiderExample(examplesPath + examples[exampleToConsider - 1] + ".txt", comparer);
                    exampleToConsider = GetExampleToConsider(examples.ToArray());
                }

                Console.WriteLine();
                Console.WriteLine("Exiting examples mode...");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.WriteLine();
            }
        }

        private static void ConsiderExample(string path, AlgorithmsComparer comparer)
        {
            var graphs = GraphFile.Read(path);
            int algorithmNo = GetAlgorithmNo(comparer);

            while (algorithmNo > 0)
            {
                Console.WriteLine();
                comparer.FindDistances(graphs.G1, graphs.G2, algorithmNo - 1);
                algorithmNo = GetAlgorithmNo(comparer);
            }
        }

        private static int GetExampleToConsider(string[] examples)
        {
            Console.WriteLine();
            Console.WriteLine($"Select example to consider:");
            foreach (var example in examples)
            {
                Console.WriteLine(example);
            }
            Console.WriteLine("0. Exit examples mode");

            bool success = int.TryParse(Console.ReadLine(), out int exampleToConsider);
            if (!success || exampleToConsider < 0 || exampleToConsider > examples.Length)
            {
                Console.WriteLine();
                throw new Exception("Invalid example selected. Redirecting to menu...");
            }

            return exampleToConsider;
        }

        private static int GetAlgorithmNo(AlgorithmsComparer comparer)
        {
            Console.WriteLine();
            Console.WriteLine("Select algorithm:");
            for (int i = 1; i <= comparer.DistanceFinders.Length; i++)
            {
                Console.WriteLine($"{i} - {comparer.DistanceFinders[i - 1]}");
            }
            Console.WriteLine("0 - stop considering example");

            bool success = int.TryParse(Console.ReadLine(), out int algorithmNo);
            if (!success || algorithmNo < 0 || algorithmNo > comparer.DistanceFinders.Length)
            {
                Console.WriteLine();
                throw new Exception("Invalid algorithm selected. Redirecting to menu...");
            }

            return algorithmNo;
        }
    }
}