using BattleshipGame.CoordinateStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.ShipStuff
{
    public class ShipDefaultCoordinates
    {
        public class AirCarrierCoordinateList
        {
            public static readonly List<Coordinate> Straight = new List<Coordinate>()
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(0, 3),
                new Coordinate(0, 4),
            };

            public static readonly List<Coordinate> Angle = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(1, 2),
                new Coordinate(2, 2),
            };

            public static readonly List<Coordinate> Index = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(0, 1),
                new Coordinate(1, 1),
                new Coordinate(0, 2),
            };

            public static readonly List<Coordinate> LetterC = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(1, 2),
            };

            public static readonly List<Coordinate> LetterL = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(0, 3),
            };

            public static readonly List<Coordinate> LetterT = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(2, 0),
                new Coordinate(1, 1),
                new Coordinate(1, 2),
            };

            public static readonly List<Coordinate> LetterZ = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(1, 1),
                new Coordinate(1, 2),
                new Coordinate(2, 2),
            };

            public static readonly List<Coordinate> Number4 = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(1, 2),
                new Coordinate(1, 3),
            };

            public static readonly List<Coordinate> Plus = new List<Coordinate>
            {
                new Coordinate(1, 0),
                new Coordinate(0, 1),
                new Coordinate(1, 1),
                new Coordinate(2, 1),
                new Coordinate(1, 2),
            };

            public static readonly List<Coordinate> Sniper = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(1, 1),
                new Coordinate(1, 2),
                new Coordinate(2, 1),
            };

            public static readonly List<Coordinate> Stairs = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(1, 1),
                new Coordinate(1, 2),
                new Coordinate(2, 2),
            };

            public static readonly List<Coordinate> Tonfa = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(0, 3),
                new Coordinate(1, 1),
            };
        }

        public class BattleshipCoordinateList
        {
            public static readonly List<Coordinate> Straight = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(0, 3),
            };

            public static readonly List<Coordinate> LetterL = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
            };

            public static readonly List<Coordinate> LetterT = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(2, 0),
                new Coordinate(1, 1),
            };

            public static readonly List<Coordinate> Square = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(0, 1),
                new Coordinate(1, 1),
            };

            public static readonly List<Coordinate> Number4 = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(1, 1),
                new Coordinate(1, 2),
            };
        }

        public class CruiserCoordinateList
        {
            public static readonly List<Coordinate> Straight = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
            };

            public static readonly List<Coordinate> Angle = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(1, 0),
                new Coordinate(0, 1),
            };
        }

        public class DestroyerCoordinateList
        {
            public static readonly List<Coordinate> Straight = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
            };
        }

        public class SubmarineCoordinateList
        {
            public static readonly List<Coordinate> Square = new List<Coordinate>
            {
                new Coordinate(0, 0),
            };
        }
    }
}
