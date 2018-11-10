using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBasic
{
    public class Pokdeng
    {
        public int PlayerBalance { get; set; }

        // Club, Diamond, Heart, Spade (case sensitive)
        public void CheckGameResult(
            int betAmount,
            int p1CardNo1, int p1CardNo2, int p1CardNo3,
            string p1CardSymbol1, string p1CardSymbol2, string p1CardSymbol3,
            int p2CardNo1, int p2CardNo2, int p2CardNo3,
            string p2CardSymbol1, string p2CardSymbol2, string p2CardSymbol3)
        {
            var isBetAmountValid = betAmount <= PlayerBalance;
            if (!isBetAmountValid)
            {
                return;
            }

            var dealerDeck = createDeck(p1CardNo1, p1CardSymbol1, p1CardNo2, p1CardSymbol2, p1CardNo3, p1CardSymbol3);
            var playerDeck = createDeck(p2CardNo1, p2CardSymbol1, p2CardNo2, p2CardSymbol2, p2CardNo3, p2CardSymbol3);
            var comparedResult = playerDeck.CompareTo(dealerDeck);

            const int Draw = 0;
            const int PlayerWin = 1;
            const int DealerWin = -1;
            switch (comparedResult)
            {
                case PlayerWin:
                    PlayerBalance += calculateAmountToUpdateBalance(playerDeck, betAmount);
                    break;
                case DealerWin:
                    PlayerBalance -= calculateAmountToUpdateBalance(dealerDeck, betAmount);
                    break;
                case Draw:
                default:
                    break;
            }
        }

        private Deck createDeck(int card1, string symbol1, int card2, string symbol2, int card3, string symbol3)
            => new Deck(new List<Card>
            {
                new Card(card1, symbol1),
                new Card(card2, symbol2),
                new Card(card3, symbol3),
            });

        private int calculateAmountToUpdateBalance(Deck deck, int betAmount)
        {
            var multiplier = 1;
            if (deck.IsGainTriple)
            {
                const int Triple = 3;
                multiplier = Triple;
            }
            else if (deck.IsGainDouble)
            {
                const int Double = 2;
                multiplier = Double;
            }

            return betAmount * multiplier;
        }
    }
}
