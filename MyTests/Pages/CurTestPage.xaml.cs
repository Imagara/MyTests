using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    // Страница с прохождением теста
    public partial class CurTestPage : Page
    {
        public CurTestPage()
        {
            InitializeComponent();
            // Заполнение вопроса из массива Content в файле-классе Session
            QuestionBox.Text = Session.Quest.Content[Session.CurQuestion];
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание переменной с вопросом из массива Content файла-класса Session
                string strContent = Session.Quest.Content[Session.CurQuestion];
                // Создание переменной с ответами из массива Answer файла-класса Session
                string strAnswer = Session.Quest.Answer[Session.CurQuestion];

                // Замена ответа пользователя в базе данных, если пользователь уже проходил этот тест
                if (cdb.db.Answers.Select(item => item.IdQuestion + " " + item.IdUser).Contains(cdb.db.Questions.Where(item => item.Content == strContent && item.Answer == strAnswer).Select(item => item.IdQuestion).FirstOrDefault() + " " + Session.User.IdUser))
                {
                    Answers answer = cdb.db.Answers.Where(item => item.IdQuestion == cdb.db.Questions.Where(i => i.Content == strContent && i.Answer == strAnswer).Select(i => i.IdQuestion).FirstOrDefault() && item.IdUser == Session.User.IdUser).FirstOrDefault();
                    answer.Answer = AnswerBox.Text;
                    cdb.db.SaveChanges();
                }
                else // Сохранение ответа пользователя, если он еще не проходил этот тест
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
                new ErrorWindow(ex.Message).ShowDialog();  // Вывод ошибки, если она есть
            }

            // Добавление баллов в переменную Points файла-класса Session, если ответ правильный
            if (AnswerBox.Text.ToLower().Trim() == Session.Quest.Answer[Session.CurQuestion].ToLower().Trim())
                Session.Points++;
            // Если пройдены все вопросы, то переход к странице с результатами
            if (Session.CurQuestion >= Session.OpenedTest.Questions.Count() - 1)
                NavigationService.Navigate(new Pages.ResultTestPage());
            else // Если еще остались вопросы, то переход к следующему вопросу
            {
                Session.CurQuestion++; // Номер вопроса прибавляется на 1
                NavigationService.Navigate(new Pages.CurTestPage());
            }
        }
    }
}
