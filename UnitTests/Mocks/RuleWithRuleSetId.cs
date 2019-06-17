using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Mocks
{
    public class RuleWithRuleSetId
    {
        public IRule Rule { get; set; }
        public int RuleSetId { get; set; }
    }
}
