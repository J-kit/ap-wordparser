using System.IO;

namespace AP.WordParser.Gui.Utils
{
    public interface IOService
    {
        Stream OpenFile();
    }
}