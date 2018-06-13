using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular6NetCoreSpa.Filters;
using LoggerService;
using Microsoft.AspNetCore.Mvc;

namespace Angular6NetCoreSpa.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly ILoggerManager _logger;

		public ValuesController(ILoggerManager logger)
		{
			_logger = logger;
		}

		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
#if true
			_logger.LogInfo("Here is info message from our values controller.");
			_logger.LogDebug("Here is debug message from our values controller.");
			_logger.LogWarn("Here is warn message from our values controller.");
			_logger.LogError("Here is error message from our values controller.");
			return new string[] { "value1", "value2" };
#else
	        string[] arrRetValues = null;
	        if (arrRetValues.Length > 0)
	        { }
	        return arrRetValues;
#endif

		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
#if RUN_DEFAULTS
            return "value";
#else
			string sMessage = "test";
			if (sMessage.Length > 0)
			{
				throw new CustomExceptionExample("My Custom Exception");
			}
			return sMessage;
#endif
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
