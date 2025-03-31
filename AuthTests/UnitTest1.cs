using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;
using System;

namespace AuthTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var authPage = new AuthPage();

            Assert.IsFalse(authPage.Auth("wrong", "wrong")); 
            Assert.IsFalse(authPage.Auth("admin", "wrong")); 
            Assert.IsFalse(authPage.Auth("", ""));
        }
    }

    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void AuthTestSuccess()
        {
            var authPage = new AuthPage();

            Assert.IsTrue(authPage.Auth("admin", "admin123"));
            Assert.IsTrue(authPage.Auth("user1", "user123"));
            Assert.IsTrue(authPage.Auth("guest1", "guest123"));
        }
    }
}
