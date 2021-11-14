using System;
using GraphDistance.ExactAlgorithm;
using GraphDistance.GreedyVF2;

namespace GraphDistance
{
    internal class Program
    {
        private static readonly string fileToReadGraph = "../../../GraphFileTests/TestReadGraph.txt";
        private static readonly string fileToWriteGraph = "../../../GraphFileTests/TestWriteGraph.txt";
        public static string path = $"../../../GraphFileTests/";

        private static void Main(string[] args)
        {
            var comparer = new AlgorithmsComparer(
                TimeSpan.FromSeconds(10),
                new ExactDistanceFinder(),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(attempts: 10),
            GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(attempts: 500)
            );

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
    }
}