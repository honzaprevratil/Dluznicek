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
    public partial class DebtSummaryPage : Page
    {
        public DebtSummaryPage()
        {
            InitializeComponent();
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            App.DBcache.RefreshOutlays();
            App.DBcache.RefreshDebts();
            Outlays.ItemsSource = App.DBcache.Outlays;
            Debts.ItemsSource = App.DBcache.Debts;
        }

        private void DeleteDebt_Click(object sender, RoutedEventArgs e)
        {
            if (Debts.SelectedItem != null)
            {
                App.DBcache.DeleteDebt(((Debt)Debts.SelectedItem));
                RefreshMoneyText();
            }
        }
        
        private void DeleteOutlay_Click(object sender, RoutedEventArgs e)
        {
            if (Outlays.SelectedItem != null)
            {
                App.DBcache.DeleteOutlay(((Outlay)Outlays.SelectedItem));
                RefreshMoneyText();
            }
        }

        private void Debts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Debts.SelectedItem != null)
            {
                App.DBcache.SelectOutlaysByDebt((Debt)Debts.SelectedItem);
                RefreshMoneyText();
            }
        }

        private void RefreshMoneyText()
        {
            if (Debts.SelectedItem != null)
            {
                TotalMoneyText.Text = ((Debt)Debts.SelectedItem).Debtor + " owe";
                TotalMoney.Text = (App.DBcache.TotalMoneySpent + ((Debt)Debts.SelectedItem).Interest).ToString();
            }
        }
    }
}
