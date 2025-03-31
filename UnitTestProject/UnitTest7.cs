using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
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
}