﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    public partial class CheckTestResultsCatalog : Page
    {
        Tests test;
        public CheckTestResultsCatalog(Tests _test)
        {
            InitializeComponent();
            test = _test;
            AnswersListBox.Items.Clear();

            List<AnswerClass> answerList = new List<AnswerClass>();

            foreach (Users user in cdb.db.Users.Where(item => item.Answers.Count() > 0))
            {
                if (cdb.db.Answers.Select(item => item.Questions.IdTest + " " + item.IdUser).Contains(_test.IdTest + " " + user.IdUser))
                {
                    AnswerClass newUserAnswer = new AnswerClass();
                    newUserAnswer.User = user;
                    newUserAnswer.Correct = Functions.CorrectAnswersCounter(_test, user);
                    newUserAnswer.Count = cdb.db.Questions.Where(item => item.IdTest == _test.IdTest).Count();

                    answerList.Add(newUserAnswer);
                }
            }

            AnswersListBox.ItemsSource = answerList;
            if (AnswersListBox.Items.Count == 0)
                ResultLabel.Content = "На данный момент тест еще никто не прошел.";

        }

        private void AnswersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((AnswerClass)AnswersListBox.SelectedItem) != null)
            {
                NavigationService.Navigate(new Pages.CheckTestResults(test,
                    cdb.db.Users.Where(item => item.IdUser == ((AnswerClass)AnswersListBox.SelectedItem).User.IdUser).FirstOrDefault()));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        public class AnswerClass
        {
            public Users User { get; set; }
            public int Correct { get; set; }
            public int Count { get; set; }
        }
    }
}
