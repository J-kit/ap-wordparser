using AP.WordParser.Lib.Annotations;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AP.WordParser.Lib.IO
{
    /// <summary>
    /// Raises status updates about the parsing progress
    /// </summary>
    public class StreamSegmentParserStatus : INotifyPropertyChanged, IStreamSegmentParserStatus
    {
        private long _currentPosition;
        private long _totalLength = 1;

        /// <inheritdoc />
        public long CurrentPosition
        {
            get => _currentPosition;
            set
            {
                if (value == _currentPosition)
                {
                    return;
                }

                _currentPosition = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ParseProgress));
            }
        }

        /// <inheritdoc />
        public long TotalLength
        {
            get => _totalLength;
            set
            {
                if (value == _totalLength)
                {
                    return;
                }

                _totalLength = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ParseProgress));
            }
        }

        /// <inheritdoc />
        public decimal ParseProgress => CurrentPosition * 100 / TotalLength;

        /// <inheritdoc />
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