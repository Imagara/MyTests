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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            TestsLoading();
            //if (cnt.db.Dispatcher.Where(item => item.IdDispatcher == profile.DispatcherId).Select(item => item.ProfileImgSource).FirstOrDefault() != null)
            //    ProfileImg.Source = new BitmapImage(new Uri(cnt.db.Dispatcher.Where(item => item.IdDispatcher == profile.DispatcherId).Select(item => item.ProfileImgSource).FirstOrDefault()));
            //NameSurnameBox.Content = cnt.db.Dispatcher.Where(item => item.IdDispatcher == profile.DispatcherId).Select(item => item.Name + " " + item.Surname).FirstOrDefault();
            //DateTime Birthday = cnt.db.Dispatcher.Where(item => item.IdDispatcher == profile.DispatcherId).Select(item => item.Birthday).FirstOrDefault();
            //BirthdayBox.Content = Birthday.ToLongDateString();
            //string phone = cnt.db.Dispatcher.Where(item => item.IdDispatcher == profile.DispatcherId).Select(item => item.PhoneNumber).FirstOrDefault();
            //PhoneNumBox.Content = "+7(" + phone.Substring(0, 3) + ")" + phone.Substring(3, 3) + "-" + phone.Substring(6, 2) + "-" + phone.Substring(8, 2);
            //EmailBox.Content = cnt.db.Dispatcher.Where(item => item.IdDispatcher == profile.DispatcherId).Select(item => item.Email).FirstOrDefault();
        }
        private void EditImageButton_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.DefaultExt = ".png";
            //ofd.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            //Nullable<bool> result = ofd.ShowDialog();
            //if (result == true)
            //{
            //    string filename = ofd.FileName;
            //    ProfileImg.Source = new BitmapImage(new Uri(filename));
            //    Dispatcher dispatcher = cnt.db.Dispatcher.Where(item => item.IdDispatcher == profile.DispatcherId).FirstOrDefault();
            //    dispatcher.ProfileImgSource = filename;
            //    cnt.db.SaveChanges();
            //}
        }
        private void TestsLoading()
        {
            foreach (Tests test in cnt.db.Tests.Where(item => item.IdUser == Session.userId).ToList())
            {
                try
                {

                    AddTest(test.Name, test.Image, cnt.db.Questions.Where(item => item.IdTest == test.IdTest).Count());
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.ToString()).ShowDialog();
                }
            }
        } 
        private void AddTest(string name, BitmapImage image, int questCount)
        {
           
        }
    }
    class Test
    {
        public string Name { get; set; }
        public Image Image { get; set; }
        public int QuestionsCount { get; set; }
    }
}
