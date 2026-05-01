using Delegates;
using GameInterface;
using System.Numerics;

namespace BlackJackClasses
{
    public class Game(IView view)
    {
        // Game = playing 1 entire Shoe empty
        internal Shoe? Shoe { get; set; }
        internal Dealer? Dealer { get; set; }
        internal HumanPlayer[]? ActivePlayers { get; set; } //assuming players don't leave or enter the game. Otherwise: public List<Player> Players;
        public IView View { get; init; } = view;

        public int DealerStandAt { get; set; } = 16;
        public int MaxRounds { get; set; } = 100;
        public int NDecks { get; set; } = 6;
        public int DealerCash { get; set; } = 18000;
        public int NPlayers { get; set;} = 5;
        public int PlayerCash { get; set; } = 1000;
        public string[] Actions { get; set; } = ["hit", "stand"];



       

        public void SetUp()
        {
            Shoe = MakeShoe(NDecks);
            Dealer = new Dealer(DealerCash);
            ActivePlayers = MakePlayers(NPlayers, PlayerCash);  // assuming all players have equal cash at beginning. 
        }

        internal static Shoe MakeShoe(int nDecks)
        {
            Deck[] decks = new Deck[nDecks];
            for (int i = 0; i < decks.Length; i++)
            {
                decks[i] = new Deck();
            }
            return new Shoe(decks, doShuffle:true);
        }

        internal static HumanPlayer[] MakePlayers(int nPlayers, int playerCash)
        {
            HumanPlayer[] players = new HumanPlayer[nPlayers];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new HumanPlayer(i + 1, playerCash);
            }
            return players;
        }

        public void Play()
        {
            int currentRound = 1;
            while (currentRound <= MaxRounds && !Shoe!.IsEmpty())
            {
                View.DisplayMessage($"Play Round {currentRound}");
                PlayRound();
                currentRound++;
            }
        }

        internal void PlayRound()
        {
            // place bets;
            PlaceBets();
            // Assign cards;
            Dealer!.DealCards(Shoe!, ActivePlayers!, 2);
            // Dealer flips card 1;
            Dealer.Hand.Cards.ElementAt(0).FacedUp = true;

            // for player in Players, executeAction;
            foreach (HumanPlayer player in ActivePlayers!)
            {
                PlayTurn(player);
            }

            // dealer flips card 2;
            Dealer.Hand.Cards.ElementAt(1).FacedUp= true;
            // calcValue, if Blackjack things happen
            
            FinishBets();
        }
        //internal string AskUserActionChoice()
        //{
        //    string? actionChoice = null;
        //    while (!Actions.Contains(actionChoice) || actionChoice is null)
        //    {
        //        View.DisplayMessage($"choose action: {string.Join(", ", Actions)}");
        //        actionChoice = View.ReadInput();
        //    }
        //    return actionChoice; 
        //}

        internal string AskUserActionChoice()
        {
            return View.GetValidatedInput<string>($"choose action: {string.Join(", ", Actions)}", CreateParseAction());
        }
            

