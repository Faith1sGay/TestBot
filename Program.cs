using System;
using Microsoft.Extensions.Logging;
using Serilog;
using LiteDB;
using Serilog.Sinks.SystemConsole;
using DSharpPlus;
using System.Threading.Tasks;
namespace DatabaseTest
{
    class Program
    {
        static int Main()
        {
            MainAsync().GetAwaiter().GetResult();

            async Task MainAsync()
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();



                var logFactory = new LoggerFactory();
                DiscordClient c = new DiscordClient(new DiscordConfiguration
                {
                    LoggerFactory = logFactory,
                    MinimumLogLevel = LogLevel.Information,
                    Token = "token",
                    TokenType = TokenType.Bot
                });
                c.MessageCreated += async (s, e) =>
                {
                    if (e.Message.Content.ToLower().StartsWith("test"))
                    {

                        var Db = new LiteDatabase("./data.db");
                        var col = Db.GetCollection<Data>("datatest");
                        var d = new Data()
                        {
                            Id = e.Author.Id,
                            UserName = e.Author.Username,
                            Date = DateTime.UtcNow
                        };
                        try
                        {
                            col.Insert(d);
                            await e.Message.RespondAsync("inserted");
                        }
                        catch (Exception E)
                        {
                            await e.Message.RespondAsync(E.ToString());
                        }
                    };
                };
                await c.ConnectAsync();
                await Task.Delay(-1);
            }
            return 0;
        }

    }
}