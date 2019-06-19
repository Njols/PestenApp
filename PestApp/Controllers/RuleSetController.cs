using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PestApp.Models;
using DataLibrary.DataAccess;
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
    public class RuleSetController : Controller
    {

        private IUserProcessor _userProcessor;
        private IRuleSetProcessor _ruleSetProcessor;
        private IRuleProcessor _ruleProcessor;
        private IAdditionalRuleProcessor _additionalRuleProcessor;
        public RuleSetController (IUserProcessor userProcessor, IRuleSetProcessor ruleSetProcessor, IRuleProcessor ruleProcessor, IAdditionalRuleProcessor additionalRuleProcessor)
        {
            _userProcessor = userProcessor;
            _ruleSetProcessor = ruleSetProcessor;
            _ruleProcessor = ruleProcessor;
            _additionalRuleProcessor = additionalRuleProcessor;
            _userLogic = new UserLogic(_userProcessor);
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
        
        public IActionResult CreateRuleSet ()
        {
            ViewData["Error"] = TempData["Error"];
            CreateRuleSetViewModel model = new CreateRuleSetViewModel
            {
                DisplayRules = RuleList,
                AdditionalRules = AdditionalRuleList,
                DropDownAddRules = Enum.GetValues(typeof(additionalRule)).Cast<additionalRule>().Where(x => !AdditionalRuleList.Contains(x))
            };
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
                if (viewModel.Name != null)
                {
                    string email = User.Claims.First().Value;
                    List<DisplayRule> rules = RuleList;
                    //You can't loop through RuleList directly
                    List<IRule> ruleList = new List<IRule>();
                    foreach (DisplayRule rule in rules)
                    {
                        ruleList.Add(rule);
                    }
                    _ruleSetLogic.TryToCreateRuleSet(ruleList, email, AdditionalRuleList, viewModel.Name);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Error"] = "Your ruleset needs a name.";
                    return RedirectToAction("CreateRuleSet");
                }
            }
            TempData["Error"] = "You need to be logged in to create a ruleset.";
            return RedirectToAction("CreateRuleSet");
        }

        public IActionResult ViewRuleSet (int id)
        {
            IRuleSet ruleSet = _ruleSetLogic.GetRuleSetById(id);
            if (ruleSet != null)
            {
                List<DisplayRule> displayRules = new List<DisplayRule>();
                foreach (IRule rule in ruleSet.Rules)
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
            else
            {
                TempData["Error"] = "Ruleset does not exist.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
