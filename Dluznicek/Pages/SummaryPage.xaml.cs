using Dluznicek.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dluznicek.Pages
{
    /// <summary>
    /// Interakční logika pro SummaryPage.xaml
    /// </summary>
    public partial class SummaryPage : Page
    {
        public SummaryPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            App.DBcache.RefreshOutlays();
            App.DBcache.RefreshDebts();
            TotalMoney.Text = App.DBcache.TotalMoneySpent.ToString();
            Outlays.ItemsSource = App.DBcache.Outlays;
            OutlaysCategories.ItemsSource = App.DBcache.Categories;
        }

        private void YearOutlay_Click(object sender, RoutedEventArgs e)
        {
            var today = DateTime.Today;
            App.DBcache.SelectOutlaysByDate(today.Year, 1, 1, DateType.year);
            YearButton.Background = (Brush)new BrushConverter().ConvertFrom("#FFBE22EC");

            TotalMoneyText.Text = "Money spent this Year";
            TotalMoney.Text = App.DBcache.TotalMoneySpent.ToString();
        }

        private void MonthOutlay_Click(object sender, RoutedEventArgs e)
        {
            var today = DateTime.Today;
            App.DBcache.SelectOutlaysByDate(today.Year, today.Month, 1, DateType.month);
            MonthButton.Background = (Brush)new BrushConverter().ConvertFrom("#FFBE22EC");

            TotalMoneyText.Text = "Money spent this month";
            TotalMoney.Text = App.DBcache.TotalMoneySpent.ToString();
        }

        private void AllOutlays_Click(object sender, RoutedEventArgs e)
        {
            App.DBcache.GetAllData();
            ToggleOffButtons();

            TotalMoneyText.Text = "Total money spent";
            TotalMoney.Text = App.DBcache.TotalMoneySpent.ToString();
        }

        private void DeleteOutlay_Click(object sender, RoutedEventArgs e)
        {
            if (Outlays.SelectedItem != null)
            {
                App.DBcache.DeleteOutlay(((Outlay)Outlays.SelectedItem));
                TotalMoney.Text = App.DBcache.TotalMoneySpent.ToString();
            }
        }

        private void OutlaysCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OutlaysCategories.SelectedItem != null)
            {
                ToggleOffButtons();
                TotalMoneyText.Text = "Total money spent in "+ ((Category)OutlaysCategories.SelectedItem).Name;
                App.DBcache.SelectOutlaysByCategory((Category)OutlaysCategories.SelectedItem);
                TotalMoney.Text = App.DBcache.TotalMoneySpent.ToString();
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (OutlaysCategories.SelectedItem != null)
            {
                App.DBcache.DeleteCategory(((Category)OutlaysCategories.SelectedItem));
                TotalMoney.Text = App.DBcache.TotalMoneySpent.ToString();
            }

        }
        private void ToggleOffButtons()
        {
            MonthButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF673AB7");
            YearButton.Background = (Brush)new BrushConverter().ConvertFrom("#FF673AB7");
        }
    }
}
