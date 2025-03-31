using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1  
    {
        [TestMethod]
        public void BasicAssertionsTest()
        {
            int expected = 10;
            int actual = 10;

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actual == expected);
            Assert.IsFalse(actual != expected);
            Assert.IsNotNull(actual);
        }
    }
}