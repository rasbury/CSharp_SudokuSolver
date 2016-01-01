using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_BusinessLogic
{
    public class Cell 
    {

        public int Value { get; set; }
        public bool IsChangeable { get; }

        /// <summary>
        /// Cell creator - Changeable can only be set here
        /// </summary>
        public Cell(int value, bool ischangeable)
        {
            Value = value;
            IsChangeable = ischangeable;
        }


        /// <summary>
        /// Cell creator - defaults to changeable
        /// </summary>
        public Cell(int value)
        {
            Value = value;
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
