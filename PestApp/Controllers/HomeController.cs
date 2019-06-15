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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        private List<DisplayRule> _ruleList;
        private List<DisplayRule> RuleList
        {
            get
            {
                if (_ruleList == null)
                {
                    if (HttpContext.Session.Get<List<DisplayRule>>("RuleList") != null)
                    {
                        _ruleList = HttpContext.Session.Get<List<DisplayRule>>("RuleList");
                    }
                    else
                    {
                        _ruleList = new List<DisplayRule>();
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
            List<SimpleRuleSetViewModel> ruleSetViewModels = new List<SimpleRuleSetViewModel>();
            foreach(IRuleSet ruleSet in _ruleSetLogic.GetRuleSets())
            {
                SimpleRuleSetViewModel simpleRuleSet = new SimpleRuleSetViewModel
                {
                    Name = ruleSet.Name,
                    Username = _userLogic.GetUserById(ruleSet.UserId).Username,
                    AmountOfRules = ruleSet.Rules.Count,
                    RuleSetId = ruleSet.Id
                };
                ruleSetViewModels.Add(simpleRuleSet);
            }
            return View(new IndexViewModel {AllRuleSets = ruleSetViewModels });
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
        public async Task<IActionResult> LogOut ()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index");
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
                ClaimsIdentity identity = new ClaimsIdentity(claims);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("loginAuth", principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
                string email = User.Claims.FirstOrDefault().Value;
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
                    new Claim(ClaimTypes.Email, viewModel.Email),
                    new Claim(ClaimTypes.Name, _userLogic.GetUserByEmail(viewModel.Email).Username)
                };
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaims(claims);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("loginAuth", principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                });
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
                DisplayRules = RuleList,
                AdditionalRules = AdditionalRuleList
            };
            model.AdditionalRuleSelectList = new SelectList(Enum.GetNames(typeof(additionalRule)).Where(x => !AdditionalRuleList.Contains((additionalRule)Enum.Parse(typeof(additionalRule), x))));
            return View(model);
        }

        [HttpPost()]
        public IActionResult RemoveRule (CreateRuleSetViewModel model, string command)
        {
            List<DisplayRule> rules = RuleList;
            rules.RemoveAt(Convert.ToInt32(command));
            RuleList = rules;
            return RedirectToAction("CreateRuleSet");
        }
        [HttpPost()]
        public IActionResult RemoveAdditionalRule (CreateRuleSetViewModel model, string command)
        {
            List<additionalRule> extraRules = AdditionalRuleList;
            extraRules.RemoveAt(Convert.ToInt32(command));
            AdditionalRuleList = extraRules;
            return RedirectToAction("CreateRuleSet");
        }

        public IActionResult AddAdditionalRule (CreateRuleSetViewModel model)
        {
            List<additionalRule> extraRules = AdditionalRuleList;
            extraRules.Add(model.AdditionalRule);
            AdditionalRuleList = extraRules;
            return RedirectToAction("CreateRuleSet");
        }

        [HttpPost()]
        public IActionResult AddRule (CreateRuleSetViewModel model)
        {
            List<DisplayRule> rules = RuleList;
            Card card = new Card();
            if (model.CheckBox)
            {
                card = new SuitedCard(model.Face, model.Suit);
            }
            else
            {
                card.Face = model.Face;
            }
            Type t = Type.GetType(model.Type.ToString());
            if (t.IsSubclassOf(typeof(RuleTypeWithAmount)))
            {
                rules.Add(new DisplayRule(model.Type.ToString(), card, model.RuleAmount));
            }
            else if (t.IsSubclassOf(typeof(RuleTypeWithoutAmount)))
            {
                rules.Add(new DisplayRule(model.Type.ToString(),card));
            }
            RuleList = rules;
            return RedirectToAction("CreateRuleSet");
        }

        public IActionResult SaveRuleSet (CreateRuleSetViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Claims.First().Value;
                List<DisplayRule> rules = RuleList;
                //You can't loop through RuleList directly
                List<IRule> ruleList = new List<IRule>();
                foreach(DisplayRule rule in rules)
                {
                    ruleList.Add((IRule)rule);
                }
                _ruleSetLogic.CreateRuleSet(ruleList, email, AdditionalRuleList, viewModel.Name);
            }
            return RedirectToAction("CreateRuleSet");
        }

        public IActionResult ViewRuleSet (int id)
        {
            IRuleSet ruleSet = (IRuleSet)_ruleSetLogic.GetRuleSetById(id);
            List<DisplayRule> displayRules = new List<DisplayRule>();
            foreach(IRule rule in ruleSet.Rules)
            {
                DisplayRule displayRule = new DisplayRule(rule.RuleTypeString);
                displayRule.Card = rule.Card;
                displayRule.RuleAmount = rule.RuleAmount;
                displayRules.Add(displayRule);
            }
            RuleSetViewModel viewModel = new RuleSetViewModel
            {
                ExtraRules = ruleSet.ExtraRules,
                DisplayRules = displayRules,
                Name = ruleSet.Name,
                UserName = _userProcessor.GetUserById(ruleSet.UserId).Username
            };
            return View(viewModel);
        }
    }
}
