using AP.WordParser.Lib.IO;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AP.WordParser
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var testFile = $"NewFile-4.txt";
            BlowFile(38836, $"Sample.txt", testFile);
            using (var fileStream = File.OpenRead(testFile))
            {
                var analyser = new StreamSegmentAnalyser(fileStream);

                var sw = Stopwatch.StartNew();
                var analyzeResult = analyser.Analyze().ToList();
                sw.Stop();
                Console.WriteLine(sw.Elapsed.TotalMilliseconds);
                Debugger.Break();
            }
        }

        //BlowFile(10000, $"Sample.txt", testFile);

        public static void BlowFile(int iterations, string srcLoc, string dstLoc)
        {
            var content = File.ReadAllBytes(srcLoc);

            using (var sw = File.OpenWrite(dstLoc))
            {
                for (int i = 0; i < iterations; i++)
                {
                    sw.Write(content, 0, content.Length);
                }
            }
        }

        private static void PerformanceTest()
        {
            var count = 0;
            var sw = Stopwatch.StartNew();

            using (var fileStream = File.OpenRead($"Sample.txt"))
            {
                var segments = new StreamSegmentParser(fileStream).ReadSegments();

                foreach (var segment in segments)
                {
                    //Console.WriteLine(segment);
                    count++;
                    if (count % 1000000 == 0)
                    {
                        var elapsed = sw.Elapsed.TotalMilliseconds;
                        var wps = count / elapsed;
                        var bpms = fileStream.Position / elapsed;

                        Console.WriteLine($"Words: {count} ({wps} words/ms - {bpms} bytes/ms) ");
                    }
                }

                Debugger.Break();
            }
        }
    }
}