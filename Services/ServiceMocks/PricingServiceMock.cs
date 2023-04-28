using qenergy.Models;
using qenergy.Services;


//THIS FILE WAS ADDED TO TEST THE GETQUOTES FUNCTION IN THE ORIGINAL FILE
namespace qenergy.Tests.Mocks
{
    public class PricingServiceMock : PricingService
    {
        public override bool hasQuoteHistory(User user)
        {
            return true; // always return true
        }

        public object getQuotes(User user, int gallons)
        {
            double factor = 0.0;
            double suggestedPrice = 0.0;
            double totalAmount = 0.0;

            // company profit factor
            factor += 0.1;

            // location factor
            if (inTexas(user))
            {
                factor += 0.02;
            }
            else
            {
                factor += 0.04; // out of state
            }

            // rate history factor
            if (hasQuoteHistory(user))
            {
                factor -= 0.01; // has history with us, no discount if its their first time
            }

            // gallons requested factor
            if (gallons > 1000)
            {
                factor += 0.02;
            }
            else
            {
                factor += 0.03;
            }

            double margin = factor * 1.5;
            suggestedPrice = 1.50 + margin;
            totalAmount = gallons * suggestedPrice;

            return new { suggestedPrice, totalAmount };
        }
    }
}