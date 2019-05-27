using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ISuitedCard : ICard
    {
        cardSuit Suit { get; set; }
    }
}
