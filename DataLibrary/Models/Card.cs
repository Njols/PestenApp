﻿
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
        public Card (cardFace face)
        {
            Face = face;
        }
        public Card ()
        {
            Face = cardFace.Joker;
        }
        public virtual string GetCard()
        {
            return ("Any " + Face.ToString());
        }
    }
}
