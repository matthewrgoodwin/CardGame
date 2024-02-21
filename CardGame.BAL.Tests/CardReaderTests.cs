using System.Formats.Tar;

namespace CardGame.BAL.Tests
{
    public class Tests
    {
        private ICardReader _Reader;

        [OneTimeSetUp]
        public void Setup()
        {
            _Reader = new CardReader();
        }

        [Test]
        [TestCase("2C", "2")]
        [TestCase("3C", "3")]
        [TestCase("4C", "4")]
        [TestCase("5C", "5")]
        [TestCase("6C", "6")]
        [TestCase("7C", "7")]
        [TestCase("8C", "8")]
        [TestCase("9C", "9")]
        [TestCase("TC", "10")]
        [TestCase("JC", "11")]
        [TestCase("QC", "12")]
        [TestCase("KC", "13")]
        [TestCase("AC", "14")]
        public void ParseCardList_OneCardNoMultiplier_ReturnCorrectValue(string input, string expected)
        {
            //Act
            string actual = _Reader.ParseCardList(input);

            //Assert
            Assert.That(actual == expected);
        }

        [Test]
        [TestCase("2D", "4")]
        [TestCase("2H", "6")]
        [TestCase("2S", "8")]
        public void ParseCardList_OneCardWithSuiteMultiplier_ReturnCorrectValue(string input, string expected)
        {
            //Act
            string actual = _Reader.ParseCardList(input);

            //Assert
            Assert.That(actual == expected);
        }

        [Test]
        [TestCase("3C,4C", "7")]
        [TestCase("TC,TD,TH,TS", "100")]
        public void ParseCardList_MultipleCardsNoJoker_ReturnCorrectValue(string input, string expected)
        {
            //Act
            string actual = _Reader.ParseCardList(input);

            //Assert
            Assert.That(actual == expected);
        }

        [Test]
        [TestCase("JK", "0")]
        [TestCase("JK,JK", "0")]
        [TestCase("JK,2C,JK", "8")]
        [TestCase("TC,TD,JK,TH,TS", "200")]
        [TestCase("TC,TD,JK,TH,TS,JK", "400")]
        public void ParseCardList_CardListContainsJoker_ReturnCorrectValue(string input, string expected)
        {
            //Act
            string actual = _Reader.ParseCardList(input);

            //Assert
            Assert.That(actual == expected);
        }

        [Test]
        [TestCase("1S")]
        [TestCase("2B")]
        [TestCase("2S,1S")]
        [TestCase("4d")]
        public void ParseCardList_CardListContainsInvalidCard_ReturnNotRecognised(string input)
        {
            //Act
            string actual = _Reader.ParseCardList(input);

            //Assert
            Assert.That(actual == "Card not recognised");
        }

        [Test]
        [TestCase("3H,3H")]
        [TestCase("4D,5D,4D")]
        public void ParseCardList_CardListContainsDuplicateCards_ReturnCardsCannotBeDuplicated(string input)
        {
            //Act
            string actual = _Reader.ParseCardList(input);

            //Assert
            Assert.That(actual == "Cards cannot be duplicated");
        }

        [Test]
        [TestCase("JK,JK,2C,JK")]
        public void ParseCardList_CardListContainsMoreThanTwoJokers_ReturnMoreThanTwoJokersError(string input)
        {
            //Act
            string actual = _Reader.ParseCardList(input);

            //Assert
            Assert.That(actual == "A hand cannot contain more than two Jokers");
        }

        [Test]
        [TestCase("2S|3D")]
        [TestCase("2D 4D 6H")]
        public void ParseCardList_CardListHasInvalidFormatting_ReturnInvalidInputString(string input)
        {
            //Act
            string actual = _Reader.ParseCardList(input);

            //Assert
            Assert.That(actual == "Invalid input string");
        }
    }
}