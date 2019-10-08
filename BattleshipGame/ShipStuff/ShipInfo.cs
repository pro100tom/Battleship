using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.ShipStuff
{
    public class ShipInfo
    {
        public bool IsDamaged { get; }
        public bool IsSunk { get; }

        public ShipInfo(bool isDamaged, bool isSunk)
        {
            IsDamaged = isDamaged;
            IsSunk = isSunk;
        }
    }
}
