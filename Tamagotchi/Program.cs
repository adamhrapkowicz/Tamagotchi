namespace Tamagotchi
{
    public class Program
    {
        // Turn LifeCycle into an ASP.net application
        public static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*************");
            Console.WriteLine(" Tamagotchi API ");
            Console.WriteLine("*************");
            Console.ForegroundColor = ConsoleColor.White;


            try
            {
                await CreateHostBuilderAsync(args)
                    .Build()
                    .RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
            }

        }

        private static IHostBuilder CreateHostBuilderAsync(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(options => options.ValidateOnBuild = true)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel()
                        .UseStartup<Startup>();
                });
        }
    }
}