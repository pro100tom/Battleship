using System;
using System.Linq;
using System.Threading.Tasks;
using BattleshipGame.BattlefieldStuff;
using BattleshipGame.CoordinateStuff;

namespace BattleshipGame.PlayerStuff
{
    public class Player : AbstractPlayer
    {
        public override event EventHandler<CoordinateEventArgs> OnGuessTaken;
        public override event EventHandler<PlayerResponseEventArgs> OnResponseGiven;
        public override event EventHandler OnTurnStarted;
        public override event EventHandler OnTurnEnded;

        public Player(Battlefield playerBattlefield, Battlefield opponentBattlefield, string name)
        {
            Battlefield = playerBattlefield;
            OpponentBattlefield = opponentBattlefield;
            Name = name;
        }

        public async override void TakeGuess(Coordinate coordinate)
        {
            if (AutoTakeGuess)
                await Task.Delay(1000).ConfigureAwait(false);

            OnGuessTaken?.Invoke(this, new CoordinateEventArgs(coordinate));
        }

        public override void Respond(PlayerResponse response)
        {
            OnResponseGiven?.Invoke(this, new PlayerResponseEventArgs(response));

            if (response == PlayerResponse.Missed)
                OnTurnStarted?.Invoke(this, new EventArgs());
        }

        protected override void Opponent_OnGuessTaken(object sender, CoordinateEventArgs e)
        {
            var ship = Battlefield.GetShip(e.Coordinate);
            e.Coordinate.SetChecked();

            if (ship is null)
            {
                Respond(PlayerResponse.Missed);

                return;
            }

            if (ship.IsSunk)
            {
                Respond(PlayerResponse.Sunk);

                return;
            }

            if (ship.IsDamaged)
                Respond(PlayerResponse.Damaged);
        }

        protected override void Opponent_OnResponseGiven(object sender, PlayerResponseEventArgs e)
        {
            if (e.PlayerResponse == PlayerResponse.Missed)
            {
                OnTurnEnded?.Invoke(this, new EventArgs());

                return;
            }
        }
    }
}
