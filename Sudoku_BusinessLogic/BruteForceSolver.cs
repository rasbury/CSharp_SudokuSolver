using System;
using System.Collections.Generic;

namespace Sudoku_BusinessLogic
{
    public class BruteForceSolver
    {
        public static string InvalidBoardMessage = "Board passed to this method is not solvable, was already invalid.";
        public static string UnsolvableBoardmessage = "This board is unsolvable.";  //actually, it looks like all sudokus are solvable via brute force
                                                                                    //it's just that logical strategies can't figure out all of them
        private Stack<int> ModifiedCells;

        public void BruteForceSolve(ref Board board)
        {
            //fail fast if they send us a bad board to solve
            if (!board.IsValid()) {throw new Exception(InvalidBoardMessage);}

            ModifiedCells = new Stack<int>();
            while (!board.IsComplete())
            {

                //try to fill in the next empty cell, since we haven't broken anything yet
                int Index = IndexOfNextBlankCell(ref board);

                //so we know this cell's value is 0, and we're going to put it to 1 now.  
                //If 1 isn't valid, we'll keep cycling through #s until we hit a valid option or we're back at 0
                //if we're back at 0, this method returns false
                if (!TrySolveCell(ref board, Index))
                {
                    //we could not find a valid value.  Blank this one out, Back up to the previous editable cell and change it by 1
                    board.Cells[Index].BlankOutCell();
                    //This exception should never be hit from brute force
                    if (!RevisePreviousGuess(ref board)) { throw new Exception(UnsolvableBoardmessage); }
                }


            }

        }

        public bool TrySolveCell(ref Board board, int IndexOfCell)
        {
            //Try all the values until we get around to the cell's original value
            //if we couldn't find another option in here, then we can't solve the cell

            int OriginalValue = board.Cells[IndexOfCell].Value;
            board.Cells[IndexOfCell].IncrementValueBy1();

            while ((!board.IsValid()) &&
                (board.Cells[IndexOfCell].Value != OriginalValue))
            {
                board.Cells[IndexOfCell].IncrementValueBy1();
            }

            //record this cell as the last one we changed
            if (board.Cells[IndexOfCell].Value != OriginalValue && board.Cells[IndexOfCell].Value > 0)
            {
                ModifiedCells.Push(IndexOfCell);
            }
            //we just came around to where we started.  We failed to solve it
            //but if we're here and the result isn't the original value, we found another solution!
            return (board.Cells[IndexOfCell].Value != OriginalValue && board.Cells[IndexOfCell].Value > 0);

        }

        /// <summary>
        /// We found a cell where no value is correct, so one of our previous guesses was wrong.  
        /// Back up through the stack of what we've done, trying alternative values
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool RevisePreviousGuess(ref Board board)
        {
            bool ValueChanged = false;
            while (!ValueChanged && ModifiedCells.Count > 0)
            {
                int IndexToTry = ModifiedCells.Pop();
                ValueChanged = TrySolveCell(ref board, IndexToTry);
                //if we couldn't solve the cell, reset it for next time
                if (!ValueChanged) { board.Cells[IndexToTry].BlankOutCell(); }
            }

            return ValueChanged;
        }

        public int IndexOfNextBlankCell(ref Board board)
        {
            for (int i = 0; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].Value == 0) { return i; }
            }
            return -1;
        }

    }
}
