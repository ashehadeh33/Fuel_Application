using qenergy.Data;
using qenergy.Models;

namespace qenergy.Services
{
    public class PricingService
    {
        private readonly AccountService _service;
        public PricingService(AccountService service) // inject AccountService
        {
            _service = service;
        }

        public bool hasQuoteHistory(User user)
        {
            if (_service.GetAllQuotesByUser(user.Id).Count() > 0) // has quotes
            {
                return true;
            }
            return false;
        }
        
        public bool inTexas(User user)
        {
            if (user.profile.State == "TX")
            {
                return true;
            }
            return false;
        }

        public object getQuotes(User user, int gallons)
        {
            double factor = 0.0;

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
            double suggestedPrice = 1.50 + margin;
            double totalAmount = gallons * suggestedPrice;
            totalAmount = Math.Round(totalAmount, 2);

            return new { suggestedPrice, totalAmount };
        }
    }
}
