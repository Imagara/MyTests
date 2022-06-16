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
using System.Text.RegularExpressions;

namespace MyTests
{
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login.Text == "" || Password.Text == "")
                    MessageBox.Show("Поля не могут быть пустыми.");
                else if (RegistrLogin(Login.Text))
                    MessageBox.Show("Данный логин уже занят");
                else
                {
                    try
                    {
                        Users newUser = new Users()
                        {
                            IdUsers = Connection.db.Users.Select(p => p.IdUsers).DefaultIfEmpty(0).Max() + 1,
                            Login = Login.Text,
                            Password = Password.Text,
                            Email = Email.Text,
                            Info = Info.Text
                        };
                        Connection.db.Users.Add(newUser);
                        Connection.db.SaveChanges();
                        MessageBox.Show("Вы успешно зарегистрировались");
                        Session.UserId = Connection.db.Users.Select(item => item.IdUsers).Max();
                        MainWindow RegAuth = new MainWindow();
                        RegAuth.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public bool RegistrLogin(string Login)
        {
            //if (Connection.db.Users.Select(item => item.Login).Contains(Login))
            //    return true;
            //else
                return false;
        }

        #region  FOCUS 
        private void LoginFocus(object sender, RoutedEventArgs e)
        {
            Login.Text = "";
        }

        private void LoginLostFocus(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Trim() == "")
                Login.Text = "Логин";
        }

        private void PasswordFocus(object sender, RoutedEventArgs e)
        {
            Password.Text = "";
        }

        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Text.Trim() == "")
                Password.Text = "123456";
        }

        private void EmailNameFocus(object sender, RoutedEventArgs e)
        {
            Email.Text = "";
        }

        private void EmailLostFocus(object sender, RoutedEventArgs e)
        {
            if (Email.Text.Trim() == "")
                Email.Text = "Почта";
        }

        private void InfoNameFocus(object sender, RoutedEventArgs e)
        {
            Info.Text = "";
        }

        private void InfoLostFocus(object sender, RoutedEventArgs e)
        {
            if (Info.Text.Trim() == "")
                Info.Text = "Личная информация";
        }
        #endregion
    }
}
