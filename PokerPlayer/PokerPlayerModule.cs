namespace PokerPlayer
{
    using System;
    using System.IO;

    using Nancy;
    using Nancy.IO;
    using Nancy.Json;
    using Nancy.ModelBinding;

    using PokerPlayer.Generated;

    /*
       Nancy Modules are globally discovered. Modules can be declared anywhere you like, just as long as they are available in the application domain at runtime.
   */

    public class PokerPlayerModule : NancyModule
    {
        private const string TeamName = "NancyLovers";

        private const string Version = "0.1";

        public PokerPlayerModule()
        {
            Rootobject gameState;
            this.Post["/"] = x =>
            {
                var invoke = this.Bind<ActionCommand>();
                Console.WriteLine("Command found: " + invoke.action);
                            
                switch (invoke.action)
                {
                    case "check":
                        return string.Concat("I am running! My team: ", TeamName);
                        break;
                    case "version":
                        return Version;
                        break;
                    case "showdown":
                        gameState = GetGameStateFromForm(this.Request.Form["game_state"]);
                        if (gameState != null)
                        {
                            Console.WriteLine("GameState found!");
                            Console.WriteLine(gameState.small_blind);
                            return "200";
                        }
                        break;
                    case "bet_request":
                        gameState = GetGameStateFromForm(this.Request.Form["game_state"]);
                        if (gameState != null)
                        {
                            Console.WriteLine("GameState found!");
                            Console.WriteLine(gameState.small_blind);
                            return "200";
                        }
                        
                        break;
                }
                return HttpStatusCode.NotAcceptable;
            };

        }

        private static Rootobject GetGameStateFromForm(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<Rootobject>(input);
        }
    }

    public class ActionCommand
    {
        public string action { get; set; }
    }

    public static class RequestBodyExtensions
    {
        public static string ReadAsString(this RequestStream requestStream)
        {
            using (var reader = new StreamReader(requestStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}