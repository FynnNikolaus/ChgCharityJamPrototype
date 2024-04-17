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
	}
}
