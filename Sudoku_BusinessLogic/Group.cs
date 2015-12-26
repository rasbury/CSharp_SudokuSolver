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

        public Group(List<int> numbers)
        {
            Cells = new List<Cell>();
            foreach (int number in numbers)
            {
                Cells.Add(new Cell(number));
            }

        }

        /// <summary>
        /// Must contain all numbers from 1 to 9 exactly once
        /// </summary>
        public bool IsComplete()
        {
            /* for (int i = 1; i <= Cells.Count; i++)
             {
                 Boolean found = false;
                 foreach (Cell mycell in Cells)
                 {
                     if mycell.Value = i;
                     {

                     }
                 }

             }*/

            List<int> intlist = ToIntList();

            return intlist.Min() == 1;
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
