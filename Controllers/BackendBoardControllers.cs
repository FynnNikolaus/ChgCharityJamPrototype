using ChgCharityJamPrototype.Models;
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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
