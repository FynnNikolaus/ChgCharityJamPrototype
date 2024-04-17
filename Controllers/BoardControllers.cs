using ChgCharityJamPrototype.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChgCharityJamPrototype.Controllers
{
	public class BoardController : Controller
	{
		private readonly ILogger<BoardController> _logger;

		public BoardController(ILogger<BoardController> logger)
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
