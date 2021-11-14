using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace GraphDistance
{
    public class AlgorithmsComparer
    {
        private IDistanceFinder[] distanceFinders;
        private TimeSpan Timeout;

        public AlgorithmsComparer(TimeSpan timeout, params IDistanceFinder[] distanceFinders)
        {
            this.Timeout = timeout;
            this.distanceFinders = distanceFinders;
        }

        public void FindDistances(Graph graph1, Graph graph2)
        {
            Console.WriteLine("===============================================================================================================================");
            Console.WriteLine("Comparing:");
            graph1.Print();
            graph2.Print();
            foreach (var distanceFinder in distanceFinders)
            {
                Console.WriteLine("------------------");
                var result = Run(distanceFinder, graph1, graph2);
                Console.WriteLine($"{distanceFinder.Name}: {result}");
            }
        }

        private Result Run(IDistanceFinder distanceFinder ,Graph graph1, Graph graph2)
        {
            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                double distance = -1;
                var task = Task.Run(() => distance = distanceFinder.FindDistance(graph1, graph2));
                if (!task.Wait(Timeout))
                    return new Timeout(Timeout);
                sw.Stop();
                return new Success(sw.Elapsed, distance);
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
        private TimeSpan Elapsed;
        private double Distance;

        public Success(TimeSpan elapsed, double distance)  => (Elapsed, Distance) = (elapsed, distance);

        public override string ToString() => $"Result={Distance} Elapsed={Elapsed}";
    }
        
    internal class Error : Result
    {
        private string Message;

        public Error(string message) => Message = message;

        public override string ToString() => $"Result=Error Message={Message}";
    }

    internal class Timeout : Result
    {
        private TimeSpan Elapsed;
        public Timeout(TimeSpan elapsed) : base() => Elapsed = elapsed;
        public override string ToString() => $"Result=Timeout After={Elapsed}";
    }
}