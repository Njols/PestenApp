﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using PestApp.Enums;
using PestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.ViewModels
{
    public class CreateRuleSetViewModel
    {
        public SelectList CardSuit = new SelectList(Enum.GetNames(typeof(cardSuit)));
        public SelectList CardFace = new SelectList(Enum.GetNames(typeof(cardFace)));
        public SelectList RuleType = new SelectList(Enum.GetNames(typeof(ruleType)));
        [BindRequired]
        public cardFace Face { get; set; }
        [BindRequired]
        public cardSuit Suit { get; set; }
        [BindRequired]
        public ruleType Type { get; set; }
        [BindRequired]
        public bool CheckBox { get; set; }
        public IEnumerable<Rule> Rules;
    }
}
