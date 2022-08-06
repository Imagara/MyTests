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

namespace MyTests.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void ProfileClick(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new Pages.ProfilePage(Session.User);
        }

        private void TestsCatalogClick(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new Pages.TestsCatalog();
        }
        private void CreateTestClick(object sender, RoutedEventArgs e)
        {
            //MainContentFrame.Content = new СatalogPage();
        }
    }
}
