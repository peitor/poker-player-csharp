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

    public class PlayerService : NancyModule
    {
        public PlayerService()
        {
            this.Post["/"] = x =>
            {
                var invoke = this.Bind<ActionCommand>();
                Console.WriteLine("Command found: " + invoke.action);

                var player = new PlayerImpl();
                Rootobject gameState;
                switch (invoke.action)
                {
                    case "check":
                        return player.Check();
                        break;
                    case "version":
                        return PlayerImpl.Version;
                        break;
                    case "showdown":
                        gameState = GetGameStateFromForm(this.Request.Form["game_state"]);
                        player.Showdown(gameState);
                        return HttpStatusCode.OK;
                        break;
                    case "bet_request":
                        gameState = GetGameStateFromForm(this.Request.Form["game_state"]);
                        int result = player.BetRequest(gameState);
                        return Response.AsJson(result);
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