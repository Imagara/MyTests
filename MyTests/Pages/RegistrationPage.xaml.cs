using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Functions.IsValidEmail(EmailBox.Text))
                    new ErrorWindow("Email введен неверно.").Show();
                else if (Functions.IsEmailAlreadyTaken(EmailBox.Text))
                    new ErrorWindow("Данный email уже используется.").Show();
                else if (!Functions.IsValidLength(LogBox.Text.Trim()))
                    new ErrorWindow("Поле «Логин» должно содержать не менее 5 символов.").Show();
                else if (!Functions.IsValidLength(PassBox.Password.Trim()))
                    new ErrorWindow("Поле «Пароль» должно содержать не менее 5 символов.").Show();
                else if (!Functions.IsLogEqualPass(LogBox.Text, PassBox.Password))
                    new ErrorWindow(" Поля «Логин» и «Пароль» не должны быть равны").Show();
                else if (Functions.IsLoginAlreadyTaken(LogBox.Text))
                    new ErrorWindow("Данный логин уже занят").Show();
                else
                {
                    string[] fio = new string[3];
                    fio = FIOBox.Text.Split(' ');
                    Users newUser = new Users()
                    {
                        IdUser = cdb.db.Users.Select(p => p.IdUser).DefaultIfEmpty(0).Max() + 1,
                        Login = LogBox.Text,
                        Password = Functions.EncryptPassword(PassBox.Password),
                        Email = EmailBox.Text,
                        Post = "Пользователь",
                        Surname = fio[0],
                        Name = fio[1],
                        Patronymic = fio[2]
                    };
                    cdb.db.Users.Add(newUser);
                    cdb.db.SaveChanges(); ;
                    new ErrorWindow("Успешная регистрация").ShowDialog();
                }
            }
            catch
            {
                new ErrorWindow("Ошибка.").ShowDialog();
            }
            NavigationService.Navigate(new Pages.LoginPage());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.LoginPage());
        }
    }
}
