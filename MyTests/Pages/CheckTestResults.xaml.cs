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
        Users user;
        public CheckTestResults(Tests _test, Users _user)
        {
            InitializeComponent();
            user = _user;
            TestName.Content = _test.Name;
            PassedLabel.Content = _user.Surname + " " + _user.Name + " " + _user.Patronymic;

            if (_user.Image == null)
                AuthorImg.Source = new BitmapImage(new Uri("../Resources/StandartImage.png", UriKind.RelativeOrAbsolute));
            else
                AuthorImg.Source = ImagesFunctions.NewImage(_user);

            AnswersListBox.Items.Clear();

            List<AnswerClass> answerList = new List<AnswerClass>();

            int counter = 1;

            foreach (Answers answer in cdb.db.Answers.Where(item => item.Questions.IdTest == _test.IdTest &&
                                                            item.IdUser == Session.User.IdUser).ToList())
            {
                AnswerClass ac = new AnswerClass();
                ac.Answer = answer;
                ac.AnswerNum = counter;
                counter++;
                answerList.Add(ac);
            }

            AnswersListBox.ItemsSource = answerList;
        }
        private void PassedLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage(user));
        }

        public class AnswerClass
        {
            public Answers Answer { get; set; }
            public int AnswerNum { get; set; }
        }

    }
}
