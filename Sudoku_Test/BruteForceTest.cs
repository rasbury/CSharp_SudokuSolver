using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sudoku_BusinessLogic.Tests
{
    [TestClass()]
    public class BruteForceTest
    {

        [TestMethod()]
        public void BruteForce_SimpleTest()
        {
            //make sure test does nothing on an already-solved board
            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board SolvedBoard = new Board(SolvedCells);
            BruteForceSolver Solver = new BruteForceSolver();
            Solver.BruteForceSolve(ref SolvedBoard);

            Assert.IsTrue(BoardTest.CellListsAreEqual(SolvedBoard.Cells, SolvedCells),
                "Brute force solver changed values on an already-solved board");

            //now let's blank out one value on our board, and see the brute force solver fill it in
            Board MissingOneBoard = new Board(SolvedCells);
            int OriginalValue = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8].Value = 0;
            Solver.BruteForceSolve(ref MissingOneBoard);

            Assert.IsTrue(MissingOneBoard.Cells[8].Value == OriginalValue,
                "Brute force solver failed to fill in 1 value missing from completed board");

            //now let's blank out three values in a row on our board, and see the brute force solver fill them in
            //this is an incredibly easy sudoku
            Board MissingThreeBoard = new Board(SolvedCells);
            int OriginalValue1 = MissingOneBoard.Cells[6].Value;
            MissingOneBoard.Cells[6].Value = 0;
            int OriginalValue2 = MissingOneBoard.Cells[7].Value;
            MissingOneBoard.Cells[7].Value = 0;
            int OriginalValue3 = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8].Value = 0;
            Solver.BruteForceSolve(ref MissingOneBoard);

            Assert.IsTrue(MissingOneBoard.Cells[6].Value == OriginalValue1 &&
                MissingOneBoard.Cells[7].Value == OriginalValue2 &&
                MissingOneBoard.Cells[8].Value == OriginalValue3,
                "Brute force solver failed to fill in 3 values in a row missing from completed board: " +
                OriginalValue1 + " is now " + MissingOneBoard.Cells[6].Value + ", " +
                 OriginalValue2 + " is now " + MissingOneBoard.Cells[7].Value + ", " +
                  OriginalValue3 + " is now " + MissingOneBoard.Cells[8].Value);


            //Let's blank out three more random cells
             OriginalValue1 = MissingOneBoard.Cells[18].Value;
            MissingOneBoard.Cells[18].Value = 0;
             OriginalValue2 = MissingOneBoard.Cells[40].Value;
            MissingOneBoard.Cells[40].Value = 0;
             OriginalValue3 = MissingOneBoard.Cells[78].Value;
            MissingOneBoard.Cells[78].Value = 0;
            Solver.BruteForceSolve(ref MissingOneBoard);

            Assert.IsTrue(MissingOneBoard.Cells[18].Value == OriginalValue1 &&
                MissingOneBoard.Cells[40].Value == OriginalValue2 &&
                MissingOneBoard.Cells[78].Value == OriginalValue3,
                "Brute force solver failed to fill in 3 scattered values missing from completed board: " +
                OriginalValue1 + " is now " + MissingOneBoard.Cells[18].Value + ", " +
                 OriginalValue2 + " is now " + MissingOneBoard.Cells[40].Value + ", " +
                  OriginalValue3 + " is now " + MissingOneBoard.Cells[78].Value);




        }

        [TestMethod()]
        public void BruteForce_ReviseGuessTest()
        {

            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board MissingOneBoard = new Board(SolvedCells);

            //Blanking out values so the first one it comes across has 2 possible valid values, and the first one would be wrong
            //i.e. force it to "guess" and then backtrack
            int OriginalValue1 = MissingOneBoard.Cells[2].Value;
            MissingOneBoard.Cells[2].Value = 0;
            int OriginalValue2 = MissingOneBoard.Cells[11].Value;
            MissingOneBoard.Cells[11].Value = 0;
            int OriginalValue3 = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8].Value = 0;
            BruteForceSolver Solver = new BruteForceSolver();
            Solver.BruteForceSolve(ref MissingOneBoard);

            Assert.IsTrue(MissingOneBoard.Cells[2].Value == OriginalValue1 &&
                MissingOneBoard.Cells[11].Value == OriginalValue2 &&
                MissingOneBoard.Cells[8].Value == OriginalValue3,
                "Brute force solver Revision failed to fill in 3 scattered values missing from completed board: " +
                OriginalValue1 + " is now " + MissingOneBoard.Cells[2].Value + ", " +
                 OriginalValue2 + " is now " + MissingOneBoard.Cells[11].Value + ", " +
                  OriginalValue3 + " is now " + MissingOneBoard.Cells[8].Value);

        }

        [TestMethod()]
        public void BruteForce_ReviseGuessTest_TwoRevisionsNeeded()
        {

            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board MissingOneBoard = new Board(SolvedCells);

            //Blanking out values so the first one it comes across has 2 possible valid values, and the first one would be wrong
            //i.e. force it to "guess" and then backtrack
            int OriginalValue1 = MissingOneBoard.Cells[2].Value;
            MissingOneBoard.Cells[2].Value = 0;
            int OriginalValue2 = MissingOneBoard.Cells[11].Value;
            MissingOneBoard.Cells[11].Value = 0;
            int OriginalValue3 = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8].Value = 0;
            int OriginalValue4 = MissingOneBoard.Cells[7].Value;
            MissingOneBoard.Cells[7].Value = 0;
            int OriginalValue5 = MissingOneBoard.Cells[18].Value;
            MissingOneBoard.Cells[18].Value = 0;
            BruteForceSolver Solver = new BruteForceSolver();
            Solver.BruteForceSolve(ref MissingOneBoard);

            Assert.IsTrue(MissingOneBoard.Cells[2].Value == OriginalValue1 &&
                MissingOneBoard.Cells[11].Value == OriginalValue2 &&
                MissingOneBoard.Cells[8].Value == OriginalValue3 &&
                MissingOneBoard.Cells[7].Value == OriginalValue4 &&
                MissingOneBoard.Cells[18].Value == OriginalValue5,
                "Brute force solver Revision failed to fill in 3 scattered values missing from completed board: " +
                OriginalValue1 + " is now " + MissingOneBoard.Cells[2].Value + ", " +
                 OriginalValue2 + " is now " + MissingOneBoard.Cells[11].Value + ", " +
                  OriginalValue3 + " is now " + MissingOneBoard.Cells[8].Value + ", " +
                  OriginalValue4 + " is now " + MissingOneBoard.Cells[7].Value + ", " +
                  OriginalValue5 + " is now " + MissingOneBoard.Cells[18].Value);

        }

        [TestMethod()]
        public void BruteForce_Board1FullSolve()
        {
            //all right, now the real test
            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board UnSolvedBoard = new Board(BoardTest.UnsolvedSudokuPuzzle1());

            BruteForceSolver Solver = new BruteForceSolver();
            Solver.BruteForceSolve(ref UnSolvedBoard);



            Assert.IsTrue(BoardTest.CellListsAreEqual(UnSolvedBoard.Cells, SolvedCells),
                "Brute force failed to solve the first test board, its result: " + UnSolvedBoard.PrintBoard());
        }
    }
}
