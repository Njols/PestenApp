using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public enum cardFace
    {
        A,a2,a3,a4,a5,a6,a7,a8,a9,a10,Ja,Q,K,Jo
    }
    public enum cardSuit
    {
        Hearts,Diamonds,Spades,Clubs
    }
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
            Face = cardFace.Jo;
        }
        public override string ToString()
        {
            if (Face != cardFace.Jo)
            {
                return Suit.ToString().Substring(0, 1) + Face.ToString().Substring(Face.ToString().Length - 1, 1);
            }
            else
            {
                return Face.ToString();
            }
        }
    }
}
