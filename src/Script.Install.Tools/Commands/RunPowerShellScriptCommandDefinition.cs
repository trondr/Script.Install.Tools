﻿using System;
using Common.Logging;
using NCmdLiner.Attributes;
using Script.Install.Tools.Library.Commands.Example;
using Script.Install.Tools.Library.Infrastructure;

namespace Script.Install.Tools.Commands
{
    public class RunPowerShellScriptCommandDefinition: CommandDefinition
    {
        private readonly IRunPowerShellScriptCommandProvider _runPowerShellScriptCommandProvider;
        private readonly ILog _logger;

        public RunPowerShellScriptCommandDefinition(IRunPowerShellScriptCommandProvider runPowerShellScriptCommandProvider, ILog logger)
        {
            _runPowerShellScriptCommandProvider = runPowerShellScriptCommandProvider;
            _logger = logger;
        }

        [Command(Description = "Just an example command. To be deleted or renamed for your own use")]
        public int RunPowerShellScript(
            [RequiredCommandParameter(Description = "PowerShell script file.", AlternativeName = "pf", ExampleValue = @"c:\temp\Install.ps1")]
            string powerShellScriptFile,
            [RequiredCommandParameter(Description = "Arguments to pass to the PowerShell script.", AlternativeName = "a", ExampleValue = new[]{"%1","%2","%3","%4","%5","%6","%7","%8","%9"})]
            string[] arguments,
            [OptionalCommandParameter(Description = "Run in native mode. If true run in 64 bit process on a 64 bit OS. If false run in 32 bit on a 64 bit OS.", AlternativeName = "rn", ExampleValue = true, DefaultValue = true)]
            bool runInNativeMode,
            [OptionalCommandParameter(Description = "Hide command line arguments in logs in case one or more arguments are secrets.", AlternativeName = "ha", ExampleValue = true, DefaultValue = true)]
            bool hideArguments,
            [OptionalCommandParameter(Description = "PowerShell verbose log preference", AlternativeName = "vp", ExampleValue = true, DefaultValue = "SilentlyContinue")]
            string verbosePreference,
            [OptionalCommandParameter(Description = "PowerShell debug log preference", AlternativeName = "dp", ExampleValue = true, DefaultValue = "SilentlyContinue")]
            string debugPreference,
            [OptionalCommandParameter(Description = "PowerShell warning log preference", AlternativeName = "wp", ExampleValue = true, DefaultValue = "Continue")]
            string warningPreference,
            [OptionalCommandParameter(Description = "PowerShell error log preference", AlternativeName = "ep", ExampleValue = true, DefaultValue = "Continue")]
            string errorActionPreference,
            [OptionalCommandParameter(Description = "PowerShell progress log preference", AlternativeName = "pp", ExampleValue = true, DefaultValue = "Continue")]
            string progressPreference
            )
        {
            return _runPowerShellScriptCommandProvider.RunPowerShellScript(powerShellScriptFile, arguments, runInNativeMode, hideArguments, verbosePreference, debugPreference, warningPreference, errorActionPreference, progressPreference);
        }
    }
}