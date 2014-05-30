namespace Specs
{
    using Nancy;
    using Nancy.Testing;

    using NUnit.Framework;

    using PokerPlayerCsharp_NancyHost;

    [TestFixture]
    public class PokerPlayerSpecs
    {
        readonly Browser browser = new Browser(with => with.Module(new PokerPlayerModule()));

        [Test]
        public void GetCheck()
        {
            var response = browser.Get("/check");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void PostVersion()
        {
            var response = browser.Post("/version");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
