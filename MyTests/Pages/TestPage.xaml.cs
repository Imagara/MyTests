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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

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
            LoadingQuestions();
        }
        void LoadingQuestions()
        {
            TestsListBox.Items.Clear();
            TestsListBox.ItemsSource = cnt.db.Questions.Where(item=>item.IdTest == testId).ToList();
        }

        private void TestsListBox_Selected(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (((Questions)TestsListBox.SelectedItem) != null)
                {
                    MessageBox.Show(((Questions)TestsListBox.SelectedItem).IdQuestion.ToString());
                }
            }
            catch
            {
                new ErrorWindow("Ошибка открытия теста.").ShowDialog();
            }
        }
    }
}
