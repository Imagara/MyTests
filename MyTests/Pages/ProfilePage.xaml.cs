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
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            TestsLoading();
            //EmailBox.Content = cnt.db.Dispatcher.Where(item => item.IdDispatcher == profile.DispatcherId).Select(item => item.Email).FirstOrDefault();
            //PhoneNumBox.Content = "+7(" + phone.Substring(0, 3) + ")" + phone.Substring(3, 3) + "-" + phone.Substring(6, 2) + "-" + phone.Substring(8, 2);
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
                    BitmapImage img = test.Image == null ?
                        new BitmapImage(new Uri("../Resources/StandartProfile.png", UriKind.RelativeOrAbsolute)) :
                        ImagesManip.NewImage(cnt.db.Users.Where(item => item.IdUser == Session.userId).FirstOrDefault());
                    AddTest(test.Name, img, cnt.db.Questions.Where(item => item.IdTest == test.IdTest).Count());
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.ToString()).ShowDialog();
                }
            }
        }
        private void AddTest(string name, BitmapImage image, int questCount)
        {
            //MessageBox.Show($"{name}, quests:, {questCount}");
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {

        }

        private void BackButton(object sender, RoutedEventArgs e)
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
