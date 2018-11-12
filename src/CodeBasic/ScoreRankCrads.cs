namespace CodeBasic
{
    public enum ScoreRankCards
    {
        // ป๊อก 9 8
        Pok = 9,
        // ป๊อก 9 2 เด้ง
        Pok2deng = 8,
        // ตอง 
        Tong = 7,
        // ผี 
        Ghost = 6,
        // เรียง
        Straight = 5,
        // สองเด้ง
        TwoPair = 4,
        // สามเด้ง
        ThreePair = 3,
        // ไพ่ธรรมดา่
        NormalCard = 0
    }

    public class SumScore
    {
        public int Score { get; set; }
    }
}