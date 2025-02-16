using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Model
{
    /// <summary>
    /// Klasa umożliwia dobór kart z talii, zgodnie z przyjętą strategią.
    /// </summary>
    public class CardDraw
    {
        private CardSetSize cardSetSize;
        private DifficultyLevel difficultyLevel;

        public CardDraw(CardSetSize cardSetSize, DifficultyLevel difficultyLevel)
        {
            this.cardSetSize = cardSetSize;
            this.difficultyLevel = difficultyLevel;
        }

        public CardSet PullSet(out Deck fromDeck)
        {
            throw new NotImplementedException();
        }

        public Card PullOne(out Deck fromDeck)
        {
            throw new NotImplementedException();
        }
    }
}
