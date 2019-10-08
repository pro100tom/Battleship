using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipGame.BattlefieldStuff;
using BattleshipGame.CoordinateStuff;

namespace BattleshipGame.PlayerStuff
{
    public class AiPlayer : AbstractPlayer
    {
        public override event EventHandler<CoordinateEventArgs> OnGuessTaken;
        public override event EventHandler<PlayerResponseEventArgs> OnResponseGiven;
        public override event EventHandler OnTurnStarted;
        public override event EventHandler OnTurnEnded;

        private readonly List<Coordinate> guessHistory = new List<Coordinate>();

        public AiPlayer(Battlefield playerBattlefield, Battlefield opponentBattlefield, string name)
        {
            Battlefield = playerBattlefield;
            OpponentBattlefield = opponentBattlefield;
            Name = name;
        }

        public async override void TakeGuess(Coordinate coordinate)
        {
            await Task.Delay(1000).ConfigureAwait(false);
            guessHistory.Add(coordinate);
            OnGuessTaken?.Invoke(this, new CoordinateEventArgs(coordinate));
        }

        public override void Respond(PlayerResponse response)
        {
            OnResponseGiven?.Invoke(this, new PlayerResponseEventArgs(response));

            if (response == PlayerResponse.Missed)
                OnTurnStarted?.Invoke(this, new EventArgs());
        }

        public void TakeRandomGuess()
        {
            var index = RandomHelper.Random.Next(OpponentBattlefield.Coordinates.Count);
            var randomCoordinate = OpponentBattlefield.Coordinates[index];
            TakeGuess(randomCoordinate);
        }

        public void TakeStrategicGuess()
        {
            var damagedShip = OpponentBattlefield.GetDamagedShips().FirstOrDefault();
            var sunkShips = OpponentBattlefield.GetSunkShips();
            var occupiedCoordinates = new List<Coordinate>();
            foreach (var sunkShip in sunkShips)
            {
                occupiedCoordinates.AddRange(sunkShip.Coordinates);
                occupiedCoordinates.AddRange(OpponentBattlefield.GetNeighbourCoordinates(sunkShip, false));
            }

            occupiedCoordinates = occupiedCoordinates.Distinct().ToList();

            List<Coordinate> coordinatesToCheck;
            if (damagedShip != null)
            {
                var checkedCoordinates = damagedShip.GetCheckedCoordinates();
                var nearbyCoordinates = new List<Coordinate>();
                foreach (var checkedCoordinate in checkedCoordinates)
                    nearbyCoordinates.AddRange(OpponentBattlefield.GetNeighbourCoordinates(checkedCoordinate));

                coordinatesToCheck = new List<Coordinate>(nearbyCoordinates);
            }
            else
                coordinatesToCheck = new List<Coordinate>(OpponentBattlefield.Coordinates);

            coordinatesToCheck = coordinatesToCheck
                    .Except(occupiedCoordinates)
                    .Except(guessHistory)
                    .ToList();

            int randomIndex = RandomHelper.Random.Next(0, coordinatesToCheck.Count);
            var coordinateToCheck = coordinatesToCheck[randomIndex];

            TakeGuess(coordinateToCheck);
        }

        protected override void Opponent_OnGuessTaken(object sender, CoordinateEventArgs e)
        {
            var ship = Battlefield.GetShip(e.Coordinate);
            e.Coordinate.SetChecked();

            if (ship is null)
            {
                Respond(PlayerResponse.Missed);
                TakeStrategicGuess();

                return;
            }

            if (ship.IsSunk)
                Respond(PlayerResponse.Sunk);
            else if (ship.IsDamaged)
                Respond(PlayerResponse.Damaged);
        }

        protected override void Opponent_OnResponseGiven(object sender, PlayerResponseEventArgs e)
        {
            if (e.PlayerResponse == PlayerResponse.Missed)
            {
                OnTurnEnded?.Invoke(this, new EventArgs());

                return;
            }

            if (e.PlayerResponse == PlayerResponse.Sunk)
                TakeRandomGuess();
            else
                TakeStrategicGuess();
        }
    }
}
