using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sudoku_BusinessLogic.Tests
{
    [TestClass()]
    public class BruteForceTest
    {

        [TestMethod()]
        public void BruteForce_SingleState_SimpleTest()
        {
            //For testing purposes, if we need to zero out a cell, we reassign the value in the list to a new "zero cell"
            //When lists of cells are initialized, if they have values filled in, those values are considered non-changeable
            //which is good, we want that, just for the purposes of fiddling with data for tests, that's less convenient

            //make sure test does nothing on an already-solved board
            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board SolvedBoard = new Board(SolvedCells);
            BruteForceSolver Solver = new BruteForceSolver();
            Solver.SingleStateBruteForceSolve(ref SolvedBoard);

            Assert.IsTrue(BoardTest.CellListsAreEqual(SolvedBoard.Cells, SolvedCells),
                "Brute force solver changed values on an already-solved board");

            //now let's blank out one value on our board, and see the brute force solver fill it in
            Board MissingOneBoard = new Board(SolvedCells);
            int OriginalValue = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8] = new Cell(0);
            Solver.SingleStateBruteForceSolve(ref MissingOneBoard);

            Assert.IsTrue(MissingOneBoard.Cells[8].Value == OriginalValue,
                "Brute force solver failed to fill in 1 value missing from completed board");

            //now let's blank out three values in a row on our board, and see the brute force solver fill them in
            //this is an incredibly easy sudoku

            Board MissingThreeBoard = new Board(SolvedCells);
            int OriginalValue1 = MissingOneBoard.Cells[6].Value;
            MissingOneBoard.Cells[6] = new Cell(0);
            int OriginalValue2 = MissingOneBoard.Cells[7].Value;
            MissingOneBoard.Cells[7] = new Cell(0);
            int OriginalValue3 = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8] = new Cell(0);
            Solver.SingleStateBruteForceSolve(ref MissingOneBoard);

            Assert.IsTrue(MissingOneBoard.Cells[6].Value == OriginalValue1 &&
                MissingOneBoard.Cells[7].Value == OriginalValue2 &&
                MissingOneBoard.Cells[8].Value == OriginalValue3,
                "Brute force solver failed to fill in 3 values in a row missing from completed board: " +
                OriginalValue1 + " is now " + MissingOneBoard.Cells[6].Value + ", " +
                 OriginalValue2 + " is now " + MissingOneBoard.Cells[7].Value + ", " +
                  OriginalValue3 + " is now " + MissingOneBoard.Cells[8].Value);


            //Let's blank out three more random cells
            OriginalValue1 = MissingOneBoard.Cells[18].Value;
            MissingOneBoard.Cells[18] = new Cell(0);
            OriginalValue2 = MissingOneBoard.Cells[40].Value;
            MissingOneBoard.Cells[40] = new Cell(0);
            OriginalValue3 = MissingOneBoard.Cells[78].Value;
            MissingOneBoard.Cells[78] = new Cell(0);
            Solver.SingleStateBruteForceSolve(ref MissingOneBoard);

            Assert.IsTrue(MissingOneBoard.Cells[18].Value == OriginalValue1 &&
                MissingOneBoard.Cells[40].Value == OriginalValue2 &&
                MissingOneBoard.Cells[78].Value == OriginalValue3,
                "Brute force solver failed to fill in 3 scattered values missing from completed board: " +
                OriginalValue1 + " is now " + MissingOneBoard.Cells[18].Value + ", " +
                 OriginalValue2 + " is now " + MissingOneBoard.Cells[40].Value + ", " +
                  OriginalValue3 + " is now " + MissingOneBoard.Cells[78].Value);




        }

        [TestMethod()]
        public void BruteForce_SingleState_ReviseGuessTest()
        {

            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board MissingOneBoard = new Board(SolvedCells);

            //Blanking out values so the first one it comes across has 2 possible valid values, and the first one would be wrong
            //i.e. force it to "guess" and then backtrack
            int OriginalValue1 = MissingOneBoard.Cells[2].Value;
            MissingOneBoard.Cells[2] = new Cell(0);
            int OriginalValue2 = MissingOneBoard.Cells[11].Value;
            MissingOneBoard.Cells[11] = new Cell(0);
            int OriginalValue3 = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8] = new Cell(0);
            BruteForceSolver Solver = new BruteForceSolver();
            Solver.SingleStateBruteForceSolve(ref MissingOneBoard);

            Assert.IsTrue(MissingOneBoard.Cells[2].Value == OriginalValue1 &&
                MissingOneBoard.Cells[11].Value == OriginalValue2 &&
                MissingOneBoard.Cells[8].Value == OriginalValue3,
                "Brute force solver Revision failed to fill in 3 scattered values missing from completed board: " +
                OriginalValue1 + " is now " + MissingOneBoard.Cells[2].Value + ", " +
                 OriginalValue2 + " is now " + MissingOneBoard.Cells[11].Value + ", " +
                  OriginalValue3 + " is now " + MissingOneBoard.Cells[8].Value);

        }

        /// <summary>
        /// Make sure we don't get into an infinite loop
        /// </summary>
        [TestMethod()]
        public void BruteForce_SingleState_ReviseGuessTest_TwoRevisionsNeeded()
        {

            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board MissingOneBoard = new Board(SolvedCells);

            //Blanking out values so the first one it comes across has 2 possible valid values, and the first one would be wrong
            //i.e. force it to "guess" and then backtrack
            int OriginalValue1 = MissingOneBoard.Cells[2].Value;
            MissingOneBoard.Cells[2] = new Cell(0);
            int OriginalValue2 = MissingOneBoard.Cells[11].Value;
            MissingOneBoard.Cells[11] = new Cell(0);
            int OriginalValue3 = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8] = new Cell(0);
            int OriginalValue4 = MissingOneBoard.Cells[7].Value;
            MissingOneBoard.Cells[7] = new Cell(0);
            int OriginalValue5 = MissingOneBoard.Cells[18].Value;
            MissingOneBoard.Cells[18] = new Cell(0);
            BruteForceSolver Solver = new BruteForceSolver();
            Solver.SingleStateBruteForceSolve(ref MissingOneBoard);

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
        public void BruteForce_SingleState_FullSolve()
        {
            //all right, now the real test
            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board UnSolvedBoard = new Board(BoardTest.UnsolvedSudokuPuzzle1());

            BruteForceSolver Solver = new BruteForceSolver();
            Solver.SingleStateBruteForceSolve(ref UnSolvedBoard);



            Assert.IsTrue(BoardTest.CellListsAreEqual(UnSolvedBoard.Cells, SolvedCells),
                "Brute force failed to solve the first test board, its result: " + UnSolvedBoard.PrintBoard());



            SolvedCells = BoardTest.SolvedSudokuPuzzle2();
            UnSolvedBoard = new Board(BoardTest.UnsolvedSudokuPuzzle2());

            Solver.SingleStateBruteForceSolve(ref UnSolvedBoard);


            Assert.IsTrue(BoardTest.CellListsAreEqual(UnSolvedBoard.Cells, SolvedCells),
    "Brute force failed to solve the second test board, its result: " + UnSolvedBoard.PrintBoard());
        }

        [TestMethod()]
        public void BruteForce_SingleState_UnsolvableHandling()
        {

            //This puzzle is not invalid yet, but it's going to become invalid real quick
            //I think this is about the only "unsolvable" option for brute force
            Board UnSolvedBoard = new Board(BoardTest.UnsolvablePuzzle());
            bool HitExpectedException = false;

            BruteForceSolver Solver = new BruteForceSolver();
            try
            {
                Solver.SingleStateBruteForceSolve(ref UnSolvedBoard);
            }
            catch (Exception e)
            {
                if (e.Message.Equals(BruteForceSolver.UnsolvableBoardmessage)) { HitExpectedException = true; }
                else { throw e; }
            }
            finally
            {
                if (!HitExpectedException) { Assert.Fail("Brute Force failed to hit the expected Unsolvable exception."); }
            }


            //And now make sure we fail fast and complain if they give us a board that's already invalid
            UnSolvedBoard = new Board(BoardTest.UnsolvablePuzzle());
            UnSolvedBoard.Cells[8] = new Cell(5);
            HitExpectedException = false;

            try
            {
                Solver.SingleStateBruteForceSolve(ref UnSolvedBoard);
            }
            catch (Exception e)
            {
                if (e.Message.Equals(BruteForceSolver.InvalidBoardMessage)) { HitExpectedException = true; }
                else { throw e; }
            }
            finally
            {
                if (!HitExpectedException) { Assert.Fail("Brute Force failed to hit the expected Invalid Board exception."); }
            }

        }



        [TestMethod()]
        public void BruteForce_Recursive_SimpleTest()
        {
            //For testing purposes, if we need to zero out a cell, we reassign the value in the list to a new "zero cell"
            //When lists of cells are initialized, if they have values filled in, those values are considered non-changeable
            //which is good, we want that, just for the purposes of fiddling with data for tests, that's less convenient

            bool MultipleSolutionsFound = false;

            //make sure test does nothing on an already-solved board
            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board SolvedBoard = new Board(SolvedCells);
            BruteForceSolver Solver = new BruteForceSolver();
            Solver.RecursiveBruteForceSolve(ref SolvedBoard, ref MultipleSolutionsFound);

            Assert.IsTrue(BoardTest.CellListsAreEqual(SolvedBoard.Cells, SolvedCells),
                "Brute force solver changed values on an already-solved board");
            Assert.IsFalse(MultipleSolutionsFound,
                "Recursive Brute force solver said there were multiple solutions on an already-solved board");

            //now let's blank out one value on our board, and see the brute force solver fill it in
            Board MissingOneBoard = new Board(SolvedCells);
            int OriginalValue = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8] = new Cell(0);
            Solver.RecursiveBruteForceSolve(ref MissingOneBoard, ref MultipleSolutionsFound);

            Assert.IsTrue(MissingOneBoard.Cells[8].Value == OriginalValue,
                "Brute force solver failed to fill in 1 value missing from completed board");
            Assert.IsFalse(MultipleSolutionsFound,
                "Recursive Brute force solver said there were multiple solutions on a board missing 1 value");

            //now let's blank out three values in a row on our board, and see the brute force solver fill them in
            //this is an incredibly easy sudoku

            Board MissingThreeBoard = new Board(SolvedCells);
            int OriginalValue1 = MissingOneBoard.Cells[6].Value;
            MissingOneBoard.Cells[6] = new Cell(0);
            int OriginalValue2 = MissingOneBoard.Cells[7].Value;
            MissingOneBoard.Cells[7] = new Cell(0);
            int OriginalValue3 = MissingOneBoard.Cells[8].Value;
            MissingOneBoard.Cells[8] = new Cell(0);
            Solver.RecursiveBruteForceSolve(ref MissingOneBoard, ref MultipleSolutionsFound);

            Assert.IsTrue(MissingOneBoard.Cells[6].Value == OriginalValue1 &&
                MissingOneBoard.Cells[7].Value == OriginalValue2 &&
                MissingOneBoard.Cells[8].Value == OriginalValue3,
                "Brute force solver failed to fill in 3 values in a row missing from completed board: " +
                OriginalValue1 + " is now " + MissingOneBoard.Cells[6].Value + ", " +
                 OriginalValue2 + " is now " + MissingOneBoard.Cells[7].Value + ", " +
                  OriginalValue3 + " is now " + MissingOneBoard.Cells[8].Value);
            Assert.IsFalse(MultipleSolutionsFound,
                "Recursive Brute force solver said there were multiple solutions on a board missing 3 values");


            //Let's blank out three more random cells
            OriginalValue1 = MissingOneBoard.Cells[18].Value;
            MissingOneBoard.Cells[18] = new Cell(0);
            OriginalValue2 = MissingOneBoard.Cells[40].Value;
            MissingOneBoard.Cells[40] = new Cell(0);
            OriginalValue3 = MissingOneBoard.Cells[78].Value;
            MissingOneBoard.Cells[78] = new Cell(0);
            Solver.RecursiveBruteForceSolve(ref MissingOneBoard, ref MultipleSolutionsFound);

            Assert.IsTrue(MissingOneBoard.Cells[18].Value == OriginalValue1 &&
                MissingOneBoard.Cells[40].Value == OriginalValue2 &&
                MissingOneBoard.Cells[78].Value == OriginalValue3,
                "Brute force solver failed to fill in 3 scattered values missing from completed board: " +
                OriginalValue1 + " is now " + MissingOneBoard.Cells[18].Value + ", " +
                 OriginalValue2 + " is now " + MissingOneBoard.Cells[40].Value + ", " +
                  OriginalValue3 + " is now " + MissingOneBoard.Cells[78].Value);
            Assert.IsFalse(MultipleSolutionsFound,
                "Recursive Brute force solver said there were multiple solutions on a board missing 3 scattered values");




        }

        [TestMethod()]
        public void BruteForce_Recursive_FullSolve()
        {

            string TestType = "Recursive Brute force ";
            bool MultipleSolutionsFound = false;

            //all right, now the real test
            List<Cell> SolvedCells = BoardTest.SolvedSudokuPuzzle1();
            Board UnSolvedBoard = new Board(BoardTest.UnsolvedSudokuPuzzle1());

            BruteForceSolver Solver = new BruteForceSolver();
            Solver.RecursiveBruteForceSolve(ref UnSolvedBoard, ref MultipleSolutionsFound);



            Assert.IsTrue(BoardTest.CellListsAreEqual(UnSolvedBoard.Cells, SolvedCells),
                TestType + "failed to solve the first test board, its result: " + UnSolvedBoard.PrintBoard());
            Assert.IsFalse(MultipleSolutionsFound,
                TestType + "said there were multiple solutions on a board with just one");



            SolvedCells = BoardTest.SolvedSudokuPuzzle2();
            UnSolvedBoard = new Board(BoardTest.UnsolvedSudokuPuzzle2());

            Solver.RecursiveBruteForceSolve(ref UnSolvedBoard, ref MultipleSolutionsFound);


            Assert.IsTrue(BoardTest.CellListsAreEqual(UnSolvedBoard.Cells, SolvedCells),
                TestType + "failed to solve the second test board, its result: " + UnSolvedBoard.PrintBoard());
            Assert.IsFalse(MultipleSolutionsFound,
                TestType + "said there were multiple solutions on a board with just one");
        }


        [TestMethod()]
        public void BruteForce_Recursive_EmptyPuzzle()
        {

            string TestType = "Recursive Brute force ";
            bool MultipleSolutionsFound = false;
            
            Board UnSolvedBoard = new Board(BoardTest.EmptySudokuPuzzle());

            BruteForceSolver Solver = new BruteForceSolver();
            Solver.RecursiveBruteForceSolve(ref UnSolvedBoard, ref MultipleSolutionsFound);



            Assert.IsTrue(UnSolvedBoard.IsValid() && UnSolvedBoard.IsComplete(),
                TestType + "failed to solve the first test board, its result: " + UnSolvedBoard.PrintBoard());
            Assert.IsTrue(MultipleSolutionsFound,
                TestType + "failed to detect multiple solutions in an empty sudoku grid");

            
        }

    }
}
