using System.Collections.Generic;

namespace Sudoku_BusinessLogic
{
    public class Cell
    {

        private int MyValue;

        public int Value
        {
            get { return MyValue; }
            set { if (IsChangeable) { MyValue = value; } }
        }


        public bool IsChangeable { get; }

        /// <summary>
        /// Cell creator - Changeable can only be set manually here
        /// </summary>
        public Cell(int value, bool isChangeable)
        {
            MyValue = value;
            IsChangeable = isChangeable;
            TriedValues = new List<int>();
        }


        /// <summary>
        /// Cell creator - defaults to changeable being true only if value == 0
        /// </summary>
        public Cell(int value)
        {
            MyValue = value;
            IsChangeable = value == 0;
            TriedValues = new List<int>();
        }


        #region Methods for BruteForceSolver

        /// <summary>
        /// Helps BruteForceSolver not get into infinite loops
        /// </summary>
        public List<int> TriedValues { get; set; }

        /// <summary>
        /// Adds 1 to current value, rolls over to 0 if it would hit 10 or we have tried all values
        /// </summary>
        public void IncrementValueBy1()
        {

            //save off the value we've already tried
            if (!TriedValues.Contains(Value) && Value > 0) { TriedValues.Add(Value); }

            //find the next untried value.  If we don't have one, return 0
            IncrementValueWithRollover();

            while (TriedValues.Contains(Value) && Value > 0)
            {
                IncrementValueWithRollover();
            }

            if (!TriedValues.Contains(Value) && Value > 0) { TriedValues.Add(Value); }

        }

        private void IncrementValueWithRollover()
        {
            Value += 1;
            if (Value > 9) { Value = 0; }

        }

        /// <summary>
        /// Also clears history
        /// </summary>
        public void BlankOutCell()
        {
            Value = 0;
            TriedValues = new List<int>();
        }


        #endregion

    }
}
