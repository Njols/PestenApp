using PestApp.Enums;

namespace PestApp.Models
{
    public class Card
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
