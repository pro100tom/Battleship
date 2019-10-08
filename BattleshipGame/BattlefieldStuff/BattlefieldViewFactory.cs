using BattleshipGame.CoordinateStuff;
using System;

namespace BattleshipGame.BattlefieldStuff
{
    public class BattlefieldViewFactory
    {
        public event EventHandler<CoordinateViewAddedEventArgs> OnCoordinateViewAdded;

        public BattlefieldView CreateBattlefieldView(Battlefield battlefield)
        {
            var battlefieldView = new BattlefieldView(battlefield);
            //battlefieldView.Style = battlefieldViewStyleFactory.CreateStyle(battlefield.Dimensions);
            foreach (var coordinate in battlefield.Coordinates)
            {
                var coordinateView = new CoordinateView(coordinate);
                /*
                var (hasShip, isDamaged, isSunk) = battlefield.GetInfo(coordinate);
                coordinateView.Style = coordinateViewStyleFactory.CreateMouseAwayStyle(
                    hideShips, coordinate, hasShip, isDamaged, isSunk
                );
                */

                battlefieldView.Children.Add(coordinateView);
                OnCoordinateViewAdded?.Invoke(this, new CoordinateViewAddedEventArgs(coordinate, coordinateView, 
                    battlefield, battlefieldView));
            }

            return battlefieldView;
        }
    }
}
