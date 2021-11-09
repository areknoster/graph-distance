using System;

namespace GraphDistance
{
    internal class Program
    {
        private static readonly string fileToReadGraph = "../../../GraphFileTests/TestReadGraph.txt";
        private static readonly string fileToWriteGraph = "../../../GraphFileTests/TestWriteGraph.txt";

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Reading graph from file: TestReadGraph.txt");
                Graph graph = GraphFile.Read(fileToReadGraph);

                Console.WriteLine();
                graph.Print();
                Console.WriteLine();

                Console.WriteLine("Writing graph to file: TestWriteGraph.txt");
                GraphFile.Write(graph, fileToWriteGraph);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}