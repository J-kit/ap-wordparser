using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace AP.WordParser.Lib.IO
{
    /// <summary>
    /// Analyzes the output of <see cref="StreamSegmentParser"/> by using the <see cref="SegmentAnalyzer"/>
    /// </summary>
    public class StreamSegmentAnalyser : SegmentAnalyzer, IDisposable
    {
        private readonly StreamSegmentParser _parser;

        /// <summary>
        /// Analyzes the output of <see cref="StreamSegmentParser"/> by using the <see cref="SegmentAnalyzer"/>
        /// </summary>
        public StreamSegmentAnalyser(StreamSegmentParser parser)
        {
            _parser = parser;
        }

        /// <summary>
        /// Shortcut for <see cref="StreamSegmentParser"/> and .ctor <see cref="StreamSegmentAnalyser"/>
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        public StreamSegmentAnalyser(Stream stream, Encoding encoding = default)
            : this(new StreamSegmentParser(stream, encoding))
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<SegmentAnalyzerResult> Analyze(CancellationToken token = default)
        {
            return base.Analyze(_parser, token);
        }

        /// <summary>
        /// Disposes the base stream
        /// </summary>
        public void Dispose()
        {
            _parser?.Dispose();
        }
    }
}