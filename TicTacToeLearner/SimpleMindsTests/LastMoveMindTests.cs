using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMinds;
using System.Linq;
using TicTacToe.Public;

namespace SimpleMindsTests
{
    [TestClass]
    public class LastMoveMindTests
    {
        private LastMoveMind testMind;

        [TestInitialize]
        public void Initialize()
        {
            testMind = new LastMoveMind();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ShouldReturnNullForNoMoves()
        {
            var noMoves = Enumerable.Empty<Move>();
            var chosenMove = testMind.PickMove(noMoves);
            Assert.IsNull(chosenMove);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ShouldReturnOnlyMoveForSingleMove()
        {
            var onlyMove = new Move(1, 1);
            var chosenMove = testMind.PickMove(new Move[] { onlyMove });
            Assert.AreEqual(onlyMove, chosenMove);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ShouldReturnLastMoveOfTwo()
        {
            var firstMove = new Move(0, 0);
            var secondMove = new Move(1, 0);
            var chosenMove = testMind.PickMove(new Move[] { firstMove, secondMove });
            Assert.AreEqual(secondMove, chosenMove);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ShouldReturnLastMoveOfMany()
        {
            var firstMove = new Move(1, 1);
            var secondMove = new Move(2, 1);
            var thirdMove = new Move(0, 2);
            var fourthMove = new Move(1, 2);
            var fifthMove = new Move(2, 2);
            var chosenMove = testMind.PickMove(new Move[] { firstMove, secondMove, thirdMove, fourthMove, fifthMove });
            Assert.AreEqual(fifthMove, chosenMove);
        }
    }
}
