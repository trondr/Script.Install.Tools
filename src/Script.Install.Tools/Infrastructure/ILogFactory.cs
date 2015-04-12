using System;
using Common.Logging;

namespace Script.Install.Tools.Infrastructure
{
    public interface ILogFactory
    {
        ILog GetLogger(Type type);
    }
}