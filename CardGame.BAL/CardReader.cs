namespace CardGame.BAL
{
    public class CardReader : ICardReader
    {
        private static readonly Dictionary<char, int> ValuesByCardNumber = new Dictionary<char, int>()
        {
            {'2', 2 },
            {'3', 3 },
            {'4', 4 },
            {'5', 5 },
            {'6', 6 },
            {'7', 7 },
            {'8', 8 },
            {'9', 9 },
            {'T', 10},
            {'J', 11 },
            {'Q', 12 },
            {'K', 13 },
            {'A', 14 }
        };

        private static readonly Dictionary<char, int> MultiplierByCardSuite = new Dictionary<char, int>()
        {
            {'C', 1 },
            {'D', 2 },
            {'H', 3 },
            {'S', 4 }
        };

        public string ParseCardList(string cardList)
        {
            if (cardList == null)
            {
                return "0";
            }

            string[] cardArr = cardList.Split(',');
            HashSet<string> cardsCounted = new HashSet<string>();
            int jokerMultiplier = 1;
            int totalValue = 0;

            foreach (string card in cardArr)
            {
                //card validation
                if (card.Length != 2)
                {
                    return "Invalid input string";
                }

                //check for special card before checking for regular card value
                if (card == "JK")
                {
                    if (jokerMultiplier >= 4)
                    {
                        return "A hand cannot contain more than two Jokers";
                    }
                    jokerMultiplier *= 2;
                    continue;
                }

                //calculate card value

                //first check if card already counted
                if (cardsCounted.Contains(card))
                {
                    return "Cards cannot be duplicated";
                }

                int currentCardValue = 0;
                if (ValuesByCardNumber.TryGetValue(card[0], out int cardValue))
                {
                    currentCardValue += cardValue;
                }
                else
                {
                    return "Card not recognised";
                }

                if (MultiplierByCardSuite.TryGetValue(card[1], out int suiteValue))
                {
                    cardValue *= suiteValue;
                }
                else
                {
                    return "Card not recognised";
                }

                cardsCounted.Add(card);
                totalValue += cardValue;
            }

            totalValue *= jokerMultiplier;

            return totalValue.ToString();
        }
    }
}
