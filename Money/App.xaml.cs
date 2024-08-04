using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Money.ViewModels;
using Money.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace Money
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindowView>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleA.ModuleAModule>();
        }
    }
}
