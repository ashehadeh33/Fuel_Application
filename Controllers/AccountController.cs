using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using qenergy.Models;
using qenergy.Services;

namespace qenergy.Controllers
{
    public class AccountController : Controller
    {
        // TODO: Add AccountService and inject it into consturctor to add, create, delete Users and profiles

        AccountService _service;

        public AccountController(AccountService service)
        {
            _service = service;
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            // Authenticate user against auth provider, i.e. check our db using our service
            int userId = IsValidUser(user);

            if (userId != -1) // id was found
            {
                // Create and store session variable to store the user's information
                HttpContext.Session.SetString("Username", user.Username);

                HttpContext.Session.SetString("UserId", userId.ToString());

                // FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("QuoteHistory", "Quote");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(user);
        }
        
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if user already exists
                IEnumerable<User> _Users = _service.GetAllUsers();
                if (_Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("", "Username already taken");
                    return View(user);
                }

                // Add user to list of registered users
                User? newUserWithId = _service.CreateUser(user);

                // Create session variable because we are "logged in"
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                // Redirect to profile page with newUserId
                return RedirectToAction("CreateProfile", "Account", new { userId = newUserWithId?.Id});
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Profile()
        {
            // ViewBag.StateList = new SelectList(GetStateList(), "Value", "Text");
            
            return View();
        }

        public ActionResult EditProfile()
        {
            Profile profile = new Profile();
            return View(profile);
        }

        [HttpGet]
        public ActionResult CreateProfile(int userId)
        {
            Profile profile = new Profile();
            System.Console.WriteLine($"In createprofile get with {userId}");
            profile.userId = userId;
            profile.States = new List<SelectListItem>
            {
                new SelectListItem() {Text="Alabama", Value="AL"},
                new SelectListItem() { Text="Alaska", Value="AK"},
                new SelectListItem() { Text="Arizona", Value="AZ"},
                new SelectListItem() { Text="Arkansas", Value="AR"},
                new SelectListItem() { Text="California", Value="CA"},
                new SelectListItem() { Text="Colorado", Value="CO"},
                new SelectListItem() { Text="Connecticut", Value="CT"},
                new SelectListItem() { Text="District of Columbia", Value="DC"},
                new SelectListItem() { Text="Delaware", Value="DE"},
                new SelectListItem() { Text="Florida", Value="FL"},
                new SelectListItem() { Text="Georgia", Value="GA"},
                new SelectListItem() { Text="Hawaii", Value="HI"},
                new SelectListItem() { Text="Idaho", Value="ID"},
                new SelectListItem() { Text="Illinois", Value="IL"},
                new SelectListItem() { Text="Indiana", Value="IN"},
                new SelectListItem() { Text="Iowa", Value="IA"},
                new SelectListItem() { Text="Kansas", Value="KS"},
                new SelectListItem() { Text="Kentucky", Value="KY"},
                new SelectListItem() { Text="Louisiana", Value="LA"},
                new SelectListItem() { Text="Maine", Value="ME"},
                new SelectListItem() { Text="Maryland", Value="MD"},
                new SelectListItem() { Text="Massachusetts", Value="MA"},
                new SelectListItem() { Text="Michigan", Value="MI"},
                new SelectListItem() { Text="Minnesota", Value="MN"},
                new SelectListItem() { Text="Mississippi", Value="MS"},
                new SelectListItem() { Text="Missouri", Value="MO"},
                new SelectListItem() { Text="Montana", Value="MT"},
                new SelectListItem() { Text="Nebraska", Value="NE"},
                new SelectListItem() { Text="Nevada", Value="NV"},
                new SelectListItem() { Text="New Hampshire", Value="NH"},
                new SelectListItem() { Text="New Jersey", Value="NJ"},
                new SelectListItem() { Text="New Mexico", Value="NM"},
                new SelectListItem() { Text="New York", Value="NY"},
                new SelectListItem() { Text="North Carolina", Value="NC"},
                new SelectListItem() { Text="North Dakota", Value="ND"},
                new SelectListItem() { Text="Ohio", Value="OH"},
                new SelectListItem() { Text="Oklahoma", Value="OK"},
                new SelectListItem() { Text="Oregon", Value="OR"},
                new SelectListItem() { Text="Pennsylvania", Value="PA"},
                new SelectListItem() { Text="Rhode Island", Value="RI"},
                new SelectListItem() { Text="South Carolina", Value="SC"},
                new SelectListItem() { Text="South Dakota", Value="SD"},
                new SelectListItem() { Text="Tennessee", Value="TN"},
                new SelectListItem() { Text="Texas", Value="TX"},
                new SelectListItem() { Text="Utah", Value="UT"},
                new SelectListItem() { Text="Vermont", Value="VT"},
                new SelectListItem() { Text="Virginia", Value="VA"},
                new SelectListItem() { Text="Washington", Value="WA"},
                new SelectListItem() { Text="West Virginia", Value="WV"},
                new SelectListItem() { Text="Wisconsin", Value="WI"},
                new SelectListItem() { Text="Wyoming", Value="WY"}
            };
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile(int userId, Profile profile)
        {
            User? _userToBindTo = _service.GetUserById(userId);
            System.Console.WriteLine($"In createProfile POST with userId = {userId}");
            if (_userToBindTo == null)
            {
                ModelState.AddModelError("", "User not found");
                System.Console.WriteLine("User not found in db, returning to view of Register");
                return RedirectToAction("Register", "Account");
            }
            //ModelState.Remove("States");
            ModelState["States"].ValidationState = ModelValidationState.Valid;
            ModelState["user"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                // Save to database or other processing here
                System.Console.WriteLine("Saving User to db...");
                _service.bindProfileToUser(profile, userId);
                System.Console.WriteLine("Save successful");

                return RedirectToAction("QuoteHistory", "Quote");
            }

            // ViewBag.StateList = new SelectList(GetStateList(), "Value", "Text", profile.State);
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int userId)
        {
            _service.DeleteUserById(userId);
            if (_service.GetUserById(userId) == null)
            {
                ModelState.AddModelError("", "User still in database");
                System.Console.WriteLine("User still in db");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            // FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private int IsValidUser(User user)
        {
            IEnumerable<User> _Users = _service.GetAllUsers();
            if (_Users.Any(u => u.Username == user.Username && u.Password == PasswordEncryption.getHash(user.Password)))
            {
                return _Users.FirstOrDefault(u => u.Username == user.Username && u.Password == PasswordEncryption.getHash(user.Password)).Id;
            }

            return -1;
        }
    }
}
