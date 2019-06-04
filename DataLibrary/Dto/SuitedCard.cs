using Enums;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Dbo
{
    public class SuitedCard : ISuitedCard
    {
        public cardFace Face { get; set; }
        public cardSuit Suit { get; set; }
        public string GetCard()
        {
            return Face.ToString() + " of " + Suit.ToString();
        }
    }
}
