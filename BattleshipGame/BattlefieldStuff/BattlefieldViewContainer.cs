using System.Windows.Controls;

namespace BattleshipGame.BattlefieldStuff
{
    public class BattlefieldViewContainer : StackPanel
    {
        public BattlefieldView Battlefield1View { get; }
        public BattlefieldView Battlefield2View { get; }

        public BattlefieldViewContainer(BattlefieldView battlefield1View, BattlefieldView battlefield2View)
        {
            Battlefield1View = battlefield1View;
            Battlefield2View = battlefield2View;

            Children.Add(Battlefield1View);
            Children.Add(Battlefield2View);

            // Apply the style externally
            Orientation = Orientation.Horizontal;
        }
    }
}
