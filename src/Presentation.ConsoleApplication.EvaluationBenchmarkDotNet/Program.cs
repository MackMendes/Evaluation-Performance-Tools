namespace Presentation.ConsoleApplication.EvaluationBenchmarkDotNet
{
    using System;
    using System.Diagnostics;
    using BenchmarkDotNet.Configs;
    using BenchmarkDotNet.Running;
    using Benchmarkers;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting work!");

            GetDiagnosticsVersion1();
            GetDiagnosticsVersion2();
            GetDiagnosticsVersion3();
            GetDiagnosticsVersion4();

#if DEBUG
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
#endif
            BenchmarkRunner.Run<CalculateMovieRatings>();
            Console.ReadLine();
        }

        private static void GetDiagnosticsVersion1()
        {
            Console.WriteLine("================ VERSION 1  ================");

            var before0 = GC.CollectionCount(0);
            var before1 = GC.CollectionCount(1);
            var before2 = GC.CollectionCount(2);

            var calculateMovie = new CalculateMovieRatings();
            var sw = new Stopwatch();

            sw.Start();

            calculateMovie.Version1();

            sw.Stop();

            Console.WriteLine($"\nTime .: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"# Gen0: {GC.CollectionCount(0) - before0}");
            Console.WriteLine($"# Gen1: {GC.CollectionCount(1) - before1}");
            Console.WriteLine($"# Gen2: {GC.CollectionCount(2) - before2}");
            Console.WriteLine($"Memory: {(Process.GetCurrentProcess().WorkingSet64 / 1024) / 1024} MB");
        }

        private static void GetDiagnosticsVersion2()
        {
            Console.WriteLine("================ VERSION 2  ================");

            var before0 = GC.CollectionCount(0);
            var before1 = GC.CollectionCount(1);
            var before2 = GC.CollectionCount(2);

            var calculateMovie = new CalculateMovieRatings();
            var sw = new Stopwatch();

            sw.Start();

            calculateMovie.Version2();

            sw.Stop();

            Console.WriteLine($"\nTime .: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"# Gen0: {GC.CollectionCount(0) - before0}");
            Console.WriteLine($"# Gen1: {GC.CollectionCount(1) - before1}");
            Console.WriteLine($"# Gen2: {GC.CollectionCount(2) - before2}");
            Console.WriteLine($"Memory: {(Process.GetCurrentProcess().WorkingSet64 / 1024) / 1024} MB");
        }

        private static void GetDiagnosticsVersion3()
        {
            Console.WriteLine("================ VERSION 3  ================");

            var before0 = GC.CollectionCount(0);
            var before1 = GC.CollectionCount(1);
            var before2 = GC.CollectionCount(2);

            var calculateMovie = new CalculateMovieRatings();
            var sw = new Stopwatch();

            sw.Start();

            calculateMovie.Version3();

            sw.Stop();

            Console.WriteLine($"\nTime .: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"# Gen0: {GC.CollectionCount(0) - before0}");
            Console.WriteLine($"# Gen1: {GC.CollectionCount(1) - before1}");
            Console.WriteLine($"# Gen2: {GC.CollectionCount(2) - before2}");
            Console.WriteLine($"Memory: {(Process.GetCurrentProcess().WorkingSet64 / 1024) / 1024} MB");
        }

        private static void GetDiagnosticsVersion4()
        {
            Console.WriteLine("================ VERSION 4  ================");

            var before0 = GC.CollectionCount(0);
            var before1 = GC.CollectionCount(1);
            var before2 = GC.CollectionCount(2);

            var calculateMovie = new CalculateMovieRatings();
            var sw = new Stopwatch();

            sw.Start();

            calculateMovie.Version4();

            sw.Stop();

            Console.WriteLine($"\nTime .: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"# Gen0: {GC.CollectionCount(0) - before0}");
            Console.WriteLine($"# Gen1: {GC.CollectionCount(1) - before1}");
            Console.WriteLine($"# Gen2: {GC.CollectionCount(2) - before2}");
            Console.WriteLine($"Memory: {(Process.GetCurrentProcess().WorkingSet64 / 1024) / 1024} MB");
        }
    }
}
