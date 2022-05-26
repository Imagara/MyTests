using System.Windows;

namespace MyTests
{
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string error)
        {
            InitializeComponent();
            ErrorLabel.Text = error;
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

