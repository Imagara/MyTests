using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace MyTests.Pages
{
    // Страница с редактированием/созданием теста
    public partial class EditTestPage : Page
    {
        Tests test;
        Questions selected;
        public EditTestPage(Tests _test = null)
        {
            InitializeComponent();

            if (_test == null) // Создание теста
            {
                try
                {
                    // Получение максимального id теста и добавления к нему 1
                    int testId = cdb.db.Tests.Select(p => p.IdTest).DefaultIfEmpty(0).Max() + 1;

                    // Создание теста
                    Tests newTest = new Tests()
                    {
                        IdTest = testId,
                        IdUser = Session.User.IdUser,
                        Name = $"Новый тест {cdb.db.Tests.Where(item => item.IdUser == Session.User.IdUser).Count() + 1}",
                        IsAnswersVisible = false,
                        IsVisible = false,
                        CanAgain = false
                    };

                    // Добавление теста в базу данных
                    cdb.db.Tests.Add(newTest);
                    cdb.db.SaveChanges();

                    // Выбор созданного теста
                    test = cdb.db.Tests.Where(item => item.IdTest == testId).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    new ErrorWindow(ex.Message);
                }
            }
            else // Редактирование теста
            {
                test = _test; // Выбор теста

                // Замена состояний CheckBox
                IsVisibleCB.IsChecked = test.IsVisible;
                IsAnswersVisibleCB.IsChecked = test.IsAnswersVisible;
                CanAgainCB.IsChecked = test.CanAgain;
            }

            // Заполнение верхней панели
            TestNameBox.Text = test.Name; // Заполнение названия теста
            TestImg.Source = test.Image == null ?                                                   // Проверка на существование картинки у теста
                new BitmapImage(new Uri("../Resources/Approval.png", UriKind.RelativeOrAbsolute)) : // в базе данных. Если существует, то устанавливается на ту, что в базе данных
                ImagesFunctions.NewImage(_test);                                                    // Если не существует, то устанавливается стандартная

            // Очистка списка с вопросами
            QuestionsListBox.Items.Clear();
            // Вызов метода с обновлением вопросов
            QuestionsUpdate();
        }
        private void QuestionsUpdate()
        {
            // Заполнение списка с вопросами данными из базы данных
            QuestionsListBox.ItemsSource = cdb.db.Questions.Where(item => item.IdTest == test.IdTest).ToList();
        }

        private void QuestionsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Заполнение выбранного элемента списка с вопросамми
            selected = (Questions)QuestionsListBox.SelectedItem;
        }

        private void AddQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            // Открытие окна с добавлением вопроса
            new QuestionAddToTestWindow(test).ShowDialog();
            // Вызов метода с обновлением вопросов
            QuestionsUpdate();
        }
        private void DeleteQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (selected != null) // Проверка, выбран ли элемент списка с вопросами
            {
                // Открытие окна с подтверждением действия
                ConfirmationWindow confWindow = new ConfirmationWindow();
                confWindow.ShowDialog();
                if (confWindow.answer) // Если ответ "Да"
                {
                    try
                    {
                        // Проход по всем ответам пользователей выбранного вопроса
                        foreach (Answers answer in cdb.db.Answers.Where(item => item.IdQuestion == selected.IdQuestion))
                            cdb.db.Answers.Remove(answer); // Удаление всех ответов пользователей, которые прошли этот вопрос.
                        cdb.db.Questions.Remove(selected); // Удаление вопроса
                        cdb.db.SaveChanges(); // Сохранение
                    }
                    catch (Exception ex)
                    {
                        new ErrorWindow(ex.Message); // Вывод ошибки, если она есть
                    }
                }
                // Вызов метода с обновлением вопросов
                QuestionsUpdate();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Возврат на прошлую страницу
            NavigationService.GoBack();
        }

        private void TestImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Выбор картинки с компьютера пользователя
            BitmapImage image = ImagesFunctions.SelectImage();
            if (image != null) // Если картинка выбрана
            {
                TestImg.Source = image; // Замена картинки теста на выбранную
                test.Image = ImagesFunctions.BitmapSourceToByteArray((BitmapSource)TestImg.Source); // Замена картинки в базе данных в виде varbinary
                cdb.db.SaveChanges(); // Сохранение
            }
        }

        private void TestNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            test.Name = TestNameBox.Text; // Замена названия теста в базе данных на содержимое TestNameBox
        }

        private void SaveInfoCB(object sender, RoutedEventArgs e)
        {
            test.IsVisible = IsVisibleCB.IsChecked == true; // замена IsVisible в базе данных на содержимое IsVisibleCB
            test.IsAnswersVisible = IsAnswersVisibleCB.IsChecked == true; // замена IsAnswersVisible в базе данных на содержимое IsAnswersVisibleCB
            test.CanAgain = CanAgainCB.IsChecked == true; // замена CanAgain в базе данных на содержимое CanAgainCB
            cdb.db.SaveChanges(); // сохранение данных
        }
    }
}
