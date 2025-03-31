using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
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
}