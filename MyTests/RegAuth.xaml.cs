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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyTests
{
    public partial class RegAuth : Window
    {
        public RegAuth()
        {
            InitializeComponent();
        }

        private void WindowRegistration(object sender, RoutedEventArgs e) //Регистрация
        {
            MainFrame.Content = new Registration();
        }
        private void WindowEntry(object sender, RoutedEventArgs e) //Авторизация
        {
            MainFrame.Content = new Authorization();
        }
    }
}
