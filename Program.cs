using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace LocalizationServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//CreateHostBuilder(args).Build().Run();

			var host = new WebHostBuilder()
				.ConfigureServices(serviceCollection =>
				{
					serviceCollection.AddSingleton(new ResourceManager("LocalizationServer.Controllers.StudentsController",
												   typeof(Startup).GetTypeInfo().Assembly));
				})
			.Build();

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
