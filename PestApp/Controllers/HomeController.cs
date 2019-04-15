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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PestApp.Controllers
{
    public class HomeController : Controller
    {
        private static List<Rule> MockDb = new List<Rule>
        {
            new Rule(new Card{Face = cardFace.Ace, Suit = cardSuit.Clubs }, ruleType.changeSuits, 0),
            new Rule(new Card{Face = cardFace.Ace, Suit = cardSuit.Clubs }, ruleType.changeSuits, 0),
            new Rule(new Card{Face = cardFace.Ace, Suit = cardSuit.Clubs }, ruleType.changeSuits, 0)
        };
        private List<Rule> _ruleList;
        private List<Rule> RuleList
        {
            get
            {
                if (_ruleList == null)
                    _ruleList = HttpContext.Session.Get<List<Rule>>("RuleList");
                return _ruleList;
            }
            set
            {
                _ruleList = value;
                HttpContext.Session.Set("RuleList", _ruleList);
            }
        }
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
        public IActionResult CreateRuleSet ()
        {
            ViewBag.CardSuit = new SelectList(Enum.GetNames(typeof(cardSuit)));
            ViewBag.CardFace = new SelectList(Enum.GetNames(typeof(cardFace)));
            ViewBag.CardRule = new SelectList(Enum.GetNames(typeof(ruleType)));

            if (RuleList == null)
            {
               RuleList = MockDb;
            }

            return View(RuleList);
        }

        [HttpPost]
        public IActionResult CreateRuleSet (cardFace cardFace, cardSuit cardSuit, ruleType ruleType)
        {
            ViewBag.CardSuit = new SelectList(Enum.GetNames(typeof(cardSuit)));
            ViewBag.CardFace = new SelectList(Enum.GetNames(typeof(cardFace)));
            ViewBag.CardRule = new SelectList(Enum.GetNames(typeof(ruleType)));

            Card card = new Card { Face = cardFace, Suit = cardSuit };

            List<Rule> rules = new List<Rule>();
            rules.AddRange(RuleList);
            rules.Add((new Rule(card, ruleType, 0)));
            RuleList = rules;

            return View(RuleList);
        }
    }
}
