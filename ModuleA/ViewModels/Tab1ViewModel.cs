using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using ModuleA.Data;

namespace ModuleA.ViewModels
{
    public class Tab1ViewModel : BindableBase
    {
        private readonly BudgetDbContext _dbContext;

        public Tab1ViewModel()
        {
            _dbContext = BudgetDbContextFactory.Create();

            BudgetCategories = new ObservableCollection<BudgetCategoryEntryViewModel>();
            BudgetCategories.CollectionChanged += BudgetCategories_CollectionChanged;

            AddCommand = new DelegateCommand(OnAdd);
            SaveCommand = new DelegateCommand(OnSave);

            LoadData();
        }

        private string _monthlyIncome;
        public string MonthlyIncome
        {
            get { return _monthlyIncome; }
            set
            {
                if (SetProperty(ref _monthlyIncome, value))
                {
                    CalculateTotals();
                }
            }
        }

        private decimal _totalBudget;
        public decimal TotalBudget
        {
            get { return _totalBudget; }
            private set { SetProperty(ref _totalBudget, value); }
        }

        private decimal _finalAmount;
        public decimal FinalAmount
        {
            get { return _finalAmount; }
            private set { SetProperty(ref _finalAmount, value); }
        }

        public ObservableCollection<BudgetCategoryEntryViewModel> BudgetCategories { get; private set; }
        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }

        private void LoadData()
        {
            _dbContext.Database.EnsureCreated();  // Ensure database is created

            var income = _dbContext.MonthlyIncomes.FirstOrDefault();
            if (income != null)
            {
                MonthlyIncome = income.Income.ToString();
            }

            var entries = _dbContext.BudgetCategoryEntries.ToList();
            foreach (var entry in entries)
            {
                var viewModel = new BudgetCategoryEntryViewModel
                {
                    Id = entry.Id,
                    CategoryName = entry.CategoryName,
                    Amount = entry.Amount.ToString()
                };
                viewModel.PropertyChanged += BudgetCategoryEntry_PropertyChanged;
                BudgetCategories.Add(viewModel);
            }

            CalculateTotals();
        }

        private void OnAdd()
        {
            var newEntry = new BudgetCategoryEntryViewModel();
            newEntry.PropertyChanged += BudgetCategoryEntry_PropertyChanged;
            BudgetCategories.Add(newEntry);
            CalculateTotals();
        }

        private void OnSave()
        {
            var income = _dbContext.MonthlyIncomes.FirstOrDefault();
            if (income == null)
            {
                income = new MonthlyIncome();
                _dbContext.MonthlyIncomes.Add(income);
            }
            income.Income = decimal.Parse(MonthlyIncome);

            foreach (var entry in BudgetCategories)
            {
                var entity = _dbContext.BudgetCategoryEntries.FirstOrDefault(e => e.Id == entry.Id);
                if (entity == null)
                {
                    entity = new BudgetCategoryEntry();
                    _dbContext.BudgetCategoryEntries.Add(entity);
                }
                entity.CategoryName = entry.CategoryName;
                entity.Amount = entry.ParsedAmount;
            }

            _dbContext.SaveChanges();
        }

        private void BudgetCategories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (BudgetCategoryEntryViewModel entry in e.NewItems)
                {
                    entry.PropertyChanged += BudgetCategoryEntry_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (BudgetCategoryEntryViewModel entry in e.OldItems)
                {
                    entry.PropertyChanged -= BudgetCategoryEntry_PropertyChanged;
                }
            }

            CalculateTotals();
        }

        private void BudgetCategoryEntry_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BudgetCategoryEntryViewModel.Amount) || e.PropertyName == nameof(BudgetCategoryEntryViewModel.ParsedAmount))
            {
                CalculateTotals();
            }
        }

        private void CalculateTotals()
        {
            TotalBudget = BudgetCategories.Sum(x => x.ParsedAmount);
            if (decimal.TryParse(MonthlyIncome, out var income))
            {
                FinalAmount = income - TotalBudget;
            }
            else
            {
                FinalAmount = -TotalBudget;
            }
        }
    }
}
