using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleMinds;
using System.Linq;
using TicTacToe.Public;

namespace SimpleMindsTests
{
    [TestClass]
    public class RandomMindTests
    {
        private RandomMind testMind;

        [TestInitialize]
        public void Initialize()
        {
            testMind = new RandomMind();
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
        public void ShouldReturnOneOfTwoMoves()
        {
            var moves = new Move[] { new Move(0, 0), new Move(1, 0) };
            var chosenMove = testMind.PickMove(moves);
            Assert.IsTrue(moves.Contains(chosenMove));
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void ShouldReturnOneMoveOfMany()
        {
            var moves = new Move[] { new Move(1, 1), new Move(2, 1), new Move(0, 2), new Move(1, 2), new Move(2, 2) };
            var chosenMove = testMind.PickMove(moves);
            Assert.IsTrue(moves.Contains(chosenMove));
        }
    }
}
