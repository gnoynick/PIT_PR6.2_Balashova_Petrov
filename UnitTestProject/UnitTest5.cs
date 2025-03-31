using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
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
}