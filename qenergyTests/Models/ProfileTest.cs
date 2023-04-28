using Microsoft.VisualStudio.TestTools.UnitTesting;
using qenergy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qenergy.Models.Tests
{
    [TestClass()]
    public class ProfileTest
    {
        [TestMethod]
        public void FullNameIsEmpty()
        {
            // Arrange
            var profile = new Profile();

            // Act
            profile.FullName = "";

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(profile.FullName));
        }

        [TestMethod]
        public void FullNameIsNotEmpty()
        {
            // Arrange
            var profile = new Profile();

            // Act
            profile.FullName = "Brandon Won";

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(profile.FullName));
        }

        [TestMethod]
        public void AddressisPresent()
        {
            // Arrange
            var profile = new Profile();

            // Act
            profile.Address1 = "123 main st";

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(profile.Address1));
        }

        [TestMethod]
        public void CityisPresent()
        {
            // Arrange
            var profile = new Profile();

            // Act
            profile.City = "Houston";

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(profile.City));
        }

        [TestMethod]
        public void StateisPresent()
        {
            // Arrange
            var profile = new Profile();

            // Act
            profile.State = "TX";

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(profile.State));
        }

        [TestMethod]
        public void ZipisPresent()
        {
            // Arrange
            var profile = new Profile();

            // Act
            profile.Zipcode = "77498";

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(profile.Zipcode));
        }

        [TestMethod]
        public void ZipRequiredMessage()
        {
            // Arrange
            var profile = new Profile();
            var validationContext = new ValidationContext(profile, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(profile, validationContext, validationResults, validateAllProperties: true);

            // Assert
            Assert.AreEqual("The user field is required.", validationResults[0].ErrorMessage);
        }

        [TestMethod]
        public void ZipRegexTest()
        {
            // Arrange
            var profile = new Profile
            {
                FullName = "John Doe",
                Address1 = "123 Main St",
                City = "Anytown",
                State = "CA",
                Zipcode = "1234" 
            };

            // Act
            var results = new List<ValidationResult>();
            var context = new ValidationContext(profile);
            var isValid = Validator.TryValidateObject(profile, context, results, true);

            // Assert
            Assert.IsFalse(isValid);
            //Console.WriteLine(results[1]);
            Assert.AreEqual("Please enter 5-9 digits", results[1].ErrorMessage);
        }


    }
}
