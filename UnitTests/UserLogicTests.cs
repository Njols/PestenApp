using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Mocks;

namespace UnitTests
{
    [TestClass]
    public class UserLogicTests
    {
        MockUserProcessor userProcessor;
        UserLogic userLogic;
        [TestInitialize]
        public void TestInitialize ()
        {
            userProcessor = new MockUserProcessor();
            userLogic = new UserLogic(userProcessor);
        }
        [TestMethod]
        public void OnlyAddsUsersWithUniqueEmails ()
        {
            //arrange
            userLogic.TryToCreateUser("email@email.com", "hank", "password");
            //act
            bool hasAddedUser = userLogic.TryToCreateUser("email@email.com", "john", "pass123");
            //assert
            Assert.IsFalse(hasAddedUser);
        }
        [TestMethod]
        public void ReturnsTrueWhenCheckingCorrectPassword ()
        {
            //arrange
            userLogic.TryToCreateUser("ema@il.com", "user", "password");
            //act
            bool passwordIsCorrect = userLogic.PasswordMatches("ema@il.com", "password");
            //assert
            Assert.IsTrue(passwordIsCorrect);
        }
        [TestMethod]
        public void ReturnsFalseWhenCheckingCorrectPassword ()
        {
            //arrange
            userLogic.TryToCreateUser("e@mail.com", "user", "password");
            //act
            bool passwordIsCorrect = userLogic.PasswordMatches("e@mail.com", "pass123");
            //assert
            Assert.IsFalse(passwordIsCorrect);
        }
    }
}
