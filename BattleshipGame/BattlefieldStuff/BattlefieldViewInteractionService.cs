using BattleshipGame.CoordinateStuff;
using BattleshipGame.PlayerStuff;
using System;
using System.Linq;

namespace BattleshipGame.BattlefieldStuff
{
    public class BattlefieldViewInteractionService
    {
        public BattlefieldView BattlefieldView { get; private set; }
        public BattlefieldViewStyleUpdateService BattlefieldViewStyleUpdateService { get; }
        public AbstractPlayer Player { get; }
        public bool IsEnabled { get; private set; }

        public BattlefieldViewInteractionService(
            BattlefieldView battlefieldView, 
            BattlefieldViewStyleUpdateService battlefieldViewStyleUpdateService,
            AbstractPlayer player = null
        )
        {
            BattlefieldView = battlefieldView;
            BattlefieldViewStyleUpdateService = battlefieldViewStyleUpdateService;
            Player = player;

            foreach (var coordinateView in BattlefieldView.Children.OfType<CoordinateView>())
            {
                coordinateView.MouseEnter += CoordinateView_MouseEnter;
                coordinateView.MouseLeftButtonDown += CoordinateView_MouseLeftButtonDown;
                coordinateView.PreviewMouseLeftButtonUp += CoordinateView_PreviewMouseLeftButtonUp;
                coordinateView.MouseLeftButtonUp += CoordinateView_MouseLeftButtonUp;
                coordinateView.MouseLeave += CoordinateView_MouseLeave;
                coordinateView.Coordinate.OnChecked += Coordinate_OnChecked;
            }

            BattlefieldViewStyleUpdateService.UpdateStyle();
        }

        private void CoordinateView_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BattlefieldViewStyleUpdateService.UpdateStyle((sender as CoordinateView), MouseState.Hover);
        }

        private void CoordinateView_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!IsEnabled)
                return;

            BattlefieldViewStyleUpdateService.UpdateStyle((sender as CoordinateView), MouseState.Down);
        }

        private void CoordinateView_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!IsEnabled)
                return;

            if (Player == null)
                return;

            var coordinate = (sender as CoordinateView).Coordinate;
            if (coordinate.IsChecked)
                return;

            Player.TakeGuess(coordinate);
        }

        private void CoordinateView_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BattlefieldViewStyleUpdateService.UpdateStyle((sender as CoordinateView), MouseState.Hover);
        }

        private void CoordinateView_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            BattlefieldViewStyleUpdateService.UpdateStyle((sender as CoordinateView), MouseState.Away);
        }

        private void Coordinate_OnChecked(object sender, EventArgs e)
        {
            var coordinateView = BattlefieldView.GetCoordinateViewByCoordinate((sender as Coordinate));
            if (coordinateView == null)
                return;

            BattlefieldViewStyleUpdateService.UpdateStyle();
            BattlefieldViewStyleUpdateService.UpdateStyle(coordinateView, MouseState.Hover, true);
        }

        public void EnableInteraction()
        {
            IsEnabled = true;
        }

        public void DisableInteraction()
        {
            IsEnabled = false;
        }
    }
}
