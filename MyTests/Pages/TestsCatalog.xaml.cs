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
    public partial class TestsCatalog : Page
    {
        public TestsCatalog()
        {
            InitializeComponent();
            LoadingTests();
        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {

        }
        void LoadingTests()
        {
            TestsListBox.Items.Clear();
            var list = cnt.db.Tests.ToList();
            if (TestNameBox.Text != "Название теста")
                list = list.Where(item => item.Name == TestNameBox.Text).ToList();
            if (AuthorTestBox.Text != "Автор")
                list = list.Where(item => item.Users.Login == AuthorTestBox.Text).ToList();
            TestsListBox.ItemsSource = list;
        }

        private void FindTests_Click(object sender, RoutedEventArgs e)
        {
            LoadingTests();
        }
        private void TestsListBox_Selected(object sender, RoutedEventArgs e)
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























































//拯救我们被关在地下室