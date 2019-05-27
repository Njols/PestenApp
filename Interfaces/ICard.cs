using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface ICard
    {
        cardFace Face { get; set; }
        string GetCard();

    }
}
