using Prism.Mvvm;

namespace ModuleA.ViewModels
{
    public class BudgetCategoryEntryViewModel : BindableBase
    {
        private string _categoryName = "Category Name";
        public string CategoryName
        {
            get { return _categoryName; }
            set { SetProperty(ref _categoryName, value); }
        }

        private string _amount = "0.00";
        public string Amount
        {
            get { return _amount; }
            set
            {
                if (SetProperty(ref _amount, value))
                {
                    RaisePropertyChanged(nameof(ParsedAmount));
                }
            }
        }

        public decimal ParsedAmount
        {
            get
            {
                return decimal.TryParse(_amount, out var parsedAmount) ? parsedAmount : 0.00m;
            }
        }
    }
}
