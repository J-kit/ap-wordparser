using AP.WordParser.Lib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Linq;

namespace AP.WordParser.Tests
{
    [TestClass]
    public class SegmentAnalyserTests
    {
        [TestMethod]
        public void TestSegmentAnalyser()
        {
            var analyser = new SegmentAnalyzer();
            var results = analyser.Analyze(GetSegments()).ToList();

            var abc = results.FirstOrDefault(x => x.Segment == "abc");
            var def = results.FirstOrDefault(x => x.Segment == "def");

            Assert.IsNotNull(abc);
            Assert.IsNotNull(def);
            Assert.AreEqual(20, abc.Occurance);
            Assert.AreEqual(15, def.Occurance);

            IEnumerable<string> GetSegments()
            {
                for (int i = 0; i < 20; i++)
                {
                    yield return "abc";
                }

                for (int i = 0; i < 15; i++)
                {
                    yield return "def";
                }
            }
        }
    }
}