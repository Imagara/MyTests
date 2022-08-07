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
            TestsListBox.Items.Clear();
            if (Session.User.Post == "Преподаватель")
                AddTestButton.Visibility = Visibility.Visible;
            LoadingTests();
        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {

        }
        void LoadingTests()
        {            
            List<Tests> list = cnt.db.Tests.Where(item => item.IsVisible == true).ToList();
            if (TestNameBox.Text != "Название теста")
                list = list.Where(item => item.Name.StartsWith(TestNameBox.Text)).ToList();
            if (AuthorTestBox.Text != "Автор")
                list = list.Where(item => item.Users.Login.StartsWith(AuthorTestBox.Text)).ToList();
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