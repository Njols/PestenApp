using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Enums
{
    public enum additionalRule
    {
        [Display(Name= "Play a card right after taking it.")]takeAndPut,
        [Display(Name = "Rules that make you take cards stack.")]takeStacks,
        [Display(Name ="Your turn ends after taking a card.")]noPlayAfterTaking,
        [Display(Name ="Last card can be a pest-card")]endOnPestCard,
        [Display(Name ="The turns go counter-clockwise")]counterClockWise
    }
}
