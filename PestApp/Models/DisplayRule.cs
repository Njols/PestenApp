using Interfaces;
using PestApp.Models;
using PestApp.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.ViewModels
{
    public class DisplayRule : Rule
    {
        public RuleType RuleType { get; set; }
        public DisplayRule (string ruleType)
        {
            Type t = Type.GetType(ruleType);
            RuleType = (RuleType)Activator.CreateInstance(t);
        }
        public DisplayRule (string ruleType, ICard card)
        {
            Card = card;
            RuleAmount = -1;
            Type t = Type.GetType(ruleType);
            RuleType = (RuleType)Activator.CreateInstance(t);
        }
        public DisplayRule(string ruleType, ICard card, int amount)
        {
            Type t = Type.GetType(ruleType);
            RuleTypeWithAmount dummy = (RuleTypeWithAmount)Activator.CreateInstance(t);
            dummy.RuleAmount = amount;
            RuleType = dummy;
            Card = card;
            RuleAmount = amount;
        }

        public DisplayRule ()
        {

        }
    }
}
