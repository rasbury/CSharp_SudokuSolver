using Sudoku_BusinessLogic;
using System;
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
            List<Cell> solvedcells = BoardTest.SolvedSudokuPuzzle1();
            Board solvedboard = new Board(solvedcells);
            BruteForceSolver solver = new BruteForceSolver();
            solver.BruteForceSolve(ref solvedboard);

            Assert.IsTrue(BoardTest.CellListsAreEqual(solvedboard.Cells, solvedcells), "Brute force solver changed values on an already-solved board");
        }

    }
}
