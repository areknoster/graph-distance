using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphDistance
{
    public class AlgorithmsComparer
    {
        private readonly IDistanceFinder[] distanceFinders;
        private readonly TimeSpan Timeout;

        public string[] DistanceFinders { get => distanceFinders.Select(x => x.Name).ToArray(); }

        public AlgorithmsComparer(TimeSpan timeout, params IDistanceFinder[] distanceFinders)
        {
            this.Timeout = timeout;
            this.distanceFinders = distanceFinders;
        }

        public void FindDistances(Graph graph1, Graph graph2, int algorithmNo)
        {
            Console.WriteLine(new string('=', ConsoleExtensions.Width));
            Console.WriteLine($"Considered graph's adjacency matrices with indexes:");

            var g1Lines = graph1.GetPrintLines();
            var g2Lines = graph2.GetPrintLines();

            Console.WriteLine();
            ConsoleExtensions.PrintColumns(g1Lines, g2Lines, 3);
            Console.WriteLine();

            Console.WriteLine(new string('-', ConsoleExtensions.Width));
            Console.WriteLine($"Algorithm {distanceFinders[algorithmNo].Name}.");

            var result = Run(distanceFinders[algorithmNo], graph1, graph2);

            Console.WriteLine($"{result}");

            Console.WriteLine(new string('=', ConsoleExtensions.Width));
        }

        private Result Run(IDistanceFinder distanceFinder, Graph graph1, Graph graph2)
        {
            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                (double Distance, List<(int G1, int G2)> Mapping) result = new();
                var task = Task.Run(() => result = distanceFinder.FindDistance(graph1, graph2));

                if (!task.Wait(Timeout))
                {
                    return new Timeout(Timeout);
                }

                sw.Stop();

                return new Success(sw.Elapsed, result.Distance, graph1, graph2, result.Mapping.OrderBy(m => m.G1).ToList());
            }
            catch (Exception e)
            {
                return new Error(e.Message);
            }
        }
    }

    internal abstract class Result
    {
        public abstract override string ToString();
    }

    internal class Success : Result
    {
        private readonly TimeSpan Elapsed;
        private readonly double Distance;
        Graph Graph1;
        Graph Graph2;
        List<(int G1, int G2)> Mapping;

        public Success(
            TimeSpan elapsed,
            double distance,
            Graph graph1,
            Graph graph2,
            List<(int G1, int G2)> mapping)
        {
            Elapsed = elapsed;
            Distance = distance;
            Graph1 = graph1;
            Graph2 = graph2;
            Mapping = mapping;
            Console.WriteLine("Adjacency matrices specified by indexes of maximum common subgraph of considered graphs:");
            Console.WriteLine();
            PrintMappedGraphs();
            Console.WriteLine();
        }

        public override string ToString() => $"Result={Distance}\nElapsed={Elapsed}";

        private void PrintMappedGraphs()
        {
            var graph1Lines = Graph1.GetPrintLines(Mapping.Select(m => m.G1).ToList());
            var graph2Lines = Graph2.GetPrintLines(Mapping.Select(m => m.G2).ToList());
            ConsoleExtensions.PrintColumns(graph1Lines, graph2Lines, 3);
        }
    }

    internal class Error : Result
    {
        private readonly string Message;

        public Error(string message) => Message = message;

        public override string ToString() => $"Result=Error Message={Message}";
    }

    internal class Timeout : Result
    {
        private readonly TimeSpan Elapsed;

        public Timeout(TimeSpan elapsed) : base() => Elapsed = elapsed;
        public override string ToString() => $"Result=Timeout After={Elapsed}";
    }
}