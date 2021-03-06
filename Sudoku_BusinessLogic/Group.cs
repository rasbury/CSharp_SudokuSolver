﻿using System.Collections.Generic;
using System.Linq;

namespace Sudoku_BusinessLogic
{
    public class Group
    {
        public List<Cell> Cells { get; set; }
        public int GroupSize = 9;
        
        public Group(List<int> numbers)
        {
            Cells = new List<Cell>();
            foreach (int number in numbers)
            {
                Cells.Add(new Cell(number));
            }
        }

        public Group(List<Cell> newCells)
        {
            Cells = newCells;
        }

        /// <summary>
        /// List must be of size 9; no other Sudoku sizes are currently supported
        /// </summary>
        /// <returns></returns>
        public bool IsValidSize()
        {
            return Cells.Count == GroupSize;
        }

        /// <summary>
        /// Must contain all numbers from 1 to 9 exactly once
        /// </summary>
        public bool IsComplete()
        {
            if (!IsValidSize()) { return false; }

            List<int> intlist = ToIntList();

            for (int i = 1; i <= intlist.Count; i++)
            {
                if (!intlist.Contains(i))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Can have zeroes (or be all 0s), but cannot contain duplicates of numbers > 0
        /// </summary>
        public bool IsValid()
        {
            if (!IsValidSize()) { return false; }

            List<int> IntList = ToIntList();

            //here's my first foray into the land of LINQ
            //  (help from http://stackoverflow.com/questions/454601/how-to-count-duplicates-in-list-with-linq)
            //Grouping the ints, selecting only the ones with count > 1 and x > 0
            //Had some trouble figuring out how to just check if q's count was > 0, 
            //  so went with a foreach that'll return the first time it's hit
            //  Inelegant, but works
            var q = from x in IntList
                    where x > 0
                    group x by x into g
                    let count = g.Count()
                    where count > 1
                    select new { Value = g.Key, Count = count };
            foreach (var x in q)
            {
                if (x.Value > 0) { return false; }
            }
            return true;
            
        }


        public List<int> ToIntList()
        {
            List<int> IntList = new List<int>();
            foreach (Cell MyCell in Cells)
            {
                IntList.Add(MyCell.Value);
            }
            return IntList;
        }

        public List<int> MissingElements()
        {

            List<int> IntList = ToIntList();
            List<int> MissingElements = new List<int>();

            for (int i = 1; i <= IntList.Count; i++)
            {
                if (!IntList.Contains(i))
                {
                    MissingElements.Add(i);
                }
            }

            return MissingElements;
        }

        /// <summary>
        /// Just to help me with debugging those squares, eventual display probably won't use this
        /// </summary>
        public string ValuesToString()
        {
            string Returnstring = string.Empty;
            foreach(Cell c in Cells)
            {
                if (c.IsChangeable)
                {
                    Returnstring += c.Value + ", ";

                }
                else
                {
                    Returnstring += "[" + c.Value + "], ";

                }
            }
            return Returnstring;
        }

    }
}
