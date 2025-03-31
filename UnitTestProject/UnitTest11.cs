using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;

namespace UnitTestProject
{
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
}