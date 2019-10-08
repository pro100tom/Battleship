using BattleshipGame.CoordinateStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BattleshipGame
{
    public static class ExtensionMethods
    {
        public static bool ContainCooresponding(this List<Coordinate> coordinates, Coordinate coordinate)
        {
            return coordinates.Where(c => c.Like(coordinate)).Any();
        }

        public static Coordinate FetchRandomCoordinate(this List<Coordinate> coordinates)
        {
            var index = RandomHelper.Random.Next(coordinates.Count);

            return coordinates[index];
        }

        public static bool Like(this Coordinate coordinate, Coordinate other)
        {
            return coordinate.Column == other.Column && coordinate.Row == other.Row;
        }

        public static Color Blend(this Color color, Color backColor, double amount)
        {
            byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
            byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
            byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));

            return Color.FromRgb(r, g, b);
        }
    }
}
