using System.Collections.Generic;
// using System.Web.Mvc;
// using System.Web.Security;
// using System.Web.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using qenergy.Models;
using qenergy.Services;

public class QuoteController : Controller
{
    AccountService _service;
    PricingService _pricingService;

    public QuoteController(AccountService service, PricingService pricingService)
    {
        _service = service;
        _pricingService = pricingService;
        
    }
  
    // GET: FuelDelivery/Create
    [HttpGet]
    public ActionResult Quote()
    {
        // Get the user's profile
        // Create a new fuel delivery request object

        // Get Session UserId variable
        int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
        User user = _service.GetUserById(userId);

        if (user == null)
        {
            // user not logged in, redirect to login
            return RedirectToAction("Login", "Account");
        }

        Quote q = new Quote();
        q.customerId = userId;
        q.DeliveryAddress = user.profile.Address1; // grab the user address etc....
        q.DeliveryDate = DateTime.Now;


        return View(q);
    }

    // POST: FuelDelivery/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Quote(Quote quote)
    {
        if (ModelState.IsValid)
        {
            // Calculate the total amount due
            quote.TotalAmountDue = quote.GallonsRequested * quote.SuggestedPricePerGallon;

            // use Session variable to add customerId to created quote
            quote.customerId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));

            // Save the fuel delivery request to the database
            _service.CreateQuote(quote);
            //quotes.Add(quote);

            // Redirect the user to a confirmation page
            return RedirectToAction("QuoteHistory", "Quote");
        }

        // If the model state is invalid, return the view with the original fuel delivery request object
        return View(quote);
    }

    [HttpPost]
    public JsonResult GetQuote(int gallons)
    {
        // Margin =  Current Price * (Location Factor - Rate History Factor + Gallons Requested Factor + Company Profit Factor)

        int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
        User user = _service.GetUserById(userId);

        double factor = 0.0;

        // company profit factor
        factor += 0.1;

        // location factor
        if (_pricingService.inTexas(user))
        {
            factor += 0.02;
        }
        else
        {
            factor += 0.04; // out of state
        }

        // rate history factor
        if (_pricingService.hasQuoteHistory(user))
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

       

        return Json( new { suggestedPrice, totalAmount });
    }

    [HttpGet]
    public ActionResult QuoteHistory()
    {
        //if (HttpContext.Session.GetString("Username") != null)
        //{
        //    ViewBag.User = HttpContext.Session.GetString("Username");

        //    IEnumerable<Quote>? quotesByUser = _service.GetAllQuotesByUser(Convert.ToInt32(HttpContext.Session.GetString("UserId")));

        //    if (quotesByUser.Any())
        //    {
        //        return View(quotesByUser.ToList());
        //    }

        //    // we must only show the quotes made by this user
        //    //return View(_service.GetAllQuotes());
        //}

        //return View();

        //// Pass the quotes list to the view
        //return View(_service.GetAllQuotes());

        int userId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
        User user = _service.GetUserById(userId);

        if (user == null)
        {
            // user not logged in, redirect to login
            return RedirectToAction("Login", "Account");
        }

        ViewBag.User = user.Username;

        IEnumerable<Quote>? quotesByUser = _service.GetAllQuotesByUser(userId);

        return View(quotesByUser?.ToList());
        
    }

    // GET: FuelDelivery/Confirmation
}
