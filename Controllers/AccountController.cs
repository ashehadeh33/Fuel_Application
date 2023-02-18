using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace qenergy.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> Users = new List<User>
        {
            new User { Username = "user1", Password = "password1" },
            new User { Username = "user2", Password = "password2" }
        };

        private static List<Profile> Profiles = new List<Profile>{};


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (IsValidUser(user))
            {
                // FormsAuthentication.SetAuthCookie(user.Username, false);
                RedirectToAction("QuoteHistory", "Quote");
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
                if (Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("", "Username already taken");
                    return View(user);
                }

                // Add user to list of registered users
                Users.Add(user);

                // Redirect to profile page
                return RedirectToAction("Profile", "Account");
            }

            return View(user);
        }

        [HttpGet]
        public ActionResult Profile()
        {
            // ViewBag.StateList = new SelectList(GetStateList(), "Value", "Text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(Profile profile)
        {
            if (ModelState.IsValid)
            {
                // Save to database or other processing here
                Profiles.Add(profile);

                return RedirectToAction("QuoteHistory", "Quote");
            }

            // ViewBag.StateList = new SelectList(GetStateList(), "Value", "Text", profile.State);
            return View(profile);
        }

        private List<SelectListItem> GetStateList()
        {
            var stateList = new List<SelectListItem>
            {
                new SelectListItem { Value = "AL", Text = "Alabama" },
                new SelectListItem { Value = "AK", Text = "Alaska" },
                new SelectListItem { Value = "AZ", Text = "Arizona" },
                new SelectListItem { Value = "AR", Text = "Arkansas" },
                new SelectListItem { Value = "CA", Text = "California" },
                // Add more states as needed
            };

            return stateList;
        }

        public ActionResult Logout()
        {
            // FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private bool IsValidUser(User user)
        {
            return Users.Exists(u => u.Username == user.Username && u.Password == user.Password);
        }
    }
}
