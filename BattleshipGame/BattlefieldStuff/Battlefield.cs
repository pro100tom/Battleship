using BattleshipGame;
using BattleshipGame.CoordinateStuff;
using BattleshipGame.ShipStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.BattlefieldStuff
{
    public class Battlefield
    {
        private readonly List<Ship> ships = new List<Ship>();
        private readonly List<Coordinate> coordinates = new List<Coordinate>();

        public string Name { get; set; }

        public IReadOnlyList<Ship> Ships { get => ships.AsReadOnly(); }
        public IReadOnlyList<Coordinate> Coordinates { get => coordinates.AsReadOnly(); }
        public Dimensions Dimensions { get; }

        public Battlefield(Dimensions dimensions)
        {
            Dimensions = dimensions;

            for (int i = 0; i < Dimensions.Rows; i++)
                for (int j = 0; j < Dimensions.Columns; j++)
                    coordinates.Add(new Coordinate(j, i));
        }

        public void PlaceShip(Ship ship, bool assignRandomCoordinate = false)
        {
            if (assignRandomCoordinate)
            {
                var availableCoordinates = GetCoordinatesWhereShipCanBePlaced(ship);
                //if (!availableCoordinates.Any())
                //throw new IndexOutOfRangeException();

                var selectedCoordinate = availableCoordinates[RandomHelper.Random.Next(availableCoordinates.Count)];
                ship.SetTranslation(selectedCoordinate.Column, selectedCoordinate.Row);
            }

            if (!CanPlace(ship))
                throw new Exception();

            ships.Add(ship);
            ship.MarkPlaced();
            ship.ReplaceCoordinatesFromPool(Coordinates.ToList());
        }

        public void PlaceShips(List<Ship> ships, bool assignRandomCoordinate = false)
        {
            foreach (var ship in ships)
                PlaceShip(ship, assignRandomCoordinate);
        }

        public bool CanPlace(Ship ship)
        {
            var occupiedCoordinates = GetOccupiedCoordinates();
            foreach (var coordinate in ship.Coordinates)
            {
                if (!IsCoordinateWithinBounds(coordinate))
                    return false;

                if (occupiedCoordinates.ContainCooresponding(coordinate))
                    return false;
            }

            return true;
        }

        public List<Coordinate> GetOccupiedCoordinates(bool includeNearbyCoordinates = true)
        {
            var coordinates = new HashSet<Coordinate>();
            foreach (var ship in Ships)
                foreach (var coordinate in ship.Coordinates)
                {
                    coordinates.Add(coordinate);

                    if (!includeNearbyCoordinates)
                        continue;

                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                        {
                            var nearbyCoordinate = new Coordinate(coordinate.Column + j, coordinate.Row + i);
                            if (IsCoordinateWithinBounds(nearbyCoordinate))
                                coordinates.Add(nearbyCoordinate);
                        }
                }

            return coordinates.ToList();
        }

        public bool IsCoordinateWithinBounds(Coordinate coordinate)
        {
            return coordinate.Column >= 0 && coordinate.Column < Dimensions.Columns
                && coordinate.Row >= 0 && coordinate.Row < Dimensions.Rows;
        }

        public Ship GetShip(Coordinate coordinate)
        {
            foreach (var ship in ships)
                foreach (var shipCoordinate in ship.Coordinates)
                    if (shipCoordinate.Row == coordinate.Row && shipCoordinate.Column == coordinate.Column)
                        return ship;

            return null;
        }

        public ShipInfo GetShipInfo(Coordinate coordinate)
        {
            var ship = GetShip(coordinate);
            if (ship == null)
                return null;

            return new ShipInfo(ship.IsDamaged, ship.IsSunk);
        }

        public bool HasShip(Coordinate coordinate)
        {
            return GetShip(coordinate) != null;
        }

        public List<Coordinate> GetCoordinatesWhereShipCanBePlaced(Ship ship)
        {
            var availableCoordinates = new List<Coordinate>();

            foreach (var coordinate in Coordinates)
            {
                var tempShip = new Ship(ship);
                tempShip.SetTranslation(coordinate.Column, coordinate.Row);
                if (CanPlace(tempShip))
                    availableCoordinates.Add(coordinate);
            }

            return availableCoordinates;
        }

        public List<Coordinate> GetUncheckedCoordinates()
        {
            return coordinates.Where(c => !c.IsChecked).ToList();
        }

        public List<Coordinate> GetNeighbourCoordinates(Coordinate coordinate, bool excludeDiagonals = true)
        {
            var neighbourCoordinates = new List<Coordinate>();
            for (int i = -1; i <= 1; i++)
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    if (excludeDiagonals)
                        if ((i != 0 && j != 0))
                            continue;

                    var neighbour = new Coordinate(coordinate.Column + i, coordinate.Row + j);
                    if (IsCoordinateWithinBounds(neighbour) && !neighbourCoordinates.ContainCooresponding(neighbour))
                        neighbourCoordinates.Add(Coordinates.Where(c => c.Like(neighbour)).FirstOrDefault());
                }

            return neighbourCoordinates;
        }

        public List<Coordinate> GetNeighbourCoordinates(Ship ship, bool excludeDiagonals = true)
        {
            var neighbourCoordinates = new List<Coordinate>();
            foreach (var coordinate in ship.Coordinates)
                neighbourCoordinates.AddRange(GetNeighbourCoordinates(coordinate, excludeDiagonals));

            neighbourCoordinates = new List<Coordinate>(neighbourCoordinates.Distinct());
            neighbourCoordinates = new List<Coordinate>(neighbourCoordinates.Except(ship.Coordinates));

            return neighbourCoordinates;
        }

        public List<Ship> GetDamagedShips()
        {
            return Ships.Where(s => s.IsDamaged && !s.IsSunk).ToList();
        }

        public List<Ship> GetSunkShips()
        {
            return Ships.Where(s => s.IsSunk).ToList();
        }
    }
}
