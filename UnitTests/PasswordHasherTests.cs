using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class PasswordHasherTests
    {
        [TestMethod]
        public void ReturnsTrueWhenComparingCorrectPassword()
        {
            //arrange
            string testPassword = "password";
            PasswordHasher hasher = new PasswordHasher(testPassword);
            byte[] hashedPassword = hasher.ToArray();
            PasswordHasher newHasher = new PasswordHasher(hashedPassword);
            //act
            bool passwordIsCorrect = newHasher.Verify(testPassword);
            //assert
            Assert.IsTrue(passwordIsCorrect);
        }
        [TestMethod]
        public void ReturnsFalseWhenComparingFalsePassword()
        {
            //arrange
            string correctPassword = "password";
            string falsePassword = "password123";
            PasswordHasher hasher = new PasswordHasher(correctPassword);
            byte[] hashedCorrectPassword = hasher.ToArray();
            PasswordHasher newHasher = new PasswordHasher(hashedCorrectPassword);
            //act
            bool passwordIsCorrect = newHasher.Verify(falsePassword);
            //assert
            Assert.IsFalse(passwordIsCorrect);
        }
    }
}
