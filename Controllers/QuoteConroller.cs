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

    public QuoteController(AccountService service)
    {
        _service = service;
        
    }


    public static List<Quote> quotes = new List<Quote>
    {
        new Quote { GallonsRequested = 20, DeliveryAddress = "234 St Mark", DeliveryDate = DateTime.Parse("2/11/2023"), SuggestedPricePerGallon = 14, TotalAmountDue= 126},
        new Quote { GallonsRequested = 40, DeliveryAddress = "8693 St Laurent", DeliveryDate = DateTime.Parse("2/11/2022"), SuggestedPricePerGallon = 36, TotalAmountDue= 477},
    };
  
    // GET: FuelDelivery/Create
    [HttpGet]
    public ActionResult Quote()
    {
        // Get the user's profile
        // Create a new fuel delivery request object

        // Pass the fuel delivery request object to the view
        Quote q = new Quote();
        q.DeliveryAddress = "123 Main St";
        q.TotalAmountDue = 20;
        q.SuggestedPricePerGallon = 4;
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

            
            // Save the fuel delivery request to the database
            quotes.Add(quote);

            // Redirect the user to a confirmation page
            return RedirectToAction("QuoteHistory", "Quote");
        }

        // If the model state is invalid, return the view with the original fuel delivery request object
        return View(quote);
    }

    [HttpGet]
    public ActionResult QuoteHistory()
    {
        // Pass the quotes list to the view
        return View(_service.GetAllQuotes());
    }

    // GET: FuelDelivery/Confirmation
}
