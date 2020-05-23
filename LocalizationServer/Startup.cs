using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LocalizationServer.Data;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using RequestCorrelation;

namespace LocalizationServer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Localization: add in the localizaton service which will enable using IStringLocalizer in the StudentsController
			services.AddLocalization(options => options.ResourcesPath = "Resources/Controllers");

			services.AddControllers();

			var connection = Configuration.GetConnectionString("LocalizationServerContext");
			services.AddDbContext<LocalizationServerContext>(options => options.UseSqlServer(connection));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			var timingLogger = loggerFactory.CreateLogger("CustomersAPI.Startup.TimingMiddleware");

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// Localization
			var supportedCultures = new[]
			 {
				new CultureInfo("en-US"),
				new CultureInfo("fr-FR"),
				new CultureInfo("es"),
			 };

			var requestLocalizationOptions = new RequestLocalizationOptions
			{
				DefaultRequestCulture = new RequestCulture("en-US"),

				// Formatting numbers, dates, etc.
				SupportedCultures = supportedCultures,

				// UI strings that we have localized.
				//SupportedUICultures = supportedCultures
			};

			app.UseRequestLocalization(requestLocalizationOptions);
			
			app.UseStaticFiles();

			app.UseMiddleware<RequestCorrelationMiddleware>();

			// end localization

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
