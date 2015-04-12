using System.Reflection;

namespace Script.Install.Tools.Library.Common.Install
{
    public interface IWindowsExplorerContextMenuInstaller
    {
        void Install(string commandId, string commandName, string command, string arguments);

        void UnInstall(string commandId);
    }
}
