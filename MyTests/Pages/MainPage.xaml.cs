using System.Windows;
using System.Windows.Controls;

namespace MyTests.Pages
{
    // Страница с кнопками навигации снизу
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            if (Session.User.Post == "Преподаватель") // Проверка, есть ли у пользователя права преподавателя
                CreateTest.Visibility = Visibility.Visible; // Отображение кнопки создания теста
        }
        private void ProfileClick(object sender, RoutedEventArgs e)
        {
            // Переход на страницу с профилем
            MainContentFrame.Content = new ProfilePage(Session.User);
        }

        private void TestsCatalogClick(object sender, RoutedEventArgs e)
        {
            // Переход на страницу с тестами
            MainContentFrame.Content = new TestsCatalog();
        }
        private void CreateTestClick(object sender, RoutedEventArgs e)
        {
            if (Session.User.Post == "Преподаватель")// Проверка, есть ли у пользователя права преподавателя
                MainContentFrame.Content = new EditTestPage();// Переход на страницу создания теста
        }
    }
}
