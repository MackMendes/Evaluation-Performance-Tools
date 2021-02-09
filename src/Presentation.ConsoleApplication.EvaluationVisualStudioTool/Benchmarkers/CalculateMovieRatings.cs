namespace Presentation.ConsoleApplication.EvaluationVisualStudioTool.Benchmarkers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public static class CalculateMovieRatings
    {
        private static readonly string filePath = @"E:\Developments\Github\Evaluation-Performance-Tools\dataset\ratings-100MB.csv";

        public static void Version1()
        {
            var lines = File.ReadAllLines(filePath);
            var sum = 0d;
            var count = 0;
            
            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts[1] == "223")
                {
                    sum += double.Parse(parts[2], CultureInfo.InvariantCulture);
                    count++;
                }
            }

            Console.WriteLine($"The average is {sum / count}.");
        }

        public static void Version2()
        {
            var sum = 0d;
            var count = 0;
            string line;

            using (var fs = File.OpenRead(filePath))
            using (var reader = new StreamReader(fs))
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');

                    if (parts[1] == "223")
                    {
                        sum += double.Parse(parts[2], CultureInfo.InvariantCulture);
                        count++;
                    }
                }

            Console.WriteLine($"The average is {sum / count}.");
        }

        public static void Version3()
        {
            var sum = 0d;
            var count = 0;
            string line;

            // Braveheart id movie id as span;
            var lookingFor = "223".AsSpan();

            using var fs = File.OpenRead(filePath);
            using var reader = new StreamReader(fs);
            while ((line = reader.ReadLine()) != null)
            {
                // ignoring the voter id
                var span = line.AsSpan(line.IndexOf(',') + 1);

                // movieId
                var firstCommaPos = span.IndexOf(',');
                var movieId = span.Slice(0, firstCommaPos);
                if (!movieId.SequenceEqual(lookingFor)) continue;

                // rating
                span = span.Slice(firstCommaPos + 1);
                firstCommaPos = span.IndexOf(',');
                var rating = double.Parse(span.Slice(0, firstCommaPos), provider: CultureInfo.InvariantCulture);

                sum += rating;
                count++;
            }

            Console.WriteLine($"The average is {sum / count}.");
        }

        public static void Version4()
        {
            var sum = 0d;
            var count = 0;

            var lookingFor = Encoding.UTF8.GetBytes("223").AsSpan();
            var rawBuffer = new byte[1024 * 1024];
            using (var fs = File.OpenRead(filePath))
            {
                var bytesBuffered = 0;
                var bytesConsumed = 0;

                while (true)
                {
                    var bytesRead = fs.Read(rawBuffer, bytesBuffered, rawBuffer.Length - bytesBuffered);

                    if (bytesRead == 0) break;
                    bytesBuffered += bytesRead;

                    int linePosition;

                    do
                    {
                        linePosition = Array.IndexOf(rawBuffer, (byte)'\n', bytesConsumed,
                            bytesBuffered - bytesConsumed);

                        if (linePosition >= 0)
                        {
                            var lineLength = linePosition - bytesConsumed;
                            var line = new Span<byte>(rawBuffer, bytesConsumed, lineLength);
                            bytesConsumed += lineLength + 1;


                            // ignoring the voter id
                            var span = line.Slice(line.IndexOf((byte)',') + 1);

                            // movieId
                            var firstCommaPos = span.IndexOf((byte)',');
                            var movieId = span.Slice(0, firstCommaPos);
                            if (!movieId.SequenceEqual(lookingFor)) continue;

                            // rating
                            span = span.Slice(firstCommaPos + 1);
                            firstCommaPos = span.IndexOf((byte)',');
                            var rating = double.Parse(Encoding.UTF8.GetString(span.Slice(0, firstCommaPos)), provider: CultureInfo.InvariantCulture);

                            sum += rating;
                            count++;
                        }

                    } while (linePosition >= 0);

                    Array.Copy(rawBuffer, bytesConsumed, rawBuffer, 0, (bytesBuffered - bytesConsumed));
                    bytesBuffered -= bytesConsumed;
                    bytesConsumed = 0;
                }
            }

            Console.WriteLine($"The average is {sum / count}.");
        }
    }
}
