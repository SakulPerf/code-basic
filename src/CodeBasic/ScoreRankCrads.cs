namespace CodeBasic
{
    public enum ScoreRankCards
    {
        // ป๊อก 9 
        Poknine = 9,
        // ตอง 
        Tong = 8,
        // ผี 
        Ghost = 7,
        // เรียง
        Straight = 6,
        // สองเด้ง
        TwoPair = 5,
        // สามเด้ง
        ThreePair = 4,
        // ไพ่ธรรมดา่
        NormalCard = 0
    }

    public class SumScore
    {
        public int Score { get; set; }
    }

    public class Cards
    {
        public int RankCards {get;set;}
    }
}