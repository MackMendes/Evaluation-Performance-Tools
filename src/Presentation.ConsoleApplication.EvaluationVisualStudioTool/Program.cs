namespace Presentation.ConsoleApplication.EvaluationVisualStudioTool
{
    using System;
    using System.Diagnostics;
    using Presentation.ConsoleApplication.EvaluationVisualStudioTool.Benchmarkers;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================ Starting work ================");

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
    }
}
