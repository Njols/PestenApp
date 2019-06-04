using DataLibrary.Dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums;
using Interfaces;

namespace DataLibrary.Dbo
{

    public class RuleSet : IRuleSet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<IRule> Rules { get; set; }
        public List<additionalRule> ExtraRules { get; set; }
        public string Name { get; set; }
        public RuleSet()
        {

        }
    }
}
