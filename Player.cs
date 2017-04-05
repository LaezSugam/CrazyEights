using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Player
    {
        public string name;
        public List<Card> hand = new List<Card>();
        public Player(string myName)
        {
           name = myName;
        }

        public void Draw(Deck myDeck){
            if (myDeck.cards.Count > 0){
                hand.Add(myDeck.Deal());
            }
            else {
                System.Console.WriteLine("The draw pile is empty, you cannot draw. Either - Pass - Quit - or play a valid card.");
            }
        }

        public Card Discard(int cardNum){
            if (hand.Count > cardNum){
                Card myCard = hand[cardNum];
                hand.RemoveAt(cardNum);
                return myCard;
            }
            else {
                return null;
            }
        }

        public string ShowHand(){
            string str = "";
            foreach (Card card in hand){
                str += " | " + card.ToString();
            }
            return str;
        }
        
    }
}