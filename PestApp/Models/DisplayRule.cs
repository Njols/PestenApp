using Interfaces;
using PestApp.Models;
using PestApp.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class DisplayRule : IRule
    {
        public RuleType RuleType { get; set; }
        public int Id { get; set; }
        public ICard Card { get; set; }
        public string RuleTypeString { get { return RuleType.GetType().ToString(); } set { } }
        public int RuleAmount { get; set; }

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
