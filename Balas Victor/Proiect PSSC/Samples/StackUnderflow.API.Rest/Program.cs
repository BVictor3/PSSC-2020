using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrainImplementation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace FakeSO.API.Rest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseOrleans(siloBuilder => // vrem sa configuram un cluster de orleans in interiorul aplicatiei web
                {
                    //pornesc un server intr-o aplicatie consola
                    siloBuilder.UseLocalhostClustering()
                    .Configure<ClusterOptions>(options => //configurarea clusterului
                     {
                         options.ClusterId = "dev";
                         options.ServiceId = "OrleansBasics";
                     })
                    .ConfigureApplicationParts(
                        parts => parts.AddApplicationPart(typeof(EmailSenderGrain).Assembly) // unde se afla grain-ul nostru
                                .WithReferences())
                    .AddSimpleMessageStreamProvider("SMSProvider", options => { options.FireAndForgetDelivery = true; });
                });
    }
}
