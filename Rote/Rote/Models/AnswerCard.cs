using System;
using System.Collections.Generic;
using System.Text;

namespace Rote.Models
{
    public class AnswerCard
    {
        public Boolean Correct { get; set; }
        public Card Card { get; set; }

        public AnswerCard(Card Card, Boolean Correct)
        {
            this.Correct = Correct;
            this.Card = Card;
        }
    }
}
