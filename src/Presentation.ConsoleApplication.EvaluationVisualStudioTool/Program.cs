namespace Presentation.ConsoleApplication.EvaluationVisualStudioTool
{
    using Benchmarkers;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================ Starting work ================");

            CalculateMovieRatings.Version4();

            Console.WriteLine("================ Finished! ================");
        }
    }
}
