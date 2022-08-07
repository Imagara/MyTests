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
                if (cnt.db.Answers.Select(item => item.IdQuestion + " " + item.IdUser).Contains(cnt.db.Questions.Where(item => item.Content == strContent && item.Answer == strAnswer).Select(item => item.IdQuestion).FirstOrDefault() + " " + Session.User.IdUser))
                {
                    Answers answer = cnt.db.Answers.Where(item => item.IdQuestion == cnt.db.Questions.Where(i => i.Content == strContent && i.Answer == strAnswer).Select(i => i.IdQuestion).FirstOrDefault() && item.IdUser == Session.User.IdUser).FirstOrDefault();
                    answer.Answer = AnswerBox.Text;
                    cnt.db.SaveChanges();
                }
                else
                {
                    Answers newAnswer = new Answers()
                    {
                        IdUserAnswer = cnt.db.Answers.Select(p => p.IdUserAnswer).DefaultIfEmpty(0).Max() + 1,
                        IdQuestion = cnt.db.Questions.Where(item => item.Content == strContent && item.Answer == strAnswer).Select(item => item.IdQuestion).FirstOrDefault(),
                        IdUser = Session.User.IdUser,
                        Answer = AnswerBox.Text
                    };
                    cnt.db.Answers.Add(newAnswer);
                    cnt.db.SaveChanges();
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
