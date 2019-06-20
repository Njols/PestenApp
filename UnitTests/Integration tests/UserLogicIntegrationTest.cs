using DataLibrary.DataAccess;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Integration_tests
{
    [TestClass]
    public class UserLogicIntegrationTest
    {
        private UserLogic _userLogic;
        [TestInitialize]
        public void TestInitialize ()
        {
            _userLogic = new UserLogic(new UserProcessor("Server = mssql.fhict.local; Database = dbi412124; User Id = dbi412124; Password = wachtwoord;"));
        }
        [TestMethod]
        public void CanLogOnWithCorrectInfo()
        {
            //arrange
            string username = "testUsername";
            string password = "testPassword";
            //act
            bool passwordMatches = _userLogic.PasswordMatches(username, password);
            //assert
            Assert.IsTrue(passwordMatches);
        }
        [TestMethod]
        public void CannotLoginWithIncorrectInfo ()
        {
            //arrange
            string username = "wrongUsername";
            string password = "wrongPassword";
            //act
            bool passwordMatches = _userLogic.PasswordMatches(username, password);
            //assert
            Assert.IsFalse(passwordMatches);
        }
    }
}
