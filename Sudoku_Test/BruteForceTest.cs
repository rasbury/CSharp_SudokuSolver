using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sudoku_BusinessLogic.Tests
{
    [TestClass()]
    public class BruteForceTest
    {

        [TestMethod()]
        public void BruteForceSimpleTest()
        {
            //make sure test does nothing on an already-solved board
            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board SolvedBoard = new Board(SolvedCells);
            BruteForceSolver Solver = new BruteForceSolver();
            Solver.BruteForceSolve(ref SolvedBoard);

            Assert.IsTrue(BoardTest.CellListsAreEqual(SolvedBoard.Cells, SolvedCells), "Brute force solver changed values on an already-solved board");

            //now let's blank out one value on our board, and see the brute force solver fill it in

            //Board MissingOneBoard = new Board(solvedcells);
        }

    }
}
