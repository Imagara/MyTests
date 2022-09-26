using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    // Страница с профилем
    public partial class ProfilePage : Page
    {
        static Users user;
        public ProfilePage(Users _user)
        {
            InitializeComponent();
            TestsListBox.Items.Clear(); // Очистка списка с тестами

            user = _user;

            //Заполняются данные из БД
            FIOLabel.Content = user.Surname + " " + user.Name + " " + user.Patronymic; // Заполнение ФИО из базы данных
            ProfileImage.Source = user.Image == null ?                                                     // Если НЕТ картинки у пользователя в базе данных, то
                new BitmapImage(new Uri("../Resources/StandartImage.png", UriKind.RelativeOrAbsolute)) :   // Устанавливается стандартная
                ImagesFunctions.NewImage(user); // Если есть - картинка устанавливается из БД

            EmailBox.Text = user.Email; // Заполняется электронная почта из БД
            InfoBox.Text = user.Info; // Заполняется информация из БД

            if (user != Session.User)                        //
            {                                                //
                EmailBox.IsEnabled = false;                  // Если пользователь не владелец профиля, то запрещается редактирование
                InfoBox.IsEnabled = false;                   //
                SaveButton.Visibility = Visibility.Collapsed;//
            }                                                //
            
            if (user.Post == "Преподаватель")// Если пользователь - преподаватель, то
                TestsLoading();              // загружается список тестов преподавателя
            else  // иначе
                TestsListBox.Visibility = Visibility.Collapsed; // список тестов скрывается
        }
        private void EditImage_Click(object sender, RoutedEventArgs e)
        {
            if (user == Session.User) // если пользователь - владелец
            {
                BitmapImage image = ImagesFunctions.SelectImage(); // Выбор картинки с компьютера пользователя
                if (image != null) // Если картинка выбрана
                {
                    ProfileImage.Source = image; // картинка профиля заменяется на выбранную
                    Session.User.Image = ImagesFunctions.BitmapSourceToByteArray((BitmapSource)ProfileImage.Source); // Замена картинки в базе данных в виде varbinary
                    cdb.db.SaveChanges(); // Сохранение данных
                }
            }
        }
        private void TestsLoading()
        {
            // Создание списка с тестами. Если профиль открыл владелец, то в список добавляются еще и скрытые тесты.
            List<Tests> list = user != Session.User ? cdb.db.Tests.Where(item => item.IdUser == user.IdUser && item.IsVisible == true).ToList() :
                cdb.db.Tests.Where(item => item.IdUser == user.IdUser).ToList();

            // Создание списка с типом TestsClass, в котором будут хранится тесты и их картинки
            List<TestsClass> testsList = new List<TestsClass>();
            // Берется каждый тест из списка с тестами
            foreach (Tests test in list)
            {
                TestsClass tc = new TestsClass();
                tc.test = test;
                tc.testImage = test.Image == null ? new BitmapImage(new Uri("../Resources/Approval.png", UriKind.RelativeOrAbsolute)) : ImagesFunctions.NewImage(test); // Если у теста нет картинки, то устанавливается стандартная. Иначе - картинка из БД
                testsList.Add(tc);// Добавление данных в список с тестами и картинками
            }
            // Добавление данных из списка с тестами и картинками в список для отображения (ListBox)
            TestsListBox.ItemsSource = testsList;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Functions.IsValidEmail(EmailBox.Text)) // Проверка на правильность ввода электронной почты
                new ErrorWindow("Email введен неверно.").Show(); // Вывод ошибки
            else if (Functions.IsEmailAlreadyTaken(EmailBox.Text) && EmailBox.Text != user.Email) // Проверка на уникальность электронной почты
                new ErrorWindow("Данный email уже используется.").Show(); // Вывод ошибки
            else
            {
                Session.User.Email = EmailBox.Text; // Замена электронной почты в БД на содержимое EmailBox
                Session.User.Info = InfoBox.Text;   // Замена информации в БД на содержимое InfoBox
                cdb.db.SaveChanges(); // Сохранение 
                new ErrorWindow("Успешно.").ShowDialog(); // Вывод диалогового окна
            }
        }

        private void CheckResultsButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender; // Создание кнопки, которая равна sender
            if (btn.DataContext is TestsClass) // Проверка на содержимое элемента ListBox
                NavigationService.Navigate(new CheckTestResultsCatalog(((TestsClass)btn.DataContext).test)); //Переход на страницу с каталогом результатов
        }
        private void EditTestButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;// Создание кнопки, которая равна sender
            if (btn.DataContext is TestsClass) // Проверка на содержимое элемента ListBox
                NavigationService.Navigate(new EditTestPage(((TestsClass)btn.DataContext).test)); // Переход на страницу с редактированием теста
        }

        private void TestsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (((Tests)TestsListBox.SelectedItem) != null)
                {
                    Session.OpenedTest = cdb.db.Tests.Where(item => item.IdTest == ((TestsClass)TestsListBox.SelectedItem).test.IdTest).FirstOrDefault();
                    Session.Points = 0;
                    Session.CurQuestion = 0;
                    Session.Quest.Content = cdb.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Content).ToArray();
                    Session.Quest.Answer = cdb.db.Questions.Where(item => item.IdTest == Session.OpenedTest.IdTest).Select(item => item.Answer).ToArray();

                    NavigationService.Navigate(new Pages.CurTestPage());
                }
            }
            catch
            {
                new ErrorWindow("Ошибка открытия теста.").ShowDialog();
            }
        }

        private void DeleteTestButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is TestsClass)
            {
                ConfirmationWindow confWindow = new ConfirmationWindow();
                confWindow.ShowDialog();
                if (confWindow.answer)
                {
                    foreach (Answers answer in cdb.db.Answers.Where(item => item.Questions.IdTest == ((TestsClass)btn.DataContext).test.IdTest))
                        cdb.db.Answers.Remove(answer);
                    foreach (Questions question in cdb.db.Questions.Where(item => item.IdTest == ((TestsClass)btn.DataContext).test.IdTest))
                        cdb.db.Questions.Remove(question);
                    cdb.db.Tests.Remove(((TestsClass)btn.DataContext).test);
                    cdb.db.SaveChanges();
                    TestsLoading();
                }
            }
        }
        public class TestsClass
        {
            public Tests test { get; set; }
            public BitmapImage testImage { get; set; }
        }
    }
}
