using AP.WordParser.Lib.Annotations;

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AP.WordParser.Lib.IO
{
    public class StreamSegmentParserStatus : INotifyPropertyChanged
    {
        private long _currentPosition;
        private long _totalLength = 1;

        public long CurrentPosition
        {
            get => _currentPosition;
            internal set
            {
                if (value == _currentPosition) return;
                _currentPosition = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ParseProgress));
            }
        }

        public long TotalLength
        {
            get => _totalLength;
            internal set
            {
                if (value == _totalLength) return;
                _totalLength = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ParseProgress));
            }
        }

        public decimal ParseProgress => CurrentPosition * 100 / TotalLength;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}