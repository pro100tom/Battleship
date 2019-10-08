using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.BattlefieldStuff
{
    public class BattlefieldViewStyleSettings
    {
        public bool HideUndamagedShips { get; }
        public bool HideDamagedShips { get; }
        public bool HideSunkShips { get; }

        public BattlefieldViewStyleSettings(bool hideUndamagedShips, bool hideDamagedShips, bool hideSunkShips)
        {
            HideUndamagedShips = hideUndamagedShips;
            HideDamagedShips = hideDamagedShips;
            HideSunkShips = hideSunkShips;
        }
    }
}
