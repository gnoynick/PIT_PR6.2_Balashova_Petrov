using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest17  // Сложность пароля
    {
        [TestMethod]
        public void TestPasswordComplexity()
        {
            var regPage = new RegPage();
            string error;

            bool noDigitsResult = regPage.Register("Степанов Артем Игоревич",
                                 "usernodigits", "Password",
                                 "Мужской", "Пользователь", "+7 (111) 222-33-44", "", out error);
            Assert.IsFalse(noDigitsResult, "Ожидалась ошибка при пароле без цифр");

            bool noLettersResult = regPage.Register("Васильева Татьяна Олеговна",
                                 "userpwnoletters", "123456",
                                 "Женский", "Пользователь", "+7 (222) 333-44-55", "", out error);
            Assert.IsFalse(noLettersResult, "Ожидалась ошибка при пароле без букв");

            bool validPassResult = regPage.Register("Филиппов Алексей Дмитриевич",
                                 "userpwvalid", "Pass123",
                                 "Мужской", "Пользователь", "+7 (333) 444-55-66", "", out error);
            Assert.IsTrue(validPassResult, $"Ошибка при валидном пароле: {error}");
        }
    }
}