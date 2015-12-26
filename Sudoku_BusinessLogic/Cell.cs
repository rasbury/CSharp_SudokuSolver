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
        //public Boolean IsChangeable { get; }

        /// <summary>
        /// Cell creator - Changeable can only be set here
        /// </summary>
        public Cell(int value)
        {
            Value = value;
            //IsChangeable = changeable;
        }
        

    }
}
