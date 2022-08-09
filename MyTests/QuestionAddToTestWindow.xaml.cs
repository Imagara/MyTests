using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyTests
{
    public partial class QuestionAddToTestWindow : Window
    {
        Tests test;
        public QuestionAddToTestWindow(Tests _test)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            test = _test;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionBox.Text != "Вопрос" || AnswerBox.Text != "Ответ")
            {
                try
                {
                    Questions newQuestion = new Questions
                    {
                        IdQuestion = cdb.db.Questions.Select(p => p.IdQuestion).DefaultIfEmpty(0).Max() + 1,
                        IdTest = test.IdTest,
                        Content = QuestionBox.Text,
                        Answer = AnswerBox.Text
                    };
                    cdb.db.Questions.Add(newQuestion);
                    cdb.db.SaveChanges();
                    Close();
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.Message).ShowDialog();
                }
            }
            else
                new ErrorWindow("Измените содержимое вопрос или ответа для сохранения.").ShowDialog();
            
        }

        private void QuestionBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).Text = String.Empty;
        }

        private void QuestionBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (QuestionBox.Text.Trim() == "")
                QuestionBox.Text = "Вопрос";
        }

        private void AnswerBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).Text = String.Empty;
        }

        private void AnswerBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (AnswerBox.Text.Trim() == "")
                AnswerBox.Text = "Ответ";
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
