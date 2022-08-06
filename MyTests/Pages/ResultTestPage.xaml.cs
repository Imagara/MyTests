using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    public partial class ResultTestPage : Page
    {
        public ResultTestPage()
        {
            InitializeComponent();
            if (Session.OpenedTest.IsAnswersVisible == true)
                CheckAnswersButton.Visibility = Visibility.Visible;
            ResultTB.Text = $"{Session.Points}/{Session.OpenedTest.Questions.Count}";
        }

        private void AnswersButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.TestPage(Session.OpenedTest.IdTest));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.TestsCatalog());
        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ProfilePage(cnt.db.Users.Where(item => item.IdUser == Session.OpenedTest.IdUser).FirstOrDefault()));
        }
    }
}
