using System;
using System.Windows.Controls;

namespace BattleshipGame
{
    public class BattlefieldViewContainerCreatedEventArgs : EventArgs
    {
        public StackPanel BattlefieldViewContainer { get; }

        public BattlefieldViewContainerCreatedEventArgs(StackPanel battlefieldViewContainer)
        {
            BattlefieldViewContainer = battlefieldViewContainer;
        }
    }
}
