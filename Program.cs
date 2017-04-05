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

            Console.WriteLine("\nEnter second player's name.");
            inputline  = Console.ReadLine();
            Player player2 = new Player(inputline);

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

                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("\n************************************" + players[turn].name.ToUpper() +"'s TURN************************************");
                Console.ForegroundColor = ConsoleColor.White;



                //show hand
                System.Console.WriteLine("\n" + players[turn].name + "'s hand: " + players[turn].ShowHand());

                //show the discard pile
                System.Console.WriteLine("\nThe top card on the discard pile is a(n) " + myDeck.ShowDiscard().ToString());

                //prompt which card to play
                System.Console.WriteLine("\nEnter a command - Draw or Quit - or enter a card number between 1 and " + players[turn].hand.Count);

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
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("\nYou can only pass if the draw pile is empty.");
                            Console.ForegroundColor = ConsoleColor.White;
                            turn--;
                            break;
                        }                        
                    case "quit":
                        gameOn = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        System.Console.WriteLine("\n" + players[turn].name + " is giving up like a big whiny baby.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    default:
                        if (int.TryParse(inputline, out cardNum) && cardNum <= players[turn].hand.Count && cardNum > 0){
                            cardNum--;
                            if(players[turn].hand[cardNum].val == 8){
                                System.Console.WriteLine("\n" +  players[turn].name + ", please enter the suit (- Hearts - Clubs - Diamonds - Spades -) for your Crazy Eight:");
                                inputline = Console.ReadLine();
                                //Change the suit of the Eight
                                if(myDeck.suits.Contains(inputline)){
                                    players[turn].hand[cardNum].suit = inputline;
                                    myDeck.discards.Add(players[turn].Discard(cardNum));
                                    // System.Console.WriteLine("\nThat's totally a suit.");
                                    if (players[turn].hand.Count <= 0){
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        System.Console.WriteLine("\n*******************CONGRATULATIONS, " + players[turn].name + ", YOU HAVE WON!*********************");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        gameOn = false;
                                        return;
                                    }
                                }
                                else {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    System.Console.WriteLine("\n" + players[turn].name + ", please enter a valid suit: - Hearts - Clubs - Diamonds - Spades -");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    turn--;
                                }

                                // System.Console.WriteLine("\nYou played a card." + players[turn].hand[cardNum-1].ToString());
                            }
                            else if(players[turn].hand[cardNum].val == myDeck.ShowDiscard().val || players[turn].hand[cardNum].suit == myDeck.ShowDiscard().suit){
                                myDeck.discards.Add(players[turn].Discard(cardNum));
                                if (players[turn].hand.Count <= 0){
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    System.Console.WriteLine("\n*******************CONGRATULATIONS, " + players[turn].name + ", YOU HAVE WON!*********************");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    gameOn = false;
                                    return;
                                  }
                            }
                            else {
                                Console.ForegroundColor = ConsoleColor.Red;
                                System.Console.WriteLine("\n" + players[turn].name + ", this is not a valid card to play, please pick a different card or draw.");
                                Console.ForegroundColor = ConsoleColor.White;
                                turn--;
                            }
                        }
                        else {
                            Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("\n" + players[turn].name + ", this is not a valid card number, please choose a card between 1 and " + players[turn].hand.Count + " or enter a command.");
                            Console.ForegroundColor = ConsoleColor.White;
                            turn--;
                        }
                        break;
                }
                turn++;
                

                //check if the card can be played

            }


            // myDeck.ShowDiscard();

            // System.Console.WriteLine(p\nlayer1.name + "'s hand: " + player1.ShowHand());
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
