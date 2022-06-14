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
            LoadingTests();
        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {

        }
        void LoadingTests()
        {
            var list = cnt.db.Tests.ToList();
            if (TestNameBox.Text != "Название теста")
                list = list.Where(item => item.Name == TestNameBox.Text).ToList();
            if (AuthorTestBox.Text != "Автор")
                list = list.Where(item => item.Users.Login == AuthorTestBox.Text).ToList();
            TestsListBox.Items.Clear();
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
                    NavigationService.Navigate(new Pages.TestPage(Session.OpenedTest.IdTest));
                    Session.Points = 0;
                    Session.CurQuestion = 0;
                    Session.Quest.Answer = cnt.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Answer).ToArray();
                    Session.Quest.Content = cnt.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Content).ToArray();
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