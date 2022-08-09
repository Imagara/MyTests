using System.Windows;
using System.Windows.Controls;

namespace MyTests.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            if (Session.User.Post != "Преподаватель")
                CreateTest.Visibility = Visibility.Collapsed;
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
            MainContentFrame.Content = new EditTestPage();
        }
    }
}
