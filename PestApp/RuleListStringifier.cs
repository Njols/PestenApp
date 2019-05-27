
using Enums;
using Interfaces;
using PestApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PestApp
{
    class RuleListStringifier
    {
        public string Stringify (List<IRule> rules)
        {
            string returnString = "";
            foreach (IRule rule in rules)
            {
                string ruleString = "|";
                ruleString += rule.RuleAmount.ToString();
                ruleString += ",";
                ruleString += Convert.ToInt32(rule.Type).ToString();
                ruleString += ",";
                ruleString += Convert.ToInt32(rule.Card.Face).ToString();
                if (rule.Card is ISuitedCard)
                {
                    ruleString += ",";
                    ISuitedCard suitedCard = (ISuitedCard)rule.Card;
                    ruleString += Convert.ToInt32(suitedCard.Suit).ToString();
                }
                returnString += ruleString;
            }
            return returnString;
        }
        public List<Rule> DeStringify (string stringToDestringify)
        {
            string[] ruleStrings = stringToDestringify.Split('|');
            List<Rule> ruleList = new List<Rule>();

            foreach (string ruleString in ruleStrings)
            { 
                string[] segments = ruleString.Split(',');
                ICard card;

                if (segments.Length == 4)
                {
                    card = new SuitedCard((cardFace)Convert.ToInt32(segments[2]), (cardSuit)Convert.ToInt32(segments[3]));
                }
                else
                {
                    card = new Card((cardFace)Convert.ToInt32(segments[2]));
                }

                int ruleAmount = Convert.ToInt32(segments[0]);
                ruleType ruleType = (ruleType)Convert.ToInt32(segments[1]);

                Rule rule = new Rule(card, ruleType, ruleAmount);
                ruleList.Add(rule);
            };

            return ruleList;
        }
    }
}
