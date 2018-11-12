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
        public void CheckGameResult(
            int betAmount,
            int p1CardNo1, int p1CardNo2, int p1CardNo3,
            string p1CardSymbol1, string p1CardSymbol2, string p1CardSymbol3,
            int p2CardNo1, int p2CardNo2, int p2CardNo3,
            string p2CardSymbol1, string p2CardSymbol2, string p2CardSymbol3)
        {
            var dealerCardsonHand = new CardsOnHand();
            var playerCardsonHand = new CardsOnHand();

            dealerCardsonHand.NumberCards = new int[] { p1CardNo1, p1CardNo2, p1CardNo3 };
            playerCardsonHand.NumberCards = new int[] { p2CardNo1, p2CardNo2, p2CardNo3 };
            dealerCardsonHand.SymbolCards = new string[] { p1CardSymbol1, p1CardSymbol2, p1CardSymbol3 };
            playerCardsonHand.SymbolCards = new string[] { p2CardSymbol1, p2CardSymbol2, p2CardSymbol3 };

            var dealerSumScore = SumScore(dealerCardsonHand);
            var playerSumScore = SumScore(playerCardsonHand);

            var isGameDraw = dealerSumScore == playerSumScore;
            if (isGameDraw) return;

            var isPlayerTheWinner = playerSumScore > dealerSumScore;

            if (isPlayerTheWinner)
            {
                PlayerBalance += betAmount * CheckRankCardsReward(playerCardsonHand);
            }
            else
            {
                PlayerBalance -= betAmount * CheckRankCardsReward(playerCardsonHand);
            }
        }
        private static int SumScore(CardsOnHand cardsOnHand)
        {
            var sumScore = new SumScore();
            return sumScore.Score = cardsOnHand.NumberCards.Where(numberCards => numberCards < 10).Sum() % 10;
        }

        private static int CheckRankCardsReward(CardsOnHand PlayerCardScore)
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

        private static ScoreRankCards CheckCardsScore(CardsOnHand cardsOnHand)
        {
            var sumScore = new SumScore();
            var score = SumScore(cardsOnHand);
            var StraightNumberCards = cardsOnHand.NumberCards.OrderBy(numberCards => numberCards).ToList();
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
                // สองเด้ง - ไพ่ในมือมี 2 ใบและเป็น ดอกเดียวกัน หรือ ตัวเลขเดียวกัน ถ้าชนะจะได้เงิน 2 เท่า
                if (cardsOnHand.SymbolCards[0] == cardsOnHand.SymbolCards[1] || cardsOnHand.NumberCards[0] == cardsOnHand.NumberCards[1])
                {
                    return ScoreRankCards.TwoPair;
                }

            }
            else
            {
                // สามเด้ง - ไพ่ในมือมี 3 ใบและเป็น ดอกเดียวกัน ถ้าชนะจะได้เงิน 3 เท่า
                if (cardsOnHand.SymbolCards[0] == cardsOnHand.SymbolCards[1] && cardsOnHand.SymbolCards[1] == cardsOnHand.SymbolCards[2])
                {
                    return ScoreRankCards.ThreePair;
                }
                // ตอง - ไพ่ในมือมี 3 ใบและเป็น เลขเดียวกัน ถ้าชนะจะได้เงิน 5 เท่า
                else if (cardsOnHand.NumberCards[0] == cardsOnHand.NumberCards[1] && cardsOnHand.NumberCards[1] == cardsOnHand.NumberCards[2])
                {
                    return ScoreRankCards.Tong;
                }
                // ผี - ไพ่ในมือมี 3 ใบและเป็นไพ่ในกลุ่ม J, Q, K 

                else if (cardsOnHand.NumberCards.All(c => c == 11 || c == 12 || c == 13))
                {
                    return ScoreRankCards.Ghost;
                }
                // เรียง - ไพ่ในมือมี 3 ใบและเป็น เลขเรียงกัน ถ้าชนะจะได้เงิน 3 เท่า (Q, K, A ไม่ถือว่าเรียง)
                else if (StraightNumberCards[0] - StraightNumberCards[1] == -1 && StraightNumberCards[1] - StraightNumberCards[2] == -1)
                {
                    return ScoreRankCards.Straight;
                }
            }
            return ScoreRankCards.NormalCard;
        }

    }
}

// วิธีการเล่น
// ผู้เล่น 2 คนทำการวางเงินเดิมพัน
// ผู้เล่นทั้งสองจะได้รับไพ่ 2 คนละ 2 ใบ
// ผู้เล่นจะขอไพ่เพิ่มได้อีก 1 ใบ ถ้าเจ้ามือไม่ได้ไพ่ป๊อก
// ผู้เล่นที่มีผลรวมของไพ่ในมือสูงสุดคือผู้ชนะ (จะได้เงินเท่ากับเงินที่ตัวเองเดิมพัน)

// เงินรางวัลพิเศษ
// ป๊อก - ไพ่ในมือมี 2 ใบ และผลรวมคือ 8 หรือ 9 (ไพ่ป๊อกจะชนะไพ่ 3 ใบเสมอ) -- 

// ตอง - ไพ่ในมือมี 3 ใบและเป็น เลขเดียวกัน ถ้าชนะจะได้เงิน 5 เท่า -- 
// ผี - ไพ่ในมือมี 3 ใบและเป็นไพ่ในกลุ่ม J, Q, K ถ้าชนะจะได้เงิน 3 เท่า -- 

// เรียง - ไพ่ในมือมี 3 ใบและเป็น เลขเรียงกัน ถ้าชนะจะได้เงิน 3 เท่า (Q, K, A ไม่ถือว่าเรียง) -- 

// สองเด้ง - ไพ่ในมือมี 2 ใบและเป็น ดอกเดียวกัน หรือ ตัวเลขเดียวกัน ถ้าชนะจะได้เงิน 2 เท่า --
// สามเด้ง - ไพ่ในมือมี 3 ใบและเป็น ดอกเดียวกัน ถ้าชนะจะได้เงิน 3 เท่า -- 

// สองเด้ง กับ สามเด้ง ความสำคัญเท่ากัน แต้มที่เยอะกว่าเป็นฝ่ายชนะ
// ไพ่ธรรมดา 1-7


// Poknine = 9, จะได้เงินเท่ากับเงินที่ตัวเองเดิมพัน
// Tong = 8, *5

// Ghost = 7, *3
// Straight = 6, *3
// ThreePair = 4, *3

// TwoPair = 5, *2
// NormalCard = 0

