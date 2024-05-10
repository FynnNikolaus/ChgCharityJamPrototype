using ChgCharityJamPrototype.HostedService;
using Microsoft.AspNetCore.Mvc;

namespace ChgCharityJamPrototype.Controllers
{
	public class BoardController : Controller
	{
		private readonly ILogger<BoardController> _logger;
		private readonly IGameStatusProvider _gameStatusProvider;

		public BoardController(ILogger<BoardController> logger, IGameStatusProvider gameStatusProvider)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_gameStatusProvider = gameStatusProvider ?? throw new ArgumentNullException(nameof(gameStatusProvider));
		}

		public IActionResult Index()
		{
			var status = _gameStatusProvider.GetLatestGameStatus();

			return View();
		}
	}
}
