using CsvHelper;
using GraphDistance;
using GraphDistance.Algorithms.Exact;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Analyzer
{

    internal class Case
    {
        public Case((MeasuredGraph, MeasuredGraph) graphs, string description)
        {
            Graphs = graphs;
            Description = description;
        }

        public string Description { get; }
        public (MeasuredGraph, MeasuredGraph) Graphs;

        public void Print()
        {
            Console.WriteLine($"{Description}: {Graphs.Item1}, {Graphs.Item2}");
        }
    }

    internal class Analyzer
    {
        private readonly TimeSpan Timeout;
        private readonly List<Case> Cases;
        private readonly IDistanceFinder[] Algorithms;
        private readonly string DataFilePath;

        public Analyzer(TimeSpan timeout, string dataFilePath, params IDistanceFinder[] algorithms)
        {
            Timeout = timeout;
            DataFilePath = dataFilePath;
            Cases = new List<Case>();
            Algorithms = algorithms;
        }

        public void Add(Case c) => Cases.Add(c);
        public void AddRange(IEnumerable<Case> collection) => Cases.AddRange(collection);

        public void Run()
        {
            var records = new List<dynamic>(Cases.Count);
            foreach (var @case in Cases)
            {
                @case.Print();
                var runs = Algorithms.Select(alg => RunAlgorithm(
                    alg,
                    @case.Graphs.Item1.Graph,
                    @case.Graphs.Item2.Graph));
                var measurement = new Measurement(@case, runs);
                records.Add(measurement.ToRecord());
            }
            using (var writer = new StreamWriter(DataFilePath))
            using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(records);
            }
        }


        private AlgorithmRun RunAlgorithm(IDistanceFinder distanceFinder, Graph graph1, Graph graph2)
        {
            if (distanceFinder is ExactDistanceFinder && graph1.Size * graph2.Size > 81) // it will always be a timeout so let's just save some time
            {
                return new AlgorithmRun
                {
                    AlgorithmName = distanceFinder.Name,
                    Result = Result.Timeout,
                    Elapsed = Timeout,
                    Distance = -1,
                };
            }
            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                double distance = -1;
                var task = Task.Run(() => (distance, _) = distanceFinder.FindDistance(graph1, graph2));

                if (!task.Wait(Timeout))
                {
                    return new AlgorithmRun
                    {
                        AlgorithmName = distanceFinder.Name,
                        Result = Result.Timeout,
                        Elapsed = Timeout,
                        Distance = -1,
                    };
                }
                sw.Stop();

                return new AlgorithmRun
                {
                    AlgorithmName = distanceFinder.Name,
                    Result = Result.Success,
                    Elapsed = sw.Elapsed,
                    Distance = distance,
                };
            }
            catch (Exception e)
            {
                return new AlgorithmRun()
                {
                    AlgorithmName = distanceFinder.Name,
                    Result = Result.Error,
                    Distance = 1,
                };
            }
        }
    }

    internal struct Measurement
    {
        public Case Case;
        public IEnumerable<AlgorithmRun> Runs;

        public Measurement(Case @case, IEnumerable<AlgorithmRun> runs)
        {
            Case = @case;
            Runs = runs;
        }

        public dynamic ToRecord()
        {
            var record = new ExpandoObject() as IDictionary<string, object>;
            record["Description"] = Case.Description;
            AddGraphToRecord(record, Case.Graphs.Item1, "G1");
            AddGraphToRecord(record, Case.Graphs.Item2, "G2");
            foreach (var run in Runs)
            {
                record[$"{run.AlgorithmName}_result"] = run.Result.ToString();
                record[$"{run.AlgorithmName}_elapsed_ms"] = run.Elapsed.TotalMilliseconds;
                record[$"{run.AlgorithmName}_distance"] = run.Distance.ToString();
            }
            return record;
        }

        private static void AddGraphToRecord(in IDictionary<string, object> record, MeasuredGraph graph, string prefix)
        {
            record[$"{prefix}_vertices"] = graph.Vertices;
            record[$"{prefix}_edges"] = graph.Edges;
            record[$"{prefix}_density"] = graph.Density;
        }
    }

    internal struct MeasuredGraph
    {
        public Graph Graph;
        public int Vertices => Graph.Size;
        public int Edges => Graph.DirectedEdges;

        public MeasuredGraph(Graph graph)
        {
            this.Graph = graph;
        }
        public double Density => Edges / (double)Vertices;

        public override string ToString()
        {
            return $"(V={Vertices} E={Edges} D={Density})";
        }
    }

    internal enum Result
    {
        Timeout,
        Error,
        Success
    }

    internal struct AlgorithmRun
    {
        public string AlgorithmName;
        public Result Result;
        public TimeSpan Elapsed;
        public double Distance;
    }









}