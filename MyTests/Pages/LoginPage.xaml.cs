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
    public partial class LoginPage : Page
    {
        bool Test = true;
        public LoginPage()
        {
            InitializeComponent();
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.RegistrationPage());
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            if (Test)
                NavigationService.Navigate(new Pages.MainPage());
            else
            {
                try
                {
                    if (!Functions.IsValidLogAndPass(LogBox.Text, PassBox.Password))
                        new ErrorWindow("Поля не могут быть пустыми").Show();
                    else if (!Functions.LoginCheck(LogBox.Text, PassBox.Password))
                        new ErrorWindow("Неверный логин или пароль").Show();
                    else
                    {
                        //Profile.userId = cnt.db.User.Where(item => item.NickName == LogBox.Text).Select(item => item.Id).FirstOrDefault();
                        NavigationService.Navigate(new Pages.MainPage());
                    }
                }
                catch
                {
                    new ErrorWindow("Ошибка входа").ShowDialog();
                }
            }
        }
    }
}
