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

        public BetResult(Player winner, Player loser)
        {
            Winner = winner;
            Loser = loser;
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
