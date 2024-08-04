using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Tab1View>();
            containerRegistry.RegisterForNavigation<Tab2View>();
            containerRegistry.RegisterForNavigation<BudgetCategoryEntry>();
        }
    }
}
