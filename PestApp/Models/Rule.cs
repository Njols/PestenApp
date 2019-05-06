﻿using PestApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{

    public class Rule
    {
        public CardWithSuit Card { get; set; }
        public int RuleAmount { get; set; }
        public ruleType RuleType { get; set; }
        public Rule (CardWithSuit card, ruleType ruletype, int ruleAmount)
        {
            Card = card;
            RuleType = ruletype;
            RuleAmount = ruleAmount;
        }
    }
}
