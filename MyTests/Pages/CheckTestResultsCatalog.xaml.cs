using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    // Страница с каталогом результатов пользователей
    public partial class CheckTestResultsCatalog : Page
    {
        Tests test;
        public CheckTestResultsCatalog(Tests _test)
        {
            InitializeComponent();
            test = _test;
            //Очистка списка ответов в программе (ListBox)
            AnswersListBox.Items.Clear();

            // Создание переменной(списка), хранящей пользователя, список правильных ответов и количество вопросов в тесте
            List<AnswerClass> answerList = new List<AnswerClass>();

            // Заполнение переменной данными из базы данных
            foreach (Users user in cdb.db.Users.Where(item => item.Answers.Count() > 0))
            {
                if (cdb.db.Answers.Select(item => item.Questions.IdTest + " " + item.IdUser).Contains(_test.IdTest + " " + user.IdUser))
                {
                    AnswerClass newUserAnswer = new AnswerClass();
                    newUserAnswer.User = user;
                    newUserAnswer.Correct = Functions.CorrectAnswersCounter(_test, user);
                    newUserAnswer.Count = cdb.db.Questions.Where(item => item.IdTest == _test.IdTest).Count();
                    // Добавление данных в переменную(список), хранящей пользователя, список правильных ответов и количество вопросов в тесте
                    answerList.Add(newUserAnswer);
                }
            }
            // Заполнение списка для отображение, хранящего пользователя и его баллы за тест
            AnswersListBox.ItemsSource = answerList;

            // Если количество ответов на этот тест = 0, то появится надпись "На данный момент тест еще никто не прошел."
            if (AnswersListBox.Items.Count == 0)
                ResultLabel.Content = "На данный момент тест еще никто не прошел.";

        }

        private void AnswersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Если выбран элемент списка
            if (((AnswerClass)AnswersListBox.SelectedItem) != null)
            {
                // переход на страницу с проверкой результатов у конкретного пользователя
                NavigationService.Navigate(new Pages.CheckTestResults(test,
                    cdb.db.Users.Where(item => item.IdUser == ((AnswerClass)AnswersListBox.SelectedItem).User.IdUser).FirstOrDefault()));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Возврат на прошлую страницу
            NavigationService.GoBack();
        }
        // Класс, хранящий пользователя, список правильных ответов и количество вопросов в тесте
        public class AnswerClass
        {
            public Users User { get; set; }
            public int Correct { get; set; }
            public int Count { get; set; }
        }
    }
}
