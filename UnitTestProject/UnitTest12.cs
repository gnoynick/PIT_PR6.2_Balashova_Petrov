using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest12  // Регистрация с неверными данными
    {
        [TestMethod]
        public void RegTestInvalidData()
        {
            var regPage = new RegPage();
            string error;

            Assert.IsFalse(regPage.Register("Кузнецов Дмитрий", "log", "pass", "Мужской", "Пользователь", "123", "", out error));
            Assert.IsFalse(regPage.Register("Волкова Екатерина Сергеевна", "log", "pass", "Женский", "Пользователь", "+7(123)456-78-90", "invalid", out error));
            Assert.IsFalse(regPage.Register("Новиков Артем Викторович", "login", "password", "Мужской", "Пользователь", "+7(123)456-78-90", "", out error));
        }
    }
}