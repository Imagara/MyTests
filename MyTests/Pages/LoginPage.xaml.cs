using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
namespace MyTests.Pages
{
    // Страница с входом
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу регистрации
            NavigationService.Navigate(new RegistrationPage());
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Functions.IsValidLogAndPass(LogBox.Text, PassBox.Password))// Проверка, заполнены ли данные
                    new ErrorWindow("Поля не могут быть пустыми").Show(); // Вывод ошибки
                else if (!Functions.LoginCheck(LogBox.Text, PassBox.Password)) // Проверка на правильность ввода логина и пароля
                    new ErrorWindow("Неверный логин или пароль").Show(); // Вывод ошибки
                else
                {
                    Session.User = cdb.db.Users.Where(item => item.Login == LogBox.Text).FirstOrDefault(); // Запись пользователя, вошедшего в программу, в файл класса Session для будущего взаимодействия
                    NavigationService.Navigate(new MainPage()); // Переход на главную страницу
                }
            }
            catch
            {
                new ErrorWindow("Ошибка входа").ShowDialog();  // Вывод ошибки, если она есть
            }
        }
    }
}
