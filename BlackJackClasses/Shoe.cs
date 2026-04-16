using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace BlackJackClasses
{
    internal class Shoe
    {
        public Card[] stack;
        private int topIndex = -1;

        public Shoe(Deck[] decks, bool doShuffle = false)
        {
            stack = new Card[decks.Length * decks[0].Cards.Length];
            foreach (Deck deck in decks)
            {
                foreach (Card card in deck.Cards)
                {
                    topIndex++;
                    stack[topIndex] = card;
                }
			}
            if (doShuffle)
            {
                Shuffle();
            }
            
        }

        public bool IsEmpty()
        {
            return topIndex == -1;
        }

        public Card DrawCard()
        {
            if (topIndex < 0)
            {
                throw new InvalidOperationException("No more cards left"); // Or start anew? no.
            }
            Card card = stack[topIndex]; 
            topIndex--;
            return card; // returns the reference to this Card object
        }

        public void Shuffle()
        {
            Random r = new Random();
            r.Shuffle(stack);
        }

    }
}
