using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sudoku_BusinessLogic.Tests
{
    [TestClass()]
    public class CellTest
    {
        [TestMethod()]
        public void CellCreatorTest()
        {
            Cell Mycell = new Cell(1);
            Assert.AreEqual(Mycell.Value, 1);
            Assert.AreEqual(Mycell.IsChangeable, false);


            Cell Mycell2 = new Cell(5, true);
            Assert.AreEqual(Mycell2.Value, 5);
            Assert.AreEqual(Mycell2.IsChangeable, true);
        }

        [TestMethod()]
        public void CellChangerTest()
        {
            Cell Mycell = new Cell(0);

            Mycell.Value = 1;
            Mycell.Value = 5;
            Assert.AreEqual(Mycell.Value, 5);


            Cell Mycell2 = new Cell(5, false);
            Mycell2.Value = 7;
            //make sure the value didn't change, since this was a non-changeable cell
            Assert.AreEqual(Mycell2.Value, 5);
        }

        [TestMethod()]
        public void CellIncrementValueBy1Test()
        {
            Cell MyCell = new Cell(1, true);

            MyCell.IncrementValueBy1();
            Assert.AreEqual(MyCell.Value, 2);


            Cell MyCell2 = new Cell(9, true);
            MyCell2.IncrementValueBy1();
            Assert.AreEqual(MyCell2.Value, 0);
        }


    }

    [TestClass()]
    public class GroupTest
    {
        [TestMethod()]
        public void Group_CompletenessTest()
        {
            //here's a complete one, do we get a yes?
            Group CompleteGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Assert.IsTrue(CompleteGroup.IsComplete(), "Complete group was not considered complete.");

            Group WrongCountGroup = new Group(new List<int> { 1, 2, 3 });
            Assert.IsFalse(WrongCountGroup.IsComplete(), "Group with the wrong count for Sudoku (3) was considered complete.");

            Group WrongCountGroup2 = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Assert.IsFalse(WrongCountGroup2.IsComplete(), "Group with the wrong count for Sudoku (10) was considered complete.");

            Group MissingNineGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 });
            Assert.IsFalse(MissingNineGroup.IsComplete(), "Group with a 0 instead of 9 was considered complete.");

            Group StartingAtZeroGroup = new Group(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            Assert.IsFalse(StartingAtZeroGroup.IsComplete(), "Group with a 0 instead of 9 was considered complete.");

            Group Repeated5Group = new Group(new List<int> { 1, 2, 3, 4, 5, 5, 6, 7, 8 });
            Assert.IsFalse(Repeated5Group.IsComplete(), "Group with two 5s was considered complete.");

            Group ZeroesGroup = new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            Assert.IsFalse(ZeroesGroup.IsComplete(), "Group with all zeroes was considered complete.");

            Group TenGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 10 });
            Assert.IsFalse(TenGroup.IsComplete(), "Group with a 10 was considered complete.");
        }

        [TestMethod()]
        public void Group_MissingElementsTest()
        {

            Group CompleteGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Assert.IsTrue(CompleteGroup.MissingElements().Count == 0, "Complete group reported that it had missing elements");


            Group ZeroesGroup = new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            Assert.IsTrue(ZeroesGroup.MissingElements().Count == 9, "Group with all zeroes returned <> 9 missing elements (" +
                ZeroesGroup.MissingElements().Count + ")");



            Group MissingNineGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 });
            Assert.IsTrue(MissingNineGroup.MissingElements().Contains(9), "Group missing the value 9 did not report that in its list of missing elements");
            Assert.IsTrue(MissingNineGroup.MissingElements().Count == 1, "Group missing only the value 9 reported more then 1 missing element");

        }

        [TestMethod()]
        public void Group_IsValidTest()
        {

            Group CompleteGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Assert.IsTrue(CompleteGroup.IsValid(), "Complete group reported that it was not valid");


            Group ZeroesGroup = new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            Assert.IsTrue(ZeroesGroup.IsValid(), "Group with all zeroes said it was not valid");


            Group MissingNineGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 });
            Assert.IsTrue(MissingNineGroup.IsValid(), "Group with a 0 instead of 9 said it was not valid");


            Group DoubleOnesGroup = new Group(new List<int> { 1, 1, 2, 3, 4, 5, 6, 7, 8 });
            Assert.IsFalse(DoubleOnesGroup.IsValid(), "Group with double 1s instead of 9 said it was valid");

        }



    }
    
    [TestClass()]
    public class BoardTest
    {
        [TestMethod()]
        public void BoardTest_RowsAreComplete()
        {
            //here's a "complete" one, do we get a yes?
            Board CompleteBoard = new Board(SolvedSudokuPuzzle1());
            Assert.IsTrue(CompleteBoard.RowsAreComplete(), "RowsAreComplete: Valid board's rows were not considered complete.");

            CompleteBoard.Cells[15] = new Cell(0);
            Assert.IsFalse(CompleteBoard.RowsAreComplete(), "RowsAreComplete: Board with one zero was considered complete.");


            Board SwappedElementsBoard = new Board(SolvedSudokuPuzzle1());

            Cell Swapcell = SwappedElementsBoard.Cells[20];
            SwappedElementsBoard.Cells[20] = SwappedElementsBoard.Cells[21];
            SwappedElementsBoard.Cells[21] = Swapcell;

            //Now, technically this should not be considered correct in the full definition of the game, but this only tests the rows
            Assert.IsTrue(SwappedElementsBoard.RowsAreComplete(), "RowsAreComplete: Board with row elements rearranged, but kept in same rows, not considered complete");

            //However, if we swap between rows, that should no longer be considered complete

            Swapcell = SwappedElementsBoard.Cells[1];
            SwappedElementsBoard.Cells[1] = SwappedElementsBoard.Cells[10];
            SwappedElementsBoard.Cells[10] = Swapcell;
            Assert.IsFalse(SwappedElementsBoard.RowsAreComplete(), "RowsAreComplete: Board with 2 elements in different rows swapped was considered complete erroneously.");
        }

        [TestMethod()]
        public void BoardTest_ColumnsAreComplete()
        {
            //here's a "complete" one, do we get a yes?
            Board CompleteBoard = new Board(SolvedSudokuPuzzle1());
            Assert.IsTrue(CompleteBoard.ColumnsAreComplete(), "ColumnsAreComplete: Valid board's columns were not considered complete.");

            CompleteBoard.Cells[15] = new Cell(0);
            Assert.IsFalse(CompleteBoard.ColumnsAreComplete(), "ColumnsAreComplete: Board with one zero was considered complete.");


            Board SwappedElementsBoard = new Board(SolvedSudokuPuzzle1());


            Cell Swapcell = SwappedElementsBoard.Cells[1];
            SwappedElementsBoard.Cells[1] = SwappedElementsBoard.Cells[10];
            SwappedElementsBoard.Cells[10] = Swapcell;
            //If we swap between rows, that should be considered complete, because the column test alone doesn't know any better
            Assert.IsTrue(SwappedElementsBoard.ColumnsAreComplete(), "ColumnsAreComplete: Board with elements rearranged within the same column was not considered complete");

            Swapcell = SwappedElementsBoard.Cells[20];
            SwappedElementsBoard.Cells[20] = SwappedElementsBoard.Cells[21];
            SwappedElementsBoard.Cells[21] = Swapcell;

            //but it should complain if elements are swapped between two columns
            Assert.IsFalse(SwappedElementsBoard.ColumnsAreComplete(), "ColumnsAreComplete: Board with elements swapped between columns was considered complete");


        }

        [TestMethod()]
        public void BoardTest_SquaresAreComplete()
        {
            //Squares are a little more complicated, so let's add a simple check that a "Square" on the solved puzzle is what we expect
            Board CompleteBoard = new Board(SolvedSudokuPuzzle1());
            List<Group> Squares = CompleteBoard.Squares();
            Group ExpectedFirstSquare = new Group(new List<int> { 5, 3, 4, 6, 7, 2, 1, 9, 8 });
            Group ExpectedMiddleSquare = new Group(new List<int> { 7, 6, 1, 8, 5, 3, 9, 2, 4 });
            Group ExpectedLastSquare = new Group(new List<int> { 2, 8, 4, 6, 3, 5, 1, 7, 9 });
            string GroupToString = string.Empty;
            foreach (Group gr in Squares)
            {
                GroupToString += gr.ValuesToString() + ";";
            }
            Assert.IsTrue(CellListsAreEqual(Squares[0].Cells, ExpectedFirstSquare.Cells), "SquaresAreComplete: First square " +
                "on solved board is different from expected! " + GroupToString);
            Assert.IsTrue(CellListsAreEqual(Squares[4].Cells, ExpectedMiddleSquare.Cells), "SquaresAreComplete: Middle square " +
                "on solved board is different from expected! " + GroupToString);
            Assert.IsTrue(CellListsAreEqual(Squares[8].Cells, ExpectedLastSquare.Cells), "SquaresAreComplete: Last square " +
                "on solved board is different from expected! " + GroupToString);

            //here's a "complete" one, do we get a yes?
            Assert.IsTrue(CompleteBoard.SquaresAreComplete(), "SquaresAreComplete: Valid board's columns were not considered complete.");

            CompleteBoard.Cells[15] = new Cell(0);
            Assert.IsFalse(CompleteBoard.SquaresAreComplete(), "SquaresAreComplete: Board with one zero was considered complete.");


            Board SwappedElementsBoard = new Board(SolvedSudokuPuzzle1());


            Cell Swapcell = SwappedElementsBoard.Cells[1];
            SwappedElementsBoard.Cells[1] = SwappedElementsBoard.Cells[10];
            SwappedElementsBoard.Cells[10] = Swapcell;
            //If we swap within a square, that should be considered complete, because the square test alone doesn't know any better
            Assert.IsTrue(SwappedElementsBoard.SquaresAreComplete(), "SquaresAreComplete: Board with elements rearranged within the same square was not considered complete");

            Swapcell = SwappedElementsBoard.Cells[2];
            SwappedElementsBoard.Cells[2] = SwappedElementsBoard.Cells[5];
            SwappedElementsBoard.Cells[5] = Swapcell;

            //but it should complain if elements are swapped between two squares
            Assert.IsFalse(SwappedElementsBoard.SquaresAreComplete(), "SquaresAreComplete: Board with elements swapped between squares was considered complete");


        }



        public static bool CellListsAreEqual(List<Cell> firstList, List<Cell> secondList)
        {
            //they obviously can't be equal if they're different lengths
            if (firstList.Count != secondList.Count) { return false; }

            //sure, they're equal if they're both empty
            if (firstList.Count == 0) { return true; }

            for (int i = 0; i < firstList.Count; i++)
            {
                if (firstList[i].Value != secondList[i].Value) { return false; }
            }
            return true;
        }


        #region Test Boards
        /// <summary>
        /// A puzzle that cannot be solved because it is already invalid
        /// </summary>
        /// <returns></returns>
        public static List<Cell> UnsolvablePuzzle()
        {

            List<Cell> Completelist = new List<Cell>();

            Completelist.AddRange(new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 2 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 3 }).Cells);

            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 4 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 5 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 6 }).Cells);

            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 7 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 8 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 9 }).Cells);

            return Completelist;


        }


        /// <summary>
        /// Solved board from from https://en.wikipedia.org/wiki/Sudoku
        /// </summary>
        /// <returns></returns>
        public static List<Cell> SolvedSudokuPuzzle1()
        {

            List<Cell> Completelist = new List<Cell>();

            Completelist.AddRange(new Group(new List<int> { 5, 3, 4, 6, 7, 8, 9, 1, 2 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 6, 7, 2, 1, 9, 5, 3, 4, 8 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 1, 9, 8, 3, 4, 2, 5, 6, 7 }).Cells);

            Completelist.AddRange(new Group(new List<int> { 8, 5, 9, 7, 6, 1, 4, 2, 3 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 4, 2, 6, 8, 5, 3, 7, 9, 1 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 7, 1, 3, 9, 2, 4, 8, 5, 6 }).Cells);

            Completelist.AddRange(new Group(new List<int> { 9, 6, 1, 5, 3, 7, 2, 8, 4 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 2, 8, 7, 4, 1, 9, 6, 3, 5 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 3, 4, 5, 2, 8, 6, 1, 7, 9 }).Cells);

            return Completelist;


        }

        /// <summary>
        /// Unsolved board from from https://en.wikipedia.org/wiki/Sudoku
        /// </summary>
        /// <returns></returns>
        public static List<Cell> UnsolvedSudokuPuzzle1()
        {

            List<Cell> Completelist = new List<Cell>();

            Completelist.AddRange(new Group(new List<int> { 5, 3, 0, 0, 7, 0, 0, 0, 0 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 6, 0, 0, 1, 9, 5, 0, 0, 0 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 9, 8, 0, 0, 0, 0, 6, 0 }).Cells);

            Completelist.AddRange(new Group(new List<int> { 8, 0, 0, 0, 6, 0, 0, 0, 3 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 4, 0, 0, 8, 0, 3, 0, 0, 1 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 7, 0, 0, 0, 2, 0, 0, 0, 6 }).Cells);

            Completelist.AddRange(new Group(new List<int> { 0, 6, 0, 0, 0, 0, 2, 8, 0 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 4, 1, 9, 0, 0, 5 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 8, 0, 0, 7, 9 }).Cells);

            return Completelist;


        }


        /// <summary>
        /// Solved EXTREME board from from http://www.sudoku.ws/extreme-1.htm
        /// </summary>
        /// <returns></returns>
        public static List<Cell> SolvedSudokuPuzzle2()
        {

            List<Cell> Completelist = new List<Cell>();

            Completelist.AddRange(new Group(new List<int> { 5, 1, 9, 7, 4, 8, 6, 3, 2 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 7, 8, 3, 6, 5, 2, 4, 1, 9 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 4, 2, 6, 1, 3, 9, 8, 7, 5 }).Cells);

            Completelist.AddRange(new Group(new List<int> { 3, 5, 7, 9, 8, 6, 2, 4, 1 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 2, 6, 4, 3, 1, 7, 5, 9, 8 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 1, 9, 8, 5, 2, 4, 3, 6, 7, }).Cells);

            Completelist.AddRange(new Group(new List<int> { 9, 7, 5, 8, 6, 3, 1, 2, 4 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 8, 3, 2, 4, 9, 1, 7, 5, 6 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 6, 4, 1, 2, 7, 5, 9, 8, 3 }).Cells);


            return Completelist;


        }

        /// <summary>
        /// Unsolved EXTREME board from from http://www.sudoku.ws/extreme-1.htm
        /// </summary>
        /// <returns></returns>
        public static List<Cell> UnsolvedSudokuPuzzle2()
        {

            List<Cell> Completelist = new List<Cell>();

            Completelist.AddRange(new Group(new List<int> { 0, 0, 9, 7, 4, 8, 0, 0, 0 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 7, 0, 0, 0, 0, 0, 0, 0, 0 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 2, 0, 1, 0, 9, 0, 0, 0 }).Cells);

            Completelist.AddRange(new Group(new List<int> { 0, 0, 7, 0, 0, 0, 2, 4, 0 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 6, 4, 0, 1, 0, 5, 9, 0 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 9, 8, 0, 0, 0, 3, 0, 0, }).Cells);

            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 8, 0, 3, 0, 2, 0 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 6 }).Cells);
            Completelist.AddRange(new Group(new List<int> { 0, 0, 0, 2, 7, 5, 9, 0, 0 }).Cells);

            return Completelist;


        }

        #endregion
    }


}

