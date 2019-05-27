using Enums;
using Interfaces;

namespace PestApp.Models
{
    public class Card : ICard
    {
        public cardFace Face { get; set; }
        public Card (cardFace face)
        {
            Face = face;
        }
        public Card ()
        {

        }
        public virtual string GetCard()
        {
            return ("Any " + Face.ToString());
        }
    }
}
