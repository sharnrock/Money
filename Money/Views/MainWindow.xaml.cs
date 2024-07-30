using Prism.Regions;
using System.Windows;

namespace Money.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("Page1Region", typeof(Page1));
            // regionManager.RegisterViewWithRegion("Page2Region", typeof(Page2));
        }
    }
}
