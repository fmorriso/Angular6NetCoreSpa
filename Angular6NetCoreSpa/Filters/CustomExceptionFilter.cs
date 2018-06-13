using System;
using System.Net;
using System.Threading.Tasks;
//
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
//
using LoggerService;
using Microsoft.Extensions.Configuration;

namespace Angular6NetCoreSpa.Filters
{
	// http://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/
	public class CustomExceptionFilter : ExceptionFilterAttribute//IExceptionFilter
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
			if (!context.ExceptionHandled)
				CommonLogic(context);

			base.OnException(context);
		}

		private void CommonLogic(ExceptionContext context)
		{
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

			if (_logger is NLog.Logger)
			{
				NLog.Logger logger = (NLog.Logger)_logger;
				logger.Error(context);
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
