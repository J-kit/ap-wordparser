namespace AP.WordParser.Lib.IO
{
    /// <summary>
    /// Informs about the progress of the parsing process
    /// </summary>
    public interface IStreamSegmentParserStatus
    {
        /// <summary>
        /// Represents the Current parse position
        /// </summary>
        long CurrentPosition { get; set; }

        /// <summary>
        /// Total amount of parsing to be done
        /// </summary>
        long TotalLength { get; set; }

        /// <summary>
        /// Represents the urrent parse percentage
        /// </summary>
        decimal ParseProgress { get; }
    }
}