// ReSharper disable InconsistentNaming
namespace Specs
{
    using Nancy;
    using Nancy.Testing;

    using NUnit.Framework;

    using PokerPlayer;

    [TestFixture]
    public class PokerPlayerSpecs
    {
        readonly Browser browser = new Browser(with => with.Module(new PokerPlayerModule()));

        [Test]
        public void Get_check()
        {
            var response = browser.Get("/check");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Post_version()
        {
            var response = browser.Post("/version");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [Test]
        public void Post_betrequest_WithFullSampleMessage()
        {
            var response = browser.Post("/bet_request", with =>
            {
                // request is sent over HTTP
                with.HttpRequest();
                with.Body(Constants.fullSampleMessage);
            });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


    }

    public class Constants
    {
       public static string fullSampleMessage = @"{
   ""small_blind"":10,
   ""current_buy_in"":320,
   ""pot"":400,
   ""minimum_raise"":240,
   ""dealer"":1,
   ""orbits"":7,
   ""in_action"":1,
   ""players"":[
      {
         ""id"":0,
         ""name"":""Albert"",
         ""status"":""active"",
         ""version"":""Default random player"",
         ""stack"":1010,
         ""bet"":320
      },
      {
         ""id"":1,
         ""name"":""Bob"",
         ""status"":""active"",
         ""version"":""Default random player"",
         ""stack"":1590,
         ""bet"":80,
         ""hole_cards"":[
            {
               ""rank"":""6"",
               ""suit"":""hearts""
            },
            {
               ""rank"":""K"",
               ""suit"":""spades""
            }
         ]
      },
      {
         ""id"":2,
         ""name"":""Chuck"",
         ""status"":""out"",
         ""version"":""Default random player"",
         ""stack"":0,
         ""bet"":0
      }
   ],
   ""community_cards"":[
      {
         ""rank"":""4"",
         ""suit"":""spades""
      },
      {
         ""rank"":""A"",
         ""suit"":""hearts""
      },
      {
         ""rank"":""6"",
         ""suit"":""clubs""
      }
   ]
}";
    }
}
// ReSharper restore InconsistentNaming
