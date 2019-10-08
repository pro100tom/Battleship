using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.ShipStuff
{
    public class ShipFactory
    {
        private readonly ShipService shipService = new ShipService();

        public List<Ship> CreateShips(NumberOfShips numberOfShips)
        {
            var ships = new List<Ship>();

            for (int i = 0; i < numberOfShips.NumberOfAirCarriers; i++)
                ships.Add(new Ship((AirCarrierType)shipService.GetRandomShipType(ShipSize.AirCarrier)));

            for (int i = 0; i < numberOfShips.NumberOfBattleships; i++)
                ships.Add(new Ship((BattleshipType)shipService.GetRandomShipType(ShipSize.Battleship)));

            for (int i = 0; i < numberOfShips.NumberOfCruisers; i++)
                ships.Add(new Ship((CruiserType)shipService.GetRandomShipType(ShipSize.Cruiser)));

            for (int i = 0; i < numberOfShips.NumberOfDestroyers; i++)
                ships.Add(new Ship(DestroyerType.Straight));

            for (int i = 0; i < numberOfShips.NumberOfSubmarines; i++)
                ships.Add(new Ship(SubmarineType.Square));

            return ships;
        }
    }
}
