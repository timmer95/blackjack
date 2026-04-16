using System.ComponentModel.DataAnnotations;

namespace BlackJackClasses
{
    internal class Player
    {
        public Hand Hand { get; internal set; }
        public int Cash { get; protected set; }
        public Player(int cash)
        {
            Cash = cash;
            Hand = new Hand();
        }
        public void AddToHand(params Card[] cards)
        {
            foreach (Card card in cards)
            {
                Hand.AddCard(card);
            }
        }
        
        public void CollectBet(int money)
        {
            Cash += money;
        }
        public int PayMoney(int money, bool isBlackJack = false)
        {
            if (isBlackJack)
            {
                money = (int)(money * 1.5);
            }
            Cash -= money;
            return money;
        }
    }
}
