using Microsoft.AspNetCore.Mvc.Filters;

namespace Angular6NetCoreSpa.Filters
{
	public interface IExceptionFilter : IFilterMetadata
	{
		void OnException(ExceptionContext context);
	}
}
