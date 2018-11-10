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
            if (Score == other.Score || (IsGainTriple && other.IsGainTriple))
            {
                return 0;
            }
            else if (Score > other.Score)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}
