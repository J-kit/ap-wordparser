namespace AP.WordParser.Lib
{
    /// <summary>
    /// A set of string and occurance count
    /// </summary>
    public class SegmentAnalyzerResult
    {
        /// <summary>
        /// The target string segment
        /// </summary>
        public string Segment { get; set; }

        /// <summary>
        /// The amount of occurances in the decoded stream
        /// </summary>
        public int Occurance { get; set; }

        internal SegmentAnalyzerResult(string segment, int occurance)
        {
            Segment = segment;
            Occurance = occurance;
        }
    }
}