        internal bool ExecuteAction(string action, HumanPlayer player)
        {
            switch (action)
            {
                case "hit":
                    player.AddToHand(Shoe!.DrawCard());
                    player.Hand.CalcValue();
                    if (player.Hand.Value > 21)
                    {
                        View.DisplayMessage($"\t❌ BUSTED (Hand: {player.Hand})");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                //        // if BlackJack? no. that must already have been considered. 
                case "stand":
                    return false;
                case "double down":
                    bool isSuccess = false;
                    while (!isSuccess)
                    {
                        View.DisplayMessage($"Player {player.PlayerId}, double bet with: (max {player.Bet})", endOnSameLine: true);
                        isSuccess = int.TryParse(View.ReadInput(), out int doubledBet);
                        if (doubledBet > player.Bet)
                        {
                            View.DisplayMessage($"Your bet of {player.Bet} can be at most doubled");
                            isSuccess = false;
                        }
                        else
                        {
                            player.Bet += doubledBet;
                        }
                    }

                    player.AddToHand(Shoe!.DrawCard());
                    player.Hand.CalcValue();
                    if (player.Hand.Value > 21) 
                    { 
                        View.DisplayMessage($"\t❌ BUSTED (Hand: {player.Hand})"); 
                    }
                    return false;
                default:
                    throw new InvalidOperationException($"Unknown action {action}");
            }
        }

        internal void PlayTurn(HumanPlayer player)
        {
            player.Hand.CalcInitialValue();
            if (player.Hand.IsBlackJack)
            {
                View.DisplayMessage($"Player {player.PlayerId}, You have got BlackJack! {player.Hand}");
                return;
            }
            bool continueTurn = true;
            while (continueTurn)
            {
                View.DisplayMessage($"Player {player.PlayerId} with Hand {player.Hand}, ", endOnSameLine:true);
                string actionChoice = AskUserActionChoice();
                // execute that action in a switch
                continueTurn = ExecuteAction(actionChoice, player);
            }
        }
        internal void FinishBets()
        {
            Dealer!.Hand.CalcInitialValue();                 // also sets IsBlackJack
            Dealer.FillHand(Shoe!, DealerStandAt);           // Upon BlackJack, no filling happens bc value is too high
            View.DisplayMessage($"Dealer with {Dealer.Hand} ({Dealer.Hand.Value})");
            // Decide Personal Bets
            //foreach (HumanPlayer player in ActivePlayers)
            for (int i = 0; i < ActivePlayers!.Length; i++)
            {
                HumanPlayer player = ActivePlayers[i];  
                BetResult betResult = DecidePersonalBet(Dealer, player);
                PayOut(betResult, player);
            }
        }

        internal void PayOut(BetResult betResult, HumanPlayer player)
        {
            if (!betResult.Tie)
            {
                //return;
                Player winner = betResult.Winner!;
                Player loser = betResult.Loser!;
                int betToPay = winner.Hand.IsBlackJack ? player.Bet * 3 / 2 : player.Bet;
                betResult.Winner!.CollectBet(betResult.Loser!.PayMoney(player.Bet, betResult.Winner!.Hand.IsBlackJack));
                View.DisplayMessage($"\t\tWinner {winner} (total cash: {winner.Cash}) collects {betToPay} from {loser} (total cash: {loser.Cash})");
            }
        }

        internal BetResult DecidePersonalBet(Dealer dealer, HumanPlayer player)
        {
            View.DisplayMessage($"\tVS Player {player.PlayerId} with {player.Hand} ({player.Hand.Value})");
            return player.Hand.Value switch
            {
                // Ugly solution to non-constant conditions

                //// If dealer has BlackJack
                int value when dealer.Hand.IsBlackJack && player.Hand.IsBlackJack => new BetResult(),  // Tie
                int value when dealer.Hand.IsBlackJack => new BetResult(dealer, player),

                //// If dealer busts
                int value when dealer.Hand.Value > 21 && player.Hand.Value > 21 => new BetResult(),  // Tie
                int value when dealer.Hand.Value > 21 => new BetResult(player, dealer),

                //// Otherwise
                int value when value > 21 => new BetResult(dealer, player),
                int value when value < dealer.Hand.Value => new BetResult(dealer, player),
                int value when value > dealer.Hand.Value => new BetResult(player, dealer),
                int value when value == dealer.Hand.Value => new BetResult(),  // Tie

                _ => throw new InvalidOperationException("Players hand couldn't be evaluated well, must be of type integer"),
            };
        }


        internal void PlaceBets() 
        {
            foreach (HumanPlayer player in ActivePlayers!)
            {
                string msg = $"Player {player.PlayerId} with cash {player.Cash}, what is your bet?";
                int bet = View.GetValidatedInput(msg, CreateParseBetValidator(player.Cash));
                player.SetBet(bet);
            }
        }

        internal Validator<int> CreateParseBetValidator(int cash)
        {
            Validator<int> ParseBet = (string inp, out int bet) =>
            {
                bool isSuccess = int.TryParse(inp, out bet);
                if (isSuccess)
                {
                    isSuccess = (bet <= cash / 1.5);
                }
                return isSuccess;
            };

            return ParseBet;
        }

        internal Validator<string> CreateParseAction()
        {
            Validator<string> ParseAction = (string inputS, out string actionChoice) =>
            {
                actionChoice = inputS;
                if (!this.Actions.Contains(actionChoice) || actionChoice is null)
                {
                    return false;
                }
                return true;
            };
            return ParseAction;
        }



    }       
}
