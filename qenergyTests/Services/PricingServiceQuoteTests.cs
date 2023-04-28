using Microsoft.VisualStudio.TestTools.UnitTesting;
using qenergy.Models;
using qenergy.Services;
using qenergy.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qenergy.Tests.Mocks.Tests
{
    [TestClass()]
    public class PricingServiceQuoteTests
    {
        [TestMethod()]
        public void getQuotes()
        {
            // Arrange
            var user = new User
            {
                profile = new Profile
                {
                    State = "TX"
                }
            };
            var gallons = 1200;
            double expectedSuggestedPrice = 1.69;
            double expectedTotalAmount = 2034.00;
            var thing = new PricingServiceMock();

            // Act
            var result = thing.getQuotes(user, gallons);
            Console.WriteLine(result);
            var suggestedPrice = result.GetType().GetProperty("suggestedPrice").GetValue(result, null);
            var totalAmount = result.GetType().GetProperty("totalAmount").GetValue(result, null);

            // Assert
            Assert.AreEqual(expectedSuggestedPrice, (double)suggestedPrice, .02);
            Assert.AreEqual(expectedTotalAmount, (double)totalAmount, .02);
        }
    }
}