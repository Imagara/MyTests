using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    public partial class TestPage : Page
    {
        int testId;
        public TestPage(int id)
        {
            InitializeComponent();
            testId = cnt.db.Tests.Where(item => item.IdTest == id).Select(item => item.IdTest).FirstOrDefault();
            Tests test = cnt.db.Tests.Where(item => item.IdTest == id).FirstOrDefault();
            TestName.Content = test.Name;
            AuthorName.Content = test.Users.Login;

            if (test.Users.Image == null)
                AuthorImg.Source = new BitmapImage(new Uri("../Resources/StandartImage.png", UriKind.RelativeOrAbsolute));
            else
                AuthorImg.Source = ImagesManip.NewImage(test.Users);

            LoadingQuestions();
        }
        void LoadingQuestions()
        {
            TestsListBox.Items.Clear();
            TestsListBox.ItemsSource = cnt.db.Answers.Where(item=>item.Questions.IdTest == testId && item.IdUser == Session.User.IdUser).ToList();
        }

        private void AuthorName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Pages.ProfilePage(cnt.db.Users.Where(item => item.IdUser == Session.OpenedTest.IdUser).FirstOrDefault()));
        }
    }
}
