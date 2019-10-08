using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.CoordinateStuff
{
    public class CoordinateEventArgs : EventArgs
    {
        public Coordinate Coordinate { get; }

        public CoordinateEventArgs(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }
    }

    public class Coordinate
    {
        public int Column { get; }
        public int Row { get; }
        public bool IsChecked { get; private set; }

        public event EventHandler OnChecked;

        public Coordinate(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public void SetChecked()
        {
            if (!IsChecked)
            {
                IsChecked = true;
                OnChecked?.Invoke(this, new EventArgs());
            }
        }
    }
}
