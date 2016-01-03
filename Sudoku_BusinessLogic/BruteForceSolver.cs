using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_BusinessLogic
{
    public class BruteForceSolver
    {

        public void BruteForceSolve(ref Board board)
        {
            while (!board.IsComplete())
            {

                if (board.IsValid())
                {
                    //try to fill in the next empty cell, since we haven't broken anything yet
                    int Index = IndexOfNextBlankCell(ref board);
                    if (Index < 0)
                    {
                        //we're valid, but we have no blank cells...that's weird, and shouldn't have happened
                        if (board.IsComplete()) { break; }
                        else { throw new Exception("No blank cells available, all is valid, but board is not complete!"); }
                    }
                    else
                    {
                        //so we know this cell's value is 0, and we're going to put it to 1 now.  
                        //If 1 isn't valid, we'll keep cycling through #s until we hit a valid option or we're back at 0
                        //if we're back at 0, this method returns false
                        if (!TrySolveCell(ref board, Index))
                        {

                            //we could not find a valid value.  Blank this one out, Back up to the previous editable cell and change it by 1
                            board.Cells[Index].Value = 0;
                            board.Cells[IndexOfPreviousEditableCell(ref board, Index)].IncrementValueBy1();
                        }

                    }
                }
                else
                {
                    //we somehow got the board into an invalid state.  Back up and try again
                }

            }

        }

        public bool TrySolveCell(ref Board board, int IndexOfCell)
        {
            //Try all the values until we get around to the cell's original value
            //if we couldn't find another option in here, then we can't solve the cell

            int OriginalValue = board.Cells[IndexOfCell].Value;
            board.Cells[IndexOfCell].IncrementValueBy1();

            while (board.Cells[IndexOfCell].Value != OriginalValue)
            {
                if (!board.IsValid()) { board.Cells[IndexOfCell].IncrementValueBy1(); }
            }

            //we just came around to where we started.  We failed to solve it
            return (board.Cells[IndexOfCell].Value == OriginalValue);

        }

        public int IndexOfNextBlankCell(ref Board board)
        {
            for (int i = 0; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].Value == 0) { return i; }
            }
            return -1;
        }

        public int IndexOfPreviousEditableCell(ref Board board, int lastindex)
        {
            int returnindex = lastindex - 1;
            while (returnindex > 0 && !board.Cells[returnindex].IsChangeable)
            {
                returnindex -= 1;
            }

            //not really sure what it would mean if we have no previous editable cells...
            //maybe that we're at the beginning?  But if so, we really shouldn't have backtracked this far
            //I suppose this could cause an infinite loop, but I think we'd just head forward instead
            return 0;
        }


        //public void FillInMissingValues(ref Group unsolvedgroup)
        //{
        //    while (!unsolvedgroup.IsComplete())
        //    {
        //        List<int> MissingElements = unsolvedgroup.MissingElements();
        //        //We will always fill in elements in a predicatable order
        //        MissingElements.Sort();
        //        foreach (Cell cell in unsolvedgroup.Cells)
        //        {
        //            if (cell.Value == 0)
        //            {
        //                //we should never have a case where there are more empty cells than values not assigned,
        //                //but if that happened, the two lines after this check would explode, probably not neatly
        //                if (MissingElements.Count == 0) { throw new Exception("Not enough missing elements to fill in a blank cell!"); }

        //                cell.Value = MissingElements[0];
        //                MissingElements.RemoveAt(0);
        //            }
        //        }
        //    }

        //}


    }
}
