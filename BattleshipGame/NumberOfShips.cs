namespace BattleshipGame
{
    public class NumberOfShips
    {
        public int NumberOfAirCarriers { get; }
        public int NumberOfBattleships { get; }
        public int NumberOfCruisers { get; }
        public int NumberOfDestroyers { get; }
        public int NumberOfSubmarines { get; }

        public NumberOfShips(int numberOfAirCarriers, int numberOfBattleships,
            int numberOfCruisers, int numberOfDestroyers, int numberOfSubmarines)
        {
            NumberOfAirCarriers = numberOfAirCarriers;
            NumberOfBattleships = numberOfBattleships;
            NumberOfCruisers = numberOfCruisers;
            NumberOfDestroyers = numberOfDestroyers;
            NumberOfSubmarines = numberOfSubmarines;
        }
    }
}
