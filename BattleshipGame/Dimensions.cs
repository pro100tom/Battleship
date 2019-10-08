using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    public class Dimensions
    {
        public int Columns { get; }
        public int Rows { get; }

        public Dimensions(int columns, int rows)
        {
            Columns = columns;
            Rows = rows;
        }
    }
}
