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

    }

    [TestClass()]
    public class GroupTest
    {
        [TestMethod()]
        public void GroupCompletenessTest()
        {
            //here's a complete one, do we get a yes?
            Group completeGroup = new Group(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            Assert.IsTrue(completeGroup.IsComplete(), "Valid group was not considered complete.");

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
    }



    [TestClass()]
    public class BoardTest
    {
        [TestMethod()]
        public void BoardCompletenessTest()
        {
            //here's a complete one, do we get a yes?
            Board completeBoard = new Board(CompleteBoardCells(), 9, 9);
            Assert.IsTrue(completeBoard.IsComplete(), "Valid board was not considered complete.");

            completeBoard.Cells[15] = new Cell(0);
            Assert.IsFalse(completeBoard.IsComplete(), "Board with one zero was not considered complete.");


            Board swappedElementsBoard = new Board(CompleteBoardCells(), 9, 9);

            Cell swapcell = swappedElementsBoard.Cells[20];
            swappedElementsBoard.Cells[20] = swappedElementsBoard.Cells[21];
            swappedElementsBoard.Cells[21] = swapcell;

            Assert.IsFalse(swappedElementsBoard.IsComplete(), "Board with 2 elements in a row swapped was considered complete erroneously.");
        }

        public List<Cell> CompleteBoardCells()
        {

            List<Cell> completelist = new List<Cell>();
            for (int i = 1; i <= 9; i++)
            {
                completelist.AddRange(CompleteNineCells());
            }
            return completelist;


        }

        public List<Cell> CompleteNineCells()
        {

            List<Cell> completelist = new List<Cell>();
            for (int i = 1; i <= 9; i++)
            {
                completelist.Add(new Cell(i));
            }
            return completelist;


        }

    }


}

