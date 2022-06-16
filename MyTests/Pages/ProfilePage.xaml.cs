﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    public partial class ProfilePage : Page
    {
        public static Users user;
        public ProfilePage(Users _user)
        {
            InitializeComponent();
            user = _user;
            UserName.Content = user.Login;
            if (user.Image == null)
                ProfileImage.Source = new BitmapImage(new Uri("../Resources/StandartImage.png", UriKind.RelativeOrAbsolute));
            else
                ProfileImage.Source = ImagesManip.NewImage(user);
            EmailBox.Text = user.Email;
            InfoBox.Text = user.Info;
            TestsLoading();
        }
        private void EditImage_Click(object sender, RoutedEventArgs e)
        {
            if(user == Session.User)
            {
                BitmapImage image = ImagesManip.SelectImage();
                if (image != null)
                {
                    ProfileImage.Source = image;
                    Session.User.Image = ImagesManip.BitmapSourceToByteArray((BitmapSource)ProfileImage.Source);
                    cnt.db.SaveChanges();
                }
            }
        }
        private void TestsLoading()
        {
            TestsListBox.Items.Clear();
            TestsListBox.ItemsSource = cnt.db.Tests.Where(item => item.IdUser == user.IdUser).ToList();
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            if (!Functions.IsValidEmail(EmailBox.Text))
                new ErrorWindow("Email введен неверно.").Show();
            else if (Functions.IsEmailAlreadyTaken(EmailBox.Text))
                new ErrorWindow("Данный email уже используется.").Show();
            else
            {
                Session.User.Email = EmailBox.Text;
                Session.User.Info = InfoBox.Text;
                cnt.db.SaveChanges();
                new ErrorWindow("Успешно.").ShowDialog();
            }
            
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {

        }
        private void TestsListBox_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((Tests)TestsListBox.SelectedItem) != null)
                {
                    Session.OpenedTest = cnt.db.Tests.Where(item => item.IdTest == ((Tests)TestsListBox.SelectedItem).IdTest).FirstOrDefault();
                    Session.Points = 0;
                    Session.CurQuestion = 0;
                    Session.Quest.Content = cnt.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Content).ToArray();
                    Session.Quest.Answer = cnt.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Answer).ToArray();

                    NavigationService.Navigate(new Pages.CurTestPage());
                }
            }
            catch
            {
                new ErrorWindow("Ошибка открытия теста.").ShowDialog();
            }
        }
    }
}
