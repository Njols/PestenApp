using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Enums;
using PestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using PestApp.Models.Rules;
using Logic;

namespace PestApp.ViewModels
{
    public class CreateRuleSetViewModel
    {
        public SelectList CardSuitSelectList = new SelectList(Enum.GetNames(typeof(cardSuit)));
        public SelectList CardFaceSelectList = new SelectList(Enum.GetNames(typeof(cardFace)));
        private ReflectionLogic _refLogic = new ReflectionLogic();
        private Type[] types = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.BaseType == typeof(RuleType)).ToArray();

        public List<DropDownListItem> DropDownListItems
        {
            get
            {
                List<RuleType> allRuleTypes = _refLogic.GetAllSubClassesOf<RuleType>(Assembly.GetExecutingAssembly());
                List<DropDownListItem> returnList = new List<DropDownListItem>();
                foreach (RuleType ruleType in allRuleTypes)
                {
                    DropDownListItem item = new DropDownListItem
                    {
                        Name = ruleType.BasicDescription,
                        HasAmount = ruleType.GetType().IsSubclassOf(typeof(RuleTypeWithAmount))
                    };
                    returnList.Add(item);
                }
                //foreach (Type type in types)
                //{
                //    if (type.IsSubclassOf(typeof(RuleTypeWithAmount)) || type.IsSubclassOf(typeof(RuleTypeWithoutAmount)))
                //    {
                //        object instance = Activator.CreateInstance(type);
                //        PropertyInfo basicDescription = type.GetProperty(nameof(RuleTypeWithAmount.BasicDescription));
                //        DropDownListItem item = new DropDownListItem
                //        {
                //            Name = (string)basicDescription.GetValue(instance),
                //            HasAmount = type.IsSubclassOf(typeof(RuleTypeWithAmount))
                //        };
                //        returnList.Add(item);
                //    }
                //}
                return returnList;
            }
        }
        public string[] RuleTypeDisplayStrings
        {
            get
            {
                List<RuleType> allRuleTypes = _refLogic.GetAllSubClassesOf<RuleType>(Assembly.GetExecutingAssembly());
                List<string> returnList = new List<string>();
                foreach(RuleType ruletype in allRuleTypes)
                {
                    returnList.Add(ruletype.DisplayString);
                }
                return returnList.ToArray();
            }
        }

        public string Type { get; set; }

        public SelectList AdditionalRuleSelectList = new SelectList(Enum.GetNames(typeof(additionalRule)));
        [BindRequired]
        public cardFace Face { get; set; }
        [BindRequired]
        public cardSuit Suit { get; set; }
        public int RuleAmount { get; set; }
        [BindRequired]
        public bool CheckBox { get; set; }
        [BindRequired]
        public string Name { get; set; }
        public additionalRule AdditionalRule { get; set; }
        public IEnumerable<Rule> Rules { get; set; }
        public IEnumerable<additionalRule> AdditionalRules { get; set; }
    }
}
