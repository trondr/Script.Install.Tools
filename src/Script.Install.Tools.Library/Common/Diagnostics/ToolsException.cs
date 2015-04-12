using System;
using System.Runtime.Serialization;

namespace Script.Install.Tools.Library.Common.Diagnostics
{
    public class ToolsException : Exception
    {
        public ToolsException() : base()
        {
        }

        public ToolsException(string message) : base(message)
        {
        }

        public ToolsException(string message, Exception exception) : base(message, exception)
        {
        }

        protected ToolsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}