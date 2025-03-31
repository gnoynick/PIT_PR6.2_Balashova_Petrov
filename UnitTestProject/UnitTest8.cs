using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;
using System.Windows;

namespace UnitTestProject
{
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
}