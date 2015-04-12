using Script.Install.Tools.Library.Common.UI;
using Script.Install.Tools.Library.ViewModels;

namespace Script.Install.Tools.Library.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : ViewBase
    {
        public MainView(MainViewModel viewModel)
        {
            this.ViewModel = viewModel;
            InitializeComponent();
        }
    }
}
