using BattleshipGame.CoordinateStuff;
using System.Linq;

namespace BattleshipGame.BattlefieldStuff
{
    public class BattlefieldViewStyleUpdateService
    {
        private readonly BattlefieldViewStyleFactory battlefieldViewStyleFactory;

        private BattlefieldView BattlefieldView { get; }
        private BattlefieldViewStyleSettings BattlefieldViewStyleSettings { get; }

        public BattlefieldViewStyleUpdateService(BattlefieldView battlefieldView, BattlefieldViewStyleSettings battlefieldViewStyleSetting)
        {
            battlefieldViewStyleFactory = new BattlefieldViewStyleFactory();
            BattlefieldView = battlefieldView;
            BattlefieldViewStyleSettings = battlefieldViewStyleSetting;
        }

        public void UpdateStyle()
        {
            BattlefieldView.Dispatcher.Invoke(() =>
            {
                BattlefieldView.Style = battlefieldViewStyleFactory.CreateBattlefieldViewStyle(BattlefieldView.Battlefield.Dimensions);
                foreach (var coordinateView in BattlefieldView.Children.OfType<CoordinateView>())
                    UpdateStyle(coordinateView, MouseState.Away);
            });
        }

        public void UpdateStyle(CoordinateView coordinateView, MouseState mouseState, bool highlight = false)
        {
            var shipInfo = BattlefieldView.Battlefield.GetShipInfo(coordinateView.Coordinate);
            coordinateView.Dispatcher.Invoke(() =>
            {
                coordinateView.Style = battlefieldViewStyleFactory.CreateCoordinateViewStyle(
                    mouseState, BattlefieldViewStyleSettings.HideUndamagedShips, coordinateView.Coordinate, shipInfo, highlight
                );
            });
        }
    }
}
