using BattleshipGame.CoordinateStuff;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BattleshipGame.ShipStuff
{
    public enum ShipSize { AirCarrier, Battleship, Cruiser, Destroyer, Submarine }

    public enum AirCarrierType { Straight, Angle, Index, LetterC, LetterL, LetterT, LetterZ, Number4, Plus, Sniper, Stairs, Tonfa }
    public enum BattleshipType { Straight, LetterL, LetterT, Square, Number4 }
    public enum CruiserType { Straight, Angle }
    public enum DestroyerType { Straight }
    public enum SubmarineType { Square }

    public class Ship
    {
        private readonly IReadOnlyCollection<Coordinate> defaultCoordinates = new List<Coordinate>();
        private List<Coordinate> coordinates = new List<Coordinate>();
        private static readonly CoordinateTransformationService transformationService = new CoordinateTransformationService();
        private TransformationSettings transformation = new TransformationSettings();

        public event EventHandler OnSunk;

        public IReadOnlyCollection<Coordinate> Coordinates { get => coordinates.AsReadOnly(); }
        public Enum ShipType { get; }
        public ShipSize ShipSize { get; private set; }
        public bool IsDamaged { get; private set; }
        public bool IsSunk { get; private set; }
        public bool IsPlaced { get; private set; }

        public Ship(AirCarrierType shipType)
        {
            ShipSize = ShipSize.AirCarrier;
            ShipType = shipType;
            defaultCoordinates = ShipCoordinatesMapper.AirCarrierCoordinates[shipType];
            coordinates = ShipCoordinatesMapper.AirCarrierCoordinates[shipType];
        }

        public Ship(BattleshipType shipType)
        {
            ShipSize = ShipSize.Battleship;
            ShipType = shipType;
            defaultCoordinates = ShipCoordinatesMapper.BattleshipCoordinates[shipType];
            coordinates = ShipCoordinatesMapper.BattleshipCoordinates[shipType];
        }

        public Ship(CruiserType shipType)
        {
            ShipSize = ShipSize.Cruiser;
            ShipType = shipType;
            defaultCoordinates = ShipCoordinatesMapper.CruiserCoordinates[shipType];
            coordinates = ShipCoordinatesMapper.CruiserCoordinates[shipType];
        }

        public Ship(DestroyerType shipType)
        {
            ShipSize = ShipSize.Destroyer;
            ShipType = shipType;
            defaultCoordinates = ShipCoordinatesMapper.DestroyerCoordinates[shipType];
            coordinates = ShipCoordinatesMapper.DestroyerCoordinates[shipType];
        }

        public Ship(SubmarineType shipType)
        {
            ShipSize = ShipSize.Submarine;
            ShipType = shipType;
            defaultCoordinates = ShipCoordinatesMapper.SubmarineCoordinates[shipType];
            coordinates = ShipCoordinatesMapper.SubmarineCoordinates[shipType];
        }

        public Ship(Ship ship)
        {
            ShipSize = ship.ShipSize;
            ShipType = ship.ShipType;

            switch (ShipSize)
            {
                case ShipSize.AirCarrier:
                    defaultCoordinates = ShipCoordinatesMapper.AirCarrierCoordinates[ShipType];
                    coordinates = ShipCoordinatesMapper.AirCarrierCoordinates[ShipType];
                    break;
                case ShipSize.Battleship:
                    defaultCoordinates = ShipCoordinatesMapper.BattleshipCoordinates[ShipType];
                    coordinates = ShipCoordinatesMapper.BattleshipCoordinates[ShipType];
                    break;
                case ShipSize.Cruiser:
                    defaultCoordinates = ShipCoordinatesMapper.CruiserCoordinates[ShipType];
                    coordinates = ShipCoordinatesMapper.CruiserCoordinates[ShipType];
                    break;
                case ShipSize.Destroyer:
                    defaultCoordinates = ShipCoordinatesMapper.DestroyerCoordinates[ShipType];
                    coordinates = ShipCoordinatesMapper.DestroyerCoordinates[ShipType];
                    break;
                case ShipSize.Submarine:
                    defaultCoordinates = ShipCoordinatesMapper.SubmarineCoordinates[ShipType];
                    coordinates = ShipCoordinatesMapper.SubmarineCoordinates[ShipType];
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        public void MarkPlaced()
        {
            IsPlaced = true;
        }

        public TransformationSettings GetTransformation()
        {
            return new TransformationSettings(transformation);
        }

        public void SetTransformation(TransformationSettings transformation)
        {
            if (IsPlaced)
                return;

            this.transformation = transformation;
            coordinates = transformationService.TransformCoordinates(defaultCoordinates.ToList(), this.transformation);
        }

        public void SetTranslation(int horizontalOffset, int verticalOffset)
        {
            if (IsPlaced)
                return;

            transformation.HorizontalOffset = horizontalOffset;
            transformation.VerticalOfsset = verticalOffset;

            coordinates = transformationService.TransformCoordinates(defaultCoordinates.ToList(), transformation);
        }

        public List<Coordinate> GetCheckedCoordinates()
        {
            return coordinates.Where(c => c.IsChecked).ToList();
        }

        public void ReplaceCoordinatesFromPool(List<Coordinate> pool)
        {
            var newCoordinates = new List<Coordinate>();
            foreach (var coordinate in pool)
            {
                if (coordinates.ContainCooresponding(coordinate))
                {
                    coordinate.OnChecked += Coordinate_OnChecked;
                    newCoordinates.Add(coordinate);
                }
            }

            coordinates = newCoordinates;
        }

        private void Coordinate_OnChecked(object sender, EventArgs e)
        {
            IsDamaged = true;
            var allChecked = coordinates.All(c => c.IsChecked);
            if (allChecked && !IsSunk)
            {
                IsSunk = true;
                OnSunk?.Invoke(this, new EventArgs());
            }
        }
    }
}
