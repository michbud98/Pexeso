using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pexeso;

namespace PexesoUnitTest
{
    [TestClass]
    public class PexesoBoardTest
    {
        [TestMethod]
        public void TestPexesoBoardConstructor()
        {
            //arrange and act
            PexesoBoard board1 = new PexesoBoard(4, 4);
            PexesoBoard board2 = new PexesoBoard(8, 8);
            //assert
            Assert.AreEqual(16,board1.GetPexesoCardsArray().Length);
            Assert.AreEqual(64, board2.GetPexesoCardsArray().Length);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPexesoBoardConstructorException()
        {
            //arrange and act and throw exception
            _ = new PexesoBoard(3, 3);
        }
        [TestMethod]
        public void TestAddToPexesoBoard()
        {
            //arrange
            PexesoBoard board1 = new PexesoBoard(4, 4);
            PexesoBoard board2 = new PexesoBoard(8, 8);
            //act
            board1.AddToPexesoBoard(0, 0);
            board2.AddToPexesoBoard(7, 7);
            //assert
            board1.GetPexesoCard(0, 0).ToString();
            Assert.IsNotNull(board1.GetPexesoCard(0, 0));
            Assert.IsNotNull(board2.GetPexesoCard(7, 7));
        }
        [TestMethod]
        public void TestGetPexesoBoardRows()
        {
            //arrange
            PexesoBoard board1 = new PexesoBoard(4, 4);
            //assert
            Assert.AreEqual(4, board1.getPexesoBoardRows());
        }
        [TestMethod]
        public void TestGetPexesoBoardColumns()
        {
            //arrange
            PexesoBoard board1 = new PexesoBoard(4, 4);
            //assert
            Assert.AreEqual(4, board1.getPexesoBoardColumns());
        }
        [TestMethod]
        public void TestCleanPexesoBoard()
        {
            //arrange
            PexesoBoard board1 = new PexesoBoard(4, 4);
            //arrange
            board1.AddToPexesoBoard(0, 0);
            board1.AddToPexesoBoard(0, 1);
            board1.CleanPexesoBoard();
            //assert
            Assert.IsNull(board1.GetPexesoCard(0,0));
            Assert.IsNull(board1.GetPexesoCard(0, 1));
        }
    }
}
