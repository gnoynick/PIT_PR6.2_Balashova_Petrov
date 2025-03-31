using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest15  // Обработка URL фото
    {
        [TestMethod]
        public void TestPhotoUrlHandling()
        {
            var regPage = new RegPage();
            string error;

            bool emptyUrlResult = regPage.Register("Максимов Илья Сергеевич", "testuser1", "Pass123",
                                 "Мужской", "Пользователь", "+7 (111) 222-33-44", "", out error);
            Assert.IsTrue(emptyUrlResult, $"Ошибка при пустом URL: {error}");

            Assert.IsFalse(regPage.Register("Соколова Виктория Андреевна", "testuser2", "Pass123",
                          "Женский", "Пользователь", "+7 (222) 333-44-55", "invalid_url", out error),
                          $"Ожидалась ошибка при невалидном URL, но её нет");

            bool validUrlResult = regPage.Register("Данилов Артур Романович", "testuser3", "Pass123",
                                 "Мужской", "Пользователь", "+7 (333) 444-55-66", "https://example.com/photo.jpg", out error);
            Assert.IsTrue(validUrlResult, $"Ошибка при валидном URL: {error}");
        }
    }
}