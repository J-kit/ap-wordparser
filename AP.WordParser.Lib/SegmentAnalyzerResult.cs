namespace AP.WordParser.Lib
{
    public class SegmentAnalyzerResult
    {
        public string Segment { get; set; }
        public int Occurance { get; set; }

        public SegmentAnalyzerResult(string segment, int occurance)
        {
            Segment = segment;
            Occurance = occurance;
        }
    }
}