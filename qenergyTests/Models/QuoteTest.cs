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
    public class QuoteTest
    {
        [TestMethod]
        public void TestQuoteProperties()
        {
            // Arrange
            var quote = new Quote();

            // Act
            quote.GallonsRequested = 1000;
            quote.DeliveryAddress = "123 Main St";
            quote.DeliveryDate = new DateTime(2022, 1, 1);
            quote.SuggestedPricePerGallon = 1.75m;
            quote.TotalAmountDue = 1750.00m;
            quote.customerId = 1;

            // Assert
            Assert.AreEqual(1000, quote.GallonsRequested);
            Assert.AreEqual("123 Main St", quote.DeliveryAddress);
            Assert.AreEqual(new DateTime(2022, 1, 1), quote.DeliveryDate);
            Assert.AreEqual(1.75m, quote.SuggestedPricePerGallon);
            Assert.AreEqual(1750.00m, quote.TotalAmountDue);
            Assert.AreEqual(1, quote.customerId);
        }
    }
}