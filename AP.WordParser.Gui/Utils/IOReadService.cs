using Microsoft.Win32;

using System.IO;

namespace AP.WordParser.Gui.Utils
{
    public class IOReadService : IOService
    {
        public Stream OpenFile()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                CheckFileExists = true,
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