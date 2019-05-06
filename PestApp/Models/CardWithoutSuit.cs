using PestApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class CardWithoutSuit:ICard
    {
        cardSuit Suit { get; set; }
        public string GetCard()
        {
            return ("Any " + Suit.ToString());
        }

    }
}
