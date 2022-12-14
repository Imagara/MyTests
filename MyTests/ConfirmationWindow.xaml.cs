using System.Windows;
using System.Windows.Input;

namespace MyTests
{
    public partial class ConfirmationWindow : Window
    {
        public bool answer;
        public ConfirmationWindow(bool _answer = false)
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
            answer = _answer;
        }
        private void YesButton(object sender, RoutedEventArgs e)
        {
            answer = true;
            this.Close();
        }
        private void NoButton(object sender, RoutedEventArgs e)
        {
            answer = false;
            this.Close();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
