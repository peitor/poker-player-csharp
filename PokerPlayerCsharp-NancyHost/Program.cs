using System;
using Host.Generated;
using Nancy;
using Nancy.Json;
using Nancy.Hosting.Self;
using System.IO;
using Nancy.IO;

namespace PokerPlayerCsharp_NancyHost
{

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
                string content = Request.Body.ReadAsString();
                JavaScriptSerializer xxx = new JavaScriptSerializer();
                var gameState = xxx.Deserialize<GameState>(content);

                // #1 Does not exist. Mentioned here: http://lucisferre.net/2011/01/23/a-restful-weekend-with-nancy/
                // Request.Body.FromJson<GameState>();

                // #2 Does not work 
                // GameState gameState = this.Bind<GameState>(bindingConfig);

                // #3 BindingConfig doesn't change anything here
                // GameState gameState = x as GameState;
                // var bindingConfig = new BindingConfig();
                // bindingConfig.BodyOnly = true;
                // GameState gameState = this.Bind<GameState>(bindingConfig);
                if (gameState != null)
                {
                    Console.WriteLine(gameState.small_blind);
                }

                return "0";
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
