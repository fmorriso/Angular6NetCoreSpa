using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular6NetCoreSpa.Filters
{
	public interface IExceptionFilter : IFilterMetadata
	{
		void OnException(ExceptionContext context);
	}
}
