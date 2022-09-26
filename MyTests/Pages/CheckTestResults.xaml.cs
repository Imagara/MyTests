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
    // Страница с результатами выбранного пользователя
    public partial class CheckTestResults : Page
    {
        Users user;
        public CheckTestResults(Tests _test, Users _user)
        {
            InitializeComponent();
            user = _user;

            //Заполнение верхней панели при проверке результатов
            TestName.Content = _test.Name; //Заполнение названия теста
            PassedLabel.Content = _user.Surname + " " + _user.Name + " " + _user.Patronymic; // Заполнение пользователя, который проходил тест

            // Проверка на картинку в базе данных у пользователя. Если нет, то устанавливается стандартная
            if (_user.Image == null)
                AuthorImg.Source = new BitmapImage(new Uri("../Resources/StandartImage.png", UriKind.RelativeOrAbsolute));
            else
                AuthorImg.Source = ImagesFunctions.NewImage(_user);

            //Очистка списка ответов в программе (ListBox)
            AnswersListBox.Items.Clear();

            // Создание переменной(списка), хранящей список ответов
            List<AnswerClass> answerList = new List<AnswerClass>();
            // Счетчик с номером ответа
            int counter = 1;

            // Заполнение переменной ответами из базы данных, которые принадлежат выбранному тесту и пользователю
            foreach (Answers answer in cdb.db.Answers.Where(item => item.Questions.IdTest == _test.IdTest &&
                                                            item.IdUser == Session.User.IdUser).ToList())
            {
                AnswerClass ac = new AnswerClass();
                ac.Answer = answer;
                ac.AnswerNum = counter;
                counter++;
                // Добавление данных в переменную(список)
                answerList.Add(ac);
            }
            // Заполнение списка ответов переменной со списком ответов
            AnswersListBox.ItemsSource = answerList;
        }
        private void PassedLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Переход на страницу профиля выбранного пользователя
            NavigationService.Navigate(new ProfilePage(user));
        }
        //Класс, необходимый для ответов
        public class AnswerClass
        {
            public Answers Answer { get; set; }
            public int AnswerNum { get; set; }
        }

    }
}
