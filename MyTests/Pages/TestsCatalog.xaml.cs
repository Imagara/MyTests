using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    public partial class TestsCatalog : Page
    {
        public TestsCatalog()
        {
            InitializeComponent();
            TestsListBox.Items.Clear();
            if (Session.User.Post == "Преподаватель")
                AddTestButton.Visibility = Visibility.Visible;
            LoadingTests();
        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditTestPage());
        }
        void LoadingTests()
        {
            List<Tests> list = cdb.db.Tests.Where(item => item.IsVisible == true && item.Questions.Count > 0).ToList();
            if (TestNameBox.Text != "Название теста")
                list = list.Where(item => item.Name.StartsWith(TestNameBox.Text)).ToList();

            if (AuthorTestBox.Text != "Преподаватель")
                list = list.Where(item => item.Users.Surname.StartsWith(AuthorTestBox.Text)
                || item.Users.Name.StartsWith(AuthorTestBox.Text)
                || item.Users.Patronymic.StartsWith(AuthorTestBox.Text)).ToList();

            List<TestsClass> testsList = new List<TestsClass>();

            foreach (Tests test in list)
            {
                TestsClass tc = new TestsClass();
                tc.test = test;
                tc.testImage = test.Image == null ? new BitmapImage(new Uri("../Resources/Approval.png", UriKind.RelativeOrAbsolute)) : ImagesFunctions.NewImage(test);
                testsList.Add(tc);
            }

            TestsListBox.ItemsSource = testsList;

        }

        private void FindTests_Click(object sender, RoutedEventArgs e)
        {
            LoadingTests();
        }

        private void TestNameBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).Text = String.Empty;
        }

        private void TestNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TestNameBox.Text.Trim() == "")
                TestNameBox.Text = "Название теста";
        }

        private void AuthorTestBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).Text = String.Empty;
        }

        private void AuthorTestBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (AuthorTestBox.Text.Trim() == "")
                AuthorTestBox.Text = "Преподаватель";
        }

        private void TestsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((Tests)TestsListBox.SelectedItem) != null)
                {
                    Tests thisTest = cdb.db.Tests.Where(item => item.IdTest == ((Tests)TestsListBox.SelectedItem).IdTest).FirstOrDefault();

                    if (!thisTest.CanAgain && cdb.db.Answers.Where(item => item.Questions.Tests.IdTest == thisTest.IdTest).Select(item => item.IdUser).Contains(Session.User.IdUser) && Session.User.IdUser != thisTest.IdUser)
                        new ErrorWindow("Этот тест не может быть пройден повторно.").ShowDialog();
                    else
                    {
                        Session.OpenedTest = thisTest;
                        Session.Points = 0;
                        Session.CurQuestion = 0;
                        Session.Quest.Content = cdb.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Content).ToArray();
                        Session.Quest.Answer = cdb.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Answer).ToArray();
                        NavigationService.Navigate(new Pages.CurTestPage());
                    }
                }
            }
            catch
            {
                new ErrorWindow("Ошибка открытия теста.").ShowDialog();
            }
        }

        public class TestsClass
        {
            public Tests test { get; set; }
            public BitmapImage testImage { get; set; }
        }
    }
}























































//拯救我们被关在地下室