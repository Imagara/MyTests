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
        static Users user;
        public ProfilePage(Users _user)
        {
            InitializeComponent();
            TestsListBox.Items.Clear();
            user = _user;
            FIOLabel.Content = user.Surname + " " + user.Name + " " + user.Patronymic; 
            ProfileImage.Source = user.Image == null ?
                new BitmapImage(new Uri("../Resources/StandartImage.png", UriKind.RelativeOrAbsolute)) :
                ProfileImage.Source = ImagesFunctions.NewImage(user);
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
        private void EditImage_Click(object sender, RoutedEventArgs e)
        {
            if (user == Session.User)
            {
                BitmapImage image = ImagesFunctions.SelectImage();
                if (image != null)
                {
                    ProfileImage.Source = image;
                    Session.User.Image = ImagesFunctions.BitmapSourceToByteArray((BitmapSource)ProfileImage.Source);
                    cdb.db.SaveChanges();
                }
            }
        }
        private void TestsLoading()
        {
            if (user != Session.User)
                TestsListBox.ItemsSource = cdb.db.Tests.Where(item => item.IdUser == user.IdUser && item.IsVisible == true).ToList();
            else
                TestsListBox.ItemsSource = cdb.db.Tests.Where(item => item.IdUser == user.IdUser).ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Functions.IsValidEmail(EmailBox.Text))
                new ErrorWindow("Email введен неверно.").Show();
            else if (Functions.IsEmailAlreadyTaken(EmailBox.Text) && EmailBox.Text != user.Email)
                new ErrorWindow("Данный email уже используется.").Show();
            else
            {
                Session.User.Email = EmailBox.Text;
                Session.User.Info = InfoBox.Text;
                cdb.db.SaveChanges();
                new ErrorWindow("Успешно.").ShowDialog();
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
                    Session.OpenedTest = cdb.db.Tests.Where(item => item.IdTest == ((Tests)TestsListBox.SelectedItem).IdTest).FirstOrDefault();
                    Session.Points = 0;
                    Session.CurQuestion = 0;
                    Session.Quest.Content = cdb.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Content).ToArray();
                    Session.Quest.Answer = cdb.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Answer).ToArray();

                    NavigationService.Navigate(new Pages.CurTestPage());
                }
            }
            catch
            {
                new ErrorWindow("Ошибка открытия теста.").ShowDialog();
            }
        }

        private void DeleteTestButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is Tests)
            {
                ConfirmationWindow confWindow = new ConfirmationWindow();
                confWindow.ShowDialog();
                if (confWindow.answer)
                {
                    foreach (Answers answer in cdb.db.Answers.Where(item => item.Questions.IdTest == ((Tests)btn.DataContext).IdTest))
                        cdb.db.Answers.Remove(answer);
                    foreach (Questions question in cdb.db.Questions.Where(item => item.IdTest == ((Tests)btn.DataContext).IdTest))
                        cdb.db.Questions.Remove(question);
                    cdb.db.Tests.Remove((Tests)btn.DataContext);
                    cdb.db.SaveChanges();
                    TestsLoading();
                }
            }
        }
    }
}
