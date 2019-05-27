
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class Card
    {
        public cardFace Face { get; set; }
        public cardSuit Suit { get; set; }
        public Card (cardFace face, cardSuit suit)
        {
            Face = face;
            Suit = suit;
        }
        public Card ()
        {
            Face = cardFace.Joker;
        }
        public override string ToString()
        {
            if (Face != cardFace.Joker)
            {
                return Suit.ToString() + " " +  Face.ToString();
            }
            else
            {
                return Face.ToString();
            }
        }
    }
}
