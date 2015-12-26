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
            //Assert.AreEqual(mycell.IsChangeable, false);
        }

        [TestMethod()]
        public void CellChangerTest()
        {
            Cell mycell = new Cell(1);

            mycell.Value = 5;
            Assert.AreEqual(mycell.Value, 5);
            //Assert.AreEqual(mycell.IsChangeable, false);
        }

        [TestMethod()]
        public void IsCompleteTest()
        {
            Assert.Fail();
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
            Assert.IsTrue(completeGroup.IsComplete());


            Group missingNineGroup = new Group(new List<int> {1, 2, 3, 4, 5, 6, 7, 8 });
            Assert.IsFalse(missingNineGroup.IsComplete());
        }
    }
    }

