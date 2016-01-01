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
        #region  Properties 

        public List<Cell> Cells { get; set; }

        /// <summary>
        /// Number of boxes in a row, a column, or a square, and also the count of rows, columns, and squares
        /// </summary>
        private int BoardDimension { get; set; }

        /// <summary>
        /// Number of boxes in a row or column in a Square, and also the count of rows and columns in that square
        /// </summary>
        private int SquareDimension { get; set; }

        private void SetDefaultValues()
        {

            BoardDimension = 9;
            SquareDimension = 3;
        }


        public List<Group> Rows()
        {
            List<Group> rowlist = new List<Group>();

            //rows = item 0-8, 9-17, etc
            for (int i = 0; i < Cells.Count; i += BoardDimension)
            {
                rowlist.Add(new Group(Cells.GetRange(i, BoardDimension)));
            }
            return rowlist;
        }

        public List<Group> Columns()
        {
            List<Group> collist = new List<Group>();

            //columns =  item 0,9,18.., 1,10,19... etc
            for (int column = 0; column < BoardDimension; column += 1)
            {

                List<Cell> ColumnValues = new List<Cell>();
                for (int index = column; index <= column + (BoardDimension * (BoardDimension - 1)); index += BoardDimension)
                {
                    ColumnValues.Add(Cells[index]);
                }

                collist.Add(new Group(ColumnValues));

            }
            return collist;
        }

        public List<Group> Squares()
        {
            List<Group> squarelist = new List<Group>();
            //square = item 0,1,2,9,10,11...3,4,5,12,13,14... etc

            for (int gridrow = 0; gridrow < BoardDimension; gridrow += SquareDimension)
            {

                for (int gridcol = 0; gridcol < BoardDimension; gridcol += SquareDimension)
                {
                    List<Cell> ColumnValues = new List<Cell>();

                    for (int Squarerow = gridrow; Squarerow < gridrow + SquareDimension; Squarerow += BoardDimension)
                    {
                        for (int squarecol = Squarerow; squarecol < Squarerow + SquareDimension; squarecol++)
                        {
                            ColumnValues.Add(Cells[squarecol]);
                        }

                    }

                    squarelist.Add(new Group(ColumnValues));
                }

            }
            return squarelist;

        }

        #endregion

        #region Initializers


        /// <summary>
        /// Any blank cells should be initialized with zeroes
        /// </summary>
        /// <param name="numbers"></param>
        public Board(List<Cell> numbers)
        {
            Cells = numbers;
            SetDefaultValues();
        }

        /// <summary>
        /// Any blank cells should be initialized with zeroes
        /// </summary>
        /// <param name="numbers"></param>
        public Board(List<int> numbers)
        {
            Cells = new List<Cell>();
            foreach (int number in numbers)
            {
                Cells.Add(new Cell(number));
            }
            SetDefaultValues();

        }

        #endregion

        #region Validations


        public bool IsValidSize()
        {
            return Cells.Count == BoardDimension * BoardDimension;
        }



        public bool IsComplete()
        {
            return IsValidSize() &&
                RowsAreComplete() &&
                ColumnsAreComplete() &&
                SquaresAreComplete();
        }

        public bool IsValid()
        {
            return IsValidSize() &&
                RowsAreValid() &&
                ColumnsAreValid() &&
                SquaresAreValid();
        }






        public bool RowsAreComplete()
        {
            foreach (Group checkgroup in Rows())
            {
                if (!checkgroup.IsComplete()) { return false; }

            }
            return true;

        }

        public bool ColumnsAreComplete()
        {

            foreach (Group checkgroup in Columns())
            {
                if (!checkgroup.IsComplete()) { return false; }

            }
            return true;

        }

        public bool SquaresAreComplete()
        {

            foreach (Group checkgroup in Squares())
            {
                if (!checkgroup.IsComplete()) { return false; }

            }
            return true;

        }


        public bool RowsAreValid()
        {
            foreach (Group checkgroup in Rows())
            {
                if (!checkgroup.IsValid()) { return false; }

            }
            return true;

        }

        public bool ColumnsAreValid()
        {

            foreach (Group checkgroup in Columns())
            {
                if (!checkgroup.IsValid()) { return false; }

            }
            return true;

        }

        public bool SquaresAreValid()
        {

            foreach (Group checkgroup in Squares())
            {
                if (!checkgroup.IsValid()) { return false; }

            }
            return true;

        }

        #endregion
    }
}
