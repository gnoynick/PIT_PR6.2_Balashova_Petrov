using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest14     
    {
        [TestMethod]
        public void TestPhoneValidation()
        {
            var regPage = new RegPage();
            string error;

            Assert.IsFalse(regPage.Register("Григорьев Павел Анатольевич", "testuser1", "Pass123",
                          "Мужской", "Пользователь", "+7 (123", "", out error),
                          $"Ошибка: {error}");

            bool result = regPage.Register("Тимофеева Анна Дмитриевна", "testuser2", "Pass123",
                         "Женский", "Пользователь", "+7 (123) 456-78-90", "", out error);

            Assert.IsTrue(result, $"Ошибка при валидном телефоне: {error}");
        }
    }
}