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
    /// Логика взаимодействия для CheckTestResultsCatalog.xaml
    /// </summary>
    public partial class CheckTestResultsCatalog : Page
    {
        Tests test;
        public CheckTestResultsCatalog(Tests _test)
        {
            InitializeComponent();
            test = _test;
            AnswersListBox.Items.Clear();

            List<AnswerClass> answerList = new List<AnswerClass>();

            foreach (Users user in cnt.db.Users.Where(item => item.Answers.Count() > 0))
            {
                if (cnt.db.Answers.Select(item => item.Questions.IdTest + " " + item.IdUser).Contains(_test.IdTest + " " + user.IdUser))
                {
                    AnswerClass newUserAnswer = new AnswerClass();
                    newUserAnswer.User = user;
                    newUserAnswer.Correct = CorrectAnswersCounter(_test, user);
                    newUserAnswer.Count = cnt.db.Questions.Where(item => item.IdTest == _test.IdTest).Count();

                    answerList.Add(newUserAnswer);
                }
            }

            AnswersListBox.ItemsSource = answerList;
            if (AnswersListBox.Items.Count == 0)
                ResultLabel.Content = "На данный момент тест еще никто не прошел.";

        }
        public int CorrectAnswersCounter(Tests test, Users user)
        {
            Quest.Answer = cnt.db.Questions.Where(item => item.IdTest == test.IdTest).Select(it => it.Answer).ToArray();
            Quest.UserAnswer = cnt.db.Answers.Where(item => item.Users.IdUser == user.IdUser && item.Questions.IdTest == test.IdTest).Select(it => it.Answer).ToArray();
            int value = 0;
            if (Quest.Answer.Length == Quest.UserAnswer.Length)
                for (int i = 0; i < Quest.Answer.Length; i++)
                    if (Quest.Answer[i] == Quest.UserAnswer[i])
                        value++;
            return value;
        }

        private void AnswersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((AnswerClass)AnswersListBox.SelectedItem) != null)
            {
                NavigationService.Navigate(new Pages.CheckTestResults(test,
                    cnt.db.Users.Where(item => item.IdUser == ((AnswerClass)AnswersListBox.SelectedItem).User.IdUser).FirstOrDefault()));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public static class Quest
        {
            public static string[] Answer;
            public static string[] UserAnswer;
        }
        public class AnswerClass
        {
            public Users User { get; set; }
            public int Correct { get; set; }
            public int Count { get; set; }
        }
    }
}
