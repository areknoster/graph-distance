using System;
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
            Console.WriteLine("=====================================================================================");
            Console.WriteLine($"Comparing graphs with {distanceFinders[algorithmNo].Name}");

            Console.WriteLine("--> First graph:");
            graph1.Print();
            Console.WriteLine("--> Second graph:");
            graph2.Print();

            Console.WriteLine("---------------------------------------------------------------------------------");
            var result = Run(distanceFinders[algorithmNo], graph1, graph2);
            Console.WriteLine($"{distanceFinders[algorithmNo].Name}: {result}");

            Console.WriteLine("=====================================================================================");
        }

        private Result Run(IDistanceFinder distanceFinder, Graph graph1, Graph graph2)
        {
            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                double distance = -1;
                var task = Task.Run(() => distance = distanceFinder.FindDistance(graph1, graph2));

                if (!task.Wait(Timeout))
                {
                    return new Timeout(Timeout);
                }

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
        private readonly TimeSpan Elapsed;
        private readonly double Distance;

        public Success(TimeSpan elapsed, double distance) => (Elapsed, Distance) = (elapsed, distance);

        public override string ToString() => $"Result={Distance} Elapsed={Elapsed}";
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