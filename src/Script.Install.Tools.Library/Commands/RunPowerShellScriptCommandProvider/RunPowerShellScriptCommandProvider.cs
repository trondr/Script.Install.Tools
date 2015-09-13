using System;
using System.IO;
using System.Text;
using Common.Logging;
using Script.Install.Tools.Library.Common.Diagnostics;
using Script.Install.Tools.Library.Infrastructure;
using Script.Install.Tools.Library.Views;

namespace Script.Install.Tools.Library.Commands.RunPowerShellScriptCommandProvider
{
    public class RunPowerShellScriptCommandProvider : CommandProvider, IRunPowerShellScriptCommandProvider
    {
        private readonly MainWindow _mainWindow;
        private readonly ICmdProcessor _cmdProcessor;
        private readonly ILog _logger;

        public RunPowerShellScriptCommandProvider(MainWindow mainWindow, ICmdProcessor cmdProcessor,ILog logger)
        {
            _mainWindow = mainWindow;
            _cmdProcessor = cmdProcessor;
            _cmdProcessor.ProcessExited += CmdProcessorProcessExited;
            _cmdProcessor.StandardErrorReceived += CmdProcessorStandardErrorReceived;
            _cmdProcessor.StandardOutReceived += CmdProcessorStandardOutReceived;
            _logger = logger;
        }

        void CmdProcessorStandardOutReceived(object sender, CmdProcessorEventArgs e)
        {
            _logger.Info(e.Text.TrimEnd());
        }

        void CmdProcessorStandardErrorReceived(object sender, CmdProcessorEventArgs e)
        {
            _logger.Error(e.Text.TrimEnd());
        }

        void CmdProcessorProcessExited(object sender, CmdProcessorEventArgs e)
        {
            _logger.Info(e.Text.TrimEnd());
        }

        public int RunPowerShellScript(string powerShellScriptFile, string[] arguments, bool runInNativeMode, bool hideArguments)
        {
            
            var powerShellArguments = new StringBuilder();
            powerShellArguments.AppendFormat("-Command \"& {{ ");
            powerShellArguments.AppendFormat(" . \\\"{0}\\\" ", powerShellScriptFile);
            foreach (var argument in arguments)
            {
                if(!string.IsNullOrEmpty(argument))
                { 
                    powerShellArguments.AppendFormat("\"{0}\" ", argument);
                }
            }
            powerShellArguments.Append("; exit $LASTEXITCODE");
            powerShellArguments.Append("}\"");
            var directoryInfo = new FileInfo(powerShellScriptFile).Directory;
            if (directoryInfo != null)
            {
                var powerShellExe = GetPowerShellExe(runInNativeMode);                
                _logger.InfoFormat("Executing: \"{0}\" {1}", powerShellExe, hideArguments? "***hidden***arguments***" : powerShellArguments.ToString());
                _cmdProcessor.Execute(powerShellExe, powerShellArguments.ToString(), directoryInfo.FullName, true);
                _logger.InfoFormat("Finished executing: \"{0}\" {1}", powerShellExe, hideArguments? "***hidden***arguments***" : powerShellArguments.ToString());
                return _cmdProcessor.ExitCode;
            }
            _logger.ErrorFormat("Could not derive parent directory from PowerShell script file '{0}'", powerShellScriptFile);
            return 1;
        }

        private string GetPowerShellExe(bool runInNativeMode)
        {
            var systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.System);
            if(!runInNativeMode && IntPtr.Size == 8)
            {
                systemFolder = Environment.GetFolderPath(Environment.SpecialFolder.SystemX86);
            }
            return Path.Combine(systemFolder, "WindowsPowershell", "v1.0", "PowerShell.exe");            
        }
    }
}