using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PestApp.Models;
using DataLibrary.DataAccess;
using PestApp.ViewModels;
using Logic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Enums;

namespace PestApp.Controllers
{
    public class HomeController : Controller
    {

        private IUserProcessor _userProcessor;
        private IRuleSetProcessor _ruleSetProcessor;
        public HomeController (IUserProcessor userProcessor, IRuleSetProcessor ruleSetProcessor)
        {
            _userProcessor = userProcessor;
            _ruleSetProcessor = ruleSetProcessor;
            _userLogic = new UserLogic(_userProcessor, _ruleSetProcessor);
            _ruleSetLogic = new RuleSetLogic(_userProcessor, _ruleSetProcessor);

        }
        private UserLogic _userLogic;
        private RuleSetLogic _ruleSetLogic;

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
        public async Task<IActionResult> SignUp (User user)
        {
            bool userCreationHasSucceeded = _userLogic.TryToCreateUser(user.Email, user.Username, user.Password);
            if (ModelState.IsValid && userCreationHasSucceeded)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username)
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "user");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
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
        public IActionResult CreateRuleSet (CreateRuleSetViewModel model, string command)
        {
            if (RuleList == null)
            {
                RuleList = MockDb;
            }

            List<Rule> rules = new List<Rule>();
            rules.AddRange(RuleList);
            if (command == "Add Rule")
            {
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
            }
            else
            {
                rules.RemoveAt(Convert.ToInt32(command));
            }
            RuleList = rules;
            model.Rules = rules;
            return View(model);
        }
        public IActionResult SaveRuleSet (CreateRuleSetViewModel viewModel)
        {
            string email = User.Claims.First().Value;
            _ruleSetLogic.CreateRuleSet(RuleList, email, AdditionalRules, viewModel.Name);
        }
    }
}
