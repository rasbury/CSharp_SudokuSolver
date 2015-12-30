using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_BusinessLogic
{
    /// <summary>
    /// A board is a collection of cells, that can be grouped in various ways
    /// </summary>
    public class Board
    {

        public List<Cell> Cells { get; set; }
        public int BoxSize { get; set; }
        public int BoxCount { get; set; }


        /// <summary>
        /// Any blank cells should be initialized with zeroes
        /// </summary>
        /// <param name="numbers"></param>
        public Board(List<Cell> numbers, int boxsize, int boxcount)
        {
            Cells = numbers;
            BoxSize = boxsize;
            BoxCount = boxcount;

        }

        /// <summary>
        /// Any blank cells should be initialized with zeroes
        /// </summary>
        /// <param name="numbers"></param>
        public Board(List<int> numbers, int boxsize, int boxcount)
        {
            Cells = new List<Cell>();
            foreach (int number in numbers)
            {
                Cells.Add(new Cell(number));
            }
            BoxSize = boxsize;
            BoxCount = boxcount;
             
        }

        /// <summary>
        /// Defaults to a 9x9 grid
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="boxsize"></param>
        /// <param name="boxcount"></param>
        public Board(List<int> numbers)
        {
            Cells = new List<Cell>();
            foreach (int number in numbers)
            {
                Cells.Add(new Cell(number));
            }
            BoxSize = 9;
            BoxCount = 9;

        }


        public bool IsValidSize()
        {
            return Cells.Count == BoxSize * BoxCount;
        }



        public bool IsComplete()
        {
            if (!IsValidSize()) { return false; }

            return RowsAreComplete();
        }

        private bool RowsAreComplete()
        {
            //for each group, item 1-9, 10-18, etc, make sure is complete
            for (int i = 1; i <= Cells.Count; i += BoxSize)
            {
                Group checkgroup = new Group(Cells.GetRange(i-1, BoxSize));
                if (!checkgroup.IsComplete()) { return false; }

            }
            return true;

        }
    }
}
