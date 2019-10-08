using BattleshipGame.CoordinateStuff;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace BattleshipGame.BattlefieldStuff
{
    public class BattlefieldView : UniformGrid
    {
        public Battlefield Battlefield { get; }

        public BattlefieldView()
        {

        }

        public BattlefieldView(Battlefield battlefield)
        {
            Battlefield = battlefield;
        }

        public CoordinateView GetCoordinateViewByCoordinate(Coordinate coordinate)
        {
            return Dispatcher.Invoke(() =>
            {
                foreach (var coordinateView in Children.OfType<CoordinateView>())
                    if (coordinateView.Coordinate.Like(coordinate))
                        return coordinateView;

                return null;
            });
        }
    }
}
