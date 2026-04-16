namespace BlackJackClasses
{
    internal class HumanPlayer : Player
    {
        public int PlayerId { get; init; }
        public int Bet { get; set; }
        public HumanPlayer(int playerId, int cash) : base(cash)
        {
            PlayerId = playerId;
        }

        public void executeAction(string action)
        {
            return;
        }

        public int PayMoney(bool isBlackJack = false) // Overloading
        {
            return base.PayMoney(Bet, isBlackJack);
        }

        public bool SetBet(int bet)
        {
            if (1.5 * bet <= Cash)
            {
                Bet = bet;
                return true;
            }
            else
            {
                throw new InvalidOperationException($"Bet ({bet}) is too high for cash amount ({Cash})");
            }
        }

        public override string ToString()
        {
            return $"Player {PlayerId}";
        }

    }
}
