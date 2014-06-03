namespace PokerPlayer
{
    using PokerPlayer.Generated;

    public class PlayerImpl
    {
        private const string TeamName = "NancyLovers";

        public const string Version = "0.1";

        public string Check()
        {
            return string.Concat("I am running! My team: ", TeamName);
        }

        public void Showdown(Rootobject gameState)
        {

        }

        public int BetRequest(Rootobject gameState)
        {
            return 0;
        }
    }
}