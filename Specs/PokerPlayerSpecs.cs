// ReSharper disable InconsistentNaming

namespace Specs
{
    using System.Globalization;
    using System.Linq;

    using Nancy;
    using Nancy.Testing;

    using NUnit.Framework;

    using PokerPlayer;

    [TestFixture]
    public class PokerPlayerSpecs
    {
        private readonly Browser browser = new Browser(with => with.Module(new PlayerService()));

        [Test]
        public void Get_check()
        {
            var response = this.browser.Post("/", with => { with.FormValue("action", "check"); });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Post_Showdown()
        {
            var response = this.browser.Post(
                "/",
                with =>
                {
                    with.FormValue("action", "showdown");
                    with.FormValue("game_state", Constants.fullSampleGameState);
                });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Post_betrequest_WithFullSampleMessage()
        {
            var response = this.browser.Post(
                "/",
                with =>
                {
                    with.FormValue("action", "bet_request");
                    with.FormValue("game_state", Constants.fullSampleGameState);
                });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.That(response.Body.AsString().Contains("0"));
        }

        [Test]
        public void Post_version()
        {
            var response = this.browser.Post("/", with => { with.FormValue("action", "version"); });
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }

    public class Constants
    {
        public static string fullSampleGameState =
            @"{  
        'players':[    
                {      'name':'Player 1',      'stack':1000,      'status':'active',      'bet':0,      'hole_cards':[],      'version':'Version name 1',    'id':0   },   
                {      'name':'Player 2',      'stack':1000,      'status':'active',      'bet':0,      'hole_cards':[],      'version':'Version name 2',     'id':1    }  
              ],  
        'small_blind':10,  'orbits':0,  'dealer':0,  'community_cards':[],  'current_buy_in':0,  'pot':0}";
    }
}

// ReSharper restore InconsistentNaming