using System;
using System.IO;

namespace Script.Install.Tools.Library.Common.Diagnostics
{
    internal class CmdProcessorAsyncState
    {
        public CmdProcessorAsyncState(StreamReader stream, byte[] buffer)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (buffer == null) throw new ArgumentNullException("buffer");
            _stream = stream;
            _buffer = buffer;
        }

        public StreamReader Stream
        {
            get { return _stream; }
        }

        public byte[] Buffer
        {
            get { return _buffer; }
        }

        private readonly StreamReader _stream;
        private readonly byte[] _buffer;
    }
}