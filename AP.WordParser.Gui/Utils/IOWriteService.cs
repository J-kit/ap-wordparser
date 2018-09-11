using Microsoft.Win32;

using System.IO;

namespace AP.WordParser.Gui.Utils
{
    public class IOWriteService : IOService
    {
        public Stream OpenFile()
        {
            var dialog = new SaveFileDialog
            {
                //CheckFileExists = true,
                ValidateNames = true,
                InitialDirectory = Directory.GetCurrentDirectory()
            };

            if ((dialog.ShowDialog() ?? false) && !string.IsNullOrEmpty(dialog.FileName))
            {
                return dialog.OpenFile();
            }
            return default;
        }
    }
}