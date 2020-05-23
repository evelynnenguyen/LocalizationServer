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
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

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
			services.AddControllers();

			var connection = Configuration.GetConnectionString("LocalizationServerContext");
			services.AddDbContext<LocalizationServerContext>(options => options.UseSqlServer(connection));

			services.AddSqlLocalization();
			
			services.AddMvc()
				.AddViewLocalization()
				.AddDataAnnotationsLocalization();

			services.AddScoped<LanguageActionFilter>();
			services.Configure<RequestLocalizationOptions>(
			options =>
			{
				var supportedCultures = new List<CultureInfo>
					{
						new CultureInfo("en-US"),
						new CultureInfo("de-CH"),
						new CultureInfo("fr-CH"),
						new CultureInfo("it-CH")
					};

				options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceCollection loggerFactory)
		{
			loggerFactory.AddLogging(opt => {
				opt.AddConsole();
			});

			loggerFactory.AddLogging(opt => {
				opt.AddDebug();
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(locOptions.Value);

			app.UseStaticFiles();

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
