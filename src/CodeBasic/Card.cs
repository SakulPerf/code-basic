using System;

namespace CodeBasic
{
    public class Card
    {
        public int No { get; set; }
        public string Symbol { get; set; }

        public Card(int no, string symbol)
        {
            No = no;
            Symbol = symbol ?? string.Empty;
        }
    }
}
