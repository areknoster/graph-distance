using System;
using GraphDistance.Algorithms.Exact;
using GraphDistance.Algorithms.GreedyVF2;

namespace GraphDistance
{
    internal class Program
    {
        public static string path = $"../../../../../Examples/";

        private static void Main(string[] args)
        {
            var comparer = new AlgorithmsComparer(
                TimeSpan.FromSeconds(10),
                new ExactDistanceFinder(),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(attempts: 10),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(attempts: 500));

            ShowMenu(comparer);

            Console.ReadLine();
        }

        private static void ShowMenu(AlgorithmsComparer comparer)
        {
            Console.WriteLine("=================");
            Console.WriteLine("Graph distance");
            Console.WriteLine("=================");
            Console.WriteLine("Select mode:");
            Console.WriteLine("1 - your own data");
            Console.WriteLine("2 - example data");
            var input = Console.ReadLine();
            if (input == "1")
            {
                CalculateOwnData(comparer);
            }
            else if (input == "2")
            {
                CalculateExamples(comparer);
            }
            else
            {
                Console.WriteLine("Invalid mode");
            }
        }

        private static void CalculateExamples(AlgorithmsComparer comparer)
        {
            var graph_3 = GraphFile.Read(path + "Graph_Size_3.txt");
            var graph_4 = GraphFile.Read(path + "Graph_Size_4.txt");
            var graph_5 = GraphFile.Read(path + "Graph_Size_5.txt");
            var graph_6 = GraphFile.Read(path + "Graph_Size_6.txt");
            var graph_7 = GraphFile.Read(path + "Graph_Size_7.txt");
            var graph_9 = GraphFile.Read(path + "Graph_Size_9.txt");
            var graph_10 = GraphFile.Read(path + "Graph_Size_10.txt");
            var graph_15 = GraphFile.Read(path + "Graph_Size_15.txt");
            var graph_35 = GraphFile.Read(path + "Graph_Size_35.txt");
            var graph_40 = GraphFile.Read(path + "Graph_Size_40.txt");

            comparer.FindDistances(graph_3, graph_3);
            comparer.FindDistances(graph_3, graph_4);
            comparer.FindDistances(graph_4, graph_3);
            comparer.FindDistances(graph_3, graph_6);
            comparer.FindDistances(graph_6, graph_3);
            comparer.FindDistances(graph_5, graph_6);
            comparer.FindDistances(graph_6, graph_5);
            comparer.FindDistances(graph_3, graph_7);
            comparer.FindDistances(graph_7, graph_3);
            comparer.FindDistances(graph_4, graph_7);
            comparer.FindDistances(graph_7, graph_4);
            comparer.FindDistances(graph_7, graph_6);
            comparer.FindDistances(graph_6, graph_7);
            comparer.FindDistances(graph_9, graph_10);
            comparer.FindDistances(graph_10, graph_15);
            comparer.FindDistances(graph_35, graph_40);
        }

        private static void CalculateOwnData(AlgorithmsComparer comparer)
        {
            Console.WriteLine("Type first graph file absolute path:");
            var absolutePath1 = Console.ReadLine();
            Console.WriteLine("Type second graph file absolute path:");
            var absolutePath2 = Console.ReadLine();

            try
            {
                var graph_1 = GraphFile.Read(absolutePath1);
                var graph_2 = GraphFile.Read(absolutePath2);
                comparer.FindDistances(graph_1, graph_2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}