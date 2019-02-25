using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public enum ruleType
    {
        take, revertTime, skipPlayer, extraTurns, switchSeats, changeSuits, stackAfter
    }
    public class Rule
    {
        private int _id;
        public int Id { get { return _id; } }
        private Card _card;
        public Card Card { get { return _card; } }
        private int _ruleAmount;
        public int RuleAmount { get { return _ruleAmount; } }
        private ruleType _ruleType;
        public ruleType RuleType {  get { return _ruleType; } }
        public Rule (Card card, ruleType ruletype, int ruleAmount)
        {
            _card = card;
            _ruleType = ruletype;
            _ruleAmount = ruleAmount;
        }
    }
}
