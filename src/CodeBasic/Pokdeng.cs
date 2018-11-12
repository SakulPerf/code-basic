using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CodeBasic
{
    public class Pokdeng
    {
        public int PlayerBalance { get; set; }
        // Club, Diamond, Heart, Spade (case sensitive)
        public void CheckGameResult(int betAmount, int p1CardNo1, int p1CardNo2, int p1CardNo3, string p1CardSymbol1, string p1CardSymbol2, string p1CardSymbol3, int p2CardNo1, int p2CardNo2, int p2CardNo3, string p2CardSymbol1, string p2CardSymbol2, string p2CardSymbol3)
        {
            var dealerCardsonHand = new CardsOnHand();
            var playerCardsonHand = new CardsOnHand();

            dealerCardsonHand.NumberCards = new int[] { p1CardNo1, p1CardNo2, p1CardNo3 };
            playerCardsonHand.NumberCards = new int[] { p2CardNo1, p2CardNo2, p2CardNo3 };
            dealerCardsonHand.SymbolCards = new string[] { p1CardSymbol1, p1CardSymbol2, p1CardSymbol3 };
            playerCardsonHand.SymbolCards = new string[] { p2CardSymbol1, p2CardSymbol2, p2CardSymbol3 };

            var dealerSumScore = SumScore(dealerCardsonHand);
            var playerSumScore = SumScore(playerCardsonHand);

            var TotalCardsOndealer = CheckTotalCardsonHands(dealerCardsonHand);
            var TotalCardsOnplayer = CheckTotalCardsonHands(playerCardsonHand);
            CheckPlayerWin(betAmount, playerCardsonHand, dealerCardsonHand, dealerSumScore, playerSumScore, TotalCardsOndealer, TotalCardsOnplayer);
        }

        public static int SumScore(CardsOnHand cardsOnHand)
        {
            var sumScore = new SumScore();
            return sumScore.Score = cardsOnHand.NumberCards.Where(numberCards => numberCards <= 9).Sum() % 10;
        }

        public static ScoreRankCards CheckCardsScore(CardsOnHand cardsOnHand)
        {
            var sumScore = new SumScore();
            var score = SumScore(cardsOnHand);
            var StraightNumberCards = OrderbyNumberCard(cardsOnHand);
            if (cardsOnHand.NumberCards[2] == 0)
            {
                if (score >= 8)
                {
                    if (cardsOnHand.SymbolCards[0] == cardsOnHand.SymbolCards[1] || cardsOnHand.NumberCards[0] == cardsOnHand.NumberCards[1])
                    {
                        return ScoreRankCards.Pok2deng;
                    }
                    else
                    {
                        return ScoreRankCards.Pok;
                    }
                }
                // สองเด้ง - ไพ่ในมือมี 2 ใบและเป็น ดอกเดียวกัน หรือ ตัวเลขเดียวกัน 
                if (cardsOnHand.SymbolCards[0] == cardsOnHand.SymbolCards[1] || cardsOnHand.NumberCards[0] == cardsOnHand.NumberCards[1])
                {
                    return ScoreRankCards.TwoPair;
                }
            }
            else
            {
                if (score >= 8)
                {
                    return ScoreRankCards.Pok;
                }
                // สามเด้ง - ไพ่ในมือมี 3 ใบและเป็น ดอกเดียวกัน ถ้าชนะจะได้เงิน 3 เท่า
                if (cardsOnHand.SymbolCards[0] == cardsOnHand.SymbolCards[1] && cardsOnHand.SymbolCards[1] == cardsOnHand.SymbolCards[2])
                {
                    return ScoreRankCards.ThreePair;
                }
                // ตอง - ไพ่ในมือมี 3 ใบและเป็น เลขเดียวกัน
                else if (cardsOnHand.NumberCards[0] == cardsOnHand.NumberCards[1] && cardsOnHand.NumberCards[1] == cardsOnHand.NumberCards[2])
                {
                    return ScoreRankCards.Tong;
                }
                // ผี - ไพ่ในมือมี 3 ใบและเป็นไพ่ในกลุ่ม J, Q, K 

                else if (cardsOnHand.NumberCards.All(c => c == 11 || c == 12 || c == 13))
                {
                    return ScoreRankCards.Ghost;
                }
                // เรียง - ไพ่ในมือมี 3 ใบและเป็น เลขเรียงกัน (Q, K, A ไม่ถือว่าเรียง)
                else if (StraightNumberCards[0] - StraightNumberCards[1] == -1 && StraightNumberCards[1] - StraightNumberCards[2] == -1)
                {
                    return ScoreRankCards.Straight;
                }
            }
            return ScoreRankCards.NormalCard;
        }

        public static List<int> OrderbyNumberCard(CardsOnHand cardsOnHand)
        {
            return cardsOnHand.NumberCards.OrderBy(numberCards => numberCards).ToList();
        }

        public static int CheckRankCardsReward(CardsOnHand PlayerCardScore)
        {
            ScoreRankCards rankCards = CheckCardsScore(PlayerCardScore);
            switch (rankCards)
            {
                case ScoreRankCards.TwoPair:
                case ScoreRankCards.Pok2deng:
                    return 2;
                case ScoreRankCards.Ghost:
                case ScoreRankCards.Straight:
                case ScoreRankCards.ThreePair:
                    return 3;
                case ScoreRankCards.Tong:
                    return 5;
                default:
                    return 1;
            }
        }

        public void CheckPlayerWin(int betAmount, CardsOnHand playerCardsonHand, CardsOnHand dealerCardsonHand, int dealerSumScore, int playerSumScore, int TotalCardsOndealer, int TotalCardsOnplayer)
        {
            var isGameDraw = dealerSumScore == playerSumScore;
            var isPlayerTheWinner = playerSumScore > dealerSumScore;
            if (betAmount > 0)
            {
                if (isPlayerTheWinner)
                {
                    PlayerBalance += betAmount * CheckRankCardsReward(playerCardsonHand);
                }
                else if (dealerSumScore == playerSumScore)
                {
                    if (TotalCardsOnplayer < TotalCardsOndealer)
                    {
                        PlayerBalance += betAmount;
                    }
                    else if (TotalCardsOnplayer > TotalCardsOndealer)
                    {
                        PlayerBalance -= betAmount;
                    }
                    else return;

                }
                else
                {
                    PlayerBalance -= betAmount * CheckRankCardsReward(dealerCardsonHand);
                }
            }
            else return;

        }

        public static int CheckTotalCardsonHands(CardsOnHand CardsonHand)
        {
            return CardsonHand.SymbolCards.Count(it => it != "");
        }
    }
}


