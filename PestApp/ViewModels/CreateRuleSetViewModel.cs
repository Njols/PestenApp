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

namespace PestApp.ViewModels
{
    public class CreateRuleSetViewModel
    {
        public SelectList CardSuitSelectList = new SelectList(Enum.GetNames(typeof(cardSuit)));
        public SelectList CardFaceSelectList = new SelectList(Enum.GetNames(typeof(cardFace)));

        public List<DropDownListItem> DropDownListItems
        {
            get
            {
                List<DropDownListItem> returnList = new List<DropDownListItem>();
                Type rule = typeof(RuleTypeWithoutAmount);
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeof(Models.Rules.RuleType)))
                    {
                        if (type.IsSubclassOf(typeof(RuleTypeWithoutAmount)))
                        {
                            DropDownListItem item = new DropDownListItem
                            {
                                Name = type.ToString().Split('.')[type.ToString().Split('.').Count() -1],
                                HasAmount = false
                            };
                            returnList.Add(item);
                        }
                        else if (type.IsSubclassOf(typeof(RuleTypeWithAmount)))
                        {
                            DropDownListItem item = new DropDownListItem
                            {
                                Name = type.ToString().Split('.')[type.ToString().Split('.').Count() - 1],
                                HasAmount = true
                            };
                            returnList.Add(item);
                        }
                    }
                }
                return returnList;
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
