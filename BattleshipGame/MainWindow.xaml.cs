using BattleshipGame.BattlefieldStuff;
using BattleshipGame.CoordinateStuff;
using BattleshipGame.PlayerStuff;
using BattleshipGame.ShipStuff;
using System.Windows;

namespace BattleshipGame
{
    public partial class MainWindow : Window
    {
        private readonly BattlefieldViewFactory battlefieldViewFactory;
        private readonly ShipFactory shipFactory;

        public MainWindow()
        {
            InitializeComponent();

            battlefieldViewFactory = new BattlefieldViewFactory();
            shipFactory = new ShipFactory();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dimensions = new Dimensions(10, 10);
            var battlefield1 = new Battlefield(dimensions);
            var battlefield2 = new Battlefield(dimensions);

            var numberOfShips = new NumberOfShips(1, 1, 2, 2, 3);
            var ships = shipFactory.CreateShips(numberOfShips);
            battlefield1.PlaceShips(ships, true);
            ships = shipFactory.CreateShips(numberOfShips);
            battlefield2.PlaceShips(ships, true);

            var player1 = new Player(battlefield1, battlefield2, "Tomas");
            var player2 = new AiPlayer(battlefield2, battlefield1, "AI");
            player1.ListenTo(player2);
            player2.ListenTo(player1);

            var battlefield1View = battlefieldViewFactory.CreateBattlefieldView(battlefield1);
            var battlefield2View = battlefieldViewFactory.CreateBattlefieldView(battlefield2);

            var battlefield1ViewStyleUpdateService = new BattlefieldViewStyleUpdateService(
                battlefield1View, new BattlefieldViewStyleSettings(false, false, false)
            );
            var battlefield2ViewStyleUpdateService = new BattlefieldViewStyleUpdateService(
                battlefield2View, new BattlefieldViewStyleSettings(true, false, false)
            );
            var battlefield1ViewInteractionService = new BattlefieldViewInteractionService(
                battlefield1View, battlefield1ViewStyleUpdateService
            );
            var battlefield2ViewInteractionService = new BattlefieldViewInteractionService(
                battlefield2View, battlefield2ViewStyleUpdateService, player1
            );

            battlefield1ViewInteractionService.EnableInteraction();
            battlefield2ViewInteractionService.EnableInteraction();

            var battlefieldViewContainerInteractionService = new BattlefieldViewContainerInteractionService(battlefield2ViewInteractionService);
            battlefieldViewContainerInteractionService.EnableInteraction();

            var battlefieldViewContainer = new BattlefieldViewContainer(battlefield1View, battlefield2View);
            MainContainer.Children.Add(battlefieldViewContainer);
        }
    }
}
