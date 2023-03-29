// created during Database seeding

using qenergy.Models;

namespace qenergy.Data
{
    // both DbInitializer class and Initialize method are defined as static
    public static class DbInitializer
    {
        public static void Initialize(qEnergyContext context) // accepts a PizzaContext as a parameter
        {

            if (context.Users.Any()
                && context.Quotes.Any())
            {
                return; // DB has been seeded
                        // if there are no records in any of the 2 tables, Users or Quotes, then this is false and we create this "test/default" contents
            }

            var adminUser = new User
            {
                Id = 1,
                Username = "Admin",
                Password = "Password",
                profile = new Profile
                {
                    FullName = "qEnergy Admin",
                    Address1 = "123 Sesame St.",
                    City = "Houston",
                    State = "TX",
                    Zipcode = "77582"
                }
            };
            var andresUser = new User
            {
                Id = 2,
                Username = "Andres",
                Password = "Password",
                profile = new Profile
                {
                    FullName = "Andres Flores",
                    Address1 = "123 Chicago Dr.",
                    City = "Corpus Christi",
                    State = "TX",
                    Zipcode = "78418"
                }
            };

            Quote adminQuote = new Quote
            {
                Id = 1,
                customerId = 1,
                GallonsRequested = 12,
                DeliveryAddress = adminUser.profile.Address1,
                DeliveryDate = new DateTime(2023, 3, 28),
                SuggestedPricePerGallon = 2.99M,
                TotalAmountDue = 12 * 2.99M, // not sure how to grab GallonsRequested and Price
            };
            Quote andresQuote = new Quote
            {
                Id = 2,
                customerId = 2,
                GallonsRequested = 3,
                DeliveryAddress = andresUser.profile.Address1,
                DeliveryDate = new DateTime(2023, 3, 30),
                SuggestedPricePerGallon = 1.99M,
                TotalAmountDue = 3 * 1.99M, // not sure how to grab GallonsRequested and Price
            };
            Quote andresQuote2 = new Quote
            {
                Id = 3,
                customerId = 2,
                GallonsRequested = 4,
                DeliveryAddress = andresUser.profile.Address1,
                DeliveryDate = new DateTime(2023, 3, 31),
                SuggestedPricePerGallon = 1.99M,
                TotalAmountDue = 4 * 1.99M, // not sure how to grab GallonsRequested and Price
            };

            var users = new User[]
            {
                adminUser, andresUser
            };

            var quotes = new Quote[]
            {
                adminQuote, andresQuote, andresQuote2
            };

            context.Users.AddRange(users); // the User objects (and their profile navigation properties) are added to the object graph with AddRange
            context.Quotes.AddRange(quotes); // the Quote objects are added to the object graph
            context.SaveChanges(); // object graph changes are committed to the database with SaveChangesvisu
        }
    }
}
