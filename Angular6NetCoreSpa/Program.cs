using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Angular6NetCoreSpa
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//OLD: CreateWebHostBuilder(args).Build().Run();
			// break apart the program beginning so we can control each aspect of it, especially logging with NLog
			IWebHostBuilder iwhb = CreateWebHostBuilder(args);
			IWebHost iwh = iwhb.Build();
			iwh.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders();
					logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
				})
				.UseNLog(); // <---- NLog addition
	}
}
