using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
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
}