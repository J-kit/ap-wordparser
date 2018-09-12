using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AP.WordParser.Lib.IO
{
    /// <summary>
    /// Reads a stream as string and returns segments wich are delimited by a <see cref="Delimiter"/>
    /// </summary>
    public class StreamSegmentParser : IDisposable
    {
        private BinaryReader _binaryReader;
        private int _cacheSize = 8 * 1024;

        /// <summary>
        /// A set of chars the decoded stream is split by
        /// </summary>
        public List<char> Delimiter { get; set; }

        /// <summary>
        /// If set, this status will be updated when the parsing process progresses
        /// </summary>
        public IStreamSegmentParserStatus StatusObject { get; set; }

        /// <summary>
        /// The size of the cache used to parse the stream
        /// </summary>
        public int CacheSize
        {
            get => _cacheSize;
            set
            {
                if (value < 64)
                {
                    throw new AggregateException($"Cannot use less cache than 64 characters");
                }
                _cacheSize = value;
            }
        }

        /// <summary>
        /// Reads a stream as string and returns segments wich are delimited by a <see cref="Delimiter"/>
        /// </summary>
        /// <param name="stream">The base stream which is read from</param>
        /// <param name="encoding">The used encoding to decode the stream</param>
        public StreamSegmentParser(Stream stream, Encoding encoding = default)
        {
            encoding = encoding ?? Encoding.UTF8;
            _binaryReader = new BinaryReader(stream, encoding);

            Delimiter = new List<char>
            {
                (char) 0x20, //SP - Space
                (char) 0x0D, //CR
                (char) 0x0A, //LF
            };
        }

        /// <summary>
        /// Returns a query that returns string segments which are delimited by the <see cref="Delimiter"/>
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<string> ReadSegments(CancellationToken token = default)
        {
            var readCount = 0;
            var buffer = new char[_cacheSize];
            var sb = new StringBuilder(32);

            if (StatusObject != null)
            {
                StatusObject.TotalLength = _binaryReader.BaseStream.Length;
            }

            while ((readCount = _binaryReader.Read(buffer, 0, buffer.Length)) != 0)
            {
                for (int i = 0; i < readCount; i++)
                {
                    if (Delimiter.Contains(buffer[i]))
                    {
                        if (sb.Length > 0)
                        {
                            yield return sb.ToString();
                            sb.Clear();
                        }
                    }
                    else
                    {
                        sb.Append(buffer[i]);
                    }
                }

                if (StatusObject != null)
                {
                    StatusObject.CurrentPosition = _binaryReader.BaseStream.Position;
                }

                if (token.IsCancellationRequested)
                {
                    yield break;
                }
            }

            if (sb.Length > 0)
            {
                yield return sb.ToString();
            }
        }

        /// <summary>
        /// Enables status updates by creating a <see cref="StreamSegmentParserStatus"/>
        /// object which implements <see cref="INotifyPropertyChanged"/>
        /// </summary>
        /// <returns></returns>
        public StreamSegmentParser EnableStatusUpdates()
        {
            return EnableStatusUpdates<StreamSegmentParserStatus>();
        }

        /// <summary>
        /// Enables status updates by creating a status object
        /// </summary>
        /// <returns></returns>
        public StreamSegmentParser EnableStatusUpdates<T>()
            where T : IStreamSegmentParserStatus, new()
        {
            StatusObject = new T();
            return this;
        }

        /// <summary>
        /// Sets the delimiter which is used to parse the decoded stream
        /// </summary>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public StreamSegmentParser SetDelimiter(params char[] delimiter)
        {
            Delimiter = delimiter.ToList();
            return this;
        }

        public StreamSegmentParser SetCacheSize(int size)
        {
            CacheSize = size;
            return this;
        }

        /// <summary>
        /// Disposes the base stream
        /// </summary>
        public void Dispose()
        {
            _binaryReader?.Dispose();
        }
    }
}