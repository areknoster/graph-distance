using GraphDistance.Algorithms.Exact;
using GraphDistance.Algorithms.GreedyVF2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphDistance
{
    internal class Program
    {
        public static int timeout;

        // Comment before running makeEXE.bat
        public static string examplesPath = $"../../../../../Examples/";
        // Uncomment before running makeEXE.bat
        //public static string examplesPath = $"../Examples/";

        private static void Main(string[] args)
        {
            ConsoleExtensions.SetParams(150, 50);
            Header();
            SetTimeout();

            var comparer = new AlgorithmsComparer(
                TimeSpan.FromSeconds(timeout),
                new ExactDistanceFinder(),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(attempts: 10),
                GreedyVf2.CreateGreedyVf2WithInOutRandomCandidates(attempts: 500));

            Start(comparer);
            Close();
        }

        private static void Header()
        {
            Console.WriteLine(new string('=', ConsoleExtensions.Width));
            Console.WriteLine("Graph distance");
            Console.WriteLine(new string('=', ConsoleExtensions.Width));
            Console.WriteLine();
        }

        private static void SetTimeout()
        {
            Console.WriteLine("Timeout is the time after which every algorithm in program will stop executing.");
            Console.WriteLine("Type your timeout (in seconds):");

            bool success = int.TryParse(Console.ReadLine(), out timeout);
            if (!success || timeout <= 0)
            {
                timeout = 10;
                Console.WriteLine($"Invalid timeout. Default timeout set to {timeout} seconds.");
            }
            else
            {
                Console.WriteLine($"Timeout set to {timeout} seconds.");
            }

            Console.WriteLine();
        }

        private static void Start(AlgorithmsComparer comparer)
        {
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
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message + " Redirecting to menu...");
                Console.WriteLine();
            }
        }

        private static void CalculateExamples(AlgorithmsComparer comparer)
        {
            GetFilesFromExampleDirectory(out string[] examples);

            if (examples == null)
            {
                return;
            }

            try
            {
                int exampleToConsider = GetExampleToConsider(examples);

                while (exampleToConsider > 0)
                {
                    ConsiderExample(examplesPath + examples[exampleToConsider - 1] + ".txt", comparer);
                    exampleToConsider = GetExampleToConsider(examples);
                }

                Console.WriteLine();
                Console.WriteLine("Exiting examples mode...");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message + " Redirecting to menu...");
                Console.WriteLine();
            }
        }

        private static void GetFilesFromExampleDirectory(out string[] examples)
        {
            try
            {
                examples = Directory.GetFiles(examplesPath, "*.txt")
                   .Select(Path.GetFileNameWithoutExtension)
                   .OrderBy(x => x)
                   .ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine("Error while reading example files. " + e.Message + " Redirecting to menu...");
                Console.WriteLine();

                examples = null;
            }
        }

        private static int GetExampleToConsider(string[] examples)
        {
            Console.WriteLine();
            Console.WriteLine($"Select example to consider:");
            for (int i = 1; i <= examples.Length; i++)
            {
                Console.WriteLine($"{i}. {examples[i - 1]}");
            }
            Console.WriteLine("0. Exit examples mode");

            bool success = int.TryParse(Console.ReadLine(), out int exampleToConsider);
            if (!success || exampleToConsider < 0 || exampleToConsider > examples.Length)
            {
                throw new Exception("Invalid example selected.");
            }

            return exampleToConsider;
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
                throw new Exception("Invalid algorithm selected.");
            }

            return algorithmNo;
        }

        private static void Close()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }
    }

    public static class ConsoleExtensions
    {
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        public static void SetParams(int width, int height)
        {
            Width = width;
            Height = height;
            Console.WindowHeight = height;
            Console.WindowWidth = width;
        }

        public static void PrintColumns(List<string> firstColumnLines, List<string> secondColumnLines, int margin)
        {
            var first = GetData(firstColumnLines);
            var second = GetData(secondColumnLines);

            if (first.Width > Width || second.Width > Width)
            {
                Console.WriteLine("Graphs too big to print in console.");
                return;
            }

            if (first.Width + second.Width + margin > Width)
            {
                foreach (var line in first.Lines)
                {
                    Console.WriteLine(line);
                }
                foreach (var line in second.Lines)
                {
                    Console.WriteLine(line);
                }
                return;
            }

            for (int i = 0; i < first.Lines.Count; i++)
            {
                Console.Write(string.Format($" {{0,{-first.Width}}}", first.Lines[i]));
                Console.Write(new string(' ', margin));
                if (i < second.Lines.Count)
                {
                    Console.Write(second.Lines[i]);
                }
                Console.WriteLine();
            }
            for (int i = first.Lines.Count; i < second.Lines.Count; i++)
            {
                Console.Write(string.Format($" {{0,{-first.Width}}}", string.Empty));
                Console.Write(new string(' ', margin));
                if (i < second.Lines.Count)
                {
                    Console.Write(second.Lines[i]);
                }
                Console.WriteLine();
            }
        }

        private static (int Width, int Height, List<string> Lines) GetData(List<string> lines)
        {
             return (lines.Max(l => l.Count()), lines.Count, lines);
        }
    }
}