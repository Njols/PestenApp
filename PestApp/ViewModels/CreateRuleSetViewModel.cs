using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Enums;
using PestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.ViewModels
{
    public class CreateRuleSetViewModel
    {
        public SelectList CardSuitSelectList = new SelectList(Enum.GetNames(typeof(cardSuit)));
        public SelectList CardFaceSelectList = new SelectList(Enum.GetNames(typeof(cardFace)));
        public SelectList RuleTypeSelectList = new SelectList(Enum.GetNames(typeof(ruleType)));
        public SelectList AdditionalRuleSelectList = new SelectList(Enum.GetNames(typeof(additionalRule)));
        [BindRequired]
        public cardFace Face { get; set; }
        [BindRequired]
        public cardSuit Suit { get; set; }
        [BindRequired]
        public ruleType Type { get; set; }
        [BindRequired]
        public bool CheckBox { get; set; }
        [BindRequired]
        public string Name { get; set; }
        public additionalRule AdditionalRule { get; set; }
        public IEnumerable<Rule> Rules { get; set; }
        public IEnumerable<additionalRule> AdditionalRules { get; set; }
        public CreateRuleSetViewModel ()
        {
            AdditionalRules = new List<additionalRule>();
        }
    }
}
