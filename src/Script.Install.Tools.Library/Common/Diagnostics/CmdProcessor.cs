using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Text;
using Common.Logging;

namespace Script.Install.Tools.Library.Common.Diagnostics
{
    public class CmdProcessor : ICmdProcessor
    {
        private readonly ILog _logger;
        private bool _executing;
        private Process _process;
        private StreamReader _standardOutput;
        private StreamReader _standardError;
        private AsyncCallback _outputReady;
        private CmdProcessorAsyncState _outputState;
        private AsyncCallback _errorReady;
        private CmdProcessorAsyncState _errorState;
        private readonly byte[] _errorBuffer = new byte[512];
        private readonly byte[] _outputBuffer= new byte[512];
        private readonly object _syncObject = new object();

        public CmdProcessor(ILog logger)
        {
            _logger = logger;
        }

        public void Execute(string command, string arguments, string workingDirectory, bool waitForExit)
        {
            if(_executing)
            {
                throw new ToolsException("Allready executing process.");
            }
            _executing = true;
            _process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            if(!string.IsNullOrEmpty(workingDirectory) && Directory.Exists(workingDirectory))
            {
                _process.StartInfo.WorkingDirectory = workingDirectory;
            }
            _process.EnableRaisingEvents = true;
            _process.Exited += ProcessOnExited;
            _process.Start();
            _standardOutput = _process.StandardOutput;
            _standardError = _process.StandardError;
            _outputReady = StandardOutputCallback;
            _outputState = new CmdProcessorAsyncState(_standardOutput, _outputBuffer);
            _errorReady = StandardErrorCallback;
            _errorState = new CmdProcessorAsyncState(_standardError, _errorBuffer);

            // read the streams asynchronously so  control will return to the caller
            _standardOutput.BaseStream.BeginRead(_outputBuffer, 0, _outputBuffer.Length, _outputReady, _outputState);
            _standardError.BaseStream.BeginRead(_errorBuffer, 0, _errorBuffer.Length, _errorReady, _errorState);

            if(waitForExit)
            {
                _process.WaitForExit();                
            }
        }

        private void StandardErrorCallback(IAsyncResult ar)
        {
            var state = ar.AsyncState as CmdProcessorAsyncState;
            if(state != null)
            {
                var count = state.Stream.BaseStream.EndRead(ar);
                if(count > 0)
                {
                    var text = Encoding.Default.GetString(state.Buffer, 0, count);
                    EventsHelper.Fire<CmdProcessorEventArgs>(StandardErrorReceived, this, new CmdProcessorEventArgs(text, 0));
                    state.Stream.BaseStream.BeginRead(state.Buffer,0,state.Buffer.Length,_outputReady,state);

                }
            }
        }

        private void StandardOutputCallback(IAsyncResult ar)
        {
            var state = ar.AsyncState as CmdProcessorAsyncState;
            if(state != null)
            {
                var count = state.Stream.BaseStream.EndRead(ar);
                if(count > 0)
                {
                    var text = Encoding.Default.GetString(state.Buffer, 0, count);
                    EventsHelper.Fire<CmdProcessorEventArgs>(StandardOutReceived, this, new CmdProcessorEventArgs(text, 0));
                    state.Stream.BaseStream.BeginRead(state.Buffer,0,state.Buffer.Length,_outputReady,state);
                }
            }
        }

        private void ProcessOnExited(object sender, EventArgs eventArgs)
        {            
            if(_process == null) return;
            _process.EnableRaisingEvents = false;
            ExitCode = _process.ExitCode;
            EventsHelper.Fire(ProcessExited, this, new CmdProcessorEventArgs("Exited with exit code: " + ExitCode, ExitCode));
            if(_process != null)
            {
                lock (_syncObject)
                {
                    if(_process != null)
                    {                        
                        _process.Dispose();
                        _process = null;
                        _executing = false;
                    }
                }                
            }            
        }

        public event EventHandler<CmdProcessorEventArgs> StandardOutReceived;
        public event EventHandler<CmdProcessorEventArgs> StandardErrorReceived;
        public event EventHandler<CmdProcessorEventArgs> ProcessExited;
        public int ExitCode { get; set; }
    }
}