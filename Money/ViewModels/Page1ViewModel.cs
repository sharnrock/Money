using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Money.ViewModels
{
    public class Page1ViewModel : BindableBase
    {
        private double _yearlyIncome;
        public double YearlyIncome
        {
            get { return _yearlyIncome; }
            set { SetProperty(ref _yearlyIncome, value); }
        }

        private ObservableCollection<BudgetCategory> _budgetCategories;
        public ObservableCollection<BudgetCategory> BudgetCategories
        {
            get { return _budgetCategories; }
            set { SetProperty(ref _budgetCategories, value); }
        }

        public ICommand AddCategoryCommand { get; private set; }

        public Page1ViewModel()
        {
            BudgetCategories = new ObservableCollection<BudgetCategory>();
            AddCategoryCommand = new DelegateCommand(AddCategory);
        }

        private void AddCategory()
        {
            BudgetCategories.Add(new BudgetCategory());
        }
    }

    public class BudgetCategory : BindableBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }
    }
}
