using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1  
    {
        [TestMethod]
        public void BasicAssertionsTest()
        {
            int expected = 10;
            int actual = 10;

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actual == expected);
            Assert.IsFalse(actual != expected);
            Assert.IsNotNull(actual);
        }
    }

    [TestClass]
    public class UnitTest2  // Тесты авторизации (неудачные случаи)
    {
        [TestMethod]
        public void AuthTestFailureCases()
        {
            var authPage = new AuthPage();

            Assert.IsFalse(authPage.Auth("wrong", "wrong"));
            Assert.IsFalse(authPage.Auth("admin", "wrong"));
            Assert.IsFalse(authPage.Auth("", ""));
            Assert.IsFalse(authPage.Auth(null, null));
            Assert.IsFalse(authPage.Auth("user1", ""));
            Assert.IsFalse(authPage.Auth("", "user123"));
        }
    }

    [TestClass]
    public class UnitTest3  // Тесты авторизации (успешные случаи)
    {
        [TestMethod]
        public void AuthTestSuccessCases()
        {
            var authPage = new AuthPage();

            Assert.IsTrue(authPage.Auth("admin", "admin123"));
            Assert.IsTrue(authPage.Auth("manager1", "mng123"));
            Assert.IsTrue(authPage.Auth("manager2", "mng456"));
            Assert.IsTrue(authPage.Auth("user1", "user123"));
            Assert.IsTrue(authPage.Auth("user2", "user456"));
            Assert.IsTrue(authPage.Auth("user3", "user789"));
            Assert.IsTrue(authPage.Auth("guest1", "guest123"));
            Assert.IsTrue(authPage.Auth("guest2", "guest456"));
            Assert.IsTrue(authPage.Auth("test1", "test123"));
            Assert.IsTrue(authPage.Auth("test2", "test456"));
            Assert.IsTrue(authPage.Auth("dev1", "dev123"));
            Assert.IsTrue(authPage.Auth("dev2", "dev456"));
        }
    }

    [TestClass]
    public class UnitTest4  // Очистка полей после авторизации
    {
        [TestMethod]
        public void TestClearFieldsAfterAuth()
        {
            var authPage = new AuthPage();
            authPage.TextBoxLogin.Text = "admin";
            authPage.PasswordBox.Text = "admin123";

            authPage.Auth("admin", "admin123");

            Assert.AreEqual("", authPage.TextBoxLogin.Text);
            Assert.AreEqual("", authPage.PasswordBox.Text);
            Assert.AreEqual("", authPage.CaptchaInput.Text);
        }
    }

    [TestClass]
    public class UnitTest5  // Генерация капчи
    {
        [TestMethod]
        public void CaptchaGenerationTest()
        {
            var authPage = new AuthPage();

            Assert.IsNotNull(authPage.currentCaptcha);
            Assert.AreEqual(6, authPage.currentCaptcha.Length);

            string firstCaptcha = authPage.currentCaptcha;
            authPage.GenerateCaptcha();
            string secondCaptcha = authPage.currentCaptcha;
            Assert.AreNotEqual(firstCaptcha, secondCaptcha);
        }
    }

    [TestClass]
    public class UnitTest6  // Видимость капчи
    {
        [TestMethod]
        public void CaptchaVisibilityTest()
        {
            var authPage = new AuthPage();

            Assert.AreEqual(Visibility.Collapsed, authPage.CaptchaPanel.Visibility);

            for (int i = 0; i < 3; i++)
            {
                authPage.Auth("wrong", "wrong");
            }
            Assert.AreEqual(Visibility.Visible, authPage.CaptchaPanel.Visibility);

            authPage.Auth("admin", "admin123", authPage.currentCaptcha);
            Assert.AreEqual(Visibility.Collapsed, authPage.CaptchaPanel.Visibility);
        }
    }

    [TestClass]
    public class UnitTest7  // Проверка капчи
    {
        [TestMethod]
        public void CaptchaValidationTest()
        {
            var authPage = new AuthPage();

            for (int i = 0; i < 3; i++)
            {
                authPage.Auth("wrong", "wrong");
            }

            Assert.IsFalse(authPage.Auth("admin", "admin123", ""));
            Assert.IsFalse(authPage.Auth("admin", "admin123", "WRONG"));
            Assert.IsFalse(authPage.Auth("wrong", "wrong", authPage.currentCaptcha));
            Assert.IsTrue(authPage.Auth("admin", "admin123", authPage.currentCaptcha));
        }
    }

    [TestClass]
    public class UnitTest8  // Счетчик неудачных попыток
    {
        [TestMethod]
        public void FailedAttemptsCounterTest()
        {
            var authPage = new AuthPage();

            Assert.AreEqual(0, authPage.failedAttempts);

            authPage.Auth("wrong", "wrong");
            Assert.AreEqual(1, authPage.failedAttempts);

            authPage.Auth("wrong", "wrong");
            Assert.AreEqual(2, authPage.failedAttempts);

            authPage.Auth("wrong", "wrong");
            Assert.AreEqual(3, authPage.failedAttempts);
            Assert.AreEqual(Visibility.Visible, authPage.CaptchaPanel.Visibility);

            authPage.Auth("admin", "admin123", authPage.currentCaptcha);
            Assert.AreEqual(0, authPage.failedAttempts);
        }
    }

    [TestClass]
    public class UnitTest9  // Авторизация с требованием капчи
    {
        [TestMethod]
        public void AuthWithCaptchaRequired()
        {
            var authPage = new AuthPage();

            for (int i = 0; i < 3; i++)
            {
                authPage.Auth("wrong", "wrong");
            }

            Assert.IsFalse(authPage.Auth("admin", "admin123"));
            Assert.IsFalse(authPage.Auth("admin", "admin123", "wrongcaptcha"));

            string correctCaptcha = authPage.currentCaptcha;
            Assert.IsTrue(authPage.Auth("admin", "admin123", correctCaptcha));
        }
    }

    [TestClass]
    public class UnitTest10  // Повторное появление капчи
    {
        [TestMethod]
        public void TestCaptchaReappearance()
        {
            var authPage = new AuthPage();

            for (int i = 0; i < 3; i++) authPage.Auth("wrong", "wrong");
            Assert.AreEqual(Visibility.Visible, authPage.CaptchaPanel.Visibility);

            authPage.Auth("admin", "admin123", authPage.currentCaptcha);
            Assert.AreEqual(Visibility.Collapsed, authPage.CaptchaPanel.Visibility);

            for (int i = 0; i < 3; i++) authPage.Auth("wrong", "wrong");
            Assert.AreEqual(Visibility.Visible, authPage.CaptchaPanel.Visibility);
        }
    }

    [TestClass]
    public class UnitTest11  // Регистрация с пустыми полями
    {
        [TestMethod]
        public void RegTestEmptyFields()
        {
            var regPage = new RegPage();
            string error;

            Assert.IsFalse(regPage.Register("", "", "", "", "", "", "", out error));
            Assert.IsFalse(regPage.Register("Смирнов Алексей", "", "", "", "", "", "", out error));
            Assert.IsFalse(regPage.Register("", "login", "", "", "", "", "", out error));
            Assert.IsFalse(regPage.Register("", "", "pass", "", "", "", "", out error));
        }
    }

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