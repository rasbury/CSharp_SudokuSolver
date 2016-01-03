namespace Sudoku_BusinessLogic
{
    public class Cell
    {

        private int MyValue; //so you make infinite loops and crash unit testing if you have a property that sets itself, like Value below...

        public int Value 
        {
            get { return MyValue; }
            set { if (IsChangeable) { MyValue = value; } }
        }

        public bool IsChangeable { get; }

        /// <summary>
        /// Cell creator - Changeable can only be set here
        /// </summary>
        public Cell(int value, bool isChangeable)
        {
            MyValue = value;
            IsChangeable = isChangeable;
        }


        /// <summary>
        /// Cell creator - defaults to changeable
        /// </summary>
        public Cell(int value)
        {
            MyValue = value;
            IsChangeable = true;
        }

        /// <summary>
        /// Adds 1 to current value, rolls over to 0 if it would hit 10
        /// </summary>
        public void IncrementValueBy1()
        {
            Value += 1;
            if (Value > 9) { Value = 0; }
        }

    }
}
