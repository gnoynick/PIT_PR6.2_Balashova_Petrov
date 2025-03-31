﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIT_PR6._2_Balashova_Petrov;
using System.Windows;

namespace UnitTestProject
{
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
}