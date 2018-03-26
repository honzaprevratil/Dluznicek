using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dluznicek.Classes
{
    public class DBcache
    {
        private List<Outlay> ALLOutlays = new List<Outlay>();
        private List<Category> ALLCategories = new List<Category>();
        private List<Debt> ALLDebts = new List<Debt>();
        private DBhelper DBhelper = new DBhelper();

        public ObservableCollection<Category> Categories = new ObservableCollection<Category>();
        public ObservableCollection<Outlay> Outlays = new ObservableCollection<Outlay>();
        public ObservableCollection<Debt> Debts = new ObservableCollection<Debt>();
        public int TotalMoneySpent;

        public DBcache()
        {
            GetAllData();
            int notPaidDebtsInTime = CheckAllDebts();

            if (notPaidDebtsInTime > 0)
            {
                var result = MessageBox.Show("Some debts (" + notPaidDebtsInTime + ") were not paid in time.", "Not paid Debts", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void GetAllData()
        {
            ALLOutlays = DBhelper.GetAllWithChildren<Outlay>();
            ALLCategories = DBhelper.GetAllWithChildren<Category>();
            ALLDebts = DBhelper.GetAllWithChildren<Debt>();
            RefreshAll();
        }
        public void RefreshAll()
        {
            RefreshOutlays();
            RefreshCategories();
            RefreshDebts();
        }
        public void RefreshOutlays()
        {
            Outlays.Clear();
            TotalMoneySpent = 0;

            foreach (Outlay item in ALLOutlays)
            {
                TotalMoneySpent += item.Price;
                Outlays.Add(item);
            }
        }
        public void RefreshCategories()
        {
            Categories.Clear();
            foreach (Category item in ALLCategories)
            {
                Categories.Add(item);
            }
        }
        public void RefreshDebts()
        {
            Debts.Clear();
            foreach (Debt item in ALLDebts)
            {
                Debts.Add(item);
            }
            CheckAllDebts();
        }
        public int CheckAllDebts()
        {
            int notPaidDebtsInTime = 0;

            foreach (Debt debt in ALLDebts)
            {
                DateTime date1 = DateTime.Today;
                DateTime date2 = debt.ReturnDate;
                int result = DateTime.Compare(date1, date2);
                int months = 0;

                if (result == 1)
                {
                    int days;

                    while (true)
                    {
                        days = date1.Subtract(date2.AddMonths(months)).Days;
                        if (days > 0)
                        {
                            months++;
                        }
                        else
                            break;
                    }
                }

                if (months > 0)
                {
                    notPaidDebtsInTime++;
                    debt.Interest = debt.InterestRate * months;
                }
            }

            return notPaidDebtsInTime;
        }

        public void SelectOutlaysByDate(int year, int month, int day, DateType dateType)
        {
            DateTime SelectDate2;
            DateTime SelectDate = new DateTime(year, month, day);

            switch (dateType)
            {
                case DateType.month:
                    SelectDate2 = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                    break;

                case DateType.year:
                    SelectDate2 = new DateTime(year, 12, 31);
                    break;

                default:
                    SelectDate2 = new DateTime(year, 12, 31);
                    break;
            }

            var classesQuery = Outlays.Where(outlay => outlay.Date >= SelectDate && outlay.Date <= SelectDate2).ToList();
            Outlays.Clear();

            TotalMoneySpent = 0;
            foreach (var item in classesQuery)
            {
                this.Outlays.Add(item);
                TotalMoneySpent += item.Price;
            }
        }
        public void SelectOutlaysByCategory(Category category)
        {
            Outlays.Clear();
            var categoryChildren = ALLOutlays.Where(outlay => outlay.CategoryId == category.Id);

            TotalMoneySpent = 0;
            foreach (var item in categoryChildren)
            {
                this.Outlays.Add(item);
                TotalMoneySpent += item.Price;
            }
        }
        public void SelectOutlaysByDebt(Debt debt)
        {
            Outlays.Clear();
            var debtChildren = ALLOutlays.Where(outlay => outlay.DebtId == debt.Id);

            TotalMoneySpent = 0;
            foreach (var item in debtChildren)
            {
                this.Outlays.Add(item);
                TotalMoneySpent += item.Price;
            }
        }

        public void AddOutlay(Outlay outlay)
        {
            ALLOutlays.Add(outlay);
            Outlays.Add(outlay);

            TotalMoneySpent += outlay.Price;

            DBhelper.Insert(outlay);
        }

        public void AddCategory(string name)
        {
            var category = new Category
            {
                Name = name
            };

            ALLCategories.Add(category);
            Categories.Add(category);

            DBhelper.Insert(category);
        }
        public void AddDebt(string debtor, DateTime returnDate, int interestRate, List<Outlay> outlays)
        {
            var Debt = new Debt
            {
                Debtor = debtor,
                ReturnDate = returnDate,
                InterestRate = interestRate,
                Outlays = outlays
            };
            ALLDebts.Add(Debt);
            Debts.Add(Debt);

            DBhelper.Insert(Debt);
            DBhelper.UpdateWithChildren(Debt);
        }

        public void DeleteOutlay(Outlay outlay)
        {
            Outlays.Remove(outlay);
            ALLOutlays.Remove(outlay);

            TotalMoneySpent -= outlay.Price;

            DBhelper.Delete(outlay);
        }
        public void DeleteCategory(Category category)
        {
            ALLOutlays = ALLOutlays.Where(x => x.CategoryId != category.Id).ToList();
            ALLCategories = ALLCategories.Where(x => x.Id != category.Id).ToList();
            RefreshAll();

            DBhelper.Delete(category);
        }
        public void DeleteDebt(Debt debt)
        {
            ALLOutlays = ALLOutlays.Where(x => x.DebtId != debt.Id).ToList();
            ALLDebts = ALLDebts.Where(x => x.Id != debt.Id).ToList();
            RefreshAll();

            DBhelper.Delete(debt);
        }
        /*
        public void UpdateOutlay(Outlay outlay, DateTime date, string name, int price)
        {
            outlay.Date = date;
            outlay.Name = name;
            outlay.Price = price;

            var x = db.Update(outlay);
            RefreshData();
        }
        */
    }
}
