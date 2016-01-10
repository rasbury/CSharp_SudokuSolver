using System.Collections.Generic;

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
            List<Group> RowList = new List<Group>();

            //rows = item 0-8, 9-17, etc
            for (int i = 0; i < Cells.Count; i += BoardDimension)
            {
                RowList.Add(new Group(Cells.GetRange(i, BoardDimension)));
            }
            return RowList;
        }

        public List<Group> Columns()
        {
            List<Group> ColumnList = new List<Group>();

            //columns =  item 0,9,18.., 1,10,19... etc
            for (int Column = 0; Column < BoardDimension; Column += 1)
            {

                List<Cell> ColumnValues = new List<Cell>();
                for (int Index = Column; Index <= Column + (BoardDimension * (BoardDimension - 1)); Index += BoardDimension)
                {
                    ColumnValues.Add(Cells[Index]);
                }

                ColumnList.Add(new Group(ColumnValues));

            }
            return ColumnList;
        }

        public List<Group> Squares()
        {
            List<Group> SquareList = new List<Group>();
            //square = item 0,1,2,9,10,11...3,4,5,12,13,14... etc

            for (int GridRow = 0; GridRow < (BoardDimension * BoardDimension); GridRow += (SquareDimension * BoardDimension))
            {

                for (int GridColumn = GridRow; GridColumn < (GridRow + BoardDimension); GridColumn += SquareDimension)
                {
                    List<Cell> ColumnValues = new List<Cell>();

                    for (int Squarerow = GridColumn; Squarerow < GridColumn + (SquareDimension * BoardDimension); Squarerow += BoardDimension)
                    {
                        for (int SquareColumn = Squarerow; SquareColumn < Squarerow + SquareDimension; SquareColumn++)
                        {
                            ColumnValues.Add(Cells[SquareColumn]);
                        }

                    }

                    SquareList.Add(new Group(ColumnValues));
                }

            }
            return SquareList;

        }

        public string PrintBoard()
        {
            string RowsToString = string.Empty;
            foreach (Group gr in Rows())
            {
                RowsToString += gr.ValuesToString() + System.Environment.NewLine;
            }
            return RowsToString;
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

        public Board DeepCopy()
        {
            //okay, so we have to make a new list of new cells based off the old one
            List<Cell> NewCells = new List<Cell>();
            foreach (Cell Oldcell in Cells)
            {
                NewCells.Add(new Cell(Oldcell.Value, Oldcell.IsChangeable));
            }
            Board NewBoard = new Board(NewCells);
            return NewBoard;
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

            foreach (Group CheckGroup in Columns())
            {
                if (!CheckGroup.IsComplete()) { return false; }

            }
            return true;

        }

        public bool SquaresAreComplete()
        {

            foreach (Group CheckGroup in Squares())
            {
                if (!CheckGroup.IsComplete()) { return false; }

            }
            return true;

        }


        public bool RowsAreValid()
        {
            foreach (Group CheckGroup in Rows())
            {
                if (!CheckGroup.IsValid()) { return false; }

            }
            return true;

        }

        public bool ColumnsAreValid()
        {

            foreach (Group CheckGroup in Columns())
            {
                if (!CheckGroup.IsValid()) { return false; }

            }
            return true;

        }

        public bool SquaresAreValid()
        {

            foreach (Group CheckGroup in Squares())
            {
                if (!CheckGroup.IsValid()) { return false; }

            }
            return true;

        }

        #endregion
    }
}
