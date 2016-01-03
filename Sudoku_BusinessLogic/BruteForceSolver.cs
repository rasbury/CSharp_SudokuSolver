using System;
using System.Collections.Generic;

namespace Sudoku_BusinessLogic
{
    public class BruteForceSolver
    {

        Stack<int> ModifiedCells;

        public void BruteForceSolve(ref Board board)
        {
            ModifiedCells = new Stack<int>();
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
                            board.Cells[Index].BlankOutCell();
                            RevisePreviousGuess(ref board);
                            //board.Cells[IndexOfPreviousEditableCell(ref board, Index)].IncrementValueBy1();
                        }

                    }
                }
                else
                {
                    //we somehow got the board into an invalid state.  Back up and try again
                    int Index = IndexOfNextBlankCell(ref board);
                    board.Cells[IndexOfPreviousEditableCell(ref board, Index)].IncrementValueBy1();
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
            return (board.Cells[IndexOfCell].Value != OriginalValue && board.Cells[IndexOfCell].Value>0);

        }

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

        public int IndexOfPreviousEditableCell(ref Board board, int lastIndex)
        {
            int ReturnIndex = lastIndex - 1;
            while (ReturnIndex > 0 && !board.Cells[ReturnIndex].IsChangeable)
            {
                ReturnIndex -= 1;
            }

            //not really sure what it would mean if we have no previous editable cells...
            //maybe that we're at the beginning?  But if so, we really shouldn't have backtracked this far
            //I suppose this could cause an infinite loop, but I think we'd just head forward instead
            if (ReturnIndex < 0) { ReturnIndex = 0; }

            return ReturnIndex;
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
