using Dluznicek.Classes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dluznicek
{
    public class AddOutlayPageViewModel : INotifyPropertyChanged
    {
        public DateTime? SelectedDate { get; set; }
        public string SelectedName { get; set; }
        public string SelectedPrice { get; set; }

        public Debt SelectedDebt { get; set; }
        public Category SelectedCategory { get; set; }

        public ObservableCollection<Debt> Debts { get; set; } = App.DBcache.Debts;
        public ObservableCollection<Category> Categories { get; set; } = App.DBcache.Categories;


        public AddOutlayPageViewModel()
        {
            ClickCommand = new RelayCommand(AddDebt);

            /*Start Data*/
            SelectedDate = DateTime.Today;
            DefaultData();
        }

        public void DefaultData()
        {
            SelectedCategory = new Category() { Id = 0 };
            SelectedDebt = new Debt() { Id = 0 };
            SelectedName = "";
            SelectedPrice = "";

            OnPropertyChanged("SelectedCategory");
            OnPropertyChanged("SelectedDebt");
            OnPropertyChanged("SelectedName");
            OnPropertyChanged("SelectedPrice");
        }

        // Command
        public RelayCommand ClickCommand { get; }

        // Command function
        private void AddDebt()
        {
            if (Check_Inputs())
            {
                var outlay = new Outlay
                {
                    Date = (DateTime)SelectedDate,
                    Name = SelectedName,
                    Price = int.Parse(SelectedPrice),
                    Category = SelectedCategory,
                    CategoryId = SelectedCategory.Id,
                    DebtId = SelectedDebt.Id,
                };
                App.DBcache.AddOutlay(outlay);
                DefaultData();
            }
        }

        private bool Check_Inputs()
        {
            if (string.IsNullOrEmpty(SelectedDate.ToString())) return false;
            if (string.IsNullOrEmpty(SelectedName)) return false;
            if (string.IsNullOrEmpty(SelectedPrice)) return false;
            if (!int.TryParse(SelectedPrice, out int ParsedInt)) return false;
            return true;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
