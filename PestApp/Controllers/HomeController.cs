using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PestApp.Models;
using DataLibrary;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PestApp.Enums;
using PestApp.ViewModels;

namespace PestApp.Controllers
{
    public class HomeController : Controller
    {

        private UserProcessor _userProcessor = new UserProcessor();
        private static List<Rule> MockDb = new List<Rule>
        {
            new Rule(new SuitedCard(cardFace.Ace, cardSuit.Diamonds), ruleType.changeSuits, 0),
            new Rule(new SuitedCard(cardFace.Five, cardSuit.Spades), ruleType.changeSuits, 0),
            new Rule(new Card(cardFace.Jack), ruleType.changeSuits, 0)
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
        private List<additionalRule> _additionalRules;
        private List<additionalRule> AdditionalRules
        {
            get
            {
                if (_additionalRules == null)
                    _additionalRules = HttpContext.Session.Get<List<additionalRule>>("AdditionalRuleList");
                return _additionalRules;
            }
            set
            {
                _additionalRules = value;
                HttpContext.Session.Set("AdditionalRuleList", _additionalRules);
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
                _userProcessor.CreateUser(user.Email, user.Username, user.Password);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult ViewUsers ()
        {
            var data = _userProcessor.GetUsers();
            List<User> users = new List<User>();
            foreach (var user in data)
            {
                users.Add(new User { Email = user.Email, Username = user.Username });
            }
            return View(users);
        }
        public IActionResult CreateRuleSet ()
        {
            if (RuleList == null)
            {
               RuleList = MockDb;
            }
            CreateRuleSetViewModel model = new CreateRuleSetViewModel
            {
                Rules = RuleList
            };
            return View(model);
        }

        [HttpPost()]
        public IActionResult CreateRuleSet (CreateRuleSetViewModel model)
        {
            if (RuleList == null)
            {
                RuleList = MockDb;
            }
            List<Rule> rules = new List<Rule>();
            rules.AddRange(RuleList);
                Card card = new Card();
                if (model.CheckBox)
                {
                    card = new SuitedCard(model.Face, model.Suit);
                }
                else
                {
                    card.Face = model.Face;
                }
                rules.Add((new Rule(card, model.Type, 0)));
            RuleList = rules;
            model.Rules = rules;
            return View(model);
        }
        
        public IActionResult DeleteRule (int Index, CreateRuleSetViewModel viewModel)
        {
            List<Rule> rules = new List<Rule>();
            rules.AddRange(RuleList);
            rules.RemoveAt(Index);
            RuleList = rules;
            return RedirectToAction("CreateRuleSet", new {model = viewModel });
        }
    }
}
