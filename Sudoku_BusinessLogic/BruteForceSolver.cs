using System;
using System.Collections.Generic;

namespace Sudoku_BusinessLogic
{
    public class BruteForceSolver
    {
        public static string InvalidBoardMessage = "Board passed to this method is not solvable, was already invalid.";
        public static string UnsolvableBoardmessage = "This board is unsolvable.";  //actually, it looks like all sudokus are solvable via brute force
                                                                                    //it's just that logical strategies can't figure out all of them




        #region Public
        /// <summary>
        /// Will update board to be one valid solution, and set multipleSolutions to true if there are more solutions
        /// </summary>
        public void RecursiveBruteForceSolve(ref Board board, ref bool multipleSolutions)
        {
            //fail fast if they send us a bad board to solve
            if (!board.IsValid()) { throw new Exception(InvalidBoardMessage); }

            List<Board> SolvedBoards = TrySolveBoard(board);

            switch (SolvedBoards.Count)
            {
                case 0:
                    throw new Exception(UnsolvableBoardmessage);
                case 1:
                    board = SolvedBoards[0];
                    multipleSolutions = false;
                    return;
                default:
                    board = SolvedBoards[0];
                    multipleSolutions = true;
                    return;
            }
        }


        /// <summary>
        /// Will return at most one solution (first one it finds)
        /// </summary>
        public void SingleStateBruteForceSolve(ref Board board)
        {
            //fail fast if they send us a bad board to solve
            if (!board.IsValid()) { throw new Exception(InvalidBoardMessage); }


            Stack<int> ModifiedCells = new Stack<int>();
            while (!board.IsComplete())
            {

                //try to fill in the next empty cell, since we haven't broken anything yet
                int Index = IndexOfNextBlankCell(board);

                //so we know this cell's value is 0, and we're going to put it to 1 now.  
                //If 1 isn't valid, we'll keep cycling through #s until we hit a valid option or we're back at 0
                //if we're back at 0, this method returns false
                if (!TrySolveCell(ref board, Index, ref ModifiedCells))
                {
                    //we could not find a valid value.  Blank this one out, Back up to the previous editable cell and change it by 1
                    board.Cells[Index].BlankOutCell();
                    //This exception should never be hit from brute force
                    if (!RevisePreviousGuess(ref board, ref ModifiedCells)) { throw new Exception(UnsolvableBoardmessage); }
                }


            }

        }
        #endregion


        #region Private

        /// <summary>
        /// method shared between the two brute force approaches
        /// </summary>
        private int IndexOfNextBlankCell(Board board)
        {
            for (int i = 0; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].Value == 0) { return i; }
            }
            return -1;
        }

        #region Recursive, multi-state solving

        /// <summary>
        /// Recursive solve
        /// </summary>
        private List<Board> TrySolveBoard(Board board)
        {

            if (board.IsComplete())
            {
                List<Board> Solution = new List<Board>();

                //learned about byval vs byRef in C#.  Any method calls are executed on the original object
                //I had been assuming it would make a deep copy of the object every time the function is called, 
                //                        (as VB does, iirc), since I don't pass it Byref, but that's not the case
                //so here, we explicitly make a deep copy by re-creating all the cells in the list
                Solution.Add(board.DeepCopy());
                return Solution;
            }

            int Index = IndexOfNextBlankCell(board);
            List<int> PossibleValues = PossibleCellValues(board, Index);

            switch (PossibleValues.Count)
            {
                case 0:
                    return new List<Board>();
                case 1:
                    board.Cells[Index].Value = PossibleValues[0];
                    return TrySolveBoard(board);
                default:
                    List<Board> Solutions = new List<Board>();
                    foreach (int PossibleValue in PossibleValues)
                    {
                        //we desperately need to short-circuit if we have more than 1 solution already
                        //an empty grid has 6,670,903,752,021,072,936,960 solutions according to Wikipedia
                        //let's not calculate them all
                        if (Solutions.Count > 1) { return Solutions; }

                        board.Cells[Index].Value = PossibleValue;
                        List<Board> NewSolution = TrySolveBoard(board);

                        if (NewSolution.Count == 0) { BlankOutLaterEditableValues(board, Index); }
                        else { Solutions.AddRange(NewSolution); }

                    }
                    return Solutions;
            }

        }

        /// <summary>
        /// Clears values on the board when we need to back up.  Needed because we don't do a deep copy for every recursion of TrySolveBoard
        /// </summary>
        private void BlankOutLaterEditableValues(Board board, int currentIndex)
        {
            for (int i = currentIndex + 1; i < board.Cells.Count; i++)
            {
                if (board.Cells[i].IsChangeable) { board.Cells[i].BlankOutCell(); }
            }
        }


        private List<int> PossibleCellValues(Board board, int IndexOfCell)
        {

            if (IndexOfCell < 0) { return new List<int>(); }
            //Try all the values until we get around to the cell's original value
            //if we couldn't find another option in here, then we can't solve the cell
            List<int> PossibleValues = new List<int>();

            int OriginalValue = board.Cells[IndexOfCell].Value;
            board.Cells[IndexOfCell].IncrementValueBy1();

            while (board.Cells[IndexOfCell].Value != OriginalValue)
            {
                if (board.IsValid() && board.Cells[IndexOfCell].Value != 0)
                {
                    //record this as a possible value
                    PossibleValues.Add(board.Cells[IndexOfCell].Value);
                }
                board.Cells[IndexOfCell].IncrementValueBy1();
            }

            //if this is empty, we failed to solve it
            return PossibleValues;

        }


        #endregion

        #region Single-state solving


        private bool TrySolveCell(ref Board board, int indexOfCell, ref Stack<int> modifiedCells)
        {
            //Try all the values until we get around to the cell's original value
            //if we couldn't find another option in here, then we can't solve the cell

            int OriginalValue = board.Cells[indexOfCell].Value;
            board.Cells[indexOfCell].IncrementValueBy1();

            while ((!board.IsValid()) &&
                (board.Cells[indexOfCell].Value != OriginalValue))
            {
                board.Cells[indexOfCell].IncrementValueBy1();
            }

            //record this cell as the last one we changed
            if (board.Cells[indexOfCell].Value != OriginalValue && board.Cells[indexOfCell].Value > 0)
            {
                modifiedCells.Push(indexOfCell);
            }
            //we just came around to where we started.  We failed to solve it
            //but if we're here and the result isn't the original value, we found another solution!
            return (board.Cells[indexOfCell].Value != OriginalValue && board.Cells[indexOfCell].Value > 0);

        }

        /// <summary>
        /// We found a cell where no value is correct, so one of our previous guesses was wrong.  
        /// Back up through the stack of what we've done, trying alternative values
        /// </summary>
        private bool RevisePreviousGuess(ref Board board, ref Stack<int> modifiedCells)
        {
            bool ValueChanged = false;
            while (!ValueChanged && modifiedCells.Count > 0)
            {
                int IndexToTry = modifiedCells.Pop();
                ValueChanged = TrySolveCell(ref board, IndexToTry, ref modifiedCells);
                //if we couldn't solve the cell, reset it for next time
                if (!ValueChanged) { board.Cells[IndexToTry].BlankOutCell(); }
            }

            return ValueChanged;
        }


        #endregion

        #endregion
    }
}
