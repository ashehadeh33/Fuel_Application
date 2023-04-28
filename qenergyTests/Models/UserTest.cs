using Microsoft.VisualStudio.TestTools.UnitTesting;
using qenergy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qenergy.Models.Tests
{
    [TestClass()]
    public class UserTest
    {
        [TestMethod]
        public void TestUserProperties()
        {
            // Arrange
            var user = new User { Id = 1, Username = "johndoe", Password = "password", profile = null };

            // Act
            int id = user.Id;
            string username = user.Username;

            // Assert
            Assert.AreEqual(1, id);
            Assert.AreEqual("johndoe", username);
        }
    }
}