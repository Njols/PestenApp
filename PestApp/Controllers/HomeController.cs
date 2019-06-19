using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLibrary.DataAccess;
using Interfaces;
using Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PestApp.Models;


namespace PestApp.Controllers
{
    public class HomeController : Controller
    {
        private UserLogic _userLogic;
        private RuleSetLogic _ruleSetLogic;
        public HomeController (IUserProcessor userProcessor, IRuleSetProcessor ruleSetProcessor, IRuleProcessor ruleProcessor, IAdditionalRuleProcessor additionalRuleProcessor)
        {
            _userLogic = new UserLogic(userProcessor);
            _ruleSetLogic = new RuleSetLogic(userProcessor, ruleSetProcessor, ruleProcessor, additionalRuleProcessor);
        }
        public IActionResult Index()
        {
            List<SimpleRuleSetViewModel> ruleSetViewModels = new List<SimpleRuleSetViewModel>();
            foreach (IRuleSet ruleSet in _ruleSetLogic.GetRuleSets())
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
            return View(new IndexViewModel { AllRuleSets = ruleSetViewModels });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SignUp()
        {
            ViewData["Message"] = "Sign up for our app";
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(User user)
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
                return RedirectToAction("Index");
            }
            user = new User();
            ViewData["Error"] = "Email already in use.";
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
                return RedirectToAction("Index");
            }
            else
            {
                viewModel = new LogInViewModel();
                ViewData["Error"] = "Email and password do not match";
                return View();
            }

        }
        public IActionResult ViewUsers()
        {
            var data = _userLogic.GetUsers();
            List<User> users = new List<User>();
            foreach (var user in data)
            {
                users.Add(new User { Email = user.Email, Username = user.Username });
            }
            return View(users);
        }
    }
}