using System.IO;

namespace AP.WordParser.Gui.Utils
{
    /// <summary>
    /// Responsible for file interactions with the OS
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public interface IIOService
    {
        /// <summary>
        /// Opens a stream for a file
        /// </summary>
        /// <returns></returns>
        Stream OpenFile();
    }
}