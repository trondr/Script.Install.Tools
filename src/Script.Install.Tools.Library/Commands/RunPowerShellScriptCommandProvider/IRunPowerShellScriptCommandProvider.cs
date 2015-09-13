namespace Script.Install.Tools.Library.Commands.RunPowerShellScriptCommandProvider
{
    public interface IRunPowerShellScriptCommandProvider
    {
        int RunPowerShellScript(string powerShellScriptFile, string[] arguments, bool runInNativeMode, bool hideArguments);
    }
}
