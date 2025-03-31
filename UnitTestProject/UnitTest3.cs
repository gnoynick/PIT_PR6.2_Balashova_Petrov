using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
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
}