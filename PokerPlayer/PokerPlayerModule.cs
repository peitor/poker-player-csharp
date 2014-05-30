using System;

namespace PokerPlayer
{
    using System.IO;

    using Nancy;
    using Nancy.IO;
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
            this.Get["/check"] = x =>
            {
                return string.Concat("I am running! My team: ", TeamName);
            };

            this.Post["/version"] = x =>
            {
                return Version;
            };


            this.Post["/bet_request"] = x =>
            {
                // HACK: If Bind<> doesn't work use this ;-)
                //   string content = Request.Body.ReadAsString();
                //    JavaScriptSerializer xxx = new JavaScriptSerializer();
                //    var gameState = xxx.Deserialize<GameState>(content);

                GameState gameState = this.Bind<GameState>();
                if (gameState != null)
                {
                    Console.WriteLine(gameState.small_blind);
                    return "200"; // HttpStatusCode.OK
                }

                return "-1";
            };

            this.Post["/showdown"] = x =>
            {
                return "Nothing";
            };
        }
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
