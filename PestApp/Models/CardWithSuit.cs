using PestApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class CardWithSuit : ICard
    {
        public cardFace Face { get; set; }
        public cardSuit Suit { get; set; }
        public CardWithSuit (cardFace face, cardSuit suit)
        {
            Face = face;
            Suit = suit;
        }
        public string GetCard()
        {
            return (Face.ToString() + Suit.ToString());
        }
    }
}
