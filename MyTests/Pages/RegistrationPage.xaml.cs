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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        bool Test = true;
        public RegistrationPage()
        {
            InitializeComponent();
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            //Регистрая тут
            NavigationService.Navigate(new Pages.LoginPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.LoginPage());
        }
    }
}
