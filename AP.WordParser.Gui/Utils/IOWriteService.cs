using Microsoft.Win32;

using System.IO;

namespace AP.WordParser.Gui.Utils
{
    /// <summary>
    /// Responsible for opening writable streams on the OS
    /// </summary>
    public class IOWriteService : IIOService
    {
        /// <summary>
        /// Opens a writable stream on the OS
        /// </summary>
        /// <returns></returns>
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