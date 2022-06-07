using System.Windows;
using System.Windows.Input;

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

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}

