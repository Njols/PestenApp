using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class SuitedCard : Card
    {
        public cardSuit Suit { get; set; }

        public SuitedCard (cardFace face, cardSuit suit)
        {
            Face = face;
            Suit = suit;
        }

        public override string GetCard ()
        {
            return Suit.ToString() + ' ' + Face.ToString();
        }
    }
}
