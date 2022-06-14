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
    public partial class ResultTestPage : Page
    {
        public ResultTestPage()
        {
            InitializeComponent();
            ResultTB.Text = $"{Session.Points}/{Session.OpenedTest.Questions.Count}";
        }

        private void AnswersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.TestsCatalog());
        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ProfilePage( cnt.db.Users.Where(item => item.IdUser == Session.OpenedTest.IdUser).FirstOrDefault()));
        }
    }
}
