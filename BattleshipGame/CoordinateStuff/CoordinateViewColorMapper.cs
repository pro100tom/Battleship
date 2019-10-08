using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BattleshipGame.CoordinateStuff
{
    public class CoordinateViewColorMapper
    {
        public static readonly IReadOnlyDictionary<Color, Color> IdleMouseOverColorPairs = new Dictionary<Color, Color>()
        {
            { Colors.AliceBlue, Color.FromRgb(213, 235, 255) },
            { Colors.Green, Color.FromRgb(0, 151, 0) },
            { Colors.Orange, Colors.DarkOrange },
            { Colors.DarkSlateGray, Color.FromRgb(67, 96, 96) },
        };

        public static readonly IReadOnlyDictionary<Color, Color> IdleMouseDownColorPairs = new Dictionary<Color, Color>()
        {
            { Colors.AliceBlue, Color.FromRgb(198, 227, 255) },
            { Colors.Green, Color.FromRgb(0, 164, 0) },
            { Colors.Orange, Colors.DarkOrange },
            { Colors.DarkSlateGray, Color.FromRgb(67, 96, 96) },
        };
    }
}
