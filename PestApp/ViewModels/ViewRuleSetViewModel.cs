using Enums;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.ViewModels
{
    public class ViewRuleSetViewModel
    {
        public List<additionalRule> ExtraRules { get; set; }
        public List<DisplayRule> DisplayRules { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
}
