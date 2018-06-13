using System;
using System.Net;
using System.Threading.Tasks;
//
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
//
using Angular6NetCoreSpa.CustomExceptions;
using LoggerService;
using NLog;
using NLog.Extensions.Logging;

namespace Angular6NetCoreSpa.Filters
{
	// http://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/ with some changes for 2.1
	public class CustomExceptionFilter : ExceptionFilterAttribute
	{
		private readonly ILoggerManager _logger;
		public CustomExceptionFilter(ILoggerManager logger)
		{
			_logger = logger;
		}

		public override Task OnExceptionAsync(ExceptionContext context)
		{
			CommonLogic(context);
			return base.OnExceptionAsync(context);
		}

		public override void OnException(ExceptionContext context)
		{
			//WARNING: since OnExceptionAsync automatically calls OnException, we have to protect ourselves from a serious
			// error that will occur if we try to respond more than once.
			if (!context.ExceptionHandled)
				CommonLogic(context);

			base.OnException(context);
		}

		private void CommonLogic(ExceptionContext context)
		{
			// https://github.com/nlog/nlog/wiki/Tutorial
			NLog.Logger logger = NLog.LogManager.GetLogger("Logger");
			logger.Error(context);
			if (!string.IsNullOrEmpty(context.Exception.StackTrace))
				logger.Log(LogLevel.Error, context.Exception.StackTrace);

			HttpStatusCode status = HttpStatusCode.InternalServerError;
			String message = String.Empty;

			var exceptionType = context.Exception.GetType();
			if (exceptionType == typeof(UnauthorizedAccessException))
			{
				message = "Unauthorized Access";
				status = HttpStatusCode.Unauthorized;
			}
			else if (exceptionType == typeof(NotImplementedException))
			{
				message = "A server error occurred.";
				status = HttpStatusCode.NotImplemented;
			}
			else if (exceptionType == typeof(CustomExceptionExample))
			{
				message = context.Exception.ToString();
				status = HttpStatusCode.InternalServerError;
			}
			else
			{
				message = context.Exception.Message;
				status = HttpStatusCode.NotFound;
			}

			context.ExceptionHandled = true; // <--- *** EXTREMELY IMPORTANT ***

			HttpResponse response = context.HttpContext.Response;
			response.StatusCode = (int)status;
			response.ContentType = "application/json";
			var err = $"{message} context.Exception.StackTrace";
			response.WriteAsync(err);
		}

	}
}
