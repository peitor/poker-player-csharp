namespace PokerPlayer.Generated
{
    // Generated from https://github.com/lean-poker/poker-croupier/wiki/Player-API 
    public class GameState
    {
        public int small_blind { get; set; }
        public int current_buy_in { get; set; }
        public int pot { get; set; }
        public int minimum_raise { get; set; }
        public int dealer { get; set; }
        public int orbits { get; set; }
        public int in_action { get; set; }
        public Player[] players { get; set; }
        public Community_Cards[] community_cards { get; set; }
    }

    public class Player
    {
        public int id { get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string version { get; set; }
        public int stack { get; set; }
        public int bet { get; set; }
        public Hole_Cards[] hole_cards { get; set; }
    }

    public class Hole_Cards
    {
        public string rank { get; set; }
        public string suit { get; set; }
    }

    public class Community_Cards
    {
        public string rank { get; set; }
        public string suit { get; set; }
    }

}