using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Create deck
            Deck myDeck = new Deck();
            // shuffle deck
            myDeck.Shuffle();
            // create players - Create 2 players, ask for their namespace
            string inputline;
            Console.WriteLine("Enter first player's name.");
            inputline  = Console.ReadLine();
            Player player1 = new Player(inputline);
            System.Console.WriteLine(player1.name);
            Console.WriteLine("Enter second player's name.");
            inputline  = Console.ReadLine();
            Player player2 = new Player(inputline);
            System.Console.WriteLine(player2.name);
            // add players to list, create turn counter
            // give players 7 cards
            for (int i = 1; i <= 7; i++){
                player1.Draw(myDeck);
                player2.Draw(myDeck);
            }

            myDeck.discards.Add(myDeck.Deal());
            List<Player> players = new List<Player>();
            players.Add(player1);
            players.Add(player2);
            int turn = 0;

            bool gameOn = true;

            while(gameOn){
                //player turn
                
                if (turn > 1){
                    turn = 0;
                }



                //show hand
                System.Console.WriteLine(players[turn].name + "'s hand: " + players[turn].ShowHand());

                //show the discard pile
                System.Console.WriteLine("The top card on the discard pile is a(n) " + myDeck.ShowDiscard().ToString());

                //prompt which card to play
                System.Console.WriteLine("Enter a command - Draw or Quit - or enter a card number between 1 and " + players[turn].hand.Count);

                inputline = Console.ReadLine().ToLower();
                //check if input is an integer and within the hand range
                int cardNum;

                switch(inputline){
                    case "draw":
                        players[turn].Draw(myDeck);
                        turn--;
                        break;
                    case "pass":
                        if (myDeck.cards.Count <= 0){
                            break;
                        }
                        else {
                            System.Console.WriteLine("You can only pass if the draw pile is empty.");
                            turn--;
                            break;
                        }                        
                    case "quit":
                        gameOn = false;
                        System.Console.WriteLine(players[turn].name + " is giving up like a big whiny baby.");
                        break;
                    default:
                        if (int.TryParse(inputline, out cardNum) && cardNum <= players[turn].hand.Count && cardNum > 0){
                            cardNum--;
                            if(players[turn].hand[cardNum].val == 8){
                                System.Console.WriteLine("Please enter the suit for your Crazy Eight:");
                                inputline = Console.ReadLine();
                                //Change the suit of the Eight
                                if(myDeck.suits.Contains(inputline)){
                                    players[turn].hand[cardNum].suit = inputline;
                                    myDeck.discards.Add(players[turn].Discard(cardNum));
                                    // System.Console.WriteLine("That's totally a suit.");
                                    if (players[turn].hand.Count <= 0){
                                        System.Console.WriteLine("Congratulations, " + players[turn].name + ", you have won!");
                                        gameOn = false;
                                        return;
                                    }
                                }
                                else {
                                    System.Console.WriteLine("Please enter a valid suit: - Hearts - Clubs - Diamonds - Spades -");
                                    turn--;
                                }

                                // System.Console.WriteLine("You played a card." + players[turn].hand[cardNum-1].ToString());
                            }
                            else if(players[turn].hand[cardNum].val == myDeck.ShowDiscard().val || players[turn].hand[cardNum].suit == myDeck.ShowDiscard().suit){
                                myDeck.discards.Add(players[turn].Discard(cardNum));
                                if (players[turn].hand.Count <= 0){
                                    System.Console.WriteLine("Congratulations, " + players[turn].name + ", you have won!");
                                    gameOn = false;
                                    return;
                                  }
                            }
                            else {
                                System.Console.WriteLine("Not a valid card to play, please pick a different card or draw.");
                                turn--;
                            }
                        }
                        else {
                            System.Console.WriteLine("Not a valid card number, please choose a card between 1 and " + players[turn].hand.Count + " or enter a command.");
                            turn--;
                        }
                        break;
                }
                turn++;


                //check if the card can be played

            }


            // myDeck.ShowDiscard();

            // System.Console.WriteLine(player1.name + "'s hand: " + player1.ShowHand());
            // System.Console.WriteLine(player2.name + "'s hand: " + player2.ShowHand());

            // myDeck.discards.Add(player1.Discard(1));
            // myDeck.ShowDiscard();
            // myDeck.discards.Add(player2.Discard(2));
            // myDeck.ShowDiscard();

            // System.Console.WriteLine(player1.name + "'s hand: " + player1.ShowHand());
            // System.Console.WriteLine(player2.name + "'s hand: " + player2.ShowHand());
            

        }
    }
}
