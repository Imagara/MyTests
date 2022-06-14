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
            if (AnswerBox.Text.ToLower().Trim() == Session.Quest.Answer[Session.CurQuestion].ToLower().Trim())
                Session.Points++;
            if(Session.CurQuestion >= Session.OpenedTest.Questions.Count()-1)
                NavigationService.Navigate(new Pages.ResultTestPage());
            else
            {
                Session.CurQuestion++; 
                NavigationService.Navigate(new Pages.CurTestPage());
            }
        }
    }
}
