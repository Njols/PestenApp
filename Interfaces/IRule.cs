using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IRule
    {
        int Id { get; set; }
        ICard Card { get; set; }
        string RuleTypeString { get; set; }
        int RuleAmount { get; set; }
    }
}
