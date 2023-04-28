using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using qenergy.Models;


namespace qenergy.Services.Tests
{
    [TestClass]
    public class PricingServiceTests
    {
        [TestMethod]
        public void inTexasTrueTest() //test when 
        {
            // Arrange
            var user = new User
            {
                profile = new Profile
                {
                    State = "TX"
                }
            };

            var thing = new PricingService();

            // Act
            var result = thing.inTexas(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void inTexasFalseTest()
        {
            // Arrange
            var user = new User
            {
                profile = new Profile
                {
                    State = "CA"
                }
            };

            var thing = new PricingService();

            // Act
            var result = thing.inTexas(user);

            // Assert
            Assert.IsFalse(result);
        }
    }
}