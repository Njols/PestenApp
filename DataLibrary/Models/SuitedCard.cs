using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class SuitedCard : Card
    {
        public cardSuit Suit { get; set; }
        public override string GetCard()
        {
            return Face.ToString() + " of " + Suit.ToString();
        }
    }
}
