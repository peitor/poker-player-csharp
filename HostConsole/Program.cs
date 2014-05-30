namespace HostConsole
{
    using System;

    using Nancy.Hosting.Self;

    public class Program
    {
        //Nancy Modules are globally discovered. Modules can be declared anywhere you like, just as long as they are available in the application domain at runtime.
        private static void Main(string[] args)
        {
            // If you change the URL here, make sure the poker-croupier knows about it: config.yml  TODO: Read that file here
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.ReadLine();
            }
        }
    }
}