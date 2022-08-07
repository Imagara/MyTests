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
    public partial class CheckTestResults : Page
    {
        public CheckTestResults(Tests _test, Users _user)
        {
            InitializeComponent();
            TestName.Content = _test.Name;
            TestsListBox.Items.Clear();
            TestsListBox.ItemsSource = cnt.db.Answers.Where(item => item.Questions.IdTest == _test.IdTest &&
            item.IdUser == Session.User.IdUser).ToList();
        }
    }
}
