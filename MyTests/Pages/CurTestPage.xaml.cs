using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    public partial class CurTestPage : Page
    {
        public CurTestPage()
        {
            InitializeComponent();
            QuestionBox.Text = Session.Quest.Content[Session.CurQuestion];
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strContent = Session.Quest.Content[Session.CurQuestion];
                string strAnswer = Session.Quest.Answer[Session.CurQuestion];
                if (cdb.db.Answers.Select(item => item.IdQuestion + " " + item.IdUser).Contains(cdb.db.Questions.Where(item => item.Content == strContent && item.Answer == strAnswer).Select(item => item.IdQuestion).FirstOrDefault() + " " + Session.User.IdUser))
                {
                    Answers answer = cdb.db.Answers.Where(item => item.IdQuestion == cdb.db.Questions.Where(i => i.Content == strContent && i.Answer == strAnswer).Select(i => i.IdQuestion).FirstOrDefault() && item.IdUser == Session.User.IdUser).FirstOrDefault();
                    answer.Answer = AnswerBox.Text;
                    cdb.db.SaveChanges();
                }
                else
                {
                    Answers newAnswer = new Answers()
                    {
                        IdUserAnswer = cdb.db.Answers.Select(p => p.IdUserAnswer).DefaultIfEmpty(0).Max() + 1,
                        IdQuestion = cdb.db.Questions.Where(item => item.Content == strContent && item.Answer == strAnswer).Select(item => item.IdQuestion).FirstOrDefault(),
                        IdUser = Session.User.IdUser,
                        Answer = AnswerBox.Text
                    };
                    cdb.db.Answers.Add(newAnswer);
                    cdb.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
            }

            if (AnswerBox.Text.ToLower().Trim() == Session.Quest.Answer[Session.CurQuestion].ToLower().Trim())
                Session.Points++;
            if (Session.CurQuestion >= Session.OpenedTest.Questions.Count() - 1)
                NavigationService.Navigate(new Pages.ResultTestPage());
            else
            {
                Session.CurQuestion++;
                NavigationService.Navigate(new Pages.CurTestPage());
            }
        }
    }
}
