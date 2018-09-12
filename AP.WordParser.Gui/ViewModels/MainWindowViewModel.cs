using AP.WordParser.Gui.Properties;
using AP.WordParser.Gui.Utils;
using AP.WordParser.Lib;
using AP.WordParser.Lib.IO;

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AP.WordParser.Gui.ViewModels
{
    /// <summary>
    /// Viewmodel of the main window
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///
        /// </summary>
        public MainWindowViewModel()
        {
            // In use-case we'd take some kind of dependency injection but this would take to much time
            // and its not a part of the .NET Framework.
            _fileReadOpenService = new IOReadService();
            _fileWriteService = new IOWriteService();
            _messagingService = new UserMessagingService();
        }

        private IIOService _fileWriteService;
        private IIOService _fileReadOpenService;
        private IUserMessagingService _messagingService;

        private IStreamSegmentParserStatus _segmentParserStatus;
        private ICollection<SegmentAnalyzerResult> _segmentAnalyzerResults;

        private ICommand _expandCommand;
        private ICommand _fileLoadCommand;
        private bool _fileLoading;
        private ICommand _cancelFileLoadingCommand;

        /// <summary>
        /// Loads and analyzes a File
        /// </summary>
        public ICommand FileLoadCommand => _fileLoadCommand ?? (_fileLoadCommand = new RelayCommand(
                                               x => LoadFile(),
                                               x => !FileLoading
                                               ));

        /// <summary>
        /// Cancels an ongoing analyzing progress
        /// </summary>
        public ICommand CancelFileLoadingCommand => _cancelFileLoadingCommand ?? (_cancelFileLoadingCommand = new RelayCommand(
                                                        x => _cancellationTokenSource.Cancel(),
                                                        x => FileLoading
                                                    ));

        /// <summary>
        /// Expands a file to a given size in MB
        /// </summary>
        public ICommand ExpandCommand => _expandCommand ?? (_expandCommand = new RelayCommand(x => ExpandFile(x)));

        private void ExpandFile(object o)
        {
            if (!int.TryParse(o.ToString(), out var param))
            {
                _messagingService.Message($"Invalid command");
                return;
            }

            var requiredBytes = param * 1024 * 1024;

            var inputStream = _fileReadOpenService.OpenFile();
            if (inputStream == null || inputStream.Length > 8 * 1024)
            {
                _messagingService.Message($"Invalid input file selected");
                return;
            }

            var outputStream = _fileWriteService.OpenFile();
            if (outputStream == null)
            {
                _messagingService.Message($"Invalid output file selected");
                return;
            }

            using (outputStream)
            using (inputStream)
            {
                var buffer = new byte[inputStream.Length];
                var actualRead = inputStream.Read(buffer, 0, buffer.Length);
                var requiredLoops = requiredBytes / actualRead;

                for (int i = 0; i < requiredLoops; i++)
                {
                    outputStream.Write(buffer, 0, actualRead);
                }
            }

            _messagingService.Message($"Successfully expanded file to {param} MB");
        }

        /// <summary>
        /// Contains a set of analystic results
        /// </summary>
        public ICollection<SegmentAnalyzerResult> SegmentAnalyzerResults
        {
            get => _segmentAnalyzerResults;
            set
            {
                if (Equals(value, _segmentAnalyzerResults))
                {
                    return;
                }

                _segmentAnalyzerResults = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Contains the current analysing status
        /// </summary>
        public IStreamSegmentParserStatus SegmentParserStatus
        {
            get => _segmentParserStatus;
            set
            {
                if (Equals(value, _segmentParserStatus))
                {
                    return;
                }

                _segmentParserStatus = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Indicates wether a file analystics process is ongoing
        /// </summary>
        public bool FileLoading
        {
            get => _fileLoading;
            private set
            {
                if (value == _fileLoading)
                {
                    return;
                }

                _fileLoading = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FileLoadCommand));
            }
        }

        private CancellationTokenSource _cancellationTokenSource;

        private async void LoadFile()
        {
            var stream = _fileReadOpenService.OpenFile();
            if (stream == null)
            {
                _messagingService.Message($"Invalid file selected");
                return;
            }
            _cancellationTokenSource = new CancellationTokenSource();
            FileLoading = true;
            await Task.Run(() =>
            {
                using (stream)
                using (var parser = new StreamSegmentParser(stream).EnableStatusUpdates())
                using (var analyser = new StreamSegmentAnalyser(parser))
                {
                    SegmentParserStatus = parser.StatusObject;
                    SegmentAnalyzerResults = analyser.Analyze(_cancellationTokenSource.Token)?.ToList();
                }
            });
            FileLoading = false;

            if (_cancellationTokenSource?.Token.IsCancellationRequested ?? false)
            {
                _messagingService.Message($"Successfully canceled parsing");
            }
            else
            {
                _messagingService.Message($"File successfully parsed. {SegmentAnalyzerResults.Count} results");
            }
        }

        /// <summary>
        ///
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}