﻿using AP.WordParser.Lib.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.Linq;
using System.Text;

namespace AP.WordParser.Tests
{
    [TestClass]
    public class StreamSegmentAnalyserTest
    {
        [TestMethod]
        public void TestStreamSegmentAnalyser()
        {
            using (var fs = GetStream())
            using (var parser = new StreamSegmentParser(fs).SetDelimiter(',').SetCacheSize(64))
            using (var analyser = new StreamSegmentAnalyser(parser))
            {
                var segments = parser.ReadSegments().ToList();
                var seqEqual = segments.SequenceEqual("abcdef".ToCharArray().Select(x => x.ToString()));
                Assert.IsTrue(seqEqual);
            }

            Stream GetStream()
            {
                var raw = "a,b,c,d,e,f";
                var encoded = Encoding.UTF8.GetBytes(raw);
                return new MemoryStream(encoded);
            }
        }
    }
}