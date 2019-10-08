using BattleshipGame.BattlefieldStuff;
using BattleshipGame.CoordinateStuff;
using System;

namespace BattleshipGame.PlayerStuff
{
    public enum PlayerResponse { Missed, Damaged, Sunk }

    public class PlayerResponseEventArgs : EventArgs
    {
        public PlayerResponse PlayerResponse { get; }

        public PlayerResponseEventArgs(PlayerResponse playerResponse)
        {
            PlayerResponse = playerResponse;
        }
    }

    public abstract class AbstractPlayer
    {
        public abstract event EventHandler<CoordinateEventArgs> OnGuessTaken;
        public abstract event EventHandler<PlayerResponseEventArgs> OnResponseGiven;
        public abstract event EventHandler OnTurnStarted, OnTurnEnded;

        public virtual string Name { get; protected set; }
        public virtual bool AutoTakeGuess { get; set; }

        public virtual Battlefield Battlefield { get; protected set; }
        public virtual Battlefield OpponentBattlefield { get; protected set; }

        public virtual void ListenTo(AbstractPlayer opponent)
        {
            opponent.OnGuessTaken += Opponent_OnGuessTaken;
            opponent.OnResponseGiven += Opponent_OnResponseGiven;
        }

        protected abstract void Opponent_OnGuessTaken(object sender, CoordinateEventArgs e);

        protected abstract void Opponent_OnResponseGiven(object sender, PlayerResponseEventArgs e);

        public abstract void TakeGuess(Coordinate coordinate);

        public abstract void Respond(PlayerResponse response);
    }
}
