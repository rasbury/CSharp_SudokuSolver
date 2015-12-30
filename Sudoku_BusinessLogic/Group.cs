using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_BusinessLogic
{
    public class Group
    {
        public List<Cell> Cells { get; set; }
        public int GroupSize {get; set; }

        public Group(List<int> numbers)
        {
            Cells = new List<Cell>();
            foreach (int number in numbers)
            {
                Cells.Add(new Cell(number));
            }
            GroupSize = 9;
        }

        public Group(List<Cell> newcells)
        {
            Cells = newcells;
            GroupSize = 9;
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

        public List<int> ToIntList()
        {
            List<int> intlist = new List<int>();
            foreach (Cell mycell in Cells)
            {
                intlist.Add(mycell.Value);
            }
            return intlist;
        }
    }
}
