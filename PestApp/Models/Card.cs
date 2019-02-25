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
        private cardFace _face;
        private cardSuit _suit;
        public cardFace Face { get { return _face; } }
        public cardSuit Suit { get { return _suit; } }
        public Card (cardFace face, cardSuit suit)
        {
            _face = face;
            _suit = suit;
        }
        public Card ()
        {
            _face = cardFace.Jo;
        }
    }
}
