using Sudoku_BusinessLogic;
using System;
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
            Cell mycell = new Cell(1);
            Assert.AreEqual(mycell.Value, 1);
            Assert.AreEqual(mycell.IsChangeable, true);


            Cell mycell2 = new Cell(5, false);
            Assert.AreEqual(mycell2.Value, 5);
            Assert.AreEqual(mycell2.IsChangeable, false);
        }

        [TestMethod()]
        public void CellChangerTest()
        {
            Cell mycell = new Cell(1);

            mycell.Value = 5;
            Assert.AreEqual(mycell.Value, 5);


            Cell mycell2 = new Cell(5, false);
            mycell.Value = 7;
            //make sure the value didn't change, since this was a non-changeable cell
            Assert.AreEqual(mycell2.Value, 5);
        }

        [TestMethod()]
        public void CellIncrementValueBy1Test()
        {
            Cell mycell = new Cell(1);

            mycell.IncrementValueBy1();
            Assert.AreEqual(mycell.Value, 2);


            Cell mycell2 = new Cell(9);
            mycell2.IncrementValueBy1();
            Assert.AreEqual(mycell2.Value, 0);
        }


    }

    [TestClass()]
    public class GroupTest
    {
        [TestMethod()]
        public void GroupCompletenessTest()
        {
            //here's a complete one, do we get a yes?
            Group completeGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Assert.IsTrue(completeGroup.IsComplete(), "Complete group was not considered complete.");

            Group wrongCountGroup = new Group(new List<int> { 1, 2, 3 });
            Assert.IsFalse(wrongCountGroup.IsComplete(), "Group with the wrong count for Sudoku (3) was considered complete.");

            Group wrongCountGroup2 = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Assert.IsFalse(wrongCountGroup2.IsComplete(), "Group with the wrong count for Sudoku (10) was considered complete.");

            Group missingNineGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 });
            Assert.IsFalse(missingNineGroup.IsComplete(), "Group with a 0 instead of 9 was considered complete.");

            Group startingAtZeroGroup = new Group(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 });
            Assert.IsFalse(startingAtZeroGroup.IsComplete(), "Group with a 0 instead of 9 was considered complete.");

            Group repeated5Group = new Group(new List<int> { 1, 2, 3, 4, 5, 5, 6, 7, 8 });
            Assert.IsFalse(repeated5Group.IsComplete(), "Group with two 5s was considered complete.");

            Group zeroesGroup = new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            Assert.IsFalse(zeroesGroup.IsComplete(), "Group with all zeroes was considered complete.");

            Group tenGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 10 });
            Assert.IsFalse(tenGroup.IsComplete(), "Group with a 10 was considered complete.");
        }

        [TestMethod()]
        public void MissingElementsTest()
        {

            Group completeGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Assert.IsTrue(completeGroup.MissingElements().Count == 0, "Complete group reported that it had missing elements");


            Group zeroesGroup = new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            Assert.IsTrue(zeroesGroup.MissingElements().Count == 9, "Group with all zeroes returned <> 9 missing elements (" +
                zeroesGroup.MissingElements().Count + ")");



            Group missingNineGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 });
            Assert.IsTrue(missingNineGroup.MissingElements().Contains(9), "Group missing the value 9 did not report that in its list of missing elements");
            Assert.IsTrue(missingNineGroup.MissingElements().Count == 1, "Group missing only the value 9 reported more then 1 missing element");

        }

        [TestMethod()]
        public void GroupIsValidTest()
        {

            Group completeGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Assert.IsTrue(completeGroup.IsValid(), "Complete group reported that it was not valid");


            Group zeroesGroup = new Group(new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            Assert.IsTrue(zeroesGroup.IsValid(), "Group with all zeroes said it was not valid");


            Group missingNineGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 0 });
            Assert.IsTrue(missingNineGroup.IsValid(), "Group with a 0 instead of 9 said it was not valid");


            Group doubleOnesGroup = new Group(new List<int> { 1, 1, 2, 3, 4, 5, 6, 7, 8 });
            Assert.IsFalse(doubleOnesGroup.IsValid(), "Group with double 1s instead of 9 said it was valid");

        }



    }



    [TestClass()]
    public class BoardTest
    {
        [TestMethod()]
        public void BoardTest_RowsAreComplete()
        {
            //here's a "complete" one, do we get a yes?
            Board completeBoard = new Board(SolvedSudokuPuzzle1());
            Assert.IsTrue(completeBoard.RowsAreComplete(), "RowsAreComplete: Valid board's rows were not considered complete.");

            completeBoard.Cells[15] = new Cell(0);
            Assert.IsFalse(completeBoard.RowsAreComplete(), "RowsAreComplete: Board with one zero was considered complete.");


            Board swappedElementsBoard = new Board(SolvedSudokuPuzzle1());

            Cell swapcell = swappedElementsBoard.Cells[20];
            swappedElementsBoard.Cells[20] = swappedElementsBoard.Cells[21];
            swappedElementsBoard.Cells[21] = swapcell;

            //Now, technically this should not be considered correct in the full definition of the game, but this only tests the rows
            Assert.IsTrue(swappedElementsBoard.RowsAreComplete(), "RowsAreComplete: Board with row elements rearranged, but kept in same rows, not considered complete");

            //However, if we swap between rows, that should no longer be considered complete

            swapcell = swappedElementsBoard.Cells[1];
            swappedElementsBoard.Cells[1] = swappedElementsBoard.Cells[10];
            swappedElementsBoard.Cells[10] = swapcell;
            Assert.IsFalse(swappedElementsBoard.RowsAreComplete(), "RowsAreComplete: Board with 2 elements in different rows swapped was considered complete erroneously.");
        }

        [TestMethod()]
        public void BoardTest_ColumnsAreComplete()
        {
            //here's a "complete" one, do we get a yes?
            Board completeBoard = new Board(SolvedSudokuPuzzle1());
            Assert.IsTrue(completeBoard.ColumnsAreComplete(), "ColumnsAreComplete: Valid board's columns were not considered complete.");

            completeBoard.Cells[15] = new Cell(0);
            Assert.IsFalse(completeBoard.ColumnsAreComplete(), "ColumnsAreComplete: Board with one zero was considered complete.");


            Board swappedElementsBoard = new Board(SolvedSudokuPuzzle1());


            Cell swapcell = swappedElementsBoard.Cells[1];
            swappedElementsBoard.Cells[1] = swappedElementsBoard.Cells[10];
            swappedElementsBoard.Cells[10] = swapcell;
            //If we swap between rows, that should be considered complete, because the column test alone doesn't know any better
            Assert.IsTrue(swappedElementsBoard.ColumnsAreComplete(), "ColumnsAreComplete: Board with elements rearranged within the same column was not considered complete");

            swapcell = swappedElementsBoard.Cells[20];
            swappedElementsBoard.Cells[20] = swappedElementsBoard.Cells[21];
            swappedElementsBoard.Cells[21] = swapcell;

            //but it should complain if elements are swapped between two columns
            Assert.IsFalse(swappedElementsBoard.ColumnsAreComplete(), "ColumnsAreComplete: Board with elements swapped between columns was considered complete");


        }

        [TestMethod()]
        public void BoardTest_SquaresAreComplete()
        {
            //Squares are a little more complicated, so let's add a simple check that a "Square" on the solved puzzle is what we expect
            Board completeBoard = new Board(SolvedSudokuPuzzle1());
            List<Group> Squares = completeBoard.Squares();
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
            Assert.IsTrue(completeBoard.SquaresAreComplete(), "SquaresAreComplete: Valid board's columns were not considered complete.");

            completeBoard.Cells[15] = new Cell(0);
            Assert.IsFalse(completeBoard.SquaresAreComplete(), "SquaresAreComplete: Board with one zero was considered complete.");


            Board swappedElementsBoard = new Board(SolvedSudokuPuzzle1());


            Cell swapcell = swappedElementsBoard.Cells[1];
            swappedElementsBoard.Cells[1] = swappedElementsBoard.Cells[10];
            swappedElementsBoard.Cells[10] = swapcell;
            //If we swap within a square, that should be considered complete, because the square test alone doesn't know any better
            Assert.IsTrue(swappedElementsBoard.SquaresAreComplete(), "SquaresAreComplete: Board with elements rearranged within the same square was not considered complete");

            swapcell = swappedElementsBoard.Cells[2];
            swappedElementsBoard.Cells[2] = swappedElementsBoard.Cells[5];
            swappedElementsBoard.Cells[5] = swapcell;

            //but it should complain if elements are swapped between two squares
            Assert.IsFalse(swappedElementsBoard.SquaresAreComplete(), "SquaresAreComplete: Board with elements swapped between squares was considered complete");


        }





        /// <summary>
        /// Solved board from from https://en.wikipedia.org/wiki/Sudoku
        /// </summary>
        /// <returns></returns>
        public static List<Cell> SolvedSudokuPuzzle1()
        {

            List<Cell> completelist = new List<Cell>();

            completelist.AddRange(new Group(new List<int> { 5, 3, 4, 6, 7, 8, 9, 1, 2 }).Cells);
            completelist.AddRange(new Group(new List<int> { 6, 7, 2, 1, 9, 5, 3, 4, 8 }).Cells);
            completelist.AddRange(new Group(new List<int> { 1, 9, 8, 3, 4, 2, 5, 6, 7 }).Cells);

            completelist.AddRange(new Group(new List<int> { 8, 5, 9, 7, 6, 1, 4, 2, 3 }).Cells);
            completelist.AddRange(new Group(new List<int> { 4, 2, 6, 8, 5, 3, 7, 9, 1 }).Cells);
            completelist.AddRange(new Group(new List<int> { 7, 1, 3, 9, 2, 4, 8, 5, 6 }).Cells);

            completelist.AddRange(new Group(new List<int> { 9, 6, 1, 5, 3, 7, 2, 8, 4 }).Cells);
            completelist.AddRange(new Group(new List<int> { 2, 8, 7, 4, 1, 9, 6, 3, 5 }).Cells);
            completelist.AddRange(new Group(new List<int> { 3, 4, 5, 2, 8, 6, 1, 7, 9 }).Cells);

            return completelist;


        }

        /// <summary>
        /// Unsolved board from from https://en.wikipedia.org/wiki/Sudoku
        /// </summary>
        /// <returns></returns>
        public static List<Cell> UnsolvedSudokuPuzzle1()
        {

            List<Cell> completelist = new List<Cell>();

            completelist.AddRange(new Group(new List<int> { 5, 3, 0, 0, 7, 0, 0, 0, 0 }).Cells);
            completelist.AddRange(new Group(new List<int> { 6, 0, 0, 1, 9, 5, 0, 0, 0 }).Cells);
            completelist.AddRange(new Group(new List<int> { 0, 9, 8, 0, 0, 0, 0, 6, 0 }).Cells);

            completelist.AddRange(new Group(new List<int> { 8, 0, 0, 0, 6, 0, 0, 0, 3 }).Cells);
            completelist.AddRange(new Group(new List<int> { 4, 0, 0, 8, 0, 3, 0, 0, 1 }).Cells);
            completelist.AddRange(new Group(new List<int> { 7, 0, 0, 0, 2, 0, 0, 0, 6 }).Cells);

            completelist.AddRange(new Group(new List<int> { 0, 6, 0, 0, 0, 0, 2, 8, 0 }).Cells);
            completelist.AddRange(new Group(new List<int> { 0, 0, 0, 4, 1, 9, 0, 0, 5 }).Cells);
            completelist.AddRange(new Group(new List<int> { 0, 0, 0, 0, 8, 0, 0, 7, 9 }).Cells);

            return completelist;


        }

        public static bool CellListsAreEqual(List<Cell> firstlist, List<Cell> secondlist)
        {
            //they obviously can't be equal if they're different lengths
            if (firstlist.Count != secondlist.Count) { return false; }

            //sure, they're equal if they're both empty
            if (firstlist.Count == 0) { return true; }

            for (int i = 0; i < firstlist.Count; i++)
            {
                if (firstlist[i].Value != secondlist[i].Value) { return false; }
            }
            return true;
        }

    }


}

