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
    }
}
