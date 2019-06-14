﻿using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IRuleSet
    {
        List<IRule> Rules { get; set; }
        string Name { get; set; }
        List<additionalRule> ExtraRules { get; set; }
        int UserId { get; set; }
        int Id { get; set; }
    }
}
