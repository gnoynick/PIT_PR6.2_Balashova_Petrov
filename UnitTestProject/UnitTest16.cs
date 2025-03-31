using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest16  // Дублирование регистрации
    {
        [TestMethod]
        public void TestDuplicateRegistration()
        {
            var regPage = new RegPage();
            string error;

            Assert.IsTrue(regPage.Register("Орлов Константин Владимирович", "uniqueuser", "Pass123",
                         "Мужской", "Пользователь", "+7 (123) 456-78-90", "", out error));

            Assert.IsFalse(regPage.Register("Зайцева Елена Петровна", "uniqueuser", "Pass456",
                          "Женский", "Менеджер", "+7 (987) 654-32-10", "", out error));
            Assert.AreEqual("Такой логин уже существует! Пожалуйста, выберите другой.", error);
        }
    }
}