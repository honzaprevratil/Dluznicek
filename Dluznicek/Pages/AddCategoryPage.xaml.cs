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
    /// Interakční logika pro AddCategoryPage.xaml
    /// </summary>
    public partial class AddCategoryPage : Page
    {
        public AddCategoryPage()
        {
            InitializeComponent();
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length != 0)
            {
                App.DBcache.AddCategory(Name.Text);
                Name.Text = "";
            }
        }
    }
}
