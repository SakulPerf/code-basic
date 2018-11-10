using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeBasic
{
    public class Deck : IComparable<Deck>
    {
        private const int ScoreBreakingPoint = 10;
        private const int SingleElement = 1;
        private const int TripleCards = 3;

        public IEnumerable<Card> Cards { get; private set; }

        public int Score => Cards.Sum(it => it.No) % ScoreBreakingPoint;
        public bool IsGainDouble => AllCardsAreSame || AllSymbolsAreSame;
        public bool IsGainTriple => AllSymbolsAreSame && Cards.Count() == TripleCards;
        private bool AllCardsAreSame => Cards.Select(it => it.No).Distinct().Count() == SingleElement;
        private bool AllSymbolsAreSame => Cards.Select(it => it.Symbol.ToLower()).Distinct().Count() == SingleElement;
        public bool IsPok
        {
            get
            {
                const int Pok9 = 9;
                const int Pok8 = 8;
                var pocks = new[] { Pok9, Pok8 };
                const int DoubleCards = 2;
                var isNoRequestMoreCards = Cards.Count() == DoubleCards;
                return pocks.Contains(Score) && isNoRequestMoreCards;
            }
        }

        public Deck(IEnumerable<Card> cards)
        {
            const int MinimumNo = 1;
            const int MaximumNo = 13;
            Cards = cards.Where(it =>
                it != null
                && it.No >= MinimumNo
                && it.No <= MaximumNo
                && !string.IsNullOrEmpty(it.Symbol));
        }

        public int CompareTo(Deck other)
        {
            if(IsPok && other.IsPok)
            {
                var isDraw = Score == other.Score || (IsGainTriple && other.IsGainTriple);
                if (isDraw)
                {
                    return 0;
                }
                else
                {
                    var win = Score > other.Score;
                    return win ? 1 : -1;
                }
            }
            else if (IsPok)
            {
                return 1;
            }
            else if (other.IsPok)
            {
                return -1;
            }

            var draw = Score == other.Score || (IsGainTriple && other.IsGainTriple);
            if (draw)
            {
                return 0;
            }
            else
            {
                var win = Score > other.Score;
                return win ? 1 : -1;
            }
        }
    }
}
