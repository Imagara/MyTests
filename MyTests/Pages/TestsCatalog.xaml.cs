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
                    NavigationService.Navigate(new Pages.TestPage(((Tests)TestsListBox.SelectedItem).IdTest));
                    Session.openedTest = ((Tests)TestsListBox.SelectedItem).IdTest;
                }
            }
            catch
            {
                new ErrorWindow("Ошибка открытия теста.").ShowDialog();
            }
        }
    }
}
