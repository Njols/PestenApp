using PestApp.Enums;

namespace PestApp.Models
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

        }
        public string GetCard()
        {
            return (Face.ToString() + Suit.ToString());
        }
    }
}
