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
    public partial class AddDebtPage : Page
    {
        public AddDebtPage()
        {
            InitializeComponent();
        }

        private void AddDebt_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Inputs())
            {
                List<Outlay> outlayList = new List<Outlay>();
                foreach(Outlay item in Outlays.SelectedItems)
                {
                    outlayList.Add(item);
                }

                App.DBcache.AddDebt(Name.Text, (DateTime)Date.SelectedDate, int.Parse(Price.Text), outlayList);

                Outlays.SelectedItems.Clear();
                Name.Text = "";
                Price.Text = "";
            }
        }

        private bool Check_Inputs()
        {
            if (string.IsNullOrEmpty( Name.Text )) return false;
            if (string.IsNullOrEmpty( Date.DisplayDate.ToString() )) return false;
            if (string.IsNullOrEmpty( Price.Text )) return false;
            if (Outlays.SelectedItems == null) return false;
            if (!int.TryParse(Price.Text, out int ParsedInt)) return false;

            return true;
        }

        private void Page_Initialized(object sender, EventArgs e)
        {
            App.DBcache.RefreshOutlays();
            App.DBcache.RefreshDebts();
            Date.SelectedDate = DateTime.Today;
            Outlays.ItemsSource = App.DBcache.Outlays;
        }
    }
}
