using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Money.Views;
using Money.ViewModels;

namespace Money
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Page1, Page1ViewModel>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            // Additional initialization code can be placed here
        }
    }
}
