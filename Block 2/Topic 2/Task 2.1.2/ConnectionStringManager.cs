using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Task_2._1._2 {
    class ConnectionStringManager {
        public string ConnectionString { get; }
        public ConnectionStringManager(string connectionStringName = "DefaultConnection",
            string environmentVariableName = "CustomersCardsDB_ConnectionString") {

            string path = Directory.GetCurrentDirectory();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            string userId = "", password = "";
            config.Providers.Any(p => p.TryGet("CustomersCardsDB:UserId", out userId));
            config.Providers.Any(p => p.TryGet("CustomersCardsDB:Password", out password));
            ConnectionString = string.Format(
                config.GetConnectionString(connectionStringName),
                userId, password
            ) ?? Environment.GetEnvironmentVariable(environmentVariableName);
        }
    }
}
