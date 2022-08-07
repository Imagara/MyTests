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
        public CheckTestResultsCatalog(Tests _test)
        {
            InitializeComponent();

            AnswersListBox.Items.Clear();

            List<AnswerClass> answerList = new List<AnswerClass>();

            foreach (Users user in cnt.db.Users)
            {
                AnswerClass newUserAnswer = new AnswerClass();
                newUserAnswer.surname = user.Surname;
                newUserAnswer.name = user.Name;
                newUserAnswer.patronymic = user.Patronymic;
                newUserAnswer.correct = -1; //add count of correct answers
                newUserAnswer.count = user.Tests.Where(item => item.Questions == _test.Questions).Count();

                answerList.Add(newUserAnswer);
            }

            AnswersListBox.ItemsSource = answerList;

        }
        public class AnswerClass
        {
            public string surname { get; set; }
            public string name { get; set; }
            public string patronymic { get; set; }
            public int correct { get; set; }
            public int count { get; set; }
        }
    }
}
