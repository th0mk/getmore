using System;
using System.IO;
using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace GetMore.DbMigration
{
    class Program
    {
        static int Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            var connectionString = configuration.GetConnectionString("GetMoreCN");

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgradeEngine = 
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToAutodetectedLog()
                    .Build();

            var result = upgradeEngine.PerformUpgrade();

            if (!result.Successful)
            {
                Log.Error(result.Error, "Error while performing database upgrade");

                #if DEBUG
                Console.ReadLine();
                #endif

                Log.CloseAndFlush();
                return -1;
            }

            Log.Information(result.Error, "Database upgrade success!");

            Log.CloseAndFlush();
            return 0;
        }
    }
}
