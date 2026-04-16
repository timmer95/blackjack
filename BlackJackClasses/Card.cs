using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlackJackClasses
{
    internal class Card
    {
        public int Value { get; init; } // nullable for Ace? or Ace has value 11? or do {get; set;} so Ace value can be changed to 1?
        public string Name { get; init; }
        public string Suit { get; init; }
        public bool FacedUp { get; set; } = false;

        public Card(int value, string suit)
        {  
            Value = value;
            Suit = suit;
            Name = value.ToString();
        }

        public Card(int value, string suit, string name)
        {
            Value = value;
            Suit = suit;
            Name = name;
        }

        public override bool Equals(object? obj) // then you also need to implement GetHashCode. what if they don't align? then both get ignored
            // card == card results in comparison of suits, value and name. however, they do not refer to same object. 
        {
            if (obj is Card card2)
            {
                return (Suit == card2.Suit && Name == card2.Name);
            }
            // when object is null
            return false;

        }

        public override int GetHashCode() // Upon having multiple decks, do similar cards then refer to the same instance on the heap: no they don't. checked with AreSame
        {
            return HashCode.Combine(Name, Suit); // but then the hashcode does not make two references refer to the same object? no. only for comparison/indexing.
        }

        public override string ToString()
        {
            return $"{Suit} {Name} ({Value})";
        }
    }


}
