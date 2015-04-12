using System.Windows;
using Script.Install.Tools.Library.Views;

namespace Script.Install.Tools.Library.Common.UI
{
    public abstract class ViewModelBase : DependencyObject
    {
        public MainWindow MainWindow { get; set; }
    }
}
