using BattleshipGame.CoordinateStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BattleshipGame.ShipStuff.ShipDefaultCoordinates;

namespace BattleshipGame.ShipStuff
{
    public class ShipCoordinatesMapper
    {
        public static readonly IReadOnlyDictionary<Enum, List<Coordinate>> AirCarrierCoordinates = new Dictionary<Enum, List<Coordinate>>
        {
            { AirCarrierType.Straight, AirCarrierCoordinateList.Straight },
            { AirCarrierType.Angle, AirCarrierCoordinateList.Angle },
            { AirCarrierType.Index, AirCarrierCoordinateList.Index },
            { AirCarrierType.LetterC, AirCarrierCoordinateList.LetterC },
            { AirCarrierType.LetterL, AirCarrierCoordinateList.LetterL },
            { AirCarrierType.LetterT, AirCarrierCoordinateList.LetterT },
            { AirCarrierType.LetterZ, AirCarrierCoordinateList.LetterZ },
            { AirCarrierType.Number4, AirCarrierCoordinateList.Number4 },
            { AirCarrierType.Plus, AirCarrierCoordinateList.Plus },
            { AirCarrierType.Sniper, AirCarrierCoordinateList.Sniper },
            { AirCarrierType.Stairs, AirCarrierCoordinateList.Stairs },
            { AirCarrierType.Tonfa, AirCarrierCoordinateList.Tonfa },
        };

        public static readonly IReadOnlyDictionary<Enum, List<Coordinate>> BattleshipCoordinates = new Dictionary<Enum, List<Coordinate>>
        {
            { BattleshipType.LetterL, BattleshipCoordinateList.LetterL },
            { BattleshipType.LetterT, BattleshipCoordinateList.LetterT },
            { BattleshipType.Number4, BattleshipCoordinateList.Number4 },
            { BattleshipType.Square, BattleshipCoordinateList.Square },
            { BattleshipType.Straight, BattleshipCoordinateList.Straight },
        };

        public static readonly IReadOnlyDictionary<Enum, List<Coordinate>> CruiserCoordinates = new Dictionary<Enum, List<Coordinate>>
        {
            { CruiserType.Angle, CruiserCoordinateList.Angle },
            { CruiserType.Straight, CruiserCoordinateList.Straight },
        };

        public static readonly IReadOnlyDictionary<Enum, List<Coordinate>> DestroyerCoordinates = new Dictionary<Enum, List<Coordinate>>
        {
            { DestroyerType.Straight, DestroyerCoordinateList.Straight },
        };

        public static readonly IReadOnlyDictionary<Enum, List<Coordinate>> SubmarineCoordinates = new Dictionary<Enum, List<Coordinate>>
        {
            { SubmarineType.Square, SubmarineCoordinateList.Square },
        };
    }
}
