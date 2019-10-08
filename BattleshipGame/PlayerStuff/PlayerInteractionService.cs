using BattleshipGame.BattlefieldStuff;

namespace BattleshipGame.PlayerStuff
{
    public class PlayerInteractionService
    {
        public AbstractPlayer Player1 { get; }
        public AbstractPlayer Player2 { get; }

        public PlayerInteractionService(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            Player1.ListenTo(Player2);
            Player2.ListenTo(Player1);
        }
    }
}
