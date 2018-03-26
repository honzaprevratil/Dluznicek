using Dluznicek.Pages;
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

namespace Dluznicek
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Main_Initialized(object sender, EventArgs e)
        {
            MainFrame.NavigationService.Navigate(new SummaryPage());
            Summary.Background = (Brush)new BrushConverter().ConvertFrom("#FFBE22EC");
        }

        private void AddOutlay_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Pages/AddOutlayPage.xaml", UriKind.Relative));
            ToggleOffButtons();
            AddOutlay.Background = (Brush)new BrushConverter().ConvertFrom("#FFBE22EC");
        }

        private void Summary_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Pages/SummaryPage.xaml", UriKind.Relative));
            ToggleOffButtons();
            Summary.Background = (Brush)new BrushConverter().ConvertFrom("#FFBE22EC");
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Pages/AddCategoryPage.xaml", UriKind.Relative));
            ToggleOffButtons();
            AddCategory.Background = (Brush)new BrushConverter().ConvertFrom("#FFBE22EC");
        }

        private void DebtSumary_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Pages/DebtSummaryPage.xaml", UriKind.Relative));
            ToggleOffButtons();
            DebtSummary.Background = (Brush)new BrushConverter().ConvertFrom("#FFBE22EC");
        }

        private void AddDebt_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Uri("Pages/AddDebtPage.xaml", UriKind.Relative));
            ToggleOffButtons();
            AddDebt.Background = (Brush)new BrushConverter().ConvertFrom("#FFBE22EC");
        }
        private void ToggleOffButtons()
        {
            Summary.Background = (Brush)new BrushConverter().ConvertFrom("#FF673AB7");
            DebtSummary.Background = (Brush)new BrushConverter().ConvertFrom("#FF673AB7");
            AddCategory.Background = (Brush)new BrushConverter().ConvertFrom("#FF673AB7");
            AddOutlay.Background = (Brush)new BrushConverter().ConvertFrom("#FF673AB7");
            AddDebt.Background = (Brush)new BrushConverter().ConvertFrom("#FF673AB7");
        }
    }
}
