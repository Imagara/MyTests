using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    public partial class ProfilePage : Page
    {
        public static Users user;
        public ProfilePage(Users _user)
        {
            InitializeComponent();
            if (_user != null)
            {
                user = _user;
                UserName.Content = user.Login;
                if (user.Image == null)
                    ProfileImage.Source = new BitmapImage(new Uri("../Resources/StandartImage.png", UriKind.RelativeOrAbsolute));
                else
                    ProfileImage.Source = ImagesManip.NewImage(user);
                EmailBox.Text = user.Email;
                InfoBox.Text = user.Info;
                if (user != Session.User)
                {
                    EmailBox.IsEnabled = false;
                    InfoBox.IsEnabled = false;
                    SaveButton.Visibility = Visibility.Collapsed;
                }
                if (user.Post == "Преподаватель")
                    TestsLoading();
                else
                    TestsListBox.Visibility = Visibility.Collapsed;
            }
        }
        private void EditImage_Click(object sender, RoutedEventArgs e)
        {
            if (user == Session.User)
            {
                BitmapImage image = ImagesManip.SelectImage();
                if (image != null)
                {
                    ProfileImage.Source = image;
                    Session.User.Image = ImagesManip.BitmapSourceToByteArray((BitmapSource)ProfileImage.Source);
                    cnt.db.SaveChanges();
                }
            }
        }
        private void TestsLoading()
        {
            TestsListBox.Items.Clear();
            if(user != Session.User)
            TestsListBox.ItemsSource = cnt.db.Tests.Where(item => item.IdUser == user.IdUser && item.IsVisible == true).ToList();
            else
                TestsListBox.ItemsSource = cnt.db.Tests.Where(item => item.IdUser == user.IdUser).ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Functions.IsValidEmail(EmailBox.Text))
                new ErrorWindow("Email введен неверно.").Show();
            else if (Functions.IsEmailAlreadyTaken(EmailBox.Text))
                new ErrorWindow("Данный email уже используется.").Show();
            else
            {
                Session.User.Email = EmailBox.Text;
                Session.User.Info = InfoBox.Text;
                cnt.db.SaveChanges();
                new ErrorWindow("Успешно.").ShowDialog();
            }

        }
        private void TestsListBox_Selectedd(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Tests)TestsListBox.SelectedItem) != null)
                {
                    Session.OpenedTest = cnt.db.Tests.Where(item => item.IdTest == ((Tests)TestsListBox.SelectedItem).IdTest).FirstOrDefault();
                    Session.Points = 0;
                    Session.CurQuestion = 0;
                    Session.Quest.Content = cnt.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Content).ToArray();
                    Session.Quest.Answer = cnt.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Answer).ToArray();

                    NavigationService.Navigate(new Pages.CurTestPage());
                }
            }
            catch
            {
                new ErrorWindow("Ошибка открытия теста.").ShowDialog();
            }
        }

        private void CheckResultsButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is Tests)
                NavigationService.Navigate(new Pages.CheckTestResultsCatalog((Tests)btn.DataContext));
        }
        private void EditTestButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is Tests)
                NavigationService.Navigate(new Pages.EditTestPage((Tests)btn.DataContext));
        }

        private void TestsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((Tests)TestsListBox.SelectedItem) != null)
                {
                    Session.OpenedTest = cnt.db.Tests.Where(item => item.IdTest == ((Tests)TestsListBox.SelectedItem).IdTest).FirstOrDefault();
                    Session.Points = 0;
                    Session.CurQuestion = 0;
                    Session.Quest.Content = cnt.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Content).ToArray();
                    Session.Quest.Answer = cnt.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Answer).ToArray();

                    NavigationService.Navigate(new Pages.CurTestPage());
                }
            }
            catch
            {
                new ErrorWindow("Ошибка открытия теста.").ShowDialog();
            }
        }
    }
}
