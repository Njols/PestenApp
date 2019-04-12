using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PestApp.Models;
using DataLibrary;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLibrary.DataAccess;

namespace PestApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SignUp ()
        {
            ViewData["Message"] = "Sign up for our app";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp (User user)
        {
            if (ModelState.IsValid)
            {
                UserProcessor.CreateUser(user.Email, user.Username, user.Password);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult ViewUsers ()
        {
            var data = UserProcessor.GetUsers();
            List<User> users = new List<User>();
            foreach (var user in data)
            {
                users.Add(new User { Email = user.Email, Username = user.Username });
            }
            return View(users);
        }
        [HttpPost]
        public IActionResult CreateRuleSet(RuleViewModel ruleViewModel)
        {
            ruleViewModel.ExistingRules.Add(new Rule(new Card(ruleViewModel.CardFace, ruleViewModel.CardSuit), ruleViewModel.CardRule, 0));
            return View(ruleViewModel);
        }
        public IActionResult CreateRuleSet()
        {
            ViewBag.CardSuit = new SelectList(Enum.GetNames(typeof(cardSuit)));
            ViewBag.CardFace = new SelectList(Enum.GetNames(typeof(cardFace)));
            ViewBag.CardRule = new SelectList(Enum.GetNames(typeof(ruleType)));
            RuleViewModel ruleViewModel = new RuleViewModel();
            ruleViewModel.ExistingRules = new List<Rule>();
            return View(ruleViewModel);
        }
    }
}
