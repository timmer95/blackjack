# User Story - Action

## Story
As a HumanPlayer, I want to execute a chosen action, so that I can play my turn.

## Acceptance criteria
Program asks for user input (askUser())
The action chosen is the action returned
The user choice can only be "hit" or "stand" (while (!options.Contains userInput) {userInput = askUser()} )
Upon "hit", a card is added to HumanPlayer's Hand ( switch userInput { case "hit" : player.AddToHand(Dealer.DrawCard(Shoe)); player.Hand.CalcValue(); if (player.Hand.Value > 21) // BUST; { continueTurn = false } } )
Upon "stand", the turn is finished ( case "stand" : continueTurn = false )
The turn ends when the HumanPlayer stands or busts (while (continueTurn) { above } )