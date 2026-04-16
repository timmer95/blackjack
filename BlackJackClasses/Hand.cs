using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlackJackClasses
{
    internal class Hand
    {
        public List<Card> Cards {  get; private set; }
        public int Value { get; protected set; } = 0;
        public bool IsBlackJack { get; private set; }

        public Hand()
        {
            Cards = new List<Card>();
        }

        public Hand(List<Card> cards)
        {
            Cards = cards;
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }
        public int CalcValue()
        {
            // calc value            
            int value = 0;
            int hasAce = 0;
            foreach (Card card in Cards)
            {
                value += card.Value;
                if (card.Name == "Ace")
                {
                    hasAce += 1;
                }
            }
            while (hasAce > 0 && value > 21)
            {
                value -= 10;
                hasAce -= 1;
            }
            Value = value;
            return value;
        }

        public int CalcInitialValue()
        {
            int value = CalcValue();
            if (value == 21)
            {
                IsBlackJack = true;
            }
            return value;
        }

        public override string ToString()
        {
            string result = "{";
            for (int i = 0; i < Cards.Count; i++)
            {
                Card card = Cards[i];
                if (i < Cards.Count - 1)
                {
                    result += $"{card.ToString()} & ";
                }
                else
                {
                    result += $"{card.ToString()}}}";
                }
            }
            return result;

        }
    }
}
