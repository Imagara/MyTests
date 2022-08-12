using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MyTests
{
    public class Functions
    {
        // Валидация логина и пароля при входе
        public static bool IsValidLogAndPass(string login, string password)
        {
            if (login.Trim() == "" || password.Trim() == "")
                return false;
            else
                return true;
        }
        // Валидация логина и пароля
        public static bool IsLogEqualPass(string login, string password)
        {
            if (login == password)
                return false;
            else
                return true;
        }
        // Валидация логина и пароля
        public static bool IsValidLength(string str)
        {
            if (str.Length < 5)
                return false;
            else
                return true;
        }
        // Проверка на правильность введеных данных при входе
        public static bool LoginCheck(string login, string password)
        {
            //if (cnt.db.Users.Select(item => item.Login + item.Password).Contains(login + Encrypt.GetHash(password)))
                return true;
            //else
            //    return false;
        }
        // Проверка на уникальность логина
        public static bool IsLoginAlreadyTaken(string login)
        {
            return cdb.db.Users.Select(item => item.Login).Contains(login);
        }
        // Валидация электронной почты
        public static bool IsValidEmail(string email)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                return true;
            else
                return false;
        }
        // Проверка на уникальность электронной почты
        public static bool IsEmailAlreadyTaken(string Email)
        {
            return cdb.db.Users.Select(item => item.Email).Contains(Email);
        }
        // Шифрование пароля
        public static string EncryptPassword(string password)
        {
            using (var hash = SHA1.Create())
            {
                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));
            }
        }
        public static int CorrectAnswersCounter(Tests test, Users user)
        {
            Quest.Answer = cdb.db.Questions.Where(item => item.IdTest == test.IdTest).Select(it => it.Answer).ToArray();
            Quest.UserAnswer = cdb.db.Answers.Where(item => item.Users.IdUser == user.IdUser && item.Questions.IdTest == test.IdTest).Select(it => it.Answer).ToArray();
            int value = 0;
            if (Quest.Answer.Length == Quest.UserAnswer.Length)
                for (int i = 0; i < Quest.Answer.Length; i++)
                    if (Quest.Answer[i] == Quest.UserAnswer[i])
                        value++;
            return value;
        }
        public static class Quest
        {
            public static string[] Answer;
            public static string[] UserAnswer;
        }
    }
}
