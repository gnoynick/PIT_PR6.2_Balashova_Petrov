using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
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
}