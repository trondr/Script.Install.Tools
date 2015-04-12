namespace Script.Install.Tools.Library.Commands.Example
{
    public interface IRunPowerShellScriptCommandProvider
    {
        int RunPowerShellScript(string powerShellScriptFile, string[] arguments, bool runInNativeMode, bool hideArguments, string verbosePreference, string debugPreference, string warningPreference, string errorActionPreference, string progressPreference);
    }
}
