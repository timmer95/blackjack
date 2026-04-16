using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJackClasses
{
    internal class Deck
    {
        public Card[] Cards { get; }

        public Deck()
        {
            Cards = InitializeCards();
        }

        private static Card[] InitializeCards()
        {
            Card[] Cards = new Card[52]; // new local var? of je kunt doen Cards = new Card[52] en in de Deck() only the call without assignment. but then the Cards cannot be merely set in constructor
            string[] suits = { "Clubs", "Spades", "Hearts", "Diamonds" };
            string[] images = { "Jack", "Queen", "King" };

            int i = 0;
            foreach (string suit in suits)
            {
                for (int n = 2; n < 11; n++)
                {
                    Cards[i] = new Card(n, suit);
                    i++;
                }
                foreach (string image in images)
                {
                    Cards[i] = new Card(10, suit, image);
                    i++;
                }
                Cards[i] = new Card(11, suit, "Ace"); // or 11? then value does not need to be nullable
                i++;
            }
            return Cards;
        }
    }
}
