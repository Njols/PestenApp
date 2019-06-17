using DataLibrary.Dbo;
using Enums;
using Interfaces;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Mocks;

namespace UnitTests
{
    [TestClass]
    public class RuleSetLogicTests
    {
        RuleSetLogic ruleSetLogic;
        MockRuleSetProcessor ruleSetProcessor;
        MockRuleProcessor ruleProcessor;
        MockAdditionalRuleProcessor additionalRuleProcessor;
        MockUserProcessor userProcessor;
        [TestInitialize]
        public void TestInitialize ()
        {
            ruleSetProcessor = new MockRuleSetProcessor();
            ruleProcessor = new MockRuleProcessor();
            userProcessor = new MockUserProcessor();
            additionalRuleProcessor = new MockAdditionalRuleProcessor();
            ruleSetLogic = new RuleSetLogic(userProcessor, ruleSetProcessor, ruleProcessor, additionalRuleProcessor);
        }
        [TestMethod]
        public void OnlyCreatesRuleSetsForExistingUsers()
        {
            //arrange
            List<IRule> rules = new List<IRule>();
            List<additionalRule> addRules = new List<additionalRule>();
            //act
            bool hasAddedRuleSet = ruleSetLogic.TryToCreateRuleSet(rules, "fake@email.com", addRules, "ruulset");
            //assert
            Assert.IsFalse(hasAddedRuleSet);
        }
        [TestMethod]
        public void ReturnsRuleSetsWithRules()
        {
            //arrange
            userProcessor.AddUser(new User { Id = 0, Email = "e@mail.com" });
            RuleSetLogic logic = new RuleSetLogic(userProcessor, ruleSetProcessor, ruleProcessor, additionalRuleProcessor);
            List<IRule> rules = new List<IRule> { new Rule() };
            logic.TryToCreateRuleSet(rules, "e@mail.com", new List<Enums.additionalRule> { Enums.additionalRule.endOnPestCard }, "ruleSet");
            //act
            RuleSet ruleSet = (RuleSet)logic.GetRuleSetById(0);
            //assert
            Assert.AreEqual(ruleSet.Rules.Count, 1);
        }

        [TestMethod]
        public void ReturnsRuleSetsWithAdditionalRules()
        {
            //arrange
            userProcessor.AddUser(new User { Id = 0, Email = "e@mail.com" });
            RuleSetLogic logic = new RuleSetLogic(userProcessor, ruleSetProcessor, ruleProcessor, additionalRuleProcessor);
            List<IRule> rules = new List<IRule> { new Rule() };
            logic.TryToCreateRuleSet(rules, "e@mail.com", new List<Enums.additionalRule> { Enums.additionalRule.endOnPestCard }, "ruleSet");
            //act
            RuleSet ruleSet = (RuleSet)logic.GetRuleSetById(0);
            //assert
            Assert.AreEqual(ruleSet.ExtraRules.Count, 1);
        }

    }
}
