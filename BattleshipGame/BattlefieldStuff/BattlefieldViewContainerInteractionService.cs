using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BattleshipGame.BattlefieldStuff
{
    public class BattlefieldViewContainerInteractionService
    {
        public BattlefieldViewInteractionService BattlefieldViewInteraction { get; }
        public bool IsEnabled { get; private set; }

        public BattlefieldViewContainerInteractionService(BattlefieldViewInteractionService battlefieldViewInteraction)
        {
            BattlefieldViewInteraction = battlefieldViewInteraction;
            BattlefieldViewInteraction.Player.OnTurnStarted += Player_OnTurnStarted;
            BattlefieldViewInteraction.Player.OnTurnEnded += Player_OnTurnEnded;
        }

        private async void Player_OnTurnStarted(object sender, EventArgs e)
        {
            if (!IsEnabled)
                return;

            await Task.Delay(300).ConfigureAwait(false);
            BattlefieldViewInteraction.EnableInteraction();
            Application.Current.Dispatcher.Invoke(delegate
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            });
        }

        private void Player_OnTurnEnded(object sender, EventArgs e)
        {
            if (!IsEnabled)
                return;

            BattlefieldViewInteraction.DisableInteraction();
            Application.Current.Dispatcher.Invoke(delegate
            {
                Mouse.OverrideCursor = Cursors.No;
            });
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
