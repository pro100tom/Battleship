using System;
using System.ComponentModel;

namespace BattleshipGame.ShipStuff
{
    public class ShipService
    {
        public Enum GetRandomShipType(ShipSize shipSize)
        {
            switch (shipSize)
            {
                case ShipSize.AirCarrier:
                    var values = Enum.GetValues(typeof(AirCarrierType));
                    return (AirCarrierType)values.GetValue(RandomHelper.Random.Next(values.Length));
                case ShipSize.Battleship:
                    values = Enum.GetValues(typeof(BattleshipType));
                    return (BattleshipType)values.GetValue(RandomHelper.Random.Next(values.Length));
                case ShipSize.Cruiser:
                    values = Enum.GetValues(typeof(CruiserType));
                    return (CruiserType)values.GetValue(RandomHelper.Random.Next(values.Length));
                case ShipSize.Destroyer:
                    return DestroyerType.Straight;
                case ShipSize.Submarine:
                    return SubmarineType.Square;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
