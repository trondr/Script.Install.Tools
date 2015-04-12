using System.Collections;
using System.ComponentModel;
using System.Reflection;
using Script.Install.Tools.Commands;
using Script.Install.Tools.Library.Common.Install;

namespace Script.Install.Tools
{
    [RunInstaller(true)]
    public partial class CustomInstaller : System.Configuration.Install.Installer
    {
        public CustomInstaller()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            //Example: Adding a command to windows explorer contect menu
            //this.Context.LogMessage("Adding Script.Install.Tools to File Explorer context menu...");
            //new WindowsExplorerContextMenuInstaller().Install("Script.Install.Tools", "Create Something...", Assembly.GetExecutingAssembly().Location, "CreateSomething /exampleParameter=\"%1\"");
            //this.Context.LogMessage("Finnished adding Script.Install.Tools to File Explorer context menu.");
            
            base.Install(stateSaver);
        }

        public override void Uninstall(IDictionary savedState)
        {
            //Example: Removing previously installed command from windows explorer contect menu
            //this.Context.LogMessage("Removing Script.Install.Tools from File Explorer context menu...");
            //new WindowsExplorerContextMenuInstaller().UnInstall("Script.Install.Tools");
            //this.Context.LogMessage("Finished removing Script.Install.Tools from File Explorer context menu.");
            
            base.Uninstall(savedState);
        }        
    }
}
