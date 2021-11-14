using System;
using System.Collections.Generic;

namespace GraphDistance
{
    public class DistancesComparer
    {
        private IDistanceFinder[] distanceFinders;

        public DistancesComparer(params IDistanceFinder[] distanceFinders)
        {
            this.distanceFinders = distanceFinders;
        }

        public void FindDistances(Graph graph1, Graph graph2)
        {
            Console.WriteLine("=============================================================================");
            Console.WriteLine("Comparing:");
            graph1.Print();
            graph2.Print();
            foreach (var distanceFinder in distanceFinders)
            {
                Console.WriteLine("------------------");
                Console.WriteLine($"{distanceFinder.Name}: {graph1.Size}, {graph2.Size}");
                var watch = System.Diagnostics.Stopwatch.StartNew();
                try
                {
                    var distance = distanceFinder.FindDistance(graph1, graph2);
                    watch.Stop();
                    Console.WriteLine($"Finished. Distance={distance} elapsed={watch.Elapsed}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error: {e.Message}");
                }
            }
        }
    }
}