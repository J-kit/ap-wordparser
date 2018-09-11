using AP.WordParser.Lib.IO;

using System.Collections.Generic;
using System.Linq;

namespace AP.WordParser.Lib
{
    public class SegmentAnalyzer
    {
        public IEnumerable<SegmentAnalyzerResult> Analyze(StreamSegmentParser parser)
        {
            return Analyze(parser.ReadSegments());
        }

        public IEnumerable<SegmentAnalyzerResult> Analyze(IEnumerable<string> segments)
        {
            var analysisDictionary = new Dictionary<string, IntWrapper>();
            foreach (var segment in segments)
            {
                if (analysisDictionary.TryGetValue(segment, out var value))
                {
                    value.Increment();
                }
                else
                {
                    analysisDictionary[segment] = 1;
                }
            }
            return analysisDictionary.OrderByDescending(x => x.Value.Value).Select(x => new SegmentAnalyzerResult(x.Key, x.Value));
        }

        private protected virtual void OnStatusChangd()
        {
        }

        /// <summary>
        /// Because int is a ValueType, it cannot be accessed by ref by default.
        /// To bypass this limitation we need a wrapper.
        /// Without this helper class we would have to access the dictionary twice for every found key
        /// </summary>
        private class IntWrapper
        {
            private IntWrapper(int value)
            {
                Value = value;
            }

            public int Value { get; set; }

            public void Increment()
            {
                Value++;
            }

            public static implicit operator int(IntWrapper i) => i.Value;

            public static implicit operator IntWrapper(int i) => new IntWrapper(i);
        }
    }
}