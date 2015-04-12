//Credits: http://odetocode.com/Articles/97.aspx
//trondr: Added support for separate handling of stdout and stderr
//trondr: Added support for wait for exit
using System;

namespace Script.Install.Tools.Library.Common.Diagnostics
{
    public interface ICmdProcessor
    {
        void Execute(string command, string arguments, string workingDirectory, bool waitForExit);
        event EventHandler<CmdProcessorEventArgs> StandardOutReceived;
        event EventHandler<CmdProcessorEventArgs> StandardErrorReceived;
        event EventHandler<CmdProcessorEventArgs> ProcessExited;
        int ExitCode { get; set; }
    }
}
