using GameInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJackClasses
{
    internal class Dealer : Player
    {

        public Dealer(int cash) : base(cash) { }
        
        public void FillHand(Shoe shoe, int standAt, IView view)
        {
            while (Hand.CalcValue() < standAt)
            {
                Card card = shoe.DrawCard();
                Hand.AddCard(card);
                view.DisplayMessage($" + {card}");
            }
        }

        public void DealCards(Shoe shoe, HumanPlayer[] humanPlayers, int numberOfCards)
        {
            Player[] players = new Player[humanPlayers.Length + 1];
            Dealer[] dealerPlayer = { this };
            Array.Copy(humanPlayers, 0, players, 0, humanPlayers.Length);
            Array.Copy(dealerPlayer, 0, players, humanPlayers.Length, dealerPlayer.Length);
            //foreach (Player player in players) { Console.WriteLine(player); };
            for (int i = 1; i <= numberOfCards; i++)
            {
                foreach (Player player in players)
                {
                    Card card = shoe.DrawCard();
                    if (player is not Dealer || i != numberOfCards)
                    {
                        card.FacedUp = true;
                    }
                    player.AddToHand(card);
                }
            }
        }

        public override string ToString()
        {
            return "Dealer";
        }
    }
}
