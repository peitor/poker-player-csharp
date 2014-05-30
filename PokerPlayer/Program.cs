namespace PokerPlayer
{
    using System;
    using System.IO;

    using Host.Generated;

    using Nancy;
    using Nancy.Hosting.Self;
    using Nancy.IO;
    using Nancy.ModelBinding;

    class Program
    {
        static void Main(string[] args)
        {
            // If you change the URL here, make sure the poker-croupier knows about it: config.yml  TODO: Read that file here
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.ReadLine();
            }
        }
    }

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
                return string.Concat("I am running version: ", Version);
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
