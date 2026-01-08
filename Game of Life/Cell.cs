using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_of_Life {
    internal class Cell {

        // state of the cell
        public bool IsAlive { get; set; }

        // default constructor sets cell to dead unless specified otherwise
        public Cell(bool alive = false) { 
            IsAlive = alive; 
        }

        // represent cell state
        public override string ToString() { 
            return IsAlive ? "■" : " ";
        }
    }
}