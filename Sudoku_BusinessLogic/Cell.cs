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

    }
}
