using System;
using GraphDistance.ExactAlgorithm;

namespace GraphDistance
{
    internal class Program
    {
        private static readonly string fileToReadGraph = "../../../GraphFileTests/TestReadGraph.txt";
        private static readonly string fileToWriteGraph = "../../../GraphFileTests/TestWriteGraph.txt";
        public static string path = $"../../../GraphFileTests/";

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(path + "Graph_Size_3.txt");
                var graph_3 = GraphFile.Read(path + "Graph_Size_3.txt");
                var graph_4 = GraphFile.Read(path + "Graph_Size_4.txt");
                var graph_5 = GraphFile.Read(path + "Graph_Size_5.txt");
                var graph_6 = GraphFile.Read(path + "Graph_Size_6.txt");
                var graph_7 = GraphFile.Read(path + "Graph_Size_7.txt");

                var exact = new ExactDistanceFinder();
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_3, graph_3));

                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_3, graph_4));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_4, graph_3));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_3, graph_6));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_6, graph_3));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_5, graph_6));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_6, graph_5));

                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_3, graph_7));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_7, graph_3));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_4, graph_7));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_7, graph_4));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_7, graph_6));
                Console.WriteLine("Exact distance: " + exact.FindDistance(graph_6, graph_7));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}