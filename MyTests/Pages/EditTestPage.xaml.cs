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

namespace MyTests.Pages
{
    public partial class EditTestPage : Page
    {
        Tests test;
        Questions selected;
        public EditTestPage(Tests _test = null)
        {
            InitializeComponent();
            if (_test == null)
            {
                try
                {
                    int testId = cnt.db.Tests.Select(p => p.IdTest).DefaultIfEmpty(0).Max() + 1;
                    Tests newTest = new Tests()
                    {
                        IdTest = testId,
                        IdUser = Session.User.IdUser,
                        Name = $"Новый тест {cnt.db.Tests.Where(item => item.IdUser == Session.User.IdUser).Count() + 1}",
                        IsAnswersVisible = false,
                        IsVisible = false,
                        CanAgain = false
                    };
                    cnt.db.Tests.Add(newTest);
                    cnt.db.SaveChanges();
                    test = cnt.db.Tests.Where(item => item.IdTest == testId).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.Message);
                }
            }
            else
            {
                test = _test;
                IsVisibleCB.IsChecked = test.IsVisible;
                IsAnswersVisibleCB.IsChecked = test.IsAnswersVisible;
                CanAgainCB.IsChecked = test.CanAgain;
            }


            TestNameBox.Text = test.Name;
            TestImg.Source = test.Image == null ? new BitmapImage(new Uri("../Resources/Approval.png", UriKind.RelativeOrAbsolute)) : ImagesFunctions.NewImage(_test);

            QuestionsListBox.Items.Clear();
            QuestionsUpdate();
        }
        private void QuestionsUpdate()
        {
            QuestionsListBox.ItemsSource = cnt.db.Questions.Where(item => item.IdTest == test.IdTest).ToList();
        }

        private void QuestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected = ((Questions)QuestionsListBox.SelectedItem);
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            //addQuestion here
        }
        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (selected != null)
            {
                ConfirmationWindow confWindow = new ConfirmationWindow();
                confWindow.ShowDialog();
                if (confWindow.answer)
                {
                    try
                    {
                        cnt.db.Questions.Remove(selected);
                        cnt.db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        new ErrorWindow(ex.Message);
                    }
                }
                QuestionsUpdate();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void TestImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage image = ImagesFunctions.SelectImage();
            if (image != null)
            {
                TestImg.Source = image;
                test.Image = ImagesFunctions.BitmapSourceToByteArray((BitmapSource)TestImg.Source);
                cnt.db.SaveChanges();
            }
        }

        private void TestNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            test.Name = TestNameBox.Text;
        }

        private void SaveInfoCB(object sender, RoutedEventArgs e)
        {
            test.IsVisible = IsVisibleCB.IsChecked == true;
            test.IsAnswersVisible = IsAnswersVisibleCB.IsChecked == true;
            test.CanAgain = CanAgainCB.IsChecked == true;
            cnt.db.SaveChanges();
        }
    }
}
