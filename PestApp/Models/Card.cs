using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public enum cardFace
    {
        Ace,Two,Three,Four,Five,Six,Seven,Eight,Nine,Ten,Jack,Queen,King,Joker
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
            Face = cardFace.Joker;
        }
        public override string ToString()
        {
            if (Face != cardFace.Joker)
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
