using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AP.WordParser.Lib.IO
{
    public class StreamSegmentAnalyser : SegmentAnalyzer, IDisposable
    {
        private readonly StreamSegmentParser _parser;

        public StreamSegmentAnalyser(StreamSegmentParser parser)
        {
            _parser = parser;
        }

        public StreamSegmentAnalyser(Stream stream, Encoding encoding = default)
            : this(new StreamSegmentParser(stream, encoding))
        {
        }

        public IEnumerable<SegmentAnalyzerResult> Analyze()
        {
            return Analyze(_parser);
        }

        public void Dispose()
        {
            _parser?.Dispose();
        }
    }
}