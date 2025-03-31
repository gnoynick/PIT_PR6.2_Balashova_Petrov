using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest13  // Успешная регистрация
    {
        [TestMethod]
        public void RegTestSuccess()
        {
            var regPage = new RegPage();
            string error;

            Assert.IsTrue(regPage.Register("Козлов Андрей Петрович", "newuser1", "Pass123", "Мужской", "Пользователь", "+7(123)456-78-90", "http://example.com/photo.jpg", out error));
            Assert.IsTrue(regPage.Register("Федорова Мария Игоревна", "newuser2", "Pass456", "Женский", "Менеджер", "+7(987)654-32-10", "", out error));
            Assert.IsTrue(regPage.Register("Белов Денис Олегович", "newuser3", "Pass789", "Мужской", "Администратор", "+7(555)123-45-67", "https://example.com/image.png", out error));
        }
    }
}