using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
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
