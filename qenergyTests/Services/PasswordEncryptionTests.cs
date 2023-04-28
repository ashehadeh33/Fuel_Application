using Microsoft.VisualStudio.TestTools.UnitTesting;
using qenergy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qenergy.Services.Tests
{
    [TestClass()]
    public class PasswordEncryptionTests
    {
        [TestMethod()]
        public void getHashTest()
        {
            // Arrange
            string input = "password123";
            string expectedOutput = "ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f";

            // Act
            string actualOutput = PasswordEncryption.getHash(input);

            // Assert
            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}