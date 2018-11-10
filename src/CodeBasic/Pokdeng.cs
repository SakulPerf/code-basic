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

            var dealerPoints = p1CardNo1 + p1CardNo2 + p1CardNo3;
            var playerPoints = p2CardNo1 + p2CardNo2 + p2CardNo3;

            var isGameDraw = dealerPoints == playerPoints;
            if (isGameDraw) return;

            var isPlayerTheWinner = playerPoints > dealerPoints;
            if (isPlayerTheWinner)
            {
                var areCardsSame = p2CardNo1 == p2CardNo2;
                var areSymbolesSame = p2CardSymbol1 == p2CardSymbol2;
                var isDouble = areCardsSame || areSymbolesSame;
                PlayerBalance += isDouble ? betAmount * 2 : betAmount;
            }
            else
            {
                var areCardsSame = p1CardNo1 == p1CardNo2;
                var areSymbolesSame = p1CardSymbol1 == p1CardSymbol2;
                var isDouble = areCardsSame || areSymbolesSame;
                PlayerBalance -= isDouble ? betAmount * 2 : betAmount;
            }
        }
    }
}
