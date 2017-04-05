using System;

namespace ConsoleApplication
{
    public class Card
    {
        public string stringVal;
        public string suit;
        public int val;

        public Card(int value, string cardSuit){
            val = value;
            suit = cardSuit;
            if (value == 1){
                stringVal = "Ace";
            }
            else if (value == 11){
                stringVal = "Jack";
            }
            else if (value == 12){
                stringVal = "Queen";
            }
            else if (value == 13){
                stringVal = "King";
            }
            else {
                stringVal = value.ToString();
            }
        }

        public override string ToString(){
            string str = "";
            str += stringVal + " of " + suit;
            return str;
        }
    }
}
