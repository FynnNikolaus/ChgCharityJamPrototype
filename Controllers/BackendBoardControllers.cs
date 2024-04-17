using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChgCharityJamPrototype.Controllers
{
	public class BackendBoardController : Controller
	{
		private readonly ILogger<BackendBoardController> _logger;

		public BackendBoardController(ILogger<BackendBoardController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
