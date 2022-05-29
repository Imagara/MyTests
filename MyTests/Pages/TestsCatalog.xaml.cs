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
    /// <summary>
    /// Логика взаимодействия для TestsCatalog.xaml
    /// </summary>
    public partial class TestsCatalog : Page
    {
        public TestsCatalog()
        {
            InitializeComponent();
            //LoadingTests();
        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AddTest(string author, BitmapImage imageSource, int test)
        {
            Grid testGrid = new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x40, 0x44, 0x4B)),
                Height = 45,
                Width = 590,
                Margin = new Thickness(10, 5, 10, 5)
            };

            Image testImage = new Image
            {
                Source = imageSource,
                Width = 35,
                Height = 35,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            testGrid.Children.Add(testImage);

            Label authorLabel = new Label();
            authorLabel.Content = author;
            authorLabel.Foreground = Brushes.White;
            authorLabel.FontWeight = FontWeights.Bold;
            authorLabel.HorizontalAlignment = HorizontalAlignment.Left;
            authorLabel.VerticalAlignment = VerticalAlignment.Top;
            authorLabel.Margin = new Thickness(40, 0, 0, 0);

            testGrid.Children.Add(authorLabel);

            Label testLabel = new Label();
            testLabel.Content = test;
            testLabel.Foreground = Brushes.White;
            testLabel.HorizontalAlignment = HorizontalAlignment.Left;
            testLabel.VerticalAlignment = VerticalAlignment.Bottom;
            testLabel.Margin = new Thickness(40, 0, 0, 0);
            testGrid.Children.Add(testLabel);

            TestsListBox.Items.Add(testGrid);
        }
        void LoadingTests()
        {
            TestsListBox.Items.Clear();
            var list = cnt.db.Tests.Where(item => item.IdUser == Session.userId).ToList();
            if(TestNameBox.Text != "Название теста")
                list = list.Where(item => item.Name == TestNameBox.Text).ToList();
            if(AuthorTestBox.Text != "Автор")
                list = list.Where(item => item.Users.Login == AuthorTestBox.Text).ToList();

            foreach (Tests test in list)
            {
                try
                {
                    BitmapImage img = test.Image == null ?
                        new BitmapImage(new Uri("../Resources/Approval.png", UriKind.RelativeOrAbsolute)) :
                        ImagesManip.NewImage(cnt.db.Users.Where(item => item.IdUser == Session.userId).FirstOrDefault());
                    TestsListBox.ItemsSource = new[] { new { TestName = test.Name, TestAuthor = test.Users.Login } };
                    //AddTest(test.Name, img, cnt.db.Questions.Where(item => item.IdTest == test.IdTest).Count());
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.ToString()).ShowDialog();
                }
            }
        }

        private void FindTests_Click(object sender, RoutedEventArgs e)
        {
            LoadingTests();
        }
    }
}
