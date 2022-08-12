using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyTests;
using MyTests.Pages;
using System.Linq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsValidEmail()
        {
            Assert.IsTrue(Functions.IsValidEmail("Dalwpx@mail.ru"));
            Assert.IsFalse(Functions.IsValidEmail("usermail.com"));
            Assert.IsFalse(Functions.IsValidEmail("usermailcom"));
            Assert.IsFalse(Functions.IsValidEmail(""));
        }
        [TestMethod]
        public void PasswordEncryptTest()
        {
            string password = "qq";
            string expected = "BED4EB698C6EEEA7F1DDF5397D480D3F2C0FB938";
            Assert.AreEqual(Functions.EncryptPassword(password), expected);
        }
        [TestMethod]
        public void LoginTest()
        {
            string login = "Dalwpx";
            string password = "mEme3";
            Assert.IsTrue(Functions.LoginCheck(login, password));
        }
        [TestMethod]
        public void IsValidLoginAndPassword()
        {
            Assert.IsTrue(Functions.IsValidLogAndPass("Dalwpx", "mEme3"));
            Assert.IsTrue(Functions.IsValidLogAndPass("Login???", "p@ssw0rd"));
            Assert.IsFalse(Functions.IsValidLogAndPass("", ""));
            Assert.IsFalse(Functions.IsValidLogAndPass("", "SimplePass"));
            Assert.IsFalse(Functions.IsValidLogAndPass("SimpleLogin", ""));
        }
        [TestMethod]
        public void IsLoginAlreadyTaken()
        {
            Assert.IsTrue(Functions.IsLoginAlreadyTaken("Dalwpx"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken("SimpleLogin"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken("Login?"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken(""));
        }
        [TestMethod]
        public void IsEmailAlreadyTaken()
        {
            Assert.IsTrue(Functions.IsEmailAlreadyTaken("Dalwpx@gmail.com"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("user3@mail.ru"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("user42@gmail.com"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("s0mpleEmail@sibmail.com"));
        }
        [TestMethod]
        public void IsLogEqualPass()
        {
            Assert.IsFalse(Functions.IsLogEqualPass("Dalwpx", "Dalwpx"));
            Assert.IsTrue(Functions.IsLogEqualPass("Dalwpx", "mEme3"));
        }
        [TestMethod]
        public void IsValidLength()
        {
            Assert.IsTrue(Functions.IsValidLength("Dalwpx"));
            Assert.IsTrue(Functions.IsValidLength("srwerwewe"));
            Assert.IsFalse(Functions.IsValidLength("Вф"));
            Assert.IsFalse(Functions.IsValidLength(""));
        }
        public void CorrectAnswersCounterTest()
        {
            int testId = 1;
            int userId = 1;
            int expected = 1;
            
            Assert.AreEqual(new CheckTestResultsCatalog(null).CorrectAnswersCounter(cdb.db.Tests.Where(item => item.IdTest == testId).FirstOrDefault(),
                cdb.db.Users.Where(item => item.IdUser == userId).FirstOrDefault()),
                expected); 
        }
    }
}