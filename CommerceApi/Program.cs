using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CommerceApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // imports transactions in Transaction list
            List<Transaction> transactions = TransactionReader.ImportTransactions("transactions/CustomerA.csv");

            CreateWebHostBuilder(args).Build().Run();

        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
