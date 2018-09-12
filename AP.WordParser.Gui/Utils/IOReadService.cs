using Microsoft.Win32;

using System.IO;

namespace AP.WordParser.Gui.Utils
{
    /// <inheritdoc />
    /// Responsible for opening read-only streams on the OS
    // ReSharper disable once InconsistentNaming
    public class IOReadService : IIOService
    {
        /// <summary>
        /// Opens a read-only stream on the OS
        /// </summary>
        /// <returns></returns>
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