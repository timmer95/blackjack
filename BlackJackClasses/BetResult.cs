using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BlackJackClasses
{
    internal class BetResult
    {
        public Player? Winner { get; init; }
        public Player? Loser { get; init;  }
        public bool Tie { get; private set; }
        public bool IsBusted { get; init; } = false;

        public BetResult(Player winner, Player loser, bool isBusted=false)
        {
            Winner = winner;
            Loser = loser;
            IsBusted = isBusted;
            SetTie();
        }

        public BetResult()
        {
            SetTie();
        }

        private void SetTie()
        {
            Tie = (Winner is null && Loser is null);
        }
    }
}
