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


namespace MyTests
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Auth(object sender, RoutedEventArgs e)
        {
            if (!emptiness(Login.Text, Password.Password))
                MessageBox.Show("Поля не могут быть пустыми.");
            else if (Connection.db.Users.Select(item => item.Login + item.Password).Contains(Login.Text + Password.Password))
            {
                Session.UserId = Connection.db.Users.First(item => item.Login == Login.Text).IdUsers;
                MessageBox.Show("КУКУ");
            }
            else
                MessageBox.Show("Неверный логин или пароль");
        }

        private void LoginFocus(object sender, RoutedEventArgs e)
        {
            Login.Text = "";
        }

        private void LostLogin(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Trim() == "")
                Login.Text = "Логин";
        }

        private void PasswordFocus(object sender, RoutedEventArgs e)
        {
            Password.Password = "";
        }

        private void LostPassword(object sender, RoutedEventArgs e)
        {
            if(Password.Password.Trim() == "")
            Password.Password = "123456";
        }

        public bool emptiness(string login, string password) //Проверка на пустоту
        {
            if (login == "" || password == "")
                return false;
            else
                return true;
        }

        public bool LoginTestEptiness(string login) //Проверка на пустоту
        {
            if (login == "")
                return false;
            else
                return true;
        }

        public bool PasswordTestEptiness(string password) //Проверка на пустоту
        {
            if (password == "")
                return false;
            else
                return true;
        }
    }
}
