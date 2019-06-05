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
using Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using PestApp.Models.Rules;

namespace PestApp.Controllers
{
    public class HomeController : Controller
    {

        private IUserProcessor _userProcessor;
        private IRuleSetProcessor _ruleSetProcessor;
        private IRuleProcessor _ruleProcessor;
        private IAdditionalRuleProcessor _additionalRuleProcessor;
        public HomeController (IUserProcessor userProcessor, IRuleSetProcessor ruleSetProcessor, IRuleProcessor ruleProcessor, IAdditionalRuleProcessor additionalRuleProcessor)
        {
            _userProcessor = userProcessor;
            _ruleSetProcessor = ruleSetProcessor;
            _ruleProcessor = ruleProcessor;
            _additionalRuleProcessor = additionalRuleProcessor;
            _userLogic = new UserLogic(_userProcessor, _ruleSetProcessor);
            _ruleSetLogic = new RuleSetLogic(_userProcessor, _ruleSetProcessor, _ruleProcessor, _additionalRuleProcessor);

        }
        private UserLogic _userLogic;
        private RuleSetLogic _ruleSetLogic;

        private List<Rule> _ruleList;
        private List<Rule> RuleList
        {
            get
            {
                if (_ruleList == null)
                {
                    if (HttpContext.Session.Get<List<Rule>>("RuleList") != null)
                    {
                        _ruleList = HttpContext.Session.Get<List<Rule>>("RuleList");
                    }
                    else
                    {
                        _ruleList = new List<Rule>();
                    }
                }
                    
                return _ruleList;
            }
            set
            {
                _ruleList = value;
                HttpContext.Session.Set("RuleList", _ruleList);
            }
        }
        private List<additionalRule> _additionalRules;
        private List<additionalRule> AdditionalRuleList
        {
            get
            {
                if (_additionalRules == null)
                {
                    if (HttpContext.Session.Get<List<additionalRule>>("AdditionalRuleList") != null)
                    {
                        _additionalRules = HttpContext.Session.Get<List<additionalRule>>("AdditionalRuleList");
                    }
                    else
                    {
                        _additionalRules = new List<additionalRule>();
                    }
                }
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
                    new Claim(ClaimTypes.Email, user.Email)
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("CreateRuleSet");
            }
            return View();
        }
        public IActionResult LogIn()
        {
            LogInViewModel viewModel = new LogInViewModel();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInViewModel viewModel)
        {
            if (_userLogic.PasswordMatches(viewModel.Email, viewModel.Password))
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, viewModel.Email)
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                string emailTest = User.Claims.First().Value;
                return RedirectToAction("CreateRuleSet");
            }
            else
            {
                return View();
            }

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
            CreateRuleSetViewModel model = new CreateRuleSetViewModel
            {
                Rules = RuleList,
                AdditionalRules = AdditionalRuleList
            };
            return View(model);
        }

        [HttpPost()]
        public IActionResult CreateRuleSet (CreateRuleSetViewModel model, string command)
        {
            List<additionalRule> additionalRules = AdditionalRuleList;
            List<Rule> rules = RuleList;
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
                Type t = Type.GetType("PestApp.Models.Rules." + model.Type.ToString());
                if (t.IsSubclassOf(typeof(RuleTypeWithAmount)))
                {
                    rules.Add(new Rule { Card = card, RuleAmount = model.RuleAmount, RuleType = model.Type });
                }
                else if (t.IsSubclassOf(typeof(RuleTypeWithoutAmount)))
                {
                    rules.Add(new Rule { Card = card, RuleAmount = -1, RuleType = model.Type });
                }
            }
            else if (command == "Add Extra Rule")
            {
                additionalRules.Add(model.AdditionalRule);
            }
            else if (command.Substring(0, 11) == "DeleteRule ")
            {
                rules.RemoveAt(Convert.ToInt32(command.Substring(11)));
            }
            else
            {
                additionalRules.RemoveAt(Convert.ToInt32(command.Substring(16)));
            }
            AdditionalRuleList = additionalRules;
            RuleList = rules;
            model.Rules = rules;
            model.AdditionalRules = additionalRules;
            model.AdditionalRuleSelectList = new SelectList(Enum.GetNames(typeof(additionalRule)).Where(x => !AdditionalRuleList.Contains((additionalRule)Enum.Parse(typeof(additionalRule), x))));
            return View(model);
        }
        public IActionResult SaveRuleSet (CreateRuleSetViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Claims.First().Value;
                List<Rule> rules = RuleList;
                //You can't loop through RuleList directly
                List<IRule> ruleList = new List<IRule>();
                foreach(Rule rule in rules)
                {
                    ruleList.Add((IRule)rule);
                }
                _ruleSetLogic.CreateRuleSet(ruleList, email, AdditionalRuleList, viewModel.Name);
            }
            return RedirectToAction("CreateRuleSet");
        }

        public IActionResult ViewRuleSet (int ruleSetId)
        {
            RuleSet ruleSet = (RuleSet)_ruleSetLogic.GetRuleSetById(ruleSetId);
            return View(ruleSet);
        }
    }
}
