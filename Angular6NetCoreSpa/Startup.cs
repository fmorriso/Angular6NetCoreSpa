using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Angular6NetCoreSpa.Filters;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
//
using NLog;
using NLog.Extensions.Logging;

namespace Angular6NetCoreSpa
{
    public class Startup
    {
		// https://github.com/CodeMazeBlog/.NET-Core-Series/blob/master/Part%203/AccountOwnerServer/Startup.cs
		public Startup(IConfiguration configuration, ILoggerFactory loggerFactory) // <-- NLog addition
		{
			string nLogConfigFile = Path.Combine(Directory.GetCurrentDirectory(), "nlog.config");
			Debug.WriteLine($"looking for nLog configuration at {nLogConfigFile}");
			//OBSOLETE: loggerFactory.ConfigureNLog(nlogConfigPath);
			LogManager.LoadConfiguration(nLogConfigFile);
			Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
	            config =>
	            {
		            config.Filters.Add(typeof(CustomExceptionFilter));
	            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			//OBSOLETE: services.ConfigureLoggerService();
			services.AddSingleton<ILoggerManager, LoggerManager>(); // <-- NLog addition

			// custom error handler
	        services.AddMvc(
		        config => {
			        config.Filters.Add(typeof(CustomExceptionFilter));
		        }
	        );
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) // TODO: inject NLog DI here
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
			
			app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